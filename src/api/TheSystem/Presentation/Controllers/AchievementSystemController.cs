using Application.AchievementSystems.Commands.CreateAchievementSystem;
using Application.AchievementSystems.Queries.GetAchievementSystemsByUserId;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        return Ok(string.Empty);
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
    public async Task<IActionResult> CreateAchievementSystem(CreateAchievementSystemCommand request)
    {
        var createAchievementSystemResult = await _sender.Send(request);

        return createAchievementSystemResult.IsSuccess
            ? Ok()
            : BadRequest(createAchievementSystemResult.Errors);
    }
}
