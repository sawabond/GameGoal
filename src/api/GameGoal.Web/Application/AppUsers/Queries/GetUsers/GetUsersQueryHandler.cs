using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.AppUsers.Queries.GetUsers;

public sealed class GetUsersQueryHandler : ICommandHandler<GetUsersQuery>
{
    public async Task<Result<IEnumerable<AppUser>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
