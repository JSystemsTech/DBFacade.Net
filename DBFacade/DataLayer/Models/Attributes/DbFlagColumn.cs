using System;
using System.Data;

namespace DBFacade.DataLayer.Models.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DbColumn" />
    public sealed class DbFlagColumn : DbColumn
    {
        /// <summary>
        /// The true value
        /// </summary>
        private object TrueValue;
        /// <summary>
        /// Initializes a new instance of the <see cref="DbFlagColumn"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="trueValue">The true value.</param>
        public DbFlagColumn(string name, object trueValue) : base(name)
        {
            initColumn(trueValue);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbFlagColumn"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <param name="trueValue">The true value.</param>
        public DbFlagColumn(string name, bool defaultValue, object trueValue) : base(name, defaultValue)
        {
            initColumn(trueValue);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbFlagColumn"/> class.
        /// </summary>
        /// <param name="dbMethodType">Type of the database method.</param>
        /// <param name="name">The name.</param>
        /// <param name="trueValue">The true value.</param>
        public DbFlagColumn(Type dbMethodType, string name, object trueValue) : base(dbMethodType, name)
        {
            initColumn(trueValue);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbFlagColumn"/> class.
        /// </summary>
        /// <param name="dbMethodType">Type of the database method.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <param name="trueValue">The true value.</param>
        public DbFlagColumn(Type dbMethodType, string name, bool defaultValue, object trueValue) : base(dbMethodType, name, defaultValue)
        {
            initColumn(trueValue);
        }
        /// <summary>
        /// Initializes the column.
        /// </summary>
        /// <param name="trueValue">The true value.</param>
        private void initColumn(object trueValue)
        {
            TrueValue = trueValue;
        }
        /// <summary>
        /// Gets the column value.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        protected override object GetColumnValue(IDataRecord data)
        {
            return GetValue(data, TrueValue.GetType()).ToString() == TrueValue.ToString();
        }
    }
}
