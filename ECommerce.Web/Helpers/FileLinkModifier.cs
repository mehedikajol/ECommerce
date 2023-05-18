using ECommerce.Core.Common;
using Microsoft.Extensions.Options;

namespace ECommerce.Web.Helpers;

internal static class FileLinkModifier
{
    internal static string GenerateImageLink(HttpRequest request, string directoryName, string fileName)
    {
        return request.Scheme + "://" + request.Host + directoryName + "/" + fileName.Replace('\\', '/');
    }
}
