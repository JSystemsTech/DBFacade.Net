using DomainFacade.Exceptions;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DomainFacade.DataLayer.Models
{
    public interface IDbResponse {
        object ReturnValue();
        bool HasError();
        FacadeException GetException();
    }
    
    public interface IDbResponse<TDbDataModel> :IDbResponse
         where TDbDataModel : DbDataModel
    {
        IEnumerable<TDbDataModel> Results();
        string ToJson();
        JsonResult ToJsonResult();
        TDbDataModel Result();
        
    }
    
}
