using Domain.Entities;

namespace Domain.Abstractions;

public interface IUserRepository : IDataRepository<AppUser>
{
    public Task<AppUser> GetUserByUsernameAsync(string username);

    public Task<AppUser> GetUserByEmailAsync(string email);

    public Task<AppUser> GetUserWithRolesById(string id);

    public Task<AppUser> GetUserIncludingAll(string id);

    public Task<IEnumerable<AppUser>> GetUsersIncludingAll();

    public bool Any();
}
