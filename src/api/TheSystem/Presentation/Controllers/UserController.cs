using Application.AppUsers.Commands.CreateUser;
using Application.AppUsers.Queries.GetUsers;
using Application.AppUsers.Queries.LogIn;
using Domain;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;

namespace Presentation.Controllers;

public class UserController : ApiController
{

    public UserController(
        ISender sender
        ) : base(sender)
    {

    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var queryResult = await _sender.Send(new GetUsersQuery());

        return queryResult.IsSuccess ? Ok(queryResult.Value) : BadRequest(queryResult.Errors);
    }

    [HttpPost("register-user")]
    [Authorize(Roles = RoleConstants.Company)]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand
        { 
            UserName = request.UserName,
            Password = request.Password,
            Role = RoleConstants.User
        };

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }

    [HttpPost("register-company")]
    public async Task<IActionResult> RegisterCompany([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand
        {
            UserName = request.UserName,
            Password = request.Password,
            Role = RoleConstants.Company
        };

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }

    [HttpPost("register-admin")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand
        {
            UserName = request.UserName,
            Password = request.Password,
            Role = RoleConstants.Admin
        };

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn(LogInQuery logInQuery)
    {
        var logInResult = await _sender.Send(logInQuery);

        return logInResult.IsSuccess ? Ok(logInResult.Value) : BadRequest(logInResult.Errors);
    }
}
