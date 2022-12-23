namespace Domain.Shared;

public sealed class ValidationResult : Result, IValidationResult
{
    public Error[] Errors => throw new NotImplementedException();

    public static ValidationResult WithErrors(Error[] errors) => new(errors);
}
