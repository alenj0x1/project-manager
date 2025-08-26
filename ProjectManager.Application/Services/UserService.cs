using ProjectManager.Application.Dtos;
using ProjectManager.Application.Interfaces.Services;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.User;
using ProjectManager.Domain.Context;
using ProjectManager.Domain.Exceptions;
using ProjectManager.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.Helpers;
using ProjectManager.Utils;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace ProjectManager.Application.Services
{
    public class UserService(IUserRepository userRepository, IRoleRepository roleRepository, IConfiguration configuration) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IConfiguration _configuration = configuration;

        public async System.Threading.Tasks.Task FirstUser()
        {
            try
            {
                if (_userRepository.Queryable().Any())
                {
                    return;
                }

                var user = new User()
                {
                    FirstName = _configuration["InitialConfiguration:User:FirstName"] 
                        ?? throw new ArgumentNotFoundException(ResponseConsts.ConfigurationArgumentNotFound("InitialConfiguration:User:FirstName")),
                    LastName = _configuration["InitialConfiguration:User:LastName"]
                        ?? throw new ArgumentNotFoundException(ResponseConsts.ConfigurationArgumentNotFound("InitialConfiguration:User:LastName")),
                    EmailAddress = _configuration["InitialConfiguration:User:EmailAddress"]
                        ?? throw new ArgumentNotFoundException(ResponseConsts.ConfigurationArgumentNotFound("InitialConfiguration:User:EmailAddress")),
                    Identification = _configuration["InitialConfiguration:User:Identification"]
                        ?? throw new ArgumentNotFoundException(ResponseConsts.ConfigurationArgumentNotFound("InitialConfiguration:User:Identification")),
                    Password = Hasher.HashPassword(_configuration["InitialConfiguration:User:Password"] 
                        ?? throw new ArgumentNotFoundException(ResponseConsts.ConfigurationArgumentNotFound("InitialConfiguration:User:Password"))),
                    IsActive = true
                };

                await _userRepository.Create(user);

                Console.WriteLine("Primer usuario creado con exito");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<GenericResponse<UserDto>> Create(CreateUserRequest request, Claim claim)
        {
            try
            {
                var executor = GetExecutor(claim);

                if (_userRepository.IfExistsByIdentification(request.Identification))
                {
                    throw new BadRequestException(ResponseConsts.UserIdentificationExists);
                }

                if (_userRepository.IfExistsByEmailAddress(request.EmailAddress))
                {
                    throw new BadRequestException(ResponseConsts.UserEmailAddressExists);
                }

                if (_roleRepository.IfExists(request.RoleId) == false)
                {
                    throw new NotFoundException(ResponseConsts.RoleNotExists);
                }

                var createUser = await _userRepository.Create(new User
                {
                    Identification = request.Identification,
                    EmailAddress = request.EmailAddress,
                    RoleId = request.RoleId,
                    Role = _roleRepository.Get(request.RoleId) ?? throw new Exception("Imposible obtener el rol del usuario"),
                    FirstName = request.FirstName,
                    Password = Hasher.HashPassword(request.Password),
                    LastName = request.LastName,
                    CreatedBy = executor.UserId,
                    UpdatedBy = executor.UserId
                });

                return ResponseHelper.Create(Map(createUser), message: ResponseConsts.UserCreated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GenericResponse<UserDto> Get(Guid userId)
        {
            try
            {
                var user = _userRepository.Get(userId);
                if (user == null)
                {
                    throw new NotFoundException(ResponseConsts.UserNotFound);
                }


                var mapped = Map(user);
                return ResponseHelper.Create(mapped);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public GenericResponse<UserDto> Get(string emailAddress)
        {
            try
            {
                var user = _userRepository.Get(emailAddress);
                if (user == null)
                {
                    throw new NotFoundException(ResponseConsts.UserNotFound);
                }


                var mapped = Map(user);
                return ResponseHelper.Create(mapped);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public GenericResponse<List<UserDto>> Get()
        {
            try
            {
                var users = _userRepository.Queryable()
                    .Include(x => x.Role).ToList();

                var mappedUsers = new List<UserDto>();

                foreach (var user in users)
                {
                    mappedUsers.Add(Map(user));
                }

                return ResponseHelper.Create(
                    mappedUsers,
                    message: ResponseConsts.UserList,
                    count: _userRepository.Queryable().Count()
                );
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GenericResponse<bool>> Remove(Guid userId, Claim claim)
        {
            try
            {
                var executor = GetExecutor(claim);

                var user = _userRepository.Get(userId) ?? throw new NotFoundException(ResponseConsts.UserNotFound);

                await _userRepository.Delete(user);

                return ResponseHelper.Create(true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<GenericResponse<UserDto>> Update(Guid userId, UpdateUserRequest request, Claim claim)
        {
            try
            {
                var executor = GetExecutor(claim);

                var user = _userRepository.Get(userId) ?? throw new NotFoundException(ResponseConsts.UserNotFound);

                if (
                    request.Identification is not null && 
                    request.Identification != user.Identification && 
                    _userRepository.IfExistsByIdentification(request.Identification)
                )
                {
                    throw new BadRequestException(ResponseConsts.UserIdentificationExists);
                }

                if (request.EmailAddress is not null &&
                    request.EmailAddress != user.EmailAddress && 
                    _userRepository.IfExistsByEmailAddress(request.EmailAddress)
                )
                {
                    throw new BadRequestException(ResponseConsts.UserEmailAddressExists);
                }

                if (request.RoleId.HasValue &&
                    request.RoleId.Value != user.RoleId && 
                    _roleRepository.IfExists(request.RoleId.Value) == false
                )
                {
                    throw new NotFoundException(ResponseConsts.RoleNotExists);
                }

                user.Identification = request.Identification ?? user.Identification;
                user.EmailAddress = request.EmailAddress ?? user.EmailAddress;
                user.RoleId = request.RoleId ?? user.RoleId;
                user.FirstName = request.FirstName ?? user.FirstName;
                user.LastName = request.LastName ?? user.LastName;

                // Añadimos una entidad adicional, en este caso, Role
                user.Role = _roleRepository.Get(user.RoleId) ?? throw new Exception("Imposible obtener el rol del usuario");

                // Auditoria
                user.UpdatedAt = DateTime.UtcNow;
                user.UpdatedBy = executor.UserId;

                await _userRepository.Update(user);
                return ResponseHelper.Create(Map(user), message: ResponseConsts.UserCreated);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private User GetExecutor(Claim claim)
        {
            try
            {
                var executorId = Parser.ToGuid(claim.Value) ?? throw new ArgumentNotFoundException(ResponseConsts.UserIdentityNotFound);
                var executor = _userRepository.Get(executorId) ?? throw new ArgumentException(ResponseConsts.UserIdentityNotFound);

                return executor;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static UserDto Map(User user)

        {
            try
            {
                return new UserDto
                {
                    UserId = user.UserId,
                    EmailAddress = user.EmailAddress,
                    Identification = user.Identification,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsActive = user.IsActive,
                    RoleId = user.RoleId,
                    Role = new RoleDto
                    {
                        RoleId = user.Role.RoleId,
                        Name = user.Role.Name
                    },
                    CreatedAt = user.CreatedAt,
                    CreatedBy = user.CreatedBy,
                    UpdatedAt = user.UpdatedAt,
                    UpdatedBy = user.UpdatedBy
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}