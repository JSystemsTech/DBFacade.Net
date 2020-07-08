using System.Data;
using DBFacade.DataLayer.Models;

namespace DBFacade.DataLayer.CommandConfig.Parameters
{
    public interface IDbCommandParameterConfig<TDbParams>
        where TDbParams : IDbParamsModel
    { }

    internal interface IInternalDbCommandParameterConfig<TDbParams> : IDbCommandParameterConfig<TDbParams>
        where TDbParams : IDbParamsModel
    {
        DbType DbType { get; }
        bool IsNullable { get; }
        bool IsOutput { get; }
        object Value(TDbParams model);
    }
}