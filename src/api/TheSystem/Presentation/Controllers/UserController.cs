using Application.AppUsers.Commands.CreateUser;
using Application.AppUsers.Queries.GetUsers;
using Application.AppUsers.Queries.LogIn;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(string username, string password, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(username, password);

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
