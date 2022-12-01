using Application.Achievements.Commands.CreateAchievement;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;

namespace Presentation.Controllers;

[Authorize]
public sealed class AchievementController : AuthorizedApiController
{
    public AchievementController(ISender sender) : base(sender)
    {

    }

    [HttpPost("achievement")]
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
}
