using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers;

[Authorize]
public sealed class AchievementController : AuthorizedApiController
{
    public AchievementController(ISender sender) : base(sender)
    {
    }
}
