using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DbFacade.Factories
{
    internal class ConversionFactory
    {
        internal static T GetValue<T>(object input, T defaultValue = default(T), bool useEmptyStringAsNull = false)
        => TryGetValue(input, out T value, defaultValue, useEmptyStringAsNull) ? value : defaultValue;
        private static bool TryGetValue<T>(object input, out T value, T defaultValue, bool useEmptyStringAsNull)
        {
            Type rootType = Nullable.GetUnderlyingType(typeof(T));
            Type type = rootType != null ? rootType : typeof(T);
            if (input != null && type == input.GetType() || type == typeof(object))
            {
                value = (T)input;
                return true;                
            }
            bool isToString = type == typeof(string);
            bool isToChar = type == typeof(char);
            bool isToNullable = rootType != null;

            bool isNullableEmptyStr = useEmptyStringAsNull && input is string nullableStr && string.IsNullOrWhiteSpace(nullableStr);
            bool isNullableNull = (isToString ||isToNullable) && input == null;

            try
            {
                if (isNullableEmptyStr || isNullableNull)
                {
                    value = (T)(object)null;
                }
                else if (isToString)
                {
                    value = (T)(object)input.ToString();
                }
                else if (TryGetEnum(input, type, out object enumType))
                {
                    value = (T)enumType;
                }
                else if (input is char chr)
                {
                    TypeConverter tc = TypeDescriptor.GetConverter(type);
                    value = (T)tc.ConvertFromString(chr.ToString());
                }
                else if (TryGetCast(input, type, out object castValue))
                {
                    value = (T)castValue;
                }
                else if (isToChar)
                {
                    value = (T)(object)input.ToString().ToCharArray().FirstOrDefault();
                }
                else if (input is string str)
                {
                    TypeConverter tc = TypeDescriptor.GetConverter(type);
                    value = (T)tc.ConvertFromString(str);
                }
                else
                {
                    TypeConverter tc = TypeDescriptor.GetConverter(type);
                    value = (T)tc.ConvertFrom(input);
                }
                return true;
            }
            catch
            {
                value = defaultValue;
                return false;
            }
        }
        internal static IEnumerable<T> ToEnumerable<T>(string arrStr, char separator = ',')
        {
            if (string.IsNullOrWhiteSpace(arrStr))
            {
                return Array.Empty<T>();
            }
            List<T> list = new List<T>();
            foreach(string str in arrStr.Split(separator))
            {
                list.Add(GetValue(str,default(T),true));
            }
            return list;
        }

        private class Types
        {
            internal static Type Byte = typeof(byte);
            internal static Type Sbyte = typeof(sbyte);
            internal static Type Short = typeof(short);
            internal static Type Ushort = typeof(ushort);
            internal static Type Int = typeof(int);
            internal static Type Uint = typeof(uint);
            internal static Type Long = typeof(long);
            internal static Type Ulong = typeof(ulong);
            internal static Type Float = typeof(float);
            internal static Type Double = typeof(double);
            internal static Type Decimal = typeof(decimal);
            internal static Type DateTime = typeof(DateTime);
            internal static Type Char = typeof(char);
            internal static Type Bool = typeof(bool);
            internal static Type[] NumericTypes = new Type[] { Types.Byte, Types.Sbyte, Types.Short, Types.Ushort, Types.Int, Types.Uint, Types.Long, Types.Ulong, Types.Float, Types.Double, Types.Decimal };
            

        }
        
        private static bool TryParseNumeric(string value)
        => byte.TryParse(value, out byte valByte) ||
            sbyte.TryParse(value, out sbyte valSByte) ||
            short.TryParse(value, out short valShort) ||
            ushort.TryParse(value, out ushort valUShort) ||
            int.TryParse(value, out int valInt) ||
            uint.TryParse(value, out uint valUInt) ||
            long.TryParse(value, out long valLong) ||
            ulong.TryParse(value, out ulong valULong) ||
            float.TryParse(value, out float valFloat) ||
            double.TryParse(value, out double valDouble) ||
            decimal.TryParse(value, out decimal valDecimal);
        internal static bool IsNumericValue(object value)
        {
            if (value == null)
            {
                return false;
            }
            Type valueType = value.GetType();
            Type rootType = Nullable.GetUnderlyingType(valueType);
            Type type = rootType != null ? rootType : valueType;

            return Types.NumericTypes.Contains(type) ? true :
                value is string str ? TryParseNumeric(str) : false;
        }
        private static bool TryGetCast(object input, Type castType, out object castValue)
        {
            if(input == null){
                castValue = null;
                return false;
            }
            Type type = input.GetType();
            try
            {
                castValue =
                    type == Types.Char && castType == Types.Double ? Convert.ToDouble(Convert.ToInt32(input)) :
                    type == Types.Char && castType == Types.Decimal ? Convert.ToDecimal(Convert.ToInt32(input)) :
                    type == Types.Char && castType == Types.Float ? Convert.ToSingle(Convert.ToInt32(input)) :
                    castType == Types.Byte ? Convert.ToByte(input) :
                    castType == Types.Sbyte ? Convert.ToSByte(input) :
                    castType == Types.Short ? Convert.ToInt16(input) :
                    castType == Types.Ushort ? Convert.ToUInt16(input) :
                    castType == Types.Int ? Convert.ToInt32(input) :
                    castType == Types.Uint ? Convert.ToUInt32(input) :
                    castType == Types.Long ? Convert.ToInt64(input) :
                    castType == Types.Ulong ? Convert.ToUInt64(input) :
                    castType == Types.Float ? Convert.ToSingle(input) :
                    castType == Types.Double ? Convert.ToDouble(input) :
                    castType == Types.Decimal ? Convert.ToDecimal(input) :
                    castType == Types.DateTime ? Convert.ToDateTime(input) :
                    castType == Types.Bool ? Convert.ToBoolean(input) :
                    (object)null;
                return castValue != null;
            }
            catch
            {
                castValue = null;
                return false;
            }
        }
        private static bool TryGetEnum(object input, Type type, out object enumValue)
        {
            if (type.IsEnum)
            {
                try
                {
                    enumValue = input is string enumName ? Enum.Parse(type, enumName, true) :
                        input is int intInput && Enum.IsDefined(type, intInput) ? (object)intInput :
                    TryGetCast(input, Types.Int, out object intValue) && Enum.IsDefined(type, (int)intValue) ? intValue : null;
                    return enumValue != null;
                }
                catch
                {
                    enumValue = null;
                    return false;
                }
            }
            enumValue = null;
            return false;
        }
    }
}
