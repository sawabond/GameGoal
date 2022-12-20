using Application.Achievements.Commands.CompleteAchievement;
using Application.Achievements.Commands.CreateAchievement;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;

namespace Presentation.Controllers;

public sealed class AchievementController : AuthorizedApiController
{
    public AchievementController(ISender sender) : base(sender)
    {

    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.Company)]
    public async Task<IActionResult> CreateAchievement([FromBody] CreateAchievementRequest request)
    {
        var command = new CreateAchievementCommand(
            request.AchievementSystemId, 
            request.Name, 
            request.Description, 
            request.IsNegative);

        var result = await _sender.Send(command);

        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result.Errors);
    }

    [HttpPost("{name}")]
    [Authorize(Roles = RoleConstants.User)]
    public async Task<IActionResult> CompleteAchievement(string name)
    {
        var command = new CompleteAchievementCommand(UserId, name);

        var result = await _sender.Send(command);

        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result.Errors);
    }
}
