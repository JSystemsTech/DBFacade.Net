using System;
using System.Linq;

namespace DbFacade.Extensions
{
    internal static class StringExtensions
    {
        internal static string FormatCommandTextPart(this string value) => $"{(value.StartsWith("[") ? "" : "[")}{value}{(value.EndsWith("]") ? "" : "]")}";

        internal static string TypeName(this object obj)
        => obj == null ? "(null)" :
            obj is Type typeObj ? typeObj.Name :
            obj.GetType().Name;

        internal static string TypeNames(this Type[] types)
        => string.Join(", ", types.Select(t => t.TypeName()));

    }
}
