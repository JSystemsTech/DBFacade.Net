using System.Collections;
using System.Threading.Tasks;
using DbFacade.DataLayer.Models;
using DbFacade.Factories;

namespace DbFacade.DataLayer.CommandConfig.Parameters
{
    public interface IDbCommandConfigParams<TDbParams> : IEnumerable
        where TDbParams : DbParamsModel
    {
        DbCommandParameterConfigFactory<TDbParams> Factory {get;}
        void Add(string key, IDbCommandParameterConfig<TDbParams> value);
        Task AddAsync(string key, IDbCommandParameterConfig<TDbParams> value);
        int Count { get; }
    }
}