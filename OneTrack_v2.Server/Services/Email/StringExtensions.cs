namespace OneTrack_v2.Services.Email
{
    public static class StringExtensions
    {
        public static string CleanAngularBindings(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input
                .Replace("SafeValue must use [property]=binding:", string.Empty)
                .Replace(" (see https://g.co/ng/security#xss)", string.Empty)
                .Trim();
        }
    }
}