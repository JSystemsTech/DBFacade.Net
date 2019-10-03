using DBFacade.DataLayer.CommandConfig;
using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using DBFacade.DataLayer.Models.Validators.Rules;
using DBFacade.Factories;

namespace DbFacadeUnitTests.TestFacade
{
    public abstract class UnitTestMethods : DbMethodManifest
    {
        public abstract class UnitTestMethod<TDbParams> : UnitTestMethods
            where TDbParams : IDbParamsModel
        {
            protected IDbCommandParameterConfigFactory<TDbParams> ParamFactory => GetCommandParameterConfigFactory<TDbParams>();
            protected sealed class Params : DbCommandConfigParams<TDbParams> { }
            protected sealed class ConfigFactory : DbCommandConfigFactory<TDbParams> { }
            protected sealed class Validator : Validator<TDbParams> { }
            protected sealed class Rules : ValidationRule<TDbParams> { }
        }
        public sealed class TestFetchData : UnitTestMethods
        {
            protected override IDbCommandConfig BuildConfig()
            {
                return DbCommandConfigFactory.FetchConfig(
                    UnitTestConnection.TestFetchData
                );
            }
        }
        public sealed class TestTransaction : UnitTestMethod<DbParamsModel<string>>
        {
            protected override IDbCommandConfig BuildConfig()
            {
                return ConfigFactory.TransactionConfig(
                    UnitTestConnection.TestTransaction,
                    new Params {
                    {
                        "MyStringParam", ParamFactory.String(model=>model.Param1) }
                    },
                    new Validator() {
                        Rules.Required(model=>model.Param1)
                    }
                );
            }
        }
    }
}
