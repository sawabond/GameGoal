using Domain.Entities;
using Domain.Shared;

namespace Infrastructure.Services.Abstractions;

public interface ITokenService
{
    Task<Result<string>> CreateToken(AppUser user);
}
