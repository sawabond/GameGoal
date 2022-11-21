using Domain.Entities;
using Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> AddToRoleAsync(AppUser appUser, string role)
    {
        return await _userManager.AddToRoleAsync(appUser, role);
    }

    public async Task<IdentityResult> CreateUserAsync(AppUser appUser, string password)
    {
        return await _userManager.CreateAsync(appUser, password);
    }

    public async Task<IdentityResult> UpdateAsync(AppUser user)
    {
        return await _userManager.UpdateAsync(user);
    }
}
