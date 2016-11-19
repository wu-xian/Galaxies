using System;
using System.ComponentModel;
using System.Reflection;

public enum RequestResultType
{
    [Description("Success")]
    Success,
    [Description("Failed")]
    Failed,
    [Description("Forbiden")]
    Forbiden
}

public static class DescriptionHelper
{
    public static string GetDescription(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes =
              (DescriptionAttribute[])fi.GetCustomAttributes(
              typeof(DescriptionAttribute), false);
        return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
    }
}