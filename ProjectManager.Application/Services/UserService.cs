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

        public async Task<GenericResponse<UserDto>> Create(CreateUserRequest request)
        {
            try
            {
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

                var createUser = new User
                {
                    Identification = request.Identification,
                    EmailAddress = request.EmailAddress,
                    RoleId = request.RoleId,
                    FirstName = request.FirstName,
                    Password = Hasher.HashPassword(request.Password),
                    LastName = request.LastName
                };

                await _userRepository.Create(createUser);

                return ResponseHelper.Create(Map(createUser), message: ResponseConsts.UserCreated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GenericResponse<UserDto?> Get(Guid userId)
        {
            throw new NotImplementedException();
        }

        public GenericResponse<UserDto?> Get(string emailAddress)
        {
            throw new NotImplementedException();
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

        public Task<GenericResponse<bool>> Remove(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<UserDto>> Update(Guid userId, UpdateUserRequest request)
        {
            throw new NotImplementedException();
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