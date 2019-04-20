using DBFacade.DataLayer.Models;
using Facade_Sabdbox_Run_Environment.TestFacade.Models;
using System.Collections.Generic;

namespace Facade_Sabdbox_Run_Environment.TestFacade
{
    public interface ITestDomain
    {
        IDbResponse<TestDbDataModel> GetAllSimpleResponse();
        IEnumerable<TestDbDataModel> GetAllSimple();
        IEnumerable<TestConstructorModel> GetAllSimple2();
        IEnumerable<TestSharedDbDataModel> GetAllMoreShared();
        IEnumerable<MoreDbDataModel> GetAllMore();
        IEnumerable<TestSharedDbDataModel> GetAllSimpleShared();
        void AddSimpleRecord(int count, string comment);
        void CallMissingSproc();
    }
}
