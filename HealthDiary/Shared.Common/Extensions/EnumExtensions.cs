using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.Extensions;

namespace Shared.Common.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum value)
    {
        var displayAttribute = value.GetAttributeOfType<DisplayAttribute>();
        return displayAttribute?.Name ?? throw new InvalidOperationException($"Enum value {value.ToString()} has no display attribute");
    }    
}