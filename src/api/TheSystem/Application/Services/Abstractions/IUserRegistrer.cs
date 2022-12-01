using Application.AppUsers.Commands.CreateUser;
using Domain.Shared;

namespace Application.Services.Abstractions;

public interface IUserRegistrer
{
    Task<Result> RegisterUserAsync(CreateUserCommand command);
}
