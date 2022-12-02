using Application.AppUsers.Commands.CreateUser;
using Application.AppUsers.ViewModels;
using Application.Extensions;
using Application.Services.Abstractions;
using AutoMapper;
using Domain;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;
using Domain.ValueObjects.Subscriptions;

namespace Application.Services;

public sealed class UserRegistrer : IUserRegistrer
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public UserRegistrer(
        IUnitOfWork uow,
        IMapper mapper,
        IUserService userService,
        ITokenService tokenService)
    {
        _uow = uow;
        _mapper = mapper;
        _userService = userService;
        _tokenService = tokenService;
    }
    public async Task<Result> RegisterUserAsync(CreateUserCommand request)
    {
        var company = await _uow.UserRepository.GetUserIncludingAll(request.CompanyId);

        var existingUser = company
            ?.CompanyMembers
            .Where(cm => cm.UserName == request.UserName)
            .FirstOrDefault();

        if (existingUser is not null)
        {
            return Result.Fail().WithError($"User with name {request.UserName} already exists");
        }

        var newUser = _mapper.Map<AppUser>(request);

        var result = await _userService.CreateUserAsync(newUser, request.Password);

        if (!result.Succeeded)
        {
            return Result.Fail().WithErrors(result.Errors.Select(e => e.Description));
        }

        var roleResult = await _userService.AddToRoleAsync(newUser, request.Role);

        if (!roleResult.Succeeded)
        {
            return Result.Fail().WithErrors(roleResult.Errors.Select(e => e.Description));
        }

        var hasCompanyId = !request.CompanyId.IsEmptyGuid();

        newUser = await _uow.UserRepository.GetUserIncludingAll(newUser.Id);

        if (hasCompanyId)
        {
            await CreateAchievementsForUser(company, newUser);
        }
        else if (request.Role.Equals(RoleConstants.Company, StringComparison.InvariantCultureIgnoreCase))
        {
            var subscription = new BasicPlan();

            newUser.AchievementSystems.Add(subscription.Smoker);
            await _uow.ConfirmAsync();
        }

        return Result.Success();
    }

    private async Task CreateAchievementsForUser(AppUser? company, AppUser newUser)
    {
        company.CompanyMembers.Add(newUser);

        company
            .AchievementSystems
            .SelectMany(x => x.Achievements)
            .ToList()
            .ForEach(a =>
            {
                var achievement = _mapper.Map<Achievement>(a);
                achievement.Id = Guid.NewGuid().ToString();

                newUser.Achievements.Add(achievement);
            });

        await _uow.ConfirmAsync();
    }
}
