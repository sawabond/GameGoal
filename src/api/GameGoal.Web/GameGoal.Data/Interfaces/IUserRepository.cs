using GameGoal.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace GameGoal.Data.Interfaces
{
    public interface IUserRepository
    {
        public Task<AppUser> GetUserByUsernameAsync(string username);

        public Task<AppUser> GetUserByEmailAsync(string email);

        public Task<AppUser> GetUserWithGoalsById(int id);

        public Task<IEnumerable<AppUser>> GetUsersAsync();

        public Task<IdentityResult> CreateUserAsync(AppUser appUser, string password);

        public Task<IdentityResult> AddToRoleAsync(AppUser appUser, string role);

        public Task<AppUser> FindAsync(int id);

        public Task<IdentityResult> UpdateAsync(AppUser user);

        public Task<ICollection<string>> GetUserRoles(AppUser user);

        public void RemoveUserById(int id);
    }
}
