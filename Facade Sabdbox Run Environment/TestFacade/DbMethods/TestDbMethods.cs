using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;
using DomainFacade.DataLayer.Models.Validators.Rules;
using DomainFacade.SampleDomainFacade.DbMethods;

namespace Facade_Sabdbox_Run_Environment.TestFacade.DbMethods
{
   
    public abstract partial class TestDbMethods : DbMethodsCore
    {
        public sealed class GetAllSimple : TestDbMethods
        {
            
            protected override DbCommandConfig GetConfigCore()
            {
                return DbCommandConfigBuilder.GetFetchRecordsConfig(TestDbConnection.GetAllSimpleData);
            }

        }
        
        public sealed class GetAllMore : TestDbMethods
        {
            protected override DbCommandConfig GetConfigCore()
            {
                return DbCommandConfigBuilder.GetFetchRecordsConfig(TestDbConnection.GetAllMoreData);
            }
        }
        
        public sealed class AddSimple : TestDbMethods, IValidator<SimpleDbParamsModel<int, string>>
        {
            public Validator<SimpleDbParamsModel<int, string>> GetValidator()
            {                
                Validator<SimpleDbParamsModel<int, string>> validator = new Validator<SimpleDbParamsModel<int, string>>(){
                        new ValidationRule<SimpleDbParamsModel<int, string>>.Required(model => model.Param1),
                        new ValidationRule<SimpleDbParamsModel<int, string>>.GreaterThanOrEqual(model => model.Param1, 123),
                        new ValidationRule<SimpleDbParamsModel<int, string>>.MinLength(model => model.Param2, true, 10)
                    };
                
                return validator;
            }

            protected override DbCommandConfig GetConfigCore()
            {
                return DbCommandConfigBuilder.GetTransactionConfig(
                    TestDbConnection.AddSimpleData,
                    new DbCommandConfigParams<SimpleDbParamsModel<int, string>>()
                    {
                        { "Count", DbCommandParameterConfig<SimpleDbParamsModel<int, string>>.Int32(model => model.Param1)},
                        { "Comment", DbCommandParameterConfig<SimpleDbParamsModel<int, string>>.String(model => model.Param2).SetIsNullable()}
                    },
                    GetValidator()
                );
            }
            
        }

    }
}
