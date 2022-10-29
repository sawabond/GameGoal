namespace GameGoal.Web.Infrastructure
{
    public sealed class Result<T>
    {
        public T? Value { get; set; }

        public bool Success { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public static Result<T> CreateSuccess(T value) =>
            new Result<T>
            {
                Value = value,
                Success = true
            };

        public static Result<T> CreateFailed() =>
            new Result<T> { Success = false };

        public Result<T> WithError(string error)
        {
            Errors = new List<string>(Errors);
            Errors.Add(error);
            Success = false;

            return this;
        }
    }
}
