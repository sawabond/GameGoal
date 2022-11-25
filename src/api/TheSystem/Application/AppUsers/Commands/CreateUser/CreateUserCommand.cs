using Application.Abstractions.Messaging;
using Domain;

namespace Application.AppUsers.Commands.CreateUser;

public sealed class CreateUserCommand : ICommand
{
    public string UserName { get; set; }

    public string Password { get; set; }

    public string Role { get; set; } = RoleConstants.User;
}