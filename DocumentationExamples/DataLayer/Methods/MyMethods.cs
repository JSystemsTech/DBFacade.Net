using DBFacade.DataLayer.CommandConfig;
using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using DBFacade.DataLayer.Models.Validators.Rules;
using DBFacade.Factories;
using DocumentationExamples.DataLayer.Connection;


namespace DocumentationExamples.DataLayer.Methods
{
    internal abstract partial class MyMethods : DbMethodManifest
    {
        /*Boilerplate Code To set up basic helpers for methods with and without parameters */
        internal abstract class DbMethod<TDbParams> : MyMethods
            where TDbParams : IDbParamsModel
        {
            protected IDbCommandParameterConfigFactory<TDbParams> ParamFactory => GetCommandParameterConfigFactory<TDbParams>();
            protected sealed class Params: DbCommandConfigParams<TDbParams> { }
            protected sealed class ConfigFactory : DbCommandConfigFactory<TDbParams> { }
            protected sealed class Validator : Validator<TDbParams> { }
            protected sealed class Rules : ValidationRule<TDbParams> { }
        }
        internal abstract class DbMethodParameterless : MyMethods { }

        /* Begin Defining Methods Below */

        internal sealed class GetData : DbMethodParameterless
        {
            protected override IDbCommandConfig BuildConfig() => DbCommandConfigFactory.FetchConfig(MySqlDbConnection.GetData);            
        }
        internal sealed class GetDataWithOutputParameters : DbMethodParameterless
        {
            protected override IDbCommandConfig BuildConfig()
            => DbCommandConfigFactory.FetchConfig(
                    MySqlDbConnection.GetDataWithOutputParameters,
                    new DbCommandConfigParams
                    {
                        {"MyStringOutputParam", DbCommandParameterConfigFactory.OutputString(8000) }
                    }
                );
        }
        internal sealed class GetDataWithParameters : DbMethod<DbParamsModel<string, int>>
        {
            protected override IDbCommandConfig BuildConfig()
            {
                return ConfigFactory.FetchConfig(
                    MySqlDbConnection.GetDataWithParameters,
                    new Params {
                        {"MyStringParam", ParamFactory.String(model=>model.Param1) },
                        {"MyIntParam", ParamFactory.Int32(model=>model.Param2) },
                        {"MyStringOutputParam", ParamFactory.OutputString(8000) }
                    },
                    new Validator() {
                        Rules.Required(model=>model.Param1),
                        Rules.IsNotNullOrWhiteSpace(model=>model.Param1),
                        Rules.Required(model=>model.Param2)
                    }
                );
            }
        }        
        internal sealed class ExecuteMyTransaction : DbMethod<DbParamsModel<string>>
        {
            protected override IDbCommandConfig BuildConfig()
            {
                return ConfigFactory.TransactionConfig(
                    MySqlDbConnection.ExecuteMyTransaction,
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
