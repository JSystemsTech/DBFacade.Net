using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DBFacade.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class MockDbTable
    {
        public static IDataReader EmptyTable = new DataTable("EmptyMockDbTable").CreateDataReader();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MockDbTable<T> where T : IMockDbTableRow
    {
        /// <summary>
        /// The test values data table
        /// </summary>
        private DataTable TestValuesDataTable;
        /// <summary>
        /// The bindable properties
        /// </summary>
        private List<PropertyInfo> BindableProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

        /// <summary>
        /// Gets the row values.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        private object[] GetRowValues(T model)
        {
            object[] values = new object[BindableProperties.Count];
            foreach (PropertyInfo prop in BindableProperties)
            {
                values[BindableProperties.IndexOf(prop)] = prop.GetValue(model);
            }
            return values;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Init()
        {
            TestValuesDataTable = new DataTable("MockDbTable");
            foreach (PropertyInfo prop in BindableProperties)
            {
                TestValuesDataTable.Columns.Add(new DataColumn(prop.Name, prop.PropertyType));
            }
        }
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
        /// Adds the rows.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <returns></returns>
        public MockDbTable<T> AddRows(IEnumerable<T> models)
        {
            foreach (T model in models)
            {
                AddRow(model);
            }
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
        public IDataReader ToDataReader()
        {
            return TestValuesDataTable.CreateDataReader();
        }

    }
}
