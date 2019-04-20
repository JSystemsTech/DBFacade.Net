using DomainFacade.Exceptions;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DomainFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbResponse {
        /// <summary>
        /// Returns the value.
        /// </summary>
        /// <returns></returns>
        object ReturnValue();
        /// <summary>
        /// Determines whether this instance has error.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.
        /// </returns>
        bool HasError();
        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <returns></returns>
        FacadeException GetException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    public interface IDbResponse<TDbDataModel> :IDbResponse
         where TDbDataModel : DbDataModel
    {
        /// <summary>
        /// Resultses this instance.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TDbDataModel> Results();
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <returns></returns>
        string ToJson();
        /// <summary>
        /// Converts to jsonresult.
        /// </summary>
        /// <returns></returns>
        JsonResult ToJsonResult();
        /// <summary>
        /// Results this instance.
        /// </summary>
        /// <returns></returns>
        TDbDataModel Result();
        
    }
    
}
