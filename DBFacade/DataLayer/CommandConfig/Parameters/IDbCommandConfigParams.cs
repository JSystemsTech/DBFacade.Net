using DBFacade.DataLayer.Models;
using System.Collections;

namespace DBFacade.DataLayer.CommandConfig.Parameters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    /// <seealso cref="IEnumerable" />
    public interface IDbCommandConfigParams<TDbParams> : IEnumerable
        where TDbParams : IDbParamsModel
    {
        /// <summary>
        /// Parameterses the count.
        /// </summary>
        /// <returns></returns>
        int ParamsCount();
    }
}
