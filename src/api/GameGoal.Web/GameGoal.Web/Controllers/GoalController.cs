using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameGoal.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public sealed class GoalController : ControllerBase
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
