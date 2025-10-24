using System;
using System.Data;

namespace DbFacade.DataLayer.Models.Parameters
{
    internal sealed class ParameterInfo
    {
        internal readonly string Name;
        internal readonly object Value;
        internal readonly ParameterDirection Direction;
        internal readonly int? Size;
        internal readonly bool IsNullable;
        internal readonly Type Type;
        internal ParameterInfo(string name, object value,Type type, ParameterDirection direction, int? size)
        {
            Name = name;
            Value = value;
            Direction = direction;
            Size = size;
            IsNullable = type == typeof(object) || type == typeof(string) || Nullable.GetUnderlyingType(type) != null;
            Type = type;
        }
    }
}
