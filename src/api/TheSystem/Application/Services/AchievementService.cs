using Application.Services.Abstractions;
using Domain.Abstractions;
using Domain.Shared;
using Microsoft.AspNet.Identity;

namespace Application.Services;

public sealed class AchievementService : IAchievementService<Domain.Entities.Achievement>
{
    private readonly IUnitOfWork _uow;

    public AchievementService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Result> CreateAsPartOfSystem(string systemId, Domain.Entities.Achievement achievement)
    {
        var system = await _uow.AchievementSystemRepository.GetIncludingAll(systemId);

        if (system is null)
        {
            return Result.Fail().WithError("Unable to create achievement - achievement system is not found");
        }

        system.Achievements.Append(achievement);

        return await _uow.ConfirmAsync()
            ? Result.Success()
            : Result.Fail().WithError($"Unable to create achievement - {achievement.Name} for system {system.Name}");
    }

    public async Task<Result> CreateForUser(string userId, Domain.Entities.Achievement achievement)
    {
        var user = await _uow.UserRepository.GetUserIncludingAll(userId);

        if (user is null)
        {
            return Result.Fail().WithError("Unable to create achievement - user is not found");
        }

        user.Achievements.Add(achievement);

        return await _uow.ConfirmAsync()
            ? Result.Success()
            : Result.Fail().WithError($"Unable to create achievement - {achievement.Name} for user {user.UserName}");
    }
}
