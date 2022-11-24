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
    public Task<Result> Handle(CreateAchievementSystemCommand request, CancellationToken cancellationToken)
    {
        var achievementSystem = _mapper.Map<AchievementSystem>(request);
    }
}
