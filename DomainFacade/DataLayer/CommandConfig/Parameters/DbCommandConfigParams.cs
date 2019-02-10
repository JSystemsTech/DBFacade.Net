using DomainFacade.DataLayer.Models;
using System.Collections.Generic;

namespace DomainFacade.DataLayer.CommandConfig.Parameters
{
    public class DbCommandConfigParams<TDbParams> : Dictionary<string, DbCommandParameterConfig<TDbParams>>, IDbCommandConfigParams<TDbParams>
        where TDbParams : IDbParamsModel
    {
        public int ParamsCount()
        {
            return Count;
        }
    }
}
