using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace DbFacade.Utils
{
    internal class MockDbTable
    {
        public static DbDataReader EmptyTable = new DataTable("EmptyMockDbTable").CreateDataReader();
    }

    internal class MockDbTable<T>
    {
        private readonly List<FieldInfo> BindableFields =
            typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance).ToList();

        private readonly List<PropertyInfo> BindableProperties =
            typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

        private DataTable TestValuesDataTable;

        public MockDbTable()
        {
            Init();
        }

        public MockDbTable(IEnumerable<T> models)
        {
            Init();
            AddRows(models);
        }

        public MockDbTable(T model)
        {
            Init();
            AddRow(model);
        }

        private object[] GetRowValues(T model)
        {
            var values = BindableProperties.Select(prop => prop.GetValue(model));
            values = values.Concat(BindableFields.Select(field => field.GetValue(model)));
            return values.ToArray();
        }

        private Type GetType(Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        private void Init()
        {
            TestValuesDataTable = new DataTable("MockDbTable");
            foreach (var prop in BindableProperties)
                TestValuesDataTable.Columns.Add(new DataColumn(prop.Name, GetType(prop.PropertyType)));
            foreach (var field in BindableFields)
                TestValuesDataTable.Columns.Add(new DataColumn(field.Name, GetType(field.FieldType)));
        }

        public MockDbTable<T> AddRows(IEnumerable<T> models)
        {
            foreach (var model in models) AddRow(model);
            return this;
        }

        public MockDbTable<T> AddRow(T model)
        {
            TestValuesDataTable.Rows.Add(GetRowValues(model));
            return this;
        }

        public DbDataReader ToDataReader()
        {
            return TestValuesDataTable.CreateDataReader();
        }
    }
}