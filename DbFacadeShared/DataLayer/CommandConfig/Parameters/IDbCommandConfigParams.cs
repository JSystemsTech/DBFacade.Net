using System.Collections;
using DbFacade.DataLayer.Models;

namespace DbFacade.DataLayer.CommandConfig.Parameters
{
    public interface IDbCommandConfigParams<TDbParams> : IEnumerable
        where TDbParams : IDbParamsModel
    {
        int Count { get; }
    }
}