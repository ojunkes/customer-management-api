using System.ComponentModel;

namespace Customers.Management.Application.Shared;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());

        var attribute = Attribute.GetCustomAttribute(field!, typeof(DescriptionAttribute)) as DescriptionAttribute;

        return attribute?.Description ?? value.ToString();
    }
}
