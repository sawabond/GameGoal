using System.Text;

namespace Application.Extensions;

public static class StringExtensions
{
    public static byte[] ToByteArray(this string @this) =>
            Encoding.UTF8.GetBytes(@this ?? string.Empty);

    public static bool IsEmptyGuid(this string @this)
    => @this.Equals(Guid.Empty.ToString(), StringComparison.InvariantCultureIgnoreCase);
}
