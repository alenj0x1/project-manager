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

namespace ProjectManager.Application.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<GenericResponse<UserDto>> Create(CreateUserRequest request)
        {
            try
            {
                if (_userRepository.IfExistsIdentification(request.Identification))
                {
                    throw new BadRequestException("Ya existe un usuario con la misma identificación");
                }

                if (_userRepository.IfExistsEmailAddress(request.EmailAddress))
                {
                    throw new BadRequestException("Ya existe un usuario con la misma dirección de correo electrónico");
                }

                var createUser = new User
                {
                    Identification = request.Identification,
                    EmailAddress = request.EmailAddress,
                    RoleId = request.RoleId,
                    FirstName = request.FirstName,
                    Password = "1234",
                    LastName = request.LastName
                };

                await _userRepository.Create(createUser);

                return new GenericResponse<UserDto>(){
                    Data = Map(createUser),
                    Message = "Usuario creado correctamente!"
                };
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
                var users = _userRepository.Queryable().ToList();

                var mappedUsers = new List<UserDto>();

                foreach (var user in users)
                {
                    mappedUsers.Add(Map(user));
                }

                return new GenericResponse<List<UserDto>>()
                {
                    Data = mappedUsers,
                    Message = "Listado de usuarios"
                };
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
