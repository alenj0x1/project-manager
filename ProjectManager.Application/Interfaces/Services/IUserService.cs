using ProjectManager.Application.Dtos;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.User;
using System.Security.Claims;

namespace ProjectManager.Application.Interfaces.Services
{
    public interface IUserService
    {

        Task FirstUser();
        Task<GenericResponse<UserDto>> Create(CreateUserRequest request, Claim claim);
        GenericResponse<UserDto> Get(Guid userId);
        GenericResponse<UserDto> Get(string emailAddress);
        GenericResponse<List<UserDto>> Get();
        Task<GenericResponse<UserDto>> Update(Guid userId, UpdateUserRequest request, Claim claim);
        Task<GenericResponse<bool>> Remove(Guid userId, Claim claim);
    }
}
