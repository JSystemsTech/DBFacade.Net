using DbFacade.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbFacade.Extensions
{
    public static class AccessorExtensions
    {
        public static bool TryGetValue<T>(this T source, string name, out object value)
            where T : class
            => Accessor<T>.GetInstance().TryGetValue(source, name, out value);
        public static IDataCollection ToDataCollection<T>(this T source)
            where T : class
        => source.ToDataCollection(c => c.BindInput());
        public static IDataCollection ToDataCollection<T, TAttribute>(this T source)
            where T : class
            where TAttribute : Attribute
            => source.ToDataCollection(c => c.BindInput<TAttribute>());

        public static IDataCollection ToDataCollection<T>(this T source, Action<ParameterDataCollection<T>> resolveBuilder)
            where T : class
        {
            var collection = new ParameterDataCollection<T>(source);
            resolveBuilder(collection);
            return collection.Collection;
        }
        internal static DataTable ToDataTable<T>(this IEnumerable<T> data)
            where T : class
        {
            DataTable dt = new DataTable(Guid.NewGuid().ToString());
            var refs = Accessor<T>.GetInstance().VariableReferences.Values;
            foreach (var variableRef in refs)
            {
                dt.Columns.Add(new DataColumn(variableRef.Name, variableRef.VariableType) { AllowDBNull = variableRef.IsNullable });
            }
            foreach (var rowData in data)
            {
                var values = refs.Select(r => r.Get(rowData)).ToArray();
                dt.Rows.Add(values);
            }
            return dt;
        }
    }
}
