using Domain.Entities;
using Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class SignInManager : ISignInManager
{
    private readonly SignInManager _signInManagerDelegated;

    public SignInManager(SignInManager signInManagerDelegated)
    {
        _signInManagerDelegated = signInManagerDelegated;
    }

    public async Task<SignInResult> CheckPasswordSignInAsync(AppUser user, string password, bool lockoutOnFailur)
    {
        return await _signInManagerDelegated.CheckPasswordSignInAsync(user, password, lockoutOnFailur);
    }
}
