using DomainFacade.DataLayer.Models;
using System.Collections.Generic;

namespace DomainFacade.DataLayer.CommandConfig.Parameters
{
    public class DbCommandConfigParams<T> : Dictionary<string, DbCommandParameterConfig<T>>, IDbCommandConfigParams<T>
        where T : IDbParamsModel
    {
        public int ParamsCount()
        {
            return Count;
        }
    }
}
