using System;
using System.Data;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Attributes
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
        public override object GetValue(IDataRecord data, Type propType = null)
        {
            return TryGetValue(data, _trueValue.GetType()).ToString() == _trueValue.ToString();
        }
        public override async Task<object> GetValueAsync(IDataRecord data, Type propType = null)
        {
            object value = await TryGetValueAsync(data, _trueValue.GetType());
            return value.ToString() == _trueValue.ToString();
        }
    }
}