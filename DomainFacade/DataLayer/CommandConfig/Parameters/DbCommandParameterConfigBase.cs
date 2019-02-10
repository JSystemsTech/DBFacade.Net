using DomainFacade.DataLayer.Models;
using System.Data;

namespace DomainFacade.DataLayer.CommandConfig.Parameters
{
    public abstract class DbCommandParameterConfigBase<T> where T : IDbParamsModel
    {
        public DbType DBType { get; private set; }
        protected DbCommandParameterConfigBase(DbType dbType)
        {
            DBType = dbType;
        }
        public virtual object GetParam(T model)
        {
            return default(object);
        }
        internal bool isNullable = true;
        public virtual bool IsNullable()
        {
            return isNullable;
        }
    }
}
