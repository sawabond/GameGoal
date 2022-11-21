using Domain.Entities;
using Domain.Shared;
using Infrastructure.Services.Abstractions;

namespace Infrastructure.Services;

public sealed class Seeder : ISeeder
{
    private readonly ApplicationContext _context;
    private readonly IUserService _userService;

    public Seeder(ApplicationContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<Result<bool>> SeedIfNeeded()
    {
        if (_context.Users.Any())
        {
            return Result<bool>.CreateSuccess(false);
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
                .CreateFailed()
                .WithError(ex.Message);
        }

        return Result<bool>.CreateSuccess(true);
    }
}
