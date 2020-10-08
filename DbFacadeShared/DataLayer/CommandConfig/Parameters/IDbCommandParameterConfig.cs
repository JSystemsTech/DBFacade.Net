using System.Data;
using System.Threading.Tasks;
using DbFacade.DataLayer.Models;

namespace DbFacade.DataLayer.CommandConfig.Parameters
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
        int OutputSize { get; }
        object Value(TDbParams model);
        Task<object> ValueAsync(TDbParams model);
    }
}