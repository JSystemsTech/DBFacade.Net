using DBFacade.DataLayer.Models;
using System.Data;

namespace DBFacade.DataLayer.CommandConfig.Parameters
{
    public interface IDbCommandParameterConfig<TDbParams>
        where TDbParams : IDbParamsModel
    { }
    interface IInternalDbCommandParameterConfig<TDbParams>: IDbCommandParameterConfig<TDbParams>
        where TDbParams : IDbParamsModel
    {
        DbType DbType { get; }
        bool IsNullable { get; }
        object Value(TDbParams model);
    }
}
