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
        // TODO: Provide adding the achievement to users when the achievement is created
        var achievement = _mapper.Map<Achievement>(request);

        return await _achievementService.CreateAsPartOfSystem(request.AchievementSystemId, achievement);
    }
}
