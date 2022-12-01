using Application.Abstractions.Messaging;
using Application.Services.Abstractions;
using Domain.Shared;

namespace Application.AppUsers.Commands.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRegistrer _registrer;

    public CreateUserCommandHandler(IUserRegistrer registrer)
    {
        _registrer = registrer;
    }
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await _registrer.RegisterUserAsync(request);
    }
}
