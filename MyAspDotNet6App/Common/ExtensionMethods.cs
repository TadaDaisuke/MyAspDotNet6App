namespace MyAspDotNet6App.Common
{
    public static class ExtensionMethods
    {
        public static int ToInt(this string? s, int defaultValue = 0)
            => int.TryParse(s, out int i) ? i : defaultValue;

        public static int? ToNullableInt(this string? s)
            => int.TryParse(s, out int i) ? i : null;

        public static DateOnly? ToNullableDateOnly(this string? s)
            => DateOnly.TryParse(s, out DateOnly d) ? d : null;
    }
}
