using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Shared.Common.Extensions;

public static class TypeExtensions
{
    public static string GetTypeDisplayName(this Type type)
    {
        var typeDisplayAttribute = type.GetCustomAttribute<DisplayAttribute>();
        return typeDisplayAttribute?.Name ?? throw new InvalidOperationException($"Type {type.FullName} has no display attribute");
    }

    public static string GetPropertyDisplayName(this PropertyInfo property) =>
        property.GetCustomAttribute<DisplayAttribute>()?.Name ?? throw new InvalidOperationException($"Property {property.Name} has no display attribute");
}