using Application.Services.Abstractions;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;

namespace Application.Services;

public sealed class Seeder : ISeeder
{
    private readonly IUnitOfWork _uow;
    private readonly IUserService _userService;

    public Seeder(IUnitOfWork uow, IUserService userService)
    {
        _uow = uow;
        _userService = userService;
    }

    public async Task<Result<bool>> SeedIfNeeded()
    {
        if (_uow.UserRepository.Any())
        {
            return Result<bool>.Success(false);
        }

        try
        {
            var user = new AppUser
            {
                UserName = "Admin"
            };

            await _userService.CreateUserAsync(user, "Pa$$w0rd");
        }
        catch (Exception ex)
        {
            return Result<bool>
                .Fail()
                .WithError(ex.Message);
        }

        return Result<bool>.Success(true);
    }
}
