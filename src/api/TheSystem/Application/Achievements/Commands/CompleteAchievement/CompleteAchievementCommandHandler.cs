using Application.Abstractions.Messaging;
using Application.Extensions;
using Domain.Abstractions;
using Domain.Shared;

namespace Application.Achievements.Commands.CompleteAchievement;

public sealed class CompleteAchievementCommandHandler : ICommandHandler<CompleteAchievementCommand>
{
    private readonly IUnitOfWork _uow;

    public CompleteAchievementCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Result> Handle(CompleteAchievementCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserRepository.GetUserIncludingAll(request.UserId);

        if (user is null)
        {
            return Result.Fail().WithError($"User with id {request.UserId} not found");
        }

        var achievement = user.Achievements
            .Where(a => a.Name == request.Name)
            .FirstOrDefault();

        var result = Result.Success();

        if (achievement is null)
        {
            return result.WithError($"Achievement with name {request.Name} is not found");
        }
        
        if (achievement.IsAchieved)
        {
            return result.WithError($"Achievement with name {request.Name} is already completed");
        }

        achievement.IsAchieved = true;

        return await _uow.ConfirmWithResult();

    }
}
