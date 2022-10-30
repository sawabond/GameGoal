using GameGoal.Data.Entities;
using GameGoal.Data.GenericRepository;
using GameGoal.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GameGoal.Data.Repositories
{
    public sealed class GoalRepository : DataRepository<Goal>, IGoalRepository
    {
        public GoalRepository(IdentityDbContext
            <AppUser, 
            AppRole, 
            int, 
            IdentityUserClaim<int>, 
            AppUserRole, 
            IdentityUserLogin<int>, 
            IdentityRoleClaim<int>, 
            IdentityUserToken<int>> context) : base(context)
        {
        }
    }
}
