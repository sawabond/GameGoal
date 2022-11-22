using Application.Abstractions.Messaging;
using Application.AppUsers.ViewModels;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;

namespace Application.AppUsers.Queries.GetUsers;

public sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<AppUserViewModel>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<Result<IEnumerable<AppUserViewModel>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _uow.UserRepository.GetAllAsync();

        if (users is null)
        {
            return Result<IEnumerable<AppUserViewModel>>.Fail();
        }

        var usersViewModels = _mapper.Map<IEnumerable<AppUser>, IEnumerable<AppUserViewModel>>(users);

        return Result<IEnumerable<AppUserViewModel>>.Success(usersViewModels);
    }
}
