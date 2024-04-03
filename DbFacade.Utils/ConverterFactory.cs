using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using static DbFacade.Utils.ValueConverter;
/* Unmerged change from project 'DbFacade.Utils (net6.0)'
Before:
namespace DbFacade.Utils.Converter
After:
namespace DbFacade.Utils.Utils;
using DbFacade.Utils.Converter
*/


namespace DbFacade.Utils
{
    internal static class ConverterFactory
    {
        private static readonly ConcurrentDictionary<Type, ValueConverter> ValueConverters = new ConcurrentDictionary<Type, ValueConverter>();

        public static bool TryParse<T>(object input, out T output)
            => TryParse(input, default, out output);
        
        public static bool TryParse<T>(object input, T defaultValue, out T output)
        {            
            Type returnType = GetUnderlyingType(typeof(T), out bool nullable);
            if (nullable && input == null)
            {
                output = (T)(object)null;
                return true;
            }
            else if (input == null)
            {
                output = defaultValue;
                return true;
            }
            else if (returnType == typeof(string))
            {
                output = (T)(object)input.ToString();
                return true;
            }
            else if (input is T curVal)
            {
                output = curVal;
                return true;
            }
            
            Type inputType = input.GetType();
            var valueConverter = GetValueConverter(inputType);
            var success = valueConverter.TryParse(returnType, input, out object val);
            output = success ? (T)val : defaultValue;
            return success;
        }
        public static bool TryParse(object input, object defaultValue,Type convertToType, out object output)
        {
            Type returnType = GetUnderlyingType(convertToType, out bool nullable);
            if (nullable && input == null)
            {
                output = null;
                return true;
            }
            else if (returnType == typeof(string))
            {
                output = input.ToString();
                return true;
            }
            else if (input.GetType() == convertToType || input.GetType() == returnType)
            {
                output = input;
                return true;
            }
            Type inputType = input.GetType();
            var valueConverter = GetValueConverter(inputType);
            var success = valueConverter.TryParse(returnType, input, out object val);
            output = success ? val : defaultValue;
            return success;
        }
        const char DefaultSeparator = ',';
        public static bool TryParseEnumerable<T>(string input, out IEnumerable<T> output)
            => TryParseEnumerable(input,DefaultSeparator, out output);
        public static bool TryParseEnumerable<T>(string input, char separator, out IEnumerable<T> output)
            => TryParseEnumerable(input, default(T), separator, out output);
        public static bool TryParseEnumerable<T>(string input, T defaultValue, out IEnumerable<T> output)
            => TryParseEnumerable(input, defaultValue, DefaultSeparator, out output);
        public static bool TryParseEnumerable<T>(string input, T defaultValue, char separator, out IEnumerable<T> output)
        {
            string[] strSplitArr = input.Split(separator);
            Type toType = typeof(T);
            if(toType == typeof(string))
            {
                output = (IEnumerable<T>)(object)strSplitArr;
                return true;
            }
            Type inputType = typeof(string);
            Type elementType = GetUnderlyingType(toType, out bool nullable);
            var valueConverter = GetValueConverter(inputType);
            if(valueConverter.TryGetConversionHandler(elementType,out ConversionHandler ch))
            {

                output = strSplitArr.Select(m => nullable && string.IsNullOrWhiteSpace(m) ? (T)(object)null : ch(m, out object val) ? (T)val : defaultValue);
                return true;
            }
            output = Array.Empty<T>();
            return false;
        }
        public static bool TryParseEnumerable(string input, object defaultValue, Type convertToType, out object output)
            => TryParseEnumerable(input, defaultValue, convertToType, DefaultSeparator, out output);
        public static bool TryParseEnumerable(string input, object defaultValue,Type convertToType, char separator, out object output)
        {
            string[] strSplitArr = input.Split(separator);
            if (convertToType == typeof(string))
            {
                output = strSplitArr;
                return true;
            }
            Type inputType = typeof(string);
            Type elementType = GetUnderlyingType(convertToType, out bool nullable);
            var valueConverter = GetValueConverter(inputType);
            if (valueConverter.TryGetConversionHandler(elementType, out ConversionHandler ch))
            {
                output = strSplitArr.Select(m => nullable && string.IsNullOrWhiteSpace(m) ? m : ch(m, out object val) ? val : defaultValue);
                return true;
            }
            output = Array.Empty<object>();
            return false;
        }
        public static bool TryRegisterConverter<TFrom, T>(Func<TFrom, T> resolver)
        {
            Type inputType = typeof(TFrom);
            Type returnType = GetUnderlyingType(typeof(T), out bool nullable);
            var valueConverter = GetValueConverter(inputType);
            return valueConverter.TryAdd(input => resolver((TFrom)input));
        }
        private static Type GetUnderlyingType(Type returnType, out bool nullable)
        {
            Type underlyingType = Nullable.GetUnderlyingType(returnType);
            nullable = underlyingType != null;
            return underlyingType != null ? underlyingType : returnType;
        }
        private static ValueConverter GetValueConverter(Type inputType)
        {
            if (ValueConverters.TryGetValue(inputType, out ValueConverter valueConverter))
            {
                return valueConverter;
            }
            var newValueConverter = new ValueConverter(inputType);
            ValueConverters.TryAdd(inputType, newValueConverter);
            return newValueConverter;
        }
        private static readonly TypeCode[] NumericTypes = new TypeCode[] {
                TypeCode.Byte,
                TypeCode.SByte,
                TypeCode.UInt16,
                TypeCode.UInt32,
                TypeCode.UInt64,
                TypeCode.Int16,
                TypeCode.Int32,
                TypeCode.Int64,
                TypeCode.Decimal,
                TypeCode.Double,
                TypeCode.Single
        };
        public static bool IsNumeric(this Type t) => NumericTypes.Contains(Type.GetTypeCode(t));
        public static bool IsNumeric(this object o) => IsNumeric(o.GetType());


    }
    internal class ValueConverter
    {
        private Type InputType { get; set; }
        public delegate bool ConversionHandler(object input, out object output);
        private ConcurrentDictionary<Type, ConversionHandler> ConversionHandlers { get; set; }

        public ValueConverter(Type inputType)
        {
            InputType = inputType;
            ConversionHandlers = new ConcurrentDictionary<Type, ConversionHandler>();
        }
        
        public bool TryParse(Type returnType, object input, out object output)
        {
            if (InputType == input.GetType() && TryGetConversionHandler(returnType, out ConversionHandler handler))
            {
                return handler(input, out output);
            }
            output = null;
            return false;
        }
        public bool TryAdd<T>(Func<object, T> resolver)
        {
            ConversionHandler handler = ToConversionHandler(resolver);
            ConversionHandlers.TryRemove(typeof(T), out ConversionHandler chr);
            return ConversionHandlers.TryAdd(typeof(T), handler);
        }

        public bool TryGetConversionHandler(Type type, out ConversionHandler handler)
        {
            if (ConversionHandlers.TryGetValue(type, out ConversionHandler h1))
            {
                handler = h1;
                return true;
            }
            else if (TryGetTypeConverter(type, out ConversionHandler h2))
            {
                ConversionHandlers.TryAdd(type, h2);
                handler = h2;
                return true;
            }
            else if (TryGetConstructor(type, out ConversionHandler h3))
            {
                ConversionHandlers.TryAdd(type, h3);
                handler = h3;
                return true;
            }
            else if (TryGetEnum(type, out ConversionHandler h4))
            {
                ConversionHandlers.TryAdd(type, h4);
                handler = h4;
                return true;
            }
            else if (TryGetCast(type, out ConversionHandler h5))
            {
                ConversionHandlers.TryAdd(type, h5);
                handler = h5;
                return true;
            }
            handler = null;
            return false;
        }
        private bool TryGetTypeConverter(Type type, out ConversionHandler handler)
        {
            if (TypeDescriptor.GetConverter(type) is TypeConverter converter && converter != null && converter.CanConvertFrom(InputType))
            {
                handler = delegate (object input, out object output)
                {
                    try
                    {
                        output = InputType == typeof(string) ? converter.ConvertFromInvariantString(input.ToString()) :
                        converter.ConvertFrom(input);
                        return output != null;
                    }
                    catch
                    {
                        output = null;
                        return false;
                    }
                };
                return true;
            }
            handler = null;
            return false;
        }
        private bool TryGetConstructor(Type type, out ConversionHandler handler)
        {
            if (type.IsClass && type.GetConstructors().Any(c => c.GetParameters().Length == 1 && c.GetParameters().FirstOrDefault().ParameterType == InputType))
            {
                handler = delegate (object input, out object output)
                {
                    try
                    {
                        output = Activator.CreateInstance(type, input);
                        return output != null;
                    }
                    catch
                    {
                        output = null;
                        return false;
                    }
                };
                return true;
            }
            else if (type.IsValueType && type.GetTypeInfo().DeclaredConstructors.Any(c => c.GetParameters().Length == 1 && c.GetParameters().FirstOrDefault().ParameterType == InputType))
            {
                handler = delegate (object input, out object output)
                {
                    try
                    {
                        output = Activator.CreateInstance(type, input);
                        return output != null;
                    }
                    catch
                    {
                        output = null;
                        return false;
                    }
                };
                return true;
            }
            handler = null;
            return false;
        }
        private bool TryGetEnum(Type type, out ConversionHandler handler)
        {
            if (type.IsEnum)
            {
                handler = delegate (object input, out object output)
                {
                    try
                    {
                        object adjustedInput = 
                        input.IsNumeric() || input is string ? input:
                        TryGetConversionHandler(typeof(int), out ConversionHandler intHandler) && intHandler(input, out object intInput) ? intInput : input;
                        output = Enum.Parse(type, adjustedInput.ToString(), true);
                        return true;
                    }
                    catch
                    {
                        output = null;
                        return false;
                    }

                };
                return true;
            }
            handler = null;
            return false;
        }
        private class Types
        {
            public static Type Byte = typeof(byte);
            public static Type Sbyte = typeof(sbyte);
            public static Type Short = typeof(short);
            public static Type Ushort = typeof(ushort);
            public static Type Int = typeof(int);
            public static Type Uint = typeof(uint);
            public static Type Long = typeof(long);
            public static Type Ulong = typeof(ulong);
            public static Type Float = typeof(float);
            public static Type Double = typeof(double);
            public static Type Decimal = typeof(decimal);
            public static Type DateTime = typeof(DateTime);
            public static Type Char = typeof(char);
            public static Type Bool = typeof(bool);


        }
        private static readonly Dictionary<Type, Func<object, object>> ConvertClassHelpers = new Dictionary<Type, Func<object, object>>()
        {
            { Types.Byte, m => Convert.ToByte(m) },
            { Types.Sbyte, m => Convert.ToSByte(m) },
            { Types.Short, m => Convert.ToInt16(m) },
            { Types.Ushort, m => Convert.ToUInt16(m) },
            { Types.Int, m => Convert.ToInt32(m) },
            { Types.Uint, m => Convert.ToUInt32(m) },
            { Types.Long, m => Convert.ToInt64(m) },
            { Types.Ulong, m => Convert.ToUInt64(m) },
            { Types.Float, m => Convert.ToSingle(m) },
            { Types.Double, m => Convert.ToDouble(m) },
            { Types.Decimal, m => Convert.ToDecimal(m) },
            { Types.DateTime, m => Convert.ToDateTime(m) },
            { Types.Char, m => Convert.ToChar(m) },
            { Types.Bool, m => Convert.ToBoolean(m) }

        };
        private static readonly Dictionary<(Type from, Type to), Func<object, object>> SpecialConvertClassHelpers = new Dictionary<(Type from, Type to), Func<object, object>>()
        {
            { (Types.Float,Types.Char), m => Convert.ToChar(Convert.ToInt32(m)) },
            { (Types.Double,Types.Char), m => Convert.ToChar(Convert.ToInt32(m)) },
            { (Types.Decimal,Types.Char), m => Convert.ToChar(Convert.ToInt32(m)) },
            { (Types.Bool,Types.Char), m => Convert.ToChar(Convert.ToInt32(m)) },
            { (Types.Char,Types.Double), m => Convert.ToDouble(Convert.ToInt32(m)) },
            { (Types.Char,Types.Decimal), m => Convert.ToDecimal(Convert.ToInt32(m)) },
            { (Types.Char,Types.Float), m => Convert.ToSingle(Convert.ToInt32(m)) }

        };
        private bool TryGetCast(Type type, out ConversionHandler handler)
        {
            
            if (ConvertClassHelpers.TryGetValue(type, out Func<object, object> converter))
            {
                handler = delegate (object input, out object output)
                {
                    try
                    {
                        if (SpecialConvertClassHelpers.TryGetValue((input.GetType(),type), out Func<object, object> specConverter))
                        {
                            output = specConverter(input);
                            return true;
                        }
                        output = converter(input);
                        return true;
                    }
                    catch
                    {
                        output = null;
                        return false;
                    }

                };
                return true;
            }
            handler = null;
            return false;
        }
        private ConversionHandler ToConversionHandler<T>(Func<object, T> handler)
        {
            return (object input, out object output) =>
            {
                try
                {
                    output = handler(input);
                    return true;
                }
                catch
                {
                    output = default(T);
                    return true;
                }

            };
        }

    }



}
