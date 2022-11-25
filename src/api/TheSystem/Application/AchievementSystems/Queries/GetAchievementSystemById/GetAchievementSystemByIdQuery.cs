using Application.Abstractions.Messaging;
using Application.AchievementSystems.ViewModels;

namespace Application.AchievementSystems.Queries.GetAchievementSystemById;

public sealed record GetAchievementSystemByIdQuery
    (string Id) : IQuery<AchievementSystemViewModel>;
