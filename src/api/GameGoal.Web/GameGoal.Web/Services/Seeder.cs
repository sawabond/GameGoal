using GameGoal.Data;
using GameGoal.Data.Entities;
using GameGoal.Data.Interfaces;
using GameGoal.Web.Infrastructure;
using GameGoal.Web.Services.Abstractions;

namespace GameGoal.Web.Services
{
    public sealed class Seeder : ISeeder
    {
        private readonly ApplicationContext _context;
        private readonly IUnitOfWork _uow;

        public Seeder(ApplicationContext context, IUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        public async Task<Result<bool>> SeedIfNeeded()
        {
            if (_context.Users.Any())
            {
                return Result<bool>.CreateSuccess(false);
            }

            try
            {
                var user = new AppUser
                {
                    UserName = "Admin",
                    Goals = new List<Goal>()
                };

                user.Goals.Add(new Goal { Name = "Gain muscles", Priority = 30, Complexity = 30 });
                user.Goals.Add(new Goal { Name = "Become a programmer", Priority = 80, Complexity = 80 });

                await _uow.UserRepository.CreateUserAsync(user, "Pa$$w0rd");
            }
            catch (Exception ex)
            {
                return Result<bool>
                    .CreateFailed()
                    .WithError(ex.Message);
            }

            return Result<bool>.CreateSuccess(true);
        }
    }
}
