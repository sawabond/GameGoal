using GameGoal.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameGoal.Data
{
    public class ApplicationContext : IdentityDbContext
        <
        AppUser,
        AppRole,
        int,
        IdentityUserClaim<int>,
        AppUserRole,
        IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        IdentityUserToken<int>
        >
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Goal> Goals { get; set; }

        public DbSet<Skin> Skins { get; set; }
    }
}