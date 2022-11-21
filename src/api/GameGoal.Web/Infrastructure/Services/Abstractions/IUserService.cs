using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Abstractions;

public interface IUserService
{
    public Task<IdentityResult> CreateUserAsync(AppUser appUser, string password);

    public Task<IdentityResult> AddToRoleAsync(AppUser appUser, string role);

    public Task<IdentityResult> UpdateAsync(AppUser user);
}
