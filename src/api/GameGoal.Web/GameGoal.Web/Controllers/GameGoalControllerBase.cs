using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameGoal.Web.Controllers
{
    [ApiController]
    public class GameGoalControllerBase : ControllerBase
    {
        protected const int NotExistingUserId = -1;

        protected int GetUserId()
        {
            return int.TryParse(User?.Claims?
                .First(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out var id)
                ? id
                : NotExistingUserId;
        }
    }
}
