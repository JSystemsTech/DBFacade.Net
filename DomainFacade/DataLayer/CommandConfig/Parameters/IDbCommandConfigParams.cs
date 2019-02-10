using DomainFacade.DataLayer.Models;
using System.Collections;

namespace DomainFacade.DataLayer.CommandConfig.Parameters
{
    public interface IDbCommandConfigParams<TDbParams> : IEnumerable
        where TDbParams : IDbParamsModel
    {
        int ParamsCount();
    }
}
