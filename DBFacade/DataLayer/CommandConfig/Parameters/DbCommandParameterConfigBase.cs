using DBFacade.DataLayer.Models;
using System.Data;

namespace DBFacade.DataLayer.CommandConfig.Parameters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public abstract class DbCommandParameterConfigBase<TDbParams> where TDbParams : IDbParamsModel
    {
        /// <summary>
        /// Gets the type of the database.
        /// </summary>
        /// <value>
        /// The type of the database.
        /// </value>
        public DbType DBType { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandParameterConfigBase{TDbParams}"/> class.
        /// </summary>
        /// <param name="dbType">Type of the database.</param>
        protected DbCommandParameterConfigBase(DbType dbType)
        {
            DBType = dbType;
        }
        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public virtual object GetParam(TDbParams model)
        {
            return default(object);
        }
        /// <summary>
        /// The is nullable
        /// </summary>
        internal bool isNullable = true;
        /// <summary>
        /// Determines whether this instance is nullable.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is nullable; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsNullable()
        {
            return isNullable;
        }
    }
}
