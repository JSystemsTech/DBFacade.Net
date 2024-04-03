using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace DbFacade.Utils
{
    public static class Utils
    {
        public static bool TryParse<T>(this object input, out T output)
            => ConverterFactory.TryParse(input, out output);
        public static bool TryParse<T>(this object input, T defaultValue, out T output) 
            => ConverterFactory.TryParse(input, defaultValue, out output);        
        public static bool TryParseEnumerable<T>(this string input, out IEnumerable<T> output)
            => ConverterFactory.TryParseEnumerable(input, out output);
        public static bool TryParseEnumerable<T>(this string input, char separator, out IEnumerable<T> output)
            => ConverterFactory.TryParseEnumerable(input, separator, out output);
        public static bool TryParseEnumerable<T>(this string input, T defaultValue, out IEnumerable<T> output)
            => ConverterFactory.TryParseEnumerable(input, defaultValue, out output);
        public static bool TryParseEnumerable<T>(this string input, T defaultValue, char separator, out IEnumerable<T> output)
            => ConverterFactory.TryParseEnumerable(input, defaultValue, separator, out output);
        public static bool TryRegisterConverter<TFrom, T>(this Func<TFrom, T> resolver)
            => ConverterFactory.TryRegisterConverter(resolver);
        public static void RegisterInstanceBuilder<T>(InitInstance constructor)
            => InstanceFactory.RegisterInstanceBuilder<T>(constructor);

        public static IDataTableParser ToDataTableParser(this DataTable dt)
            => DataTableParser.Create(dt);
        public static T MakeInstance<T>(DataRow dr, IDataTableParser dtp)
            where T: class,IDataCollectionModel
            => InstanceFactory.Build<T>(dr, dtp);
        public static T MakeInstance<T>(IDataCollection collection)
            where T : class, IDataCollectionModel
            => InstanceFactory.Build<T>(collection);
        public static T MakeInstance<T>()
            => InstanceFactory.Build<T>();
        public static T MakeInstance<T>(params object[] parameters)
            => InstanceFactory.Build<T>(parameters);
        public static bool TryMakeInstance<T>(out T value)
            => InstanceFactory.TryBuild(out value);
        public static bool TryMakeInstance<T>(out T value, params object[] parameters)
            => InstanceFactory.TryBuild(out value, parameters);

        public static bool TryRegisterCollectionParser<T, TValue>()
            where T : IDictionary<string, TValue>
            => CollectionParserFactory.TryRegisterCollectionParser<T, TValue>();
        public static bool TryRegisterCollectionParser<T>(Func<T, string, object> getValueResolver, Func<T, IEnumerable<string>> getKeysResolver)
            => CollectionParserFactory.TryRegisterCollectionParser(getValueResolver, getKeysResolver);
        public static bool TryGetValue(this object data, string searchKey, out object value, object defaultValue = null)
            => CollectionParserFactory.TryGetValue(data, searchKey, out value, defaultValue);
        public static bool TryGetValue<To>(this object data, string searchKey, out To value, To defaultValue = default(To))
            => CollectionParserFactory.TryGetValue(data, searchKey, out value, defaultValue);
        public static bool TryGetEnumerable<To>(this object data, string searchKey, out IEnumerable<To> value)
            => CollectionParserFactory.TryGetEnumerable(data, searchKey, out value);
        public static bool TryGetEnumerable<To>(this object data, string searchKey, char separator, out IEnumerable<To> value)
            => CollectionParserFactory.TryGetEnumerable(data, searchKey, separator, out value);
        public static bool TryGetEnumerable<To>(this object data, string searchKey, out IEnumerable<To> value, To defaultValue)
            => CollectionParserFactory.TryGetEnumerable(data, searchKey, out value, defaultValue);
        public static bool TryGetEnumerable<To>(this object data, string searchKey, char separator, out IEnumerable<To> value, To defaultValue)
            => CollectionParserFactory.TryGetEnumerable(data, searchKey, separator, out value, defaultValue);
        public static bool TryGetFlagValue<T>(this object data, string searchKey, T trueValue, out bool value)
           where T : IComparable
            => CollectionParserFactory.TryGetFlagValue(data, searchKey, trueValue, out value);
        public static bool TryGetDateTimeValue(this object data, string searchKey, string format, out DateTime? value, DateTimeStyles style = DateTimeStyles.None)
            => CollectionParserFactory.TryGetDateTimeValue(data, searchKey, format, out value, style);
        public static bool TryGetDateTimeValue(this object data, string searchKey, string format, IFormatProvider provider, out DateTime? value, DateTimeStyles style = DateTimeStyles.None)
            => CollectionParserFactory.TryGetDateTimeValue(data, searchKey, format, provider, out value, style);
        public static bool TryGetFormattedDateTimeStringValue(this object data, string searchKey, string format, out string value)
            => CollectionParserFactory.TryGetFormattedDateTimeStringValue(data, searchKey, format, out value);

        public static bool TryGetDataTable<To>(this IEnumerable<To> data, out DataTable dt)
            => DataTableFactory.TryGetDataTable(data, out dt);
    }
    public delegate object InitInstance();
    public interface IDataCollectionModel
    {
        void InitDataCollection(IDataCollection collection);
    }
}
