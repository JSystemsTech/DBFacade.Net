using DomainFacade.Facade;
using DomainFacade.Utils;
using Facade_Sabdbox_Run_Environment.TestFacade.DbMethods;
using Facade_Sabdbox_Run_Environment.TestFacade.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Facade_Sabdbox_Run_Environment.TestFacade
{
    public sealed partial class MockTestDomain : DomainFacade<TestManager, TestDbMethods>, ITestDomain
    {       
        private class MockDbModel : IMockDbTableRow
        {
            public int id { get; set; }            
            public Guid guid { get; set; }
            public DateTime CreateDate { get; set; }
            public int count { get; set; }
            public string comment { get; set; }

            public MockDbModel(int id, Guid guid, DateTime createDate, int count, string comment)
            {
                this.id = id;
                this.guid = guid;
                this.CreateDate = createDate;
                this.count = count;
                this.comment = comment;
            }
        }
        public IEnumerable<TestDbDataModel> GetAllSimple()
        {
            MockDbModel data1 = new MockDbModel(1, Guid.NewGuid(), DateTime.Now, 23, "Testing comment");
            MockDbModel data2 = new MockDbModel(2, Guid.NewGuid(), DateTime.Now, 54, null);
            MockDbModel data3 = new MockDbModel(3, Guid.NewGuid(), DateTime.Now, 123, "other comment");
            MockDbTable<MockDbModel> data = new MockDbTable<MockDbModel>(new List<MockDbModel> { data1, data2, data3 });
            IDataReader reader = data.ToDataReader();
            
            return MockFetch<TestDbDataModel, TestDbMethods.GetAllSimple>(reader).Results();
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
