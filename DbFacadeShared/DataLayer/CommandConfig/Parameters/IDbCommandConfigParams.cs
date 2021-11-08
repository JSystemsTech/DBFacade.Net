using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DbFacade.DataLayer.Models;
using DbFacade.Factories;

namespace DbFacade.DataLayer.CommandConfig.Parameters
{
    public interface IDbCommandConfigParams<TDbParams> : IEnumerable, IDictionary<string, IDbCommandParameterConfig<TDbParams>>
        where TDbParams : DbParamsModel
    {
        DbCommandParameterConfigFactory<TDbParams> Factory {get;}
        Task AddAsync(string key, IDbCommandParameterConfig<TDbParams> value);
    }
}