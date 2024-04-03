using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;

namespace DbFacade.Utils
{
    public interface IDataCollection{}
    internal class DataCollection: IDataCollection
    {
        public ICollectionParser Parser { get; private set; }
        public object Data { get; private set; }
        public DataCollection(ICollectionParser parser, object data) {
            Parser = parser;
            Data = data;
        }
    }
    internal interface ICollectionParser
    {
        Type CollectionType { get; }
        bool TryGetValue(object data, string searchKey, out object value, object defaultValue = null);
        bool TryGetValue<To>(object data, string searchKey, out To value, To defaultValue = default(To));
        bool TryGetValue(object data, string searchKey, Type convertToType, out object value, object defaultValue = null);
        bool TryGetEnumerable<To>(object data, string searchKey, out IEnumerable<To> value);
        bool TryGetEnumerable<To>(object data, string searchKey, char separator, out IEnumerable<To> value);
        bool TryGetEnumerable<To>(object data, string searchKey, out IEnumerable<To> value, To defaultValue);
        bool TryGetEnumerable<To>(object data, string searchKey, char separator, out IEnumerable<To> value, To defaultValue);
        bool TryGetEnumerable(object data, string searchKey, Type convertToType, out object value, object defaultValue);
        bool TryGetEnumerable(object data, string searchKey, Type convertToType, char separator, out object value, object defaultValue);
        bool TryGetFlagValue<T2>(object data, string searchKey, T2 trueValue, out bool value);
        bool TryGetDateTimeValue(object data, string searchKey, string format, out DateTime? value, DateTimeStyles style = DateTimeStyles.None);
        bool TryGetDateTimeValue(object data, string searchKey, string format, IFormatProvider provider, out DateTime? value, DateTimeStyles style = DateTimeStyles.None);
        bool TryGetFormattedDateTimeStringValue(object data, string searchKey, string format, out string value);
    }
    internal abstract class CollectionParser<T>: ICollectionParser
    {
        public Type CollectionType => typeof(T);

        protected virtual object GetValue(T data, string key) => null;
        protected virtual IEnumerable<string> GetKeys(T data) => Array.Empty<string>();
        private bool TryGetKey(T data, string searchKey, out string key)
        {
            if (GetKeys(data).FirstOrDefault(k => string.Equals(k, searchKey, StringComparison.OrdinalIgnoreCase)) is string foundKey)
            {
                key = foundKey;
                return true;
            }
            key = null;
            return false;
        }
        protected virtual bool TryGetBaseValue(object data, string searchKey, object defaultValue, out object value)
        {
            if (data is T tData && TryGetKey(tData, searchKey, out string key)) 
            {
                object val = GetValue(tData, key);
                value = val == DBNull.Value ? null : val;
                return true;
            }
            value = defaultValue;
            return false;
        }
        public bool TryGetValue(object data, string searchKey, out object value, object defaultValue = null)
            => TryGetBaseValue(data, searchKey, defaultValue, out value);
        public bool TryGetValue<To>(object data, string searchKey, out To value, To defaultValue = default(To))
        {
            if (TryGetBaseValue(data, searchKey, defaultValue, out object val) && ConverterFactory.TryParse(val, defaultValue, out To convertedVal))
            {                
                value = convertedVal;
                return true;
            }
            value = defaultValue;
            return false;
        }
        public bool TryGetValue(object data, string searchKey,Type convertToType, out object value, object defaultValue = null)
        {
            if (TryGetBaseValue(data, searchKey, defaultValue, out object val) && ConverterFactory.TryParse(val, defaultValue, convertToType, out object convertedVal))
            {
                value = convertedVal;
                return true;
            }
            value = defaultValue;
            return false;
        }
        public bool TryGetEnumerable<To>(object data, string searchKey, out IEnumerable<To> value)
        {
            if (TryGetValue(data, searchKey, out string str) && ConverterFactory.TryParseEnumerable(str, out IEnumerable<To> output))
            {
                value = output;
                return true;
            }
            value = Array.Empty<To>();
            return false;
        }
        public bool TryGetEnumerable<To>(object data, string searchKey, char separator, out IEnumerable<To> value)
        {
            if (TryGetValue(data, searchKey, out string str) && ConverterFactory.TryParseEnumerable(str, separator, out IEnumerable<To> output))
            {
                value = output;
                return true;
            }
            value = Array.Empty<To>();
            return false;
        }
        public bool TryGetEnumerable<To>(object data, string searchKey, out IEnumerable<To> value, To defaultValue)
        {
            if (TryGetValue(data, searchKey, out string str) && ConverterFactory.TryParseEnumerable(str, defaultValue, out IEnumerable<To> output))
            {
                value = output;
                return true;
            }
            value = Array.Empty<To>();
            return false;
        }
        public bool TryGetEnumerable<To>(object data, string searchKey, char separator, out IEnumerable<To> value, To defaultValue)
        {
            if (TryGetValue(data, searchKey, out string str) && ConverterFactory.TryParseEnumerable(str, defaultValue, separator, out IEnumerable<To> output))
            {
                value = output;
                return true;
            }
            value = Array.Empty<To>();
            return false;
        }
        public bool TryGetEnumerable(object data, string searchKey,Type convertToType, out object value, object defaultValue)
        {
            if (TryGetValue(data, searchKey, out string str) && ConverterFactory.TryParseEnumerable(str, defaultValue, convertToType, out object output))
            {
                value = output;
                return true;
            }
            value = Array.Empty<object>();
            return false;
        }
        public bool TryGetEnumerable(object data, string searchKey, Type convertToType, char separator, out object value, object defaultValue)
        {
            if (TryGetValue(data, searchKey, out string str) && ConverterFactory.TryParseEnumerable(str, defaultValue, convertToType, separator, out object output))
            {
                value = output;
                return true;
            }
            value = Array.Empty<object>();
            return false;
        }
        public bool TryGetFlagValue<To>(object data, string searchKey, To trueValue, out bool value)
        {
            if (TryGetValue(data, searchKey, out To val) && val is IComparable comp)
            {
                value = comp.CompareTo(trueValue) == 0;
                return true;
            }
            value = false;
            return false;
        }
        public bool TryGetDateTimeValue(object data, string searchKey, string format, out DateTime? value, DateTimeStyles style = DateTimeStyles.None)
            => TryGetDateTimeValue(data, searchKey, format, CultureInfo.InvariantCulture, out value, style);
        public bool TryGetDateTimeValue(object data, string searchKey, string format, IFormatProvider provider, out DateTime? value, DateTimeStyles style = DateTimeStyles.None)
        {
            if (TryGetValue(data, searchKey, out string val) &&
                            !string.IsNullOrWhiteSpace(val) &&
                            DateTime.TryParseExact(val, format, provider, style, out DateTime outValue))
            {
                value = outValue;
                return true;
            }
            value = default(DateTime?);
            return false;
        }
        public bool TryGetFormattedDateTimeStringValue(object data, string searchKey, string format, out string value)
        {
            if (TryGetValue(data, searchKey, out DateTime? val) && val is DateTime date)
            {
                try
                {
                    value = date.ToString(format);
                    return true;
                }
                catch
                {
                    value = null;
                    return false;
                }
            }
            value = null;
            return false;
        }
    }
    
    internal class DictionaryParser<T> : CollectionParser<IDictionary<string,T>>
    {
        protected override object GetValue(IDictionary<string, T> data, string key) => data[key];

        protected override IEnumerable<string> GetKeys(IDictionary<string, T> data) => data.Keys;
        public static ICollectionParser Default = new DictionaryParser<T>();
    }
    internal class NameValueCollectionParser : CollectionParser<NameValueCollection>
    {
        protected override object GetValue(NameValueCollection data, string key) => data[key];

        protected override IEnumerable<string> GetKeys(NameValueCollection data) => data.AllKeys;
        public static ICollectionParser Default = new NameValueCollectionParser();
    }
    internal class DataRowParser : CollectionParser<DataRow>
    {
        protected override object GetValue(DataRow data, string key) => data[key];

        protected override IEnumerable<string> GetKeys(DataRow data) => data.Table.Columns.Cast<DataColumn>().Select(c=> c.ColumnName);
        public static ICollectionParser Default = new DataRowParser();

    }
    public interface IDataTableParser { }
    internal interface IDataTableParserInternal : IDataTableParser
    {
        ICollectionParser CollectionParser { get; }
    }
    internal class DataTableParser: IDataTableParserInternal
    {
        private class DataTableDataRowParser : CollectionParser<DataRow>
        {
            TryGetOrdinalHandler TryGetOrdinal { get; set; }
            public static ICollectionParser Create(TryGetOrdinalHandler tryGetOrdinal)
            => new DataTableDataRowParser() { TryGetOrdinal = tryGetOrdinal };
            protected override bool TryGetBaseValue(object data, string searchKey, object defaultValue, out object value)
            {
                if (data is DataRow tData && TryGetOrdinal(searchKey, out int ordinal))
                {
                    object val = tData[ordinal];
                    value = val == DBNull.Value ? null : val;
                    return true;
                }
                value = defaultValue;
                return false;
            }
        }
        private Dictionary<string,int> OrdinalMap { get; set; }
        public ICollectionParser CollectionParser { get; private set; }
        private DataTableParser(DataTable dt) {
            var comparer = StringComparer.OrdinalIgnoreCase;
            OrdinalMap = dt.Columns.Cast<DataColumn>().ToDictionary(c => c.ColumnName, c => c.Ordinal, comparer);
            CollectionParser = DataTableDataRowParser.Create(TryGetOrdinal);
        }
        private delegate bool TryGetOrdinalHandler(string name, out int ordinal);
        private bool TryGetOrdinal(string name, out int ordinal) => OrdinalMap.TryGetValue(name, out ordinal);
        public static IDataTableParser Create(DataTable dt)
            => new DataTableParser(dt);

    }
    internal class GenericCollectionParser<T> : CollectionParser<T>
    {
        private Func<T, string, object> GetValueResolver { get; set; }
        private Func<T, IEnumerable<string>> GetKeysResolver { get; set; }
        public GenericCollectionParser(Func<T, string, object> getValueResolver, Func<T, IEnumerable<string>> getKeysResolver)
        {
            GetValueResolver = getValueResolver;
            GetKeysResolver = getKeysResolver;
        }
        protected override object GetValue(T data, string key) => GetValueResolver(data, key);
        protected override IEnumerable<string> GetKeys(T data) => GetKeysResolver(data);

    }
    internal class CollectionParserFactory
    {
        private static IEnumerable<KeyValuePair<Type, ICollectionParser>> DefaultCollectionParsers = new[] {
            new KeyValuePair<Type, ICollectionParser>(NameValueCollectionParser.Default.CollectionType, NameValueCollectionParser.Default ),
            new KeyValuePair<Type, ICollectionParser>(DataRowParser.Default.CollectionType, DataRowParser.Default ),
            new KeyValuePair<Type, ICollectionParser>(DictionaryParser<object>.Default.CollectionType, DictionaryParser<object>.Default )
        };
        private static ConcurrentDictionary<Type, ICollectionParser> CollectionParsers = new ConcurrentDictionary<Type, ICollectionParser>(DefaultCollectionParsers);

        public static bool TryGetCollectionParser(Type type, out ICollectionParser parser)
        {
            if (CollectionParsers.Keys.FirstOrDefault(t => t.IsInterface && type.GetInterfaces().Any(iType => iType == t)) is Type interfaceKey &&
                CollectionParsers.TryGetValue(interfaceKey, out ICollectionParser p1))
            {
                parser = p1;
                return true;
            }
            else if (CollectionParsers.TryGetValue(type, out ICollectionParser p2))
            {
                parser = p2;
                return true;
            }
            parser = null;
            return false;
        }
        private static bool TryGetCollectionParser(object data, out (ICollectionParser parser, object data) result)
        {
            if (data is DataCollection dc)
            {
                result = (dc.Parser, dc.Data);
                return true;
            }
            else if (TryGetCollectionParser(data.GetType(), out ICollectionParser parser))
            {
                result = (parser, data);
                return true;
            }
            result = (null,null);
            return false;
        }
        public static bool TryRegisterCollectionParser<T, TValue>()
            where T: IDictionary<string, TValue>
        {
            ICollectionParser parser = new DictionaryParser<TValue>();
            return CollectionParsers.TryAdd(parser.CollectionType, parser);
        }
        public static bool TryRegisterCollectionParser<T>(Func<T, string, object> getValueResolver, Func<T, IEnumerable<string>> getKeysResolver)
        {
            ICollectionParser parser = new GenericCollectionParser<T>(getValueResolver, getKeysResolver);
            return CollectionParsers.TryAdd(parser.CollectionType, parser);
        }
        private delegate bool ParserHandler(ICollectionParser parser, out object value);
        private bool TryWithParser(object data, ParserHandler handler, object defaultValue, out object value)
        {
            if (TryGetCollectionParser(data.GetType(), out ICollectionParser parser))
            {
                return handler(parser, out value);
            }
            value = defaultValue;
            return false;
        }
        public static bool TryGetValue(object data, string searchKey, out object value, object defaultValue = null)
        {
            
            if (TryGetCollectionParser(data, out (ICollectionParser parser, object data) result))
            {
                return result.parser.TryGetValue(result.data, searchKey, out value, defaultValue);
            }
            value = defaultValue;
            return false;
        }
        public static bool TryGetValue<To>(object data, string searchKey, out To value, To defaultValue = default(To))
        {
            if (TryGetCollectionParser(data, out (ICollectionParser parser, object data) result))
            {
                return result.parser.TryGetValue(result.data, searchKey, out value, defaultValue);
            }
            value = defaultValue;
            return false;
        }
        public static bool TryGetValue(object data, string searchKey, Type convertToType, out object value, object defaultValue)
        {
            if (TryGetCollectionParser(data, out (ICollectionParser parser, object data) result))
            {
                return result.parser.TryGetValue(result.data, searchKey, convertToType, out value, defaultValue);
            }
            value = defaultValue;
            return false;
        }
        public static bool TryGetEnumerable<To>(object data, string searchKey, out IEnumerable<To> value)
        {
            if (TryGetCollectionParser(data, out (ICollectionParser parser, object data) result))
            {
                return result.parser.TryGetEnumerable(result.data, searchKey, out value);
            }
            value = Array.Empty<To>();
            return false;
        }
        public static bool TryGetEnumerable<To>(object data, string searchKey, char separator, out IEnumerable<To> value)
        {
            if (TryGetCollectionParser(data, out (ICollectionParser parser, object data) result))
            {
                return result.parser.TryGetEnumerable(result.data, searchKey, separator, out value);
            }
            value = Array.Empty<To>();
            return false;
        }
        public static bool TryGetEnumerable<To>(object data, string searchKey, out IEnumerable<To> value, To defaultValue)
        {
            if (TryGetCollectionParser(data, out (ICollectionParser parser, object data) result))
            {
                return result.parser.TryGetEnumerable(result.data, searchKey, out value, defaultValue);
            }
            value = Array.Empty<To>();
            return false;
        }
        public static bool TryGetEnumerable<To>(object data, string searchKey, char separator, out IEnumerable<To> value, To defaultValue)
        {
            if (TryGetCollectionParser(data, out (ICollectionParser parser, object data) result))
            {
                return result.parser.TryGetEnumerable(result.data, searchKey, separator, out value, defaultValue);
            }
            value = Array.Empty<To>();
            return false;
        }
        public static bool TryGetEnumerable(object data, string searchKey,Type convertToType, char separator, out object value, object defaultValue)
        {
            if (TryGetCollectionParser(data, out (ICollectionParser parser, object data) result))
            {
                return result.parser.TryGetEnumerable(result.data, searchKey, convertToType, separator, out value, defaultValue);
            }
            value = Array.Empty<object>();
            return false;
        }
        public static bool TryGetEnumerable(object data, string searchKey, Type convertToType, out object value, object defaultValue)
        {
            if (TryGetCollectionParser(data, out (ICollectionParser parser, object data) result))
            {
                return result.parser.TryGetEnumerable(result.data, searchKey, convertToType, out value, defaultValue);
            }
            value = Array.Empty<object>();
            return false;
        }
        internal static bool TryGetFlagValue<T>(object data, string searchKey, T trueValue, out bool value)
            where T : IComparable
        {
            if (TryGetCollectionParser(data, out (ICollectionParser parser, object data) result))
            {
                return result.parser.TryGetFlagValue(result.data, searchKey, trueValue, out value);
            }
            value = false;
            return false;
        }
        internal static bool TryGetDateTimeValue(object data, string searchKey, string format, out DateTime? value, DateTimeStyles style = DateTimeStyles.None)
            => TryGetDateTimeValue(data, searchKey, format, CultureInfo.InvariantCulture,out value, style);
        internal static bool TryGetDateTimeValue(object data, string searchKey, string format, IFormatProvider provider, out DateTime? value, DateTimeStyles style = DateTimeStyles.None)
        {
            if (TryGetCollectionParser(data, out (ICollectionParser parser, object data) result))
            {
                return result.parser.TryGetDateTimeValue(result.data, searchKey, format, provider, out value, style);
            }
            value = default(DateTime?);
            return false;
        }
        internal static bool TryGetFormattedDateTimeStringValue(object data, string searchKey, string format, out string value)
        {
            if (TryGetCollectionParser(data, out (ICollectionParser parser, object data) result))
            {
                return result.parser.TryGetFormattedDateTimeStringValue(result.data, searchKey, format, out value);
            }
            value = null;
            return false;
        }
    }
}
