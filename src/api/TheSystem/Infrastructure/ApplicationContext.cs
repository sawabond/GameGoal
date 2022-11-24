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
            var userId = Guid.NewGuid().ToString();
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = userId,
                    UserName = "Alex",
                });

            modelBuilder.Entity<AchievementSystem>().HasData(
                new AchievementSystem 
                { 
                    Id = Guid.NewGuid().ToString(), 
                    AppUserId = userId, 
                    Name = "Just in time",
                    Description = "Some descr"
                });
        }
    }
}