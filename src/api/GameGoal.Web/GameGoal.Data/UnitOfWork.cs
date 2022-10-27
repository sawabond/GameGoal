using GameGoal.Data.Entities;
using GameGoal.Data.Interfaces;
using GameGoal.Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace GameGoal.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;

            _context.Database.EnsureCreated();

            UserRepository = new UserRepository(userManager, context);
            SignInManager = new SignInManager(signInManager);
        }

        public IUserRepository UserRepository { get; private set; }

        public ISignInManager SignInManager { get; private set; }

        public async Task<bool> ConfirmAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
