using Domain.Abstractions;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();

            UserRepository = new UserRepository(context);
            AchievementSystemRepository = new AchievementSystemRepository(context);
            AchievementRepository = new AchievementRepository(context);
        }

        public IUserRepository UserRepository { get; }

        public IAchievementSystemRepository AchievementSystemRepository { get; }

        public IAchievementRepository AchievementRepository { get; }

        public async Task<bool> ConfirmAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
