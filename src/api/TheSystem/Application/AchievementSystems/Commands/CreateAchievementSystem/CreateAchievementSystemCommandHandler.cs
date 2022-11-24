using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;

namespace Application.AchievementSystems.Commands.CreateAchievementSystem;

public sealed class CreateAchievementSystemCommandHandler : ICommandHandler<CreateAchievementSystemCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CreateAchievementSystemCommandHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<Result> Handle(CreateAchievementSystemCommand request, CancellationToken cancellationToken)
    {
        var achievementSystem = _mapper.Map<AchievementSystem>(request);

        var user = await _uow.UserRepository.GetAsync(request.AppUserId);

        user.AchievementSystems.Add(achievementSystem);

        return await _uow.ConfirmAsync()
            ? Result.Success() 
            : Result.Fail().WithError("Could not add the achievement system");
    }
}
