namespace GameGoal.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        ISignInManager SignInManager { get; }

        IGoalRepository GoalRepository { get; }

        Task<bool> ConfirmAsync();
    }
}