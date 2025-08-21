using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.Dtos;
using ProjectManager.Application.Interfaces.Services;
using ProjectManager.Application.Models;
using ProjectManager.Application.Models.Requests.User;
using ProjectManager.Utils;
using System.Security.Claims;

namespace ProjectManager.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [Authorize(Roles = UserTypeConsts.Administrator)]
    [HttpPost("create")]
    public async Task<GenericResponse<UserDto>> Create([FromBody] CreateUserRequest request)
    {
        try
        {
            var claim = GetExecutorClaim();
            return await _userService.Create(request, claim);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [Authorize]
    [HttpGet("getById/{userId:guid}")]
    public GenericResponse<UserDto> GetById(Guid userId)
    {
        try
        {
            return _userService.Get(userId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [Authorize]
    [HttpGet("getByEmailAddress/{emailAddress}")]
    public GenericResponse<UserDto> GetById(string emailAddress)
    {
        try
        {
            return _userService.Get(emailAddress);
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

    [Authorize(Roles = UserTypeConsts.Administrator)]
    [HttpPut("update/{userId:guid}")]
    public async Task<GenericResponse<UserDto>> Update(Guid userId, [FromBody] UpdateUserRequest request)
    {
        try
        {
            var claim = GetExecutorClaim();
            return await _userService.Update(userId, request, claim);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [Authorize(Roles = UserTypeConsts.Administrator)]
    [HttpDelete("remove/{userId:guid}")]
    public async Task<GenericResponse<bool>> Update(Guid userId)
    {
        try
        {
            var claim = GetExecutorClaim();
            return await _userService.Remove(userId, claim);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private Claim GetExecutorClaim()
    {
        try
        {
            return User.FindFirst("UserId") ?? throw new UnauthorizedAccessException(ResponseConsts.UnauthorizedAccessForExecuteThisAction);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}