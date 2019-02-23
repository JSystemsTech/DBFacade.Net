using System.Collections.Generic;

namespace DomainFacade.DataLayer.Models
{
    public interface IDbResponse {
        object ReturnValue();
    }
    
    public interface IDbResponse<TDbDataModel> :IDbResponse
         where TDbDataModel : DbDataModel
    {
        IEnumerable<TDbDataModel> Data();
        TDbDataModel Model();
    }    
}
