using Application.Abstractions.Messaging;
using Application.Achievements.ViewModels;
using AutoMapper;
using Domain.Abstractions;
using Domain.Shared;

namespace Application.Achievements.Queries.GetAllAchievements;

public sealed class GetAllAchievementsQueryHandler
    : IQueryHandler<GetAllAchievementsQuery, IEnumerable<AchievementViewModel>>
{
    private readonly IAchievementRepository _achievementRepository;
    private readonly IMapper _mapper;

    public GetAllAchievementsQueryHandler(IAchievementRepository achievementRepository, IMapper mapper)
    {
        _achievementRepository = achievementRepository;
        _mapper = mapper;
    }
    public async Task<Result<IEnumerable<AchievementViewModel>>> Handle(GetAllAchievementsQuery request, CancellationToken cancellationToken)
        => Result<IEnumerable<AchievementViewModel>>.Success(
            _mapper.Map<IEnumerable<AchievementViewModel>>(
                await _achievementRepository.GetAllAsync()));
}
