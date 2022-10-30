using GameGoal.Data.Entities;
using GameGoal.Web.Infrastructure;

namespace GameGoal.Web.Services.Abstractions
{
    public interface IGoalPrioritizer
    {
        Result<Goal> AdviseBestGoal(AppUser user);
    }
}
