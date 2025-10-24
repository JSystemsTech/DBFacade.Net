using System;
using System.Data;
using System.Linq;

namespace DbFacade.DataLayer.Models
{
    internal class DataRowDataCollection : IDataCollection
    {
        public string[] Keys { get; private set; }
        private readonly DataRow Row;

        public object this[int columnIndex]
        {
            get
            {
                return GetValueByIndex(columnIndex);
            }
        }
        public object this[string columnName]
        {
            get
            {
                return GetValue(GetName(columnName));
            }
        }
        internal DataRowDataCollection(DataRow dr, string[] keys)
        {
            Keys = keys;
            Row = dr;
        }
        private object GetValue(string name)
        {
            try
            {
                object value = Row[name];
                return value == DBNull.Value ? null : value;
            }
            catch
            {
                return null;
            }
        }
        private object GetValueByIndex(int columnIndex)
            => Keys.Length == 0 || columnIndex < 0 || columnIndex >= Keys.Length ? null :
            GetValue(Keys[columnIndex]);
        private string GetName(string columnName)
        {
            string name = Keys.FirstOrDefault(m => m.Equals(columnName, StringComparison.OrdinalIgnoreCase));
            return name ?? columnName;
        }
    }
}
