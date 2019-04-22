using DBFacade.DataLayer.Models;
using DBFacade.Facade;
using DBFacade.Utils;
using DomainLayerSandbox1.Facades.SampleFacade;
using DomainLayerSandbox1.Facades.Models.DbDataModels;
using System;
using System.Collections.Generic;
using System.Data;
using DomainLayerSandbox1.Facades.SampleFunctionalTestFacade.MockDbDataModels;

namespace DomainLayerSandbox1.TestFacade
{
    public sealed partial class MockTestDomain : DomainFacade<TestManager, TestDbMethods>, IDomainFacade
    {

        public IDbResponse<TestDbDataModel> GetAllSimpleResponse()
        {
            MockTestModel data1 = new MockTestModel(1, Guid.NewGuid(), DateTime.Now, 23, "Testing comment");
            MockTestModel data2 = new MockTestModel(2, Guid.NewGuid(), DateTime.Now, 54, null);
            MockTestModel data3 = new MockTestModel(3, Guid.NewGuid(), DateTime.Now, 123, "other comment");
            MockDbTable<MockTestModel> data = new MockDbTable<MockTestModel>(new List<MockTestModel> { data1, data2, data3 });
            IDataReader reader = data.ToDataReader();

            return MockFetch<TestDbDataModel, TestDbMethods.GetAllSimple>(reader);
        }
        public IEnumerable<TestDbDataModel> GetAllSimple()
        {
            return GetAllSimpleResponse().Results();
        }
        public IEnumerable<TestConstructorModel> GetAllSimple2()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TestSharedDbDataModel> GetAllMoreShared()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MoreDbDataModel> GetAllMore()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TestSharedDbDataModel> GetAllSimpleShared()
        {
            throw new NotImplementedException();
        }

        public void AddSimpleRecord(int count, string comment)
        {
            throw new NotImplementedException();
        }
        public void CallMissingSproc()
        {
            throw new NotImplementedException();
        }
    }
}
