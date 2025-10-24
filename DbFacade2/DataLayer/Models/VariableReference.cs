using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DbFacade.DataLayer.Models
{
    internal sealed class VariableReference
    {
        internal Func<object, object> Get { get; private set; }
        internal readonly MemberInfo MemberInfo;
        internal readonly string Name;
        internal readonly Type VariableType;
        internal readonly bool IsNullable;
        internal VariableReference(PropertyInfo property)
        {
            MemberInfo = property;
            Get = property.GetValue;
            Name = property.Name;
            Type underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
            VariableType = underlyingType != null ? underlyingType : property.PropertyType;
            IsNullable = underlyingType != null;
        }
        internal VariableReference(FieldInfo field)
        {
            MemberInfo = field;
            Get = field.GetValue;
            Name = field.Name;
            Type underlyingType = Nullable.GetUnderlyingType(field.FieldType);
            VariableType = underlyingType != null ? underlyingType : field.FieldType;
            IsNullable = underlyingType != null;
        }
        internal bool TryGetAttribute<T>(out T attr) where T : Attribute
        {
            try
            {
                attr = MemberInfo.GetCustomAttribute<T>();
                return attr != null;
            }
            catch
            {
                attr = null;
                return false;
            }
        }
    }
}
