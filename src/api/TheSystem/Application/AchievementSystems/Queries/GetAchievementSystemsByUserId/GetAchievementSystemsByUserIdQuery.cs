using Application.Abstractions.Messaging;
using Application.AchievementSystems.ViewModels;

namespace Application.AchievementSystems.Queries.GetAchievementSystemsByUserId;

public sealed record GetAchievementSystemsByUserIdQuery
    (string userId) : IQuery<IEnumerable<AchievementSystemViewModel>>;
