using Application.Services.Abstractions;
using Domain;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public sealed class Seeder : ISeeder
{
    private readonly IUnitOfWork _uow;
    private readonly IUserService _userService;
    private readonly RoleManager<AppRole> _roleManager;

    public Seeder(IUnitOfWork uow, IUserService userService, RoleManager<AppRole> roleManager)
    {
        _uow = uow;
        _userService = userService;
        _roleManager = roleManager;
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
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin"
            };

            await _userService.CreateUserAsync(user, "Pa$$w0rd");
            await _uow.ConfirmAsync();

            await _roleManager.CreateAsync(new AppRole { Name = RoleConstants.Admin });
            await _roleManager.CreateAsync(new AppRole { Name = RoleConstants.Company });
            await _roleManager.CreateAsync(new AppRole { Name = RoleConstants.User });

            await _userService.AddToRoleAsync(user, RoleConstants.Admin);
            await _userService.AddToRoleAsync(user, RoleConstants.Company);
            await _userService.AddToRoleAsync(user, RoleConstants.User);

            await _uow.ConfirmAsync();

            user = await _uow.UserRepository.GetUserIncludingAll(user.Id);

            user.AchievementSystems.Add(new AchievementSystem
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Smoker",
                Description = "Achievement system aimed on decreasing time workers spend on smoking",
                Achievements = new[]
                {
                    new Achievement
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Not to smoke",
                        Description = "You should not smoke during working day",
                        AchievementResult = "5% salary increasing",
                        IsAchieved = false,
                        IsNegative = false,
                    }
                },
            });

            await _uow.ConfirmAsync();
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
