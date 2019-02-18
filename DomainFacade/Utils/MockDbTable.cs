using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DomainFacade.Utils
{
    public class MockDbTable<T> where T: IMockDbTableRow
    {
        private DataTable TestValuesDataTable;
        private List<PropertyInfo> BindableProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

        private object[] GetRowValues(T model)
        {
            object[] values = new object[BindableProperties.Count];
            foreach (PropertyInfo prop in BindableProperties)
            {
                values[BindableProperties.IndexOf(prop)] = prop.GetValue(model);
            }
            return values;
        }
        
        private void Init()
        {
            TestValuesDataTable = new DataTable("MockDbTable");
            foreach (PropertyInfo prop in BindableProperties)
            {
                TestValuesDataTable.Columns.Add(new DataColumn(prop.Name, prop.PropertyType));
            }
        }
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
        public MockDbTable<T> AddRows(IEnumerable<T> models)
        {
            foreach(T model in models)
            {
                AddRow(model);
            }
            return this;
        }
        public MockDbTable<T> AddRow(T model)
        {
            TestValuesDataTable.Rows.Add(GetRowValues(model));
            return this;
        }

        public IDataReader ToDataReader()
        {
            return TestValuesDataTable.CreateDataReader();
        }
    }
}
