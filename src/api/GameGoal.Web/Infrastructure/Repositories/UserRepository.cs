using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : DataRepository<AppUser>, IUserRepository
{
    public UserRepository(ApplicationContext context)
        : base(context)
    {

    }

    private DbSet<AppUser> Users => _context.Users;

    public async Task<AppUser> GetUserByEmailAsync(string email)
    {
        return await Users.Where(u => u.Email == email).FirstOrDefaultAsync();
    }

    public async Task<AppUser> GetUserByUsernameAsync(string username)
    {
        return await Users.Where(u => u.UserName == username).FirstOrDefaultAsync();
    }

    public async Task<AppUser> GetUserWithRolesById(string id)
    {
        var userWithRoles = await Users
            .Where(u => u.Id == id)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync();

        return userWithRoles;
    }
}
