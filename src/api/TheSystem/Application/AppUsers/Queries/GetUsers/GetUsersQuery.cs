using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.AppUsers.Queries.GetUsers;

public sealed record GetUsersQuery() : IQuery<IEnumerable<AppUser>>;