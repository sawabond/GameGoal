using Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace Application.AppUsers.Commands.CreateUserFromFile;

public sealed record CreateUsersFromFileCommand
    (IFormFile FileForm,
    string CompanyId) : ICommand;
