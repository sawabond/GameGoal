using Domain.Entities;
using Domain.Shared;

namespace Application.Services.Abstractions;

public interface ITokenService
{
    Task<Result<string>> CreateToken(AppUser user);
}
