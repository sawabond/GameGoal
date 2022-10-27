namespace GameGoal.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        ISignInManager SignInManager { get; }

        Task<bool> ConfirmAsync();
    }
}