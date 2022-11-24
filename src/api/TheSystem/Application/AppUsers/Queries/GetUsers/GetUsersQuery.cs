using Application.Abstractions.Messaging;
using Application.AppUsers.ViewModels;
using Domain.Entities;

namespace Application.AppUsers.Queries.GetUsers;

public sealed record GetUsersQuery() : IQuery<IEnumerable<AppUser>>;