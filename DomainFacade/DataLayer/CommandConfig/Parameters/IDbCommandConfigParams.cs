using DomainFacade.DataLayer.Models;
using System.Collections;

namespace DomainFacade.DataLayer.CommandConfig.Parameters
{
    public interface IDbCommandConfigParams<T> : IEnumerable
        where T : IDbParamsModel
    {
        int ParamsCount();
    }
}
