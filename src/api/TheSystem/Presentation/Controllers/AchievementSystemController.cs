using Application.AchievementSystems.Commands.CreateAchievementSystem;
using Application.AchievementSystems.Queries.GetAchievementSystemById;
using Application.AchievementSystems.Queries.GetAchievementSystemsByUserId;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;

namespace Presentation.Controllers;

[Authorize(Roles = RoleConstants.Company)]
public sealed class AchievementSystemController : AuthorizedApiController
{
    public AchievementSystemController(ISender sender) : base(sender)
    {

    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAchievementSystemById(string id)
    {
        var query = new GetAchievementSystemByIdQuery(id);

        var result = await _sender.Send(query);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetAchievementSystemsOfUser()
    {
        var query = new GetAchievementSystemsByUserIdQuery(UserId);

        var result = await _sender.Send(query);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAchievementSystem([FromBody] CreateAchievementSystemRequest request)
    {
        var command = new CreateAchievementSystemCommand(UserId, request.Name, request.Description);

        var createAchievementSystemResult = await _sender.Send(command);

        return createAchievementSystemResult.IsSuccess
            ? Ok()
            : BadRequest(createAchievementSystemResult.Errors);
    }
}
