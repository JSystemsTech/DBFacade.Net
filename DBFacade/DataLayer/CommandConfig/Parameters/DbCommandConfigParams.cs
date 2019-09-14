using DBFacade.DataLayer.Models;
using System.Collections.Generic;

namespace DBFacade.DataLayer.CommandConfig.Parameters
{
    
    /// <summary></summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public class DbCommandConfigParams<TDbParams> : Dictionary<string, IDbCommandParameterConfig<TDbParams>>, IDbCommandConfigParams<TDbParams>
        where TDbParams : IDbParamsModel
    {}
}
