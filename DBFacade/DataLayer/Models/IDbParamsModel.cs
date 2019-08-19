using System.Collections.Generic;
using System.Data;

namespace DBFacade.DataLayer.Models
{
    public interface IDbParamsModel
    {
       void RunAsTest(object returnValue);
       void RunAsTest<T>(IEnumerable<T> responseData, object returnValue);
       void RunAsTest<T>(T responseData, object returnValue);
       MethodRunMode _GetRunMode();
       IDataReader _GetResponseData();
       object _GetReturnValue();
    }
}
