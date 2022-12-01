using Application.Abstractions.Messaging;
using Application.AchievementSystems.ViewModels;
using AutoMapper;
using Domain.Abstractions;
using Domain.Shared;

namespace Application.AchievementSystems.Queries.GetAchievementSystemsByUserId
{
    internal class GetAchievementSystemsByUserIdQueryHandler
        : IQueryHandler<GetAchievementSystemsByUserIdQuery, IEnumerable<AchievementSystemViewModel>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAchievementSystemsByUserIdQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<AchievementSystemViewModel>>> Handle(
            GetAchievementSystemsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _uow.UserRepository.GetUserIncludingAll(request.userId);

            if (user is null)
            {
                return Result<IEnumerable<AchievementSystemViewModel>>
                    .Fail().WithError($"User with id {request.userId} is not found");
            }

            return Result<IEnumerable<AchievementSystemViewModel>>.Success(
                _mapper.Map<IEnumerable<AchievementSystemViewModel>>(user.AchievementSystems));
        }
    }
}
