using Application.AppUsers.Commands.CreateUser;
using Application.AppUsers.Commands.CreateUserFromFile;
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

[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public sealed class UserController : AuthorizedApiController
{
    public UserController(
        ISender sender
        ) : base(sender)
    {

    }

    [HttpGet]
    [AllowAnonymous]
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
            Role = RoleConstants.User,
            CompanyId = UserId
        };

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? await LogIn(new LogInQuery(command.UserName, command.Password))
            : BadRequest(result.Errors);
    }

    [HttpPost("register-company")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterCompany([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand
        {
            UserName = request.UserName,
            Password = request.Password,
            Role = RoleConstants.Company
        };

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? await LogIn(new LogInQuery(command.UserName, command.Password))
            : BadRequest(result.Errors);
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

        return result.IsSuccess
            ? await LogIn(new LogInQuery(command.UserName, command.Password))
            : BadRequest(result.Errors);
    }

    [HttpPost("import-members")]
    [Authorize(Roles = RoleConstants.Company)]
    public async Task<IActionResult> RegisterMemberList([FromForm]IFormFile memberList, CancellationToken cancellationToken)
    {
        var command = new CreateUsersFromFileCommand(memberList, UserId);

        var result =  await _sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Ok()
            : BadRequest(result.Errors);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LogIn(LogInQuery logInQuery)
    {
        var logInResult = await _sender.Send(logInQuery);

        return logInResult.IsSuccess ? Ok(logInResult.Value) : BadRequest(logInResult.Errors);
    }
}
