using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

public static class EnumStatic
{
    public static string GetEnumDescription(Round value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        if (attributes != null && attributes.Any())
        {
            return attributes.First().Description;
        }

        return value.ToString();
    }

    public static T Next<T>(this T src) where T : struct
    {
        if (!typeof(T).IsEnum)
            throw new ArgumentException(String.Format("Argument {0} is not an Enum",
                                                       typeof(T).FullName));

        T[] Arr = (T[])Enum.GetValues(src.GetType());

        int j = (Array.IndexOf<T>(Arr, src) + 1) % Arr.Length;

        return Arr[j];
    }
}
