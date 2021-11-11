using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace DbFacade.Utils
{
    /// <summary>
    /// 
    /// </summary>
    internal class MockDbTable
    {
        /// <summary>
        /// The empty table
        /// </summary>
        public static DbDataReader EmptyTable = new DataTable("EmptyMockDbTable").CreateDataReader();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class MockDbTable<T>
    {
        /// <summary>
        /// The bindable fields
        /// </summary>
        private readonly List<FieldInfo> BindableFields =
            typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance).ToList();

        /// <summary>
        /// The bindable properties
        /// </summary>
        private readonly List<PropertyInfo> BindableProperties =
            typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

        /// <summary>
        /// The test values data table
        /// </summary>
        private DataTable TestValuesDataTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockDbTable{T}"/> class.
        /// </summary>
        public MockDbTable()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockDbTable{T}"/> class.
        /// </summary>
        /// <param name="models">The models.</param>
        public MockDbTable(IEnumerable<T> models)
        {
            Init();
            AddRows(models);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockDbTable{T}"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MockDbTable(T model)
        {
            Init();
            AddRow(model);
        }

        /// <summary>
        /// Gets the row values.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        private object[] GetRowValues(T model)
        {
            var values = BindableProperties.Select(prop => prop.GetValue(model));
            values = values.Concat(BindableFields.Select(field => field.GetValue(model)));
            return values.ToArray();
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private Type GetType(Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Init()
        {
            TestValuesDataTable = new DataTable("MockDbTable");
            foreach (var prop in BindableProperties)
                TestValuesDataTable.Columns.Add(new DataColumn(prop.Name, GetType(prop.PropertyType)));
            foreach (var field in BindableFields)
                TestValuesDataTable.Columns.Add(new DataColumn(field.Name, GetType(field.FieldType)));
        }

        /// <summary>
        /// Adds the rows.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <returns></returns>
        public MockDbTable<T> AddRows(IEnumerable<T> models)
        {
            foreach (T model in models) AddRow(model);
            return this;
        }

        /// <summary>
        /// Adds the row.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public MockDbTable<T> AddRow(T model)
        {
            TestValuesDataTable.Rows.Add(GetRowValues(model));
            return this;
        }

        /// <summary>
        /// Converts to datareader.
        /// </summary>
        /// <returns></returns>
        public DbDataReader ToDataReader()
        {
            return TestValuesDataTable.CreateDataReader();
        }
    }
}