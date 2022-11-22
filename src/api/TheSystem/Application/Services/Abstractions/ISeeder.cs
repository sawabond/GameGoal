using Domain.Shared;

namespace Application.Services.Abstractions;

public interface ISeeder
{
    Task<Result<bool>> SeedIfNeeded();
}
