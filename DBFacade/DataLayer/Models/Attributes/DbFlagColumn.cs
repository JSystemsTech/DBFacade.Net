using System;
using System.Data;

namespace DBFacade.DataLayer.Models.Attributes
{
    /// <summary>
    /// </summary>
    /// <seealso cref="DbColumn" />
    public sealed class DbFlagColumn : DbColumn
    {
        /// <summary>
        ///     The true value
        /// </summary>
        private object _trueValue;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DbFlagColumn" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="trueValue">The true value.</param>
        public DbFlagColumn(string name, object trueValue) : base(name)
        {
            InitColumn(trueValue);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DbFlagColumn" /> class.
        /// </summary>
        /// <param name="tDbMethodManifestMethodType">Type of the database method.</param>
        /// <param name="name">The name.</param>
        /// <param name="trueValue">The true value.</param>
        public DbFlagColumn(Type tDbMethodManifestMethodType, string name, object trueValue) : base(
            tDbMethodManifestMethodType, name)
        {
            InitColumn(trueValue);
        }

        /// <summary>
        ///     Initializes the column.
        /// </summary>
        /// <param name="trueValue">The true value.</param>
        private void InitColumn(object trueValue)
        {
            _trueValue = trueValue;
        }

        /// <summary>
        ///     Gets the column value.
        /// </summary>
        /// <param name="data">The data.</param>
        /// ///
        /// <param name="propType">The data.</param>
        /// <returns></returns>
        protected override object GetColumnValue(IDataRecord data, Type propType = null)
        {
            return GetValue(data, _trueValue.GetType()).ToString() == _trueValue.ToString();
        }
    }
}