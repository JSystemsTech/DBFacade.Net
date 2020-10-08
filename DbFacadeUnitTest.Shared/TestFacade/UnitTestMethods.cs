using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.DataLayer.Manifest;
using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Validators;
using DbFacade.DataLayer.Models.Validators.Rules;
using DbFacade.Factories;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.TestFacade
{
    public abstract class UnitTestMethods : DbMethodManifest
    {
        public abstract class UnitTestMethod<TDbParams> : UnitTestMethods
            where TDbParams : IDbParamsModel
        {
            protected sealed class ParamsFactory : DbCommandParameterConfigFactory<TDbParams> { }
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
            protected override async Task<IDbCommandConfig> BuildConfigAsync()
            {
                return await DbCommandConfigFactory.FetchConfigAsync(
                    UnitTestConnection.TestFetchData
                );
            }
        }
        public sealed class TestFetchDataWithOutput : UnitTestMethods
        {
            protected override IDbCommandConfig BuildConfig()
            {
                return DbCommandConfigFactory.FetchConfig(
                    UnitTestConnection.TestFetchDataWithOutputParameters,
                    new DbCommandConfigParams
                    {
                        {"MyStringOutputParam", DbCommandParameterConfigFactory.OutputString(8000) }
                    }
                );
            }
            protected override async Task<IDbCommandConfig> BuildConfigAsync()
            {
                return await DbCommandConfigFactory.FetchConfigAsync(
                    UnitTestConnection.TestFetchDataWithOutputParameters,
                    new DbCommandConfigParams
                    {
                        {"MyStringOutputParam", await DbCommandParameterConfigFactory.OutputStringAsync(8000) }
                    }
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
                        "MyStringParam", ParamsFactory.Create(model=>model.Param1) }
                    },
                    new Validator() {
                        Rules.Required(model=>model.Param1)
                    }
                );
            }
            protected override async Task<IDbCommandConfig> BuildConfigAsync()
            {
                return await ConfigFactory.TransactionConfigAsync(
                    UnitTestConnection.TestTransaction,
                    new Params {
                        {"MyStringParam", await ParamsFactory.CreateAsync(model=>model.Param1) }
                    },
                    await Validator.CreateAsync(
                        await Rules.RequiredAsync(model => model.Param1)
                        )
                );
            }
        }
        public sealed class TestTransactionWithOutput : UnitTestMethod<DbParamsModel<string>>
        {
            protected override IDbCommandConfig BuildConfig()
            {
                return ConfigFactory.TransactionConfig(
                    UnitTestConnection.TestTransaction,
                    new Params {
                        {"MyStringParam", ParamsFactory.Create(model=>model.Param1) },
                        {"MyStringOutputParam", ParamsFactory.OutputString(8000) }
                    },
                    new Validator() {
                        Rules.Required(model=>model.Param1)
                    }
                );
            }
            protected override async Task<IDbCommandConfig> BuildConfigAsync()
            {
                return await ConfigFactory.TransactionConfigAsync(
                    UnitTestConnection.TestTransaction,
                    new Params {
                        {"MyStringParam", await ParamsFactory.CreateAsync(model=>model.Param1) },
                        {"MyStringOutputParam", await ParamsFactory.OutputStringAsync(8000) }
                    },
                    await Validator.CreateAsync(
                        await Rules.RequiredAsync(model => model.Param1)
                        )
                );
            }
        }
    }
}
