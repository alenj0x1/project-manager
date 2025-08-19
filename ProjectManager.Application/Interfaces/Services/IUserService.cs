using ProjectManager.Application.Dtos;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.User;

namespace ProjectManager.Application.Interfaces.Services
{
    public interface IUserService
    {

        Task FirstUser();
        Task<GenericResponse<UserDto>> Create(CreateUserRequest request);
        GenericResponse<UserDto?> Get(Guid userId);
        GenericResponse<UserDto?> Get(string emailAddress);
        GenericResponse<List<UserDto>> Get();
        Task<GenericResponse<UserDto>> Update(Guid userId, UpdateUserRequest request);
        Task<GenericResponse<bool>> Remove(Guid userId);
    }
}
