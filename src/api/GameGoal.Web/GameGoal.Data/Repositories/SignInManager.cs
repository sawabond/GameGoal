using GameGoal.Data.Entities;
using GameGoal.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GameGoal.Data.Repositories
{
    public class SignInManager : ISignInManager
    {
        private readonly SignInManager<AppUser> _signInManagerDelegated;

        public SignInManager(SignInManager<AppUser> signInManagerDelegated)
        {
            _signInManagerDelegated = signInManagerDelegated;
        }

        public async Task<SignInResult> CheckPasswordSignInAsync(AppUser user, string password, bool lockoutOnFailur)
        {
            return await _signInManagerDelegated.CheckPasswordSignInAsync(user, password, lockoutOnFailur);
        }
    }
}
