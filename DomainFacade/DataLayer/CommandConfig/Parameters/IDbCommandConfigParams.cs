using DomainFacade.DataLayer.Models;
using System.Collections;

namespace DomainFacade.DataLayer.CommandConfig.Parameters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    /// <seealso cref="System.Collections.IEnumerable" />
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
