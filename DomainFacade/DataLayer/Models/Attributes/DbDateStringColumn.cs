using System;
using System.Data;

namespace DomainFacade.DataLayer.Models.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DomainFacade.DataLayer.Models.Attributes.DbColumn" />
    public sealed class DbDateStringColumn : DbColumn
    {
        /// <summary>
        /// The date format
        /// </summary>
        private string dateFormat;
        /// <summary>
        /// Initializes a new instance of the <see cref="DbDateStringColumn"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dateFormat">The date format.</param>
        public DbDateStringColumn(string name, string dateFormat) : base(name)
        {
            this.dateFormat = dateFormat;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbDateStringColumn"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="dateFormat">The date format.</param>
        public DbDateStringColumn(string name, System.DateTime defaultValue, string dateFormat) : base(name, defaultValue)
        {
            this.dateFormat = dateFormat;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbDateStringColumn"/> class.
        /// </summary>
        /// <param name="dbMethodType">Type of the database method.</param>
        /// <param name="name">The name.</param>
        /// <param name="dateFormat">The date format.</param>
        public DbDateStringColumn(Type dbMethodType, string name, string dateFormat) : base(dbMethodType, name)
        {
            this.dateFormat = dateFormat;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbDateStringColumn"/> class.
        /// </summary>
        /// <param name="dbMethodType">Type of the database method.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="dateFormat">The date format.</param>
        public DbDateStringColumn(Type dbMethodType, string name, System.DateTime defaultValue, string dateFormat) : base(dbMethodType, name, defaultValue)
        {
            this.dateFormat = dateFormat;
        }

        /// <summary>
        /// Gets the column value.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
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
