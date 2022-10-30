using GameGoal.Web.Infrastructure;

namespace GameGoal.Web.Services.Abstractions
{
    public interface ISeeder
    {
        Task<Result<bool>> SeedIfNeeded();
    }
}
