using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace Hermes.Common.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var fi = value.GetType().GetField(value.ToString());
        var attributes =
            fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[] ??
            [];
        return attributes.Length != 0 ? attributes.First().Description : value.ToString();
    }

    public static List<string> GetEnumValues<T>() where T : Enum
    {
        return GetValues<T>().Select(x => x.ToString()).ToList();
    }

    public static T[] GetValues<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
    }
}