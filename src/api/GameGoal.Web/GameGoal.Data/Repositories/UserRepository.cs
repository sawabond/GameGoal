using GameGoal.Data;
using GameGoal.Data.Entities;
using GameGoal.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameGoal.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationContext _context;

        public UserRepository(UserManager<AppUser> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IdentityResult> AddToRoleAsync(AppUser appUser, string role)
        {
            return await _userManager.AddToRoleAsync(appUser, role);
        }

        public async Task<IdentityResult> CreateUserAsync(AppUser appUser, string password)
        {
            return await _userManager.CreateAsync(appUser, password);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            if (username is null)
            {
                return await Task.FromResult(null as AppUser);
            }

            return await _userManager.Users.FirstOrDefaultAsync(u => u.UserName.ToUpper() == username.ToUpper());
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            if (email is null)
            {
                return await Task.FromResult(null as AppUser);
            }

            return await _userManager.Users.FirstOrDefaultAsync(u => u.Email.ToUpper() == email.ToUpper());
        }

        public async Task<ICollection<string>> GetUserRoles(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<AppUser> FindAsync(int id)
        {
            return await _userManager.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(AppUser user)
        {
            await _userManager.UpdateAsync(user);
        }

        public void RemoveUserById(int id)
        {
            var user = _userManager.Users.Where(u => u.Id == id).FirstOrDefault();

            _context.Remove(user);
        }
    }
}
