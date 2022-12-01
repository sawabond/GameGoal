namespace Domain.Abstractions;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }

    IAchievementSystemRepository AchievementSystemRepository { get; }

    IAchievementRepository AchievementRepository { get; }

    Task<bool> ConfirmAsync();
}