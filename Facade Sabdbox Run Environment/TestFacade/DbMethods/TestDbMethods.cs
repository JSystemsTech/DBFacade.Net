using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;
using DomainFacade.SampleDomainFacade.DbMethods;
using DomainFacade.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Facade_Sabdbox_Run_Environment.TestFacade.DbMethods
{
   
    public abstract partial class TestDbMethods : DbMethodsCore
    {
        public static GetAllSimpleData GetAllSimple = new GetAllSimpleData();
        public sealed class GetAllSimpleData : TestDbMethods
        {
            protected override DbCommandConfig GetConfigCore()
            {
                return GetFetchRecordsConfig(TestDbConnection.GetAllSimpleData);
            }
        }

        public static GetAllMoreData GetAllMore = new GetAllMoreData();
        public sealed class GetAllMoreData : TestDbMethods
        {
            protected override DbCommandConfig GetConfigCore()
            {
                return GetFetchRecordsConfig(TestDbConnection.GetAllMoreData);
            }
        }
        
        public static AddSimpleData AddSimple = new AddSimpleData();
        public sealed class AddSimpleData : TestDbMethods, IValidator<SimpleDbParamsModel<int, string>>
        {
            public Validator<SimpleDbParamsModel<int, string>> GetValidator()
            {                
                Validator<SimpleDbParamsModel<int, string>> validator = new Validator<SimpleDbParamsModel<int, string>>(){
                        new ValidationRule<SimpleDbParamsModel<int, string>>.Required(model => model.Param1),
                        new ValidationRule<SimpleDbParamsModel<int, string>>.GreatorThanOrEqual(model => model.Param1, 123),
                        new ValidationRule<SimpleDbParamsModel<int, string>>.MinLength(model => model.Param2,true, 10)
                    };
                
                return validator;
            }

            protected override DbCommandConfig GetConfigCore()
            {
                return GetTransactionConfig(
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
