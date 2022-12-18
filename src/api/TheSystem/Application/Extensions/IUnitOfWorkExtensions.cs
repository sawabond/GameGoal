using Domain.Abstractions;
using Domain.Shared;

namespace Application.Extensions;

public static class IUnitOfWorkExtensions
{
    public static async Task<Result> ConfirmWithResult(this IUnitOfWork uow) =>
        (await uow.ConfirmAsync())
        ? Result.Success()
        : Result.Fail().WithError("Could not save changes");

    public static async Task<Result<T>> ConfirmWithResult<T>(this IUnitOfWork uow, T value) =>
        (await uow.ConfirmAsync())
        ? Result<T>.Success(value)
        : Result<T>.Fail().WithError("Could not save changes");
}
