using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameGoal.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public sealed class GoalController : GameGoalControllerBase
    {
        public GoalController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetGoalsOfCurrentUser()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewGoal()
        {
            throw new NotImplementedException();
        }
    }
}
