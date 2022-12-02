using Application.Abstractions.Messaging;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entities;
using Domain.Shared;

namespace Application.Achievements.Commands.CreateAchievement;

public sealed class CreateAchievementCommandHandler : ICommandHandler<CreateAchievementCommand>
{
    private readonly IAchievementService<Achievement> _achievementService;
    private readonly IMapper _mapper;

    public CreateAchievementCommandHandler(IAchievementService<Achievement> achievementService, IMapper mapper)
    {
        _achievementService = achievementService;
        _mapper = mapper;
    }

    public async Task<Result> Handle(CreateAchievementCommand request, CancellationToken cancellationToken)
    {
        var achievement = _mapper.Map<Achievement>(request);

        var systemResult = await _achievementService.CreateAchievement(request.AchievementSystemId, achievement);

        return systemResult;
    }
}
