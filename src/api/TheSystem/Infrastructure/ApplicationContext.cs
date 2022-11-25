using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationContext : IdentityDbContext
        <
        AppUser,
        AppRole,
        string,
        IdentityUserClaim<string>,
        AppUserRole,
        AppUserLogin,
        IdentityRoleClaim<string>,
        AppUserToken
        >
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<AchievementSystem> AchievementSystems { get; set; }

        public DbSet<Achievement> Achievements { get; set; }

        public DbSet<RelativeAchievement> RelativeAchievements { get; set; }

        public DbSet<MeasurableAchievement> MeasurableAchievements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<AppUserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
        }
    }
}