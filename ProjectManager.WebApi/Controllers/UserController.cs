using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Dtos;
using ProjectManager.Application.Interfaces.Services;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.User;

namespace ProjectManager.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService)
{
    private readonly IUserService _userService = userService;

    [Authorize]
    [HttpPost("create")]
    public async Task<GenericResponse<UserDto>> Create([FromBody] CreateUserRequest request)
    {
        try
        {
            return await _userService.Create(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [Authorize]
    [HttpGet("all")]
    public GenericResponse<List<UserDto>> GetAll()
    {
        try
        {
            return _userService.Get();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}