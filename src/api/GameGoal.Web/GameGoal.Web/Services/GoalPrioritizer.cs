using GameGoal.Data.Entities;
using GameGoal.Web.Infrastructure;
using GameGoal.Web.Services.Abstractions;

namespace GameGoal.Web.Services
{
    public sealed class GoalPrioritizer : IGoalPrioritizer
    {
        /// <summary>
        /// Advices best goal for the user depending on his/her state
        /// </summary>
        /// <param name="user">User whose goals will be selected</param>
        /// <returns>Best goal</returns>
        public Result<Goal> AdviseBestGoal(AppUser user)
        {
            if (user is null)
            {
                return Result<Goal>
                    .CreateFailed()
                    .WithError("Given user has been equal to null");
            }

            if (user.Goals is null || !user.Goals.Any())
            {
                return Result<Goal>
                    .CreateFailed()
                    .WithError("There are not any goals to pick the best from");
            }

            var orderedGoals = user.Goals
            .Where(x => !x.IsCompleted)
            .OrderByDescending(x => x.Priority)
            .ThenByDescending(x => x.Complexity)
            .ThenBy(x => x.DeadLine)
            .ThenByDescending(x => x.Progression)
            .ThenBy(x => x.DateOfCreation);

            var readinessCoef = CalculateReadinessCoefficient(user);

            var bestGoal = orderedGoals
                .Where(x => x.Complexity < readinessCoef)
                .FirstOrDefault();


            return Result<Goal>.CreateSuccess(bestGoal ?? orderedGoals.Last());
        }

        /// <summary>
        /// The readiness coefficient can be between -100 and 100.
        /// It depends on level of hormones in blood and one's well-being
        /// </summary>
        /// <param name="user">User whose readiness coefficient will be calculated</param>
        /// <returns>Readiness coefficient between -100 and 100</returns>
        private double CalculateReadinessCoefficient(AppUser user) =>
            Enumerable.Average(new[] { user.Dopamine, user.Oxytocin, user.Serotonin, user.Endorphins })
            * user.Health / 100
            - user.Cortisol;
    }
}
