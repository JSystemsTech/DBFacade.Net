using System;
using System.Data;

namespace DomainFacade.DataLayer.Models.Attributes
{
    public sealed class DbFlagColumn : DbColumn
    {
        private object TrueValue;
        public DbFlagColumn(string name, object trueValue) : base(name)
        {
            initColumn(trueValue);
        }
        public DbFlagColumn(string name, bool defaultValue, object trueValue) : base(name, defaultValue)
        {
            initColumn(trueValue);
        }
        public DbFlagColumn(Type dbMethodType, string name, object trueValue) : base(dbMethodType, name)
        {
            initColumn(trueValue);
        }
        public DbFlagColumn(Type dbMethodType, string name, bool defaultValue, object trueValue) : base(dbMethodType, name, defaultValue)
        {
            initColumn(trueValue);
        }
        private void initColumn(object trueValue)
        {
            TrueValue = trueValue;
        }
        protected override object GetColumnValue(IDataRecord data)
        {
            return GetValue(data, TrueValue.GetType()).ToString() == TrueValue.ToString();
        }
    }
}
