using Application.Abstractions.Messaging;
using Application.AppUsers.ViewModels;

namespace Application.AppUsers.Queries.GetUsers;

public sealed record GetUsersQuery() : IQuery<IEnumerable<AppUserViewModel>>;