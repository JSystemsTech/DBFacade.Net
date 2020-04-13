using System.Collections;
using DBFacade.DataLayer.Models;

namespace DBFacade.DataLayer.CommandConfig.Parameters
{
    public interface IDbCommandConfigParams<TDbParams> : IEnumerable
        where TDbParams : IDbParamsModel
    {
        int Count { get; }
    }
}