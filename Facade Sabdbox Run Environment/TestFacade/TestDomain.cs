using DomainFacade.DataLayer.Models;
using DomainFacade.Facade;
using Facade_Sabdbox_Run_Environment.TestFacade.DbMethods;
using Facade_Sabdbox_Run_Environment.TestFacade.Models;
using System.Collections.Generic;

namespace Facade_Sabdbox_Run_Environment.TestFacade
{
    public sealed partial class TestDomain: DomainFacade<TestManager, TestDbMethods>,ITestDomain
    {
        public IEnumerable<TestDbDataModel> GetAllSimple()
        {
            return Fetch<TestDbDataModel, TestDbMethods.GetAllSimple>().Data();
        }
        public IEnumerable<TestConstructorModel> GetAllSimple2()
        {
            return Fetch<TestConstructorModel, TestDbMethods.GetAllSimple>().Data();
        }
        public IEnumerable<TestSharedDbDataModel> GetAllMoreShared()
        {
            return Fetch<TestSharedDbDataModel, TestDbMethods.GetAllSimple>().Data();
        }
        public IEnumerable<MoreDbDataModel> GetAllMore()
        {
            return Fetch<MoreDbDataModel, TestDbMethods.GetAllMore>().Data();
        }
        public IEnumerable<TestSharedDbDataModel> GetAllSimpleShared()
        {
            return Fetch<TestSharedDbDataModel, TestDbMethods.GetAllMore>().Data();
        }
        public void AddSimpleRecord(int count, string comment)
        { 
            Transaction<SimpleDbParamsModel<int, string>, TestDbMethods.AddSimple>(new SimpleDbParamsModel<int, string>(count, comment));
        }
    }
}
