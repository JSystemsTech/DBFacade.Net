using DBFacade.DataLayer.Models;
using System.Collections;

namespace DBFacade.DataLayer.CommandConfig.Parameters
{
    public interface IDbCommandConfigParams<TDbParams> : IEnumerable
        where TDbParams : IDbParamsModel
    {
        int Count { get; }
    }
}
