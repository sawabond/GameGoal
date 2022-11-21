using Domain.Shared;

namespace Infrastructure.Services.Abstractions;

public interface ISeeder
{
    Task<Result<bool>> SeedIfNeeded();
}
