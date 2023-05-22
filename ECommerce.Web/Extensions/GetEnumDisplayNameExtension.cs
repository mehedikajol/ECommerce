using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ECommerce.Web.Extensions;

internal static class GetEnumDisplayNameExtension
{
    internal static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?.GetName();
    }

}
