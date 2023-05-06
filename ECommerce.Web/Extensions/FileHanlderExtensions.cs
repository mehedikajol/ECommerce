using ECommerce.Web.Helpers;

namespace ECommerce.Web.Extensions;

internal static class FileHanlderExtensions
{
    internal static async Task<byte[]> ToByteArray(this IFormFile file)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var bytes = ms.ToArray();
        return bytes;
    }
}
