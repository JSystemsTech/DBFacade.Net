using DBFacade.DataLayer.Models;
using DomainLayerSandbox1.Facades.Models.DbDataModels;
using System.Collections.Generic;

namespace DomainLayerSandbox1
{
    public interface IDomainFacade
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
