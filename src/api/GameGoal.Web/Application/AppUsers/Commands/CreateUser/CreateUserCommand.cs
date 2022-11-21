using Application.Abstractions.Messaging;

namespace Application.AppUsers.Commands.CreateUser;

public record CreateUserCommand
    (string UserName, 
    string Password) : ICommand;
