using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace DbFacade.Utils
{
    /// <summary>
    ///   <br />
    /// </summary>
    internal class MockDbTable
    {
        /// <summary>The empty table</summary>
        public static DbDataReader EmptyTable = new DataTable("EmptyMockDbTable").CreateDataReader();

        /// <summary>Gets or sets the bindable fields.</summary>
        /// <value>The bindable fields.</value>
        private FieldInfo[] BindableFields { get; set; }
        /// <summary>Gets or sets the bindable properties.</summary>
        /// <value>The bindable properties.</value>
        private PropertyInfo[] BindableProperties { get; set; }

        /// <summary>The test values data table</summary>
        private DataTable TestValuesDataTable;
        /// <summary>Initializes a new instance of the <see cref="MockDbTable" /> class.</summary>
        /// <param name="dataType">Type of the data.</param>
        private MockDbTable(Type dataType)
        {
            BindableFields = dataType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            BindableProperties = dataType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            TestValuesDataTable = new DataTable("MockDbTable");
            foreach (var prop in BindableProperties)
                TestValuesDataTable.Columns.Add(new DataColumn(prop.Name, GetPropType(prop.PropertyType)));
            foreach (var field in BindableFields)
                TestValuesDataTable.Columns.Add(new DataColumn(field.Name, GetPropType(field.FieldType)));

        }
        /// <summary>Creates the specified models.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="models">The models.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static MockDbTable Create<T>(IEnumerable<T> models)
        {
            MockDbTable mockDbTable = new MockDbTable(models.FirstOrDefault().GetType());
            foreach (T model in models) {
                object[] values = mockDbTable.GetRowValues(model);
                mockDbTable.TestValuesDataTable.Rows.Add(values); 
            }
            return mockDbTable;
        }

        /// <summary>Gets the row values.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private object[] GetRowValues<T>(T model)
        {
            var values = BindableProperties.Select(prop => prop.GetValue(model));
            values = values.Concat(BindableFields.Select(field => field.GetValue(model)));
            return values.ToArray();
        }


        /// <summary>Gets the type of the property.</summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private Type GetPropType(Type type) => Nullable.GetUnderlyingType(type) ?? type;


        /// <summary>Converts to datareader.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public DbDataReader ToDataReader()
        => TestValuesDataTable.CreateDataReader();
    }
}