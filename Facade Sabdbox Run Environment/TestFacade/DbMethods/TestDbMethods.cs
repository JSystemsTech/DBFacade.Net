using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.SampleDomainFacade.DbMethods;

namespace Facade_Sabdbox_Run_Environment.TestFacade.DbMethods
{
   
    public abstract partial class TestDbMethods : DbMethodsCore
    {
        public TestDbMethods(int id) : base(id) { }

        public static TestDbMethods GetAllSimple = new GetAllSimpleData();
        protected class GetAllSimpleData : TestDbMethods
        {
            public GetAllSimpleData() : base(1) { }

            protected override DbCommandConfig GetConfigCore()
            {
                return GetFetchRecordsConfig(TestDbConnection.GetAllSimpleData);
            }
        }
        public static TestDbMethods GetAllMore = new GetAllMoreData();
        protected class GetAllMoreData : TestDbMethods
        {
            public GetAllMoreData() : base(2) { }

            protected override DbCommandConfig GetConfigCore()
            {
                return GetFetchRecordsConfig(TestDbConnection.GetAllMoreData);
            }
        }
        public static TestDbMethods AddSimple = new AddSimpleData();
        protected class AddSimpleData : TestDbMethods
        {
            public AddSimpleData() : base(3) { }

            protected override DbCommandConfig GetConfigCore()
            {
                return GetTransactionConfig(
                    TestDbConnection.AddSimpleData,
                    new DbCommandConfigParams<SimpleDbParamsModel<int, string>>()
                    {
                        { "Count", DbCommandParameterConfig<SimpleDbParamsModel<int, string>>.Int32(model => model.Param1)},
                        { "Comment", DbCommandParameterConfig<SimpleDbParamsModel<int, string>>.String(model => model.Param2).SetIsNullable()}
                    }                
                );
            }
        }

    }
}
