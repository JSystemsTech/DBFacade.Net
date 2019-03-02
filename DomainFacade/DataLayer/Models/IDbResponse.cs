using DomainFacade.Exceptions;
using System.Collections.Generic;

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
        TDbDataModel Result();
        
    }    
}
