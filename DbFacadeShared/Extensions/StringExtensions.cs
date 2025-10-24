using System;
using System.Collections.Generic;
using System.Text;

namespace DbFacade.Extensions
{
    internal static class StringExtensions
    {
        internal static string FormatCommandTextPart(this string value) => $"{(value.StartsWith("[") ? "" : "[")}{value}{(value.EndsWith("]") ? "" : "]")}";

        internal static string TypeName(this object obj)
        => obj == null ? "(null)" :
            obj is Type typeObj ? typeObj.Name :
            obj.GetType().Name;

    }
}
