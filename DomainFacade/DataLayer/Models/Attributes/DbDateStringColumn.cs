using System;
using System.Data;

namespace DomainFacade.DataLayer.Models.Attributes
{
    public sealed class DbDateStringColumn : DbColumn
    {
        private string dateFormat;
        public DbDateStringColumn(string name, string dateFormat) : base(name)
        {
            this.dateFormat = dateFormat;
        }
        public DbDateStringColumn(string name, System.DateTime defaultValue, string dateFormat) : base(name, defaultValue)
        {
            this.dateFormat = dateFormat;
        }
        public DbDateStringColumn(Type dbMethodType, string name, string dateFormat) : base(dbMethodType, name)
        {
            this.dateFormat = dateFormat;
        }
        public DbDateStringColumn(Type dbMethodType, string name, System.DateTime defaultValue, string dateFormat) : base(dbMethodType, name, defaultValue)
        {
            this.dateFormat = dateFormat;
        }

        protected override object GetColumnValue(IDataRecord data)
        {
            System.DateTime value = GetValue<System.DateTime>(data);
            if (value != null)
            {
                return GetValue<System.DateTime>(data).ToString(dateFormat);
            }
            return null;
        }
    }
}
