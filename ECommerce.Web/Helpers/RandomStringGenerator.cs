namespace ECommerce.Web.Helpers;

internal static class RandomStringGenerator
{
    internal static string GenerateUppercaseRandomString(int length)
    {
        var r = new Random();
        return new string(Enumerable.Range(0, length).Select(n => (char)r.Next(65, 90 + 1)).ToArray()); // only uppercase
    }

    internal static string GenerateLowercaseRandomString(int length)
    {
        var r = new Random();
        return new string(Enumerable.Range(0, length).Select(n => (char)(r.Next(97, 122 + 1))).ToArray()); // only lowercase
    }
}
