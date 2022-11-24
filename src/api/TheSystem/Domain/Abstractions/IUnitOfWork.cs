namespace Domain.Abstractions;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }

    IAchievementSystemRepository AchievementSystemRepository { get; }

    Task<bool> ConfirmAsync();
}