using System.Collections.Generic;
using System.Data.Common;

namespace DBFacade.DataLayer.Models
{
    public interface IDbParamsModel
    {
        void RunAsTest(int returnValue, IDictionary<string, object> outputValues = null);
        void RunAsTest<T>(IEnumerable<T> responseData, int returnValue, IDictionary<string, object> outputValues = null);
        void RunAsTest<T>(T responseData, int returnValue, IDictionary<string, object> outputValues = null);
    }

    internal interface IInternalDbParamsModel : IDbParamsModel
    {
        MethodRunMode RunMode { get; }
        DbDataReader ResponseData { get; }
        int ReturnValue { get; }
        IDictionary<string,object> OutputValues { get; }
    }
}