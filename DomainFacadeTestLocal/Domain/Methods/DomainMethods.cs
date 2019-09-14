using DBFacade.DataLayer.CommandConfig;
using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using DBFacade.DataLayer.Models.Validators.Rules;
using DomainFacadeTestLocal.Domain.Connection;
using System.Data;

namespace DomainFacadeTestLocal.Domain.Methods
{
    public abstract class DomainMethods : DbManifest
    {        
        public abstract class DomainMethod<TDbParams>: DomainMethods
            where TDbParams : IDbParamsModel
        {
            protected IDbCommandParameterConfigFactory<TDbParams> ParamFactory => GetCommandParameterConfigFactory<TDbParams>();
            protected sealed class Params : DbCommandConfigParams<TDbParams> { }
            protected sealed class ConfigFactory : DbCommandConfigFactory<TDbParams> { }
            protected sealed class Validator : Validator<TDbParams> { }
            protected sealed class Rules : ValidationRule<TDbParams> { } 
        }
        
        public class GetMyData : DomainMethod<DbParamsModel<string, int>>
        {
            private const string ReturnValueParam = "MyReturnValueParam";
            private const bool ReturnValueParamIsOutput = true;
            
            protected override IDbCommandConfig BuildConfig()
            {
                return ConfigFactory.FetchConfig(
                    MyConnection.GetMyData,
                    CommandType.Text,
                    new Params {
                        {"MyStringParam", ParamFactory.String(model=> model.Param1) },
                        {"MyIntParam", ParamFactory.Int32(model=> model.Param2) }
                    },
                    new Validator() {
                        Rules.Required(model=>model.Param1),
                        Rules.Required(model=>model.Param2),
                        Rules.GreaterThanOrEqual(model=>model.Param2, 10)
                    },
                    ReturnValueParam,
                    ReturnValueParamIsOutput
                    );
            }
        }
    }
}
