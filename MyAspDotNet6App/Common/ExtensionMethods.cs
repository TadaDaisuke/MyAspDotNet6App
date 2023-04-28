using System.Globalization;

namespace MyAspDotNet6App.Common;

public static class ExtensionMethods
{
    public static string? OrNullIfWhiteSpace(this string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value;

    public static int ToInt(this string? s, int defaultValue = 0)
        => int.TryParse(s, out int i) ? i : defaultValue;

    public static int? ToNullableInt(this string? s)
        => int.TryParse(s, out int i) ? i : null;

    public static DateOnly? ToNullableDateOnly(this string? s, string? format = null)
        => DateOnly.TryParseExact(s, format ?? DEFAULT_DATEONLY_FORMAT, null, DateTimeStyles.None, out DateOnly d) ? d : null;

    public static DateTime? ToNullableDateTime(this string? s, string? format = null)
        => DateTime.TryParseExact(s, format ?? DEFAULT_DATETIME_FORMAT, null, DateTimeStyles.None, out DateTime d) ? d : null;

    public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T>? values)
        => values ?? Enumerable.Empty<T>();
}
