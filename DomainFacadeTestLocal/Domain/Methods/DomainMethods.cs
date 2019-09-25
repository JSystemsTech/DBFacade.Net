using DBFacade.DataLayer.CommandConfig;
using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using DBFacade.DataLayer.Models.Validators.Rules;
using DBFacade.Factories;
using DomainFacadeTestLocal.Domain.Connection;
using System;

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

        public sealed class GetMyData : DomainMethods
        {
            protected override IDbCommandConfig BuildConfig()
            {
                return DbCommandConfigFactory.FetchConfig(
                    MyConnection.GetMyData
                );
            }
        }
        public sealed class AddNewData : DomainMethod<DbParamsModel<string, string, int>>
        {
            private string GetMyString(DbParamsModel<string, string, int> model)
            {
                return $"{model.Param1}{model.Param2}";
            }
            protected override IDbCommandConfig BuildConfig()
            {
                return ConfigFactory.TransactionConfig(
                    MyConnection.AddData,
                    new Params {
                    {"MyStringParam", ParamFactory.String(GetMyString) },
                    {"MyIntParam", ParamFactory.Int32(model=> model.Param3) }
                    },
                    new Validator() {
                    Rules.Required(model=>model.Param1),
                    Rules.Required(model=>model.Param2),
                    Rules.MinLength(model=>model.Param1, 5),
                    Rules.MinLength(model=>model.Param2, 15),
                    Rules.GreaterThanOrEqual(model=>model.Param3, 10)
                    }
                );
            }
        }
        public sealed class UpdateData : DomainMethod<DbParamsModel<Guid, string, int?>>
        {
            protected override IDbCommandConfig BuildConfig()
            {
                return ConfigFactory.TransactionConfig(
                    MyConnection.UpdateData,
                    new Params {
                    {"Id", ParamFactory.Guid(model=> model.Param1) },
                    {"MyStringParam", ParamFactory.String(model=> model.Param2) },
                    {"MyIntParam", ParamFactory.Int32(model=> model.Param3) }
                    },
                    new Validator() {
                    Rules.MinLength(model=>model.Param2, 10,true),
                    Rules.GreaterThanOrEqual(model=>model.Param3, 10)
                    }
                );
            }
        }
        public sealed class DeleteData : DomainMethod<DbParamsModel<Guid>>
        {
            protected override IDbCommandConfig BuildConfig()
            {
                return ConfigFactory.TransactionConfig(
                    MyConnection.DeleteData,
                    new Params {
                    {"Id", ParamFactory.Guid(model=> model.Param1) }
                    }
                );
            }
        }
    }
}
