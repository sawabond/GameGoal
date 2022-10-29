using GameGoal.Data.Entities;
using GameGoal.Web.Infrastructure;

namespace GameGoal.Web.Services.Abstractions
{
    public interface ITokenService
    {
        Task<Result<string>> CreateToken(AppUser user);
    }
}
