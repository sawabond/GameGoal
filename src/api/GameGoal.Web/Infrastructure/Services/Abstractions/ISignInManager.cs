using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Abstractions;

public interface ISignInManager
{
    Task<SignInResult> CheckPasswordSignInAsync(AppUser user, string password, bool lockoutOnFailure);
}
