using GameGoal.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace GameGoal.Data.Interfaces
{
    public interface ISignInManager
    {
        Task<SignInResult> CheckPasswordSignInAsync(AppUser user, string password, bool lockoutOnFailure);
    }
}
