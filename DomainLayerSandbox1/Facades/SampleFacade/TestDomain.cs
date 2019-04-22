using DBFacade.DataLayer.Models;
using DBFacade.Facade;
using DomainLayerSandbox1.Facades.SampleFacade;
using DomainLayerSandbox1.Facades.Models.DbDataModels;
using System.Collections.Generic;

namespace DomainLayerSandbox1.TestFacade
{
    public sealed partial class TestDomain : DomainFacade<TestManager, TestDbMethods>, IDomainFacade
    {
        public IDbResponse<TestDbDataModel> GetAllSimpleResponse()
        {
            return Fetch<TestDbDataModel, TestDbMethods.GetAllSimple>();
        }
        public IEnumerable<TestDbDataModel> GetAllSimple()
        {
            return Fetch<TestDbDataModel, TestDbMethods.GetAllSimple>().Results();
        }
        public IEnumerable<TestConstructorModel> GetAllSimple2()
        {
            return Fetch<TestConstructorModel, TestDbMethods.GetAllSimple>().Results();
        }
        public IEnumerable<TestSharedDbDataModel> GetAllMoreShared()
        {
            return Fetch<TestSharedDbDataModel, TestDbMethods.GetAllSimple>().Results();
        }
        public IEnumerable<MoreDbDataModel> GetAllMore()
        {
            return Fetch<MoreDbDataModel, TestDbMethods.GetAllMore>().Results();
        }
        public IEnumerable<TestSharedDbDataModel> GetAllSimpleShared()
        {
            return Fetch<TestSharedDbDataModel, TestDbMethods.GetAllMore>().Results();
        }
        public void AddSimpleRecord(int count, string comment)
        {
            Transaction<SimpleDbParamsModel<int, string>, TestDbMethods.AddSimple>(new SimpleDbParamsModel<int, string>(count, comment));
        }
        public void CallMissingSproc()
        {
            Transaction<TestDbMethods.MissingSproc>();
        }
        protected override void OnError(IDbResponse response)
        {
            throw response.GetException();
        }
    }
}