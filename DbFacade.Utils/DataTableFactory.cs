using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbFacade.Utils
{
    internal class DataTableFactory
    {
        public static bool TryGetDataTable<To>(IEnumerable<To> data, out DataTable dt)
        {
            if (ClassTypeDetailsFactory.TryGetClassTypeDetails(typeof(To), out ClassTypeDetails details))
            {
                dt = new DataTable(Guid.NewGuid().ToString());
                AddColumns(dt, details);
                AddRows(dt, data, details);
                return true;
            }
            dt = null;
            return false;
        }
        private static void AddColumns(DataTable dt, ClassTypeDetails details)
        {
            foreach (var e in details.Entries) {
                var col = new DataColumn(e.Name, Nullable.GetUnderlyingType(e.Type) is Type t ? t : e.Type)
                {
                    AllowDBNull = true
                };
                dt.Columns.Add(col);
            }            
        }
        private static void AddRows<To>(DataTable dt, IEnumerable<To> data, ClassTypeDetails details)
        {
            foreach (var rowData in data)
            {
                dt.Rows.Add(details.Entries.Select(e => e.GetValue(rowData)).ToArray());
            }
        }
    }

    internal class ClassTypeDetailsFactory
    {
        private static readonly ConcurrentDictionary<Type, ClassTypeDetails> Collection = new ConcurrentDictionary<Type, ClassTypeDetails>();

        public static bool TryGetClassTypeDetails(Type type, out ClassTypeDetails details)
        {
            if (Collection.TryGetValue(type, out ClassTypeDetails d1))
            {
                details = d1;
                return true;
            }
            ClassTypeDetails d2 = new ClassTypeDetails(type);
            if (Collection.TryAdd(type, d2))
            {
                details = d2;
                return true;
            }
            details = null;
            return false;
        }
    }
    internal class ClassTypeDetails
    {
        public Type ClassType { get; private set; }
        private List<(string Name, Type Type, Type NullableType, Func<object,object> GetValue, Action<object, object> SetValue)> _Entries { get; set; }
        public IEnumerable<(string Name, Type Type, Type NullableType, Func<object,object> GetValue, Action<object, object> SetValue)> Entries => _Entries;
        public ClassTypeDetails(Type classType) {
            _Entries = new List<(string Name, Type Type, Type NullableType, Func<object, object> GetValue, Action<object, object> SetValue)>();
            ClassType = classType;
            FillEntriesWithProperties();
            FillEntriesWithFields();
        }
        private void FillEntriesWithProperties()
        {
            foreach (var prop in ClassType.GetProperties())
            {
                var nullType = Nullable.GetUnderlyingType(prop.PropertyType);
                (string Name, Type Type, Type NullableType, Func<object,object> GetValue, Action<object, object> SetValue) entry = (
                    prop.Name,
                    prop.PropertyType,
                    nullType is Type nType ? nType : prop.PropertyType,
                    m => m.GetType() == ClassType && prop.CanRead ? ClassType.GetProperty(prop.Name).GetValue(m) : null,
                 (m, v) =>
                 {
                     bool propIsMatch = v.GetType() == prop.PropertyType || v.GetType() == nullType;
                     if (m.GetType() == ClassType && propIsMatch && prop.CanWrite)
                     {
                         ClassType.GetProperty(prop.Name).SetValue(m, v);
                     }
                 }
                );
                _Entries.Add(entry);
            }
        }
        private void FillEntriesWithFields()
        {
            foreach (var field in ClassType.GetFields())
            {
                var nullType = Nullable.GetUnderlyingType(field.FieldType);
                (string Name, Type Type, Type NullableType, Func<object, object> GetValue, Action<object, object> SetValue) entry = (
                    field.Name,
                    field.FieldType,
                    nullType is Type nType ? nType : field.FieldType,
                    m => m.GetType() == ClassType && field.IsPublic && !field.IsStatic ? ClassType.GetField(field.Name).GetValue(m) : null,
                 (m, v) =>
                 {
                     bool propIsMatch = v.GetType() == field.FieldType || v.GetType() == nullType;
                     if (m.GetType() == ClassType && propIsMatch && field.IsPublic && !field.IsStatic)
                     {
                         ClassType.GetProperty(field.Name).SetValue(m, v);
                     }
                 }
                );
                _Entries.Add(entry);
            }
        }
    }
}
