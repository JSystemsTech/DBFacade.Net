using System.Data;
using System.Threading.Tasks;
using DbFacade.DataLayer.Models;

namespace DbFacade.DataLayer.CommandConfig.Parameters
{
    public interface IDbCommandParameterConfig<TDbParams>
    { }

    internal interface IInternalDbCommandParameterConfig<TDbParams> : IDbCommandParameterConfig<TDbParams>
    {
        DbType DbType { get; }
        bool IsNullable { get; }
        ParameterDirection ParameterDirection { get;}
        bool IsOutput { get; }
        int? OutputSize { get; }
        object Value(TDbParams model);
        Task<object> ValueAsync(TDbParams model);
    }
}