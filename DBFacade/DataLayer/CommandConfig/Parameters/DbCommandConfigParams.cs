using System.Collections.Generic;
using DBFacade.DataLayer.Models;

namespace DBFacade.DataLayer.CommandConfig.Parameters
{
    /// <summary></summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public class DbCommandConfigParams<TDbParams> : Dictionary<string, IDbCommandParameterConfig<TDbParams>>,
        IDbCommandConfigParams<TDbParams>
        where TDbParams : IDbParamsModel
    {
    }
    public class DbCommandConfigParams : DbCommandConfigParams<DbParamsModel> { }
}