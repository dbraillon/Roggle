using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Roggle.Core
{
    public static class RoggleHelper
    {
        public static string GetDisplayValue<TEnum>(this TEnum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(TEnum), false) as DisplayAttribute[];

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }

        public static IReadOnlyList<T> GetEnumValues<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }
    }
}
