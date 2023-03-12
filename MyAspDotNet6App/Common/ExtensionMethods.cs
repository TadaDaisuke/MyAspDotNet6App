namespace MyAspDotNet6App.Common
{
    public static class ExtensionMethods
    {
        public static int ToInt(this string? s, int defaultValue = 0)
            => int.TryParse(s, out int workInt) ? workInt : defaultValue;
    }
}
