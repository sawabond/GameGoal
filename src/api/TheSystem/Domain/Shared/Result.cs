namespace Domain.Shared;

public class Result<T>
{
    public T? Value { get; set; }

    public bool IsSuccess { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    public static Result<T> Success(T value) =>
        new Result<T>
        {
            Value = value,
            IsSuccess = true
        };

    public static Result<T> Fail() =>
        new Result<T> { IsSuccess = false };

    public Result<T> WithError(string error)
    {
        Errors = new List<string>(Errors)
        {
            error
        };

        IsSuccess = false;

        return this;
    }

    public Result<T> WithErrors(IEnumerable<string> errors)
    {
        Errors.AddRange(errors);

        IsSuccess = false;

        return this;
    }
}

public class Result
{
    public bool IsSuccess { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    public static Result Success() =>
        new Result{ IsSuccess = true };

    public static Result Fail() =>
        new Result { IsSuccess = false };

    public Result WithError(string error)
    {
        Errors = new List<string>(Errors)
        {
            error
        };

        IsSuccess = false;

        return this;
    }

    public Result WithErrors(IEnumerable<string> errors)
    {
        Errors.AddRange(errors);

        IsSuccess = false;

        return this;
    }
}
