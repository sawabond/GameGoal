using Application.Abstractions.Messaging;
using Application.Achievements.ViewModels;

namespace Application.Achievements.Queries.GetAllAchievements;

public sealed record GetAllAchievementsQuery() : IQuery<IEnumerable<AchievementViewModel>>;
