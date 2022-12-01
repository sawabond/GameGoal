namespace Application.Extensions;

public static class StringExtensions
{
    public static bool IsEmptyGuid(this string @this)
        => @this.Equals(Guid.Empty.ToString(), StringComparison.InvariantCultureIgnoreCase);
}
