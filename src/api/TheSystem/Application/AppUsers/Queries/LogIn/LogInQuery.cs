using Application.Abstractions.Messaging;
using Application.AppUsers.ViewModels;

namespace Application.AppUsers.Queries.LogIn;

public sealed record LogInQuery(
    string UserName,
    string Password) : IQuery<AppUserViewModel>;
