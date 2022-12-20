using Application.Abstractions.Messaging;
using Application.AchievementSystems.ViewModels;
using AutoMapper;
using Domain.Abstractions;
using Domain.Shared;

namespace Application.AchievementSystems.Queries.GetAchievementSystemById;

public sealed class GetAchievementSystemByIdQueryHandler
    : IQueryHandler<GetAchievementSystemByIdQuery, AchievementSystemViewModel>
{
    private readonly IAchievementSystemRepository _achievementSystemRepository;
    private readonly IMapper _mapper;

    public GetAchievementSystemByIdQueryHandler(
        IAchievementSystemRepository achievementSystemRepository,
        IMapper mapper)
    {
        _achievementSystemRepository = achievementSystemRepository;
        _mapper = mapper;
    }

    public async Task<Result<AchievementSystemViewModel>> Handle(
        GetAchievementSystemByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var achievementSystem = await _achievementSystemRepository.GetAsync(request.Id);

        if (achievementSystem is null)
        {
            return Result<AchievementSystemViewModel>.Fail()
                .WithError($"Achievement system with id {request.Id} not found");
        }

        return Result<AchievementSystemViewModel>
            .Success(_mapper.Map<AchievementSystemViewModel>(achievementSystem));
    }
}
