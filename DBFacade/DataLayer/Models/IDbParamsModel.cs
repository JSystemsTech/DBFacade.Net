using System.Collections.Generic;
using System.Data.Common;

namespace DBFacade.DataLayer.Models
{
    public interface IDbParamsModel
    {
        void RunAsTest(object returnValue);
        void RunAsTest<T>(IEnumerable<T> responseData, object returnValue);
        void RunAsTest<T>(T responseData, object returnValue);
    }

    internal interface IInternalDbParamsModel : IDbParamsModel
    {
        MethodRunMode RunMode { get; }
        DbDataReader ResponseData { get; }
        object ReturnValue { get; }
    }
}