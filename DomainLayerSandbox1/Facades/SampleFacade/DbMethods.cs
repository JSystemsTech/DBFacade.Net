using DBFacade.DataLayer.CommandConfig;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.Utils;
using DomainFacade.Facades.DbConnection;
using DomainLayerSandbox1.Facades.Extensions;
using System;
using System.Linq.Expressions;

namespace DomainLayerSandbox1.Facades.SampleFacade
{
    public abstract partial class TestDbMethods : DbManifest
    {
        public abstract class DbMethod<DbParams> : TestDbMethods where DbParams : IDbParamsModel
        {
            internal class Tools : DbMethodUtils<DbParams> { }
            internal abstract class Rules : CustomValidationRules<DbParams>
            {
                public Rules(Selector<DbParams> selector) : base(selector) { }
                public Rules(Selector<DbParams> selector, bool isNullable) : base(selector, isNullable) { }
            }
            internal virtual Tools.Validator GetValidator() { return default(Tools.Validator); }
            internal static Selector<DbParams> Selector(Expression<Func<DbParams, object>> selectorExpression)
            {
                return Tools.Selector(selectorExpression);
            }
        }
        public sealed class GetAllSimple : TestDbMethods
        {
            protected override IDbCommandConfig GetConfigCore()
            {
                return DbCommandConfigBuilder.GetFetchConfig(DbConnection.GetAllSimpleData);
            }
        }

        public sealed class GetAllMore : TestDbMethods
        {
            protected override IDbCommandConfig GetConfigCore()
            {
                return DbCommandConfigBuilder.GetFetchConfig(DbConnection.GetAllMoreData);
            }
        }
        public sealed class MissingSproc : TestDbMethods
        {
            protected override IDbCommandConfig GetConfigCore()
            {
                return DbCommandConfigBuilder.GetTransactionConfig(DbConnection.MissingSproc2);
            }
        }

        public sealed class AddSimple : DbMethod<SimpleDbParamsModel<int, string>>
        {
            internal override Tools.Validator GetValidator()
            {
                Tools.Validator validator = new Tools.Validator(){
                        new Rules.Required(Selector(model => model.Param1)),
                        new Rules.GreaterThanOrEqual(Selector(model => model.Param1), 123),
                        new Rules.MinLength(Selector(model => model.Param2), true, 10),
                        new Rules.CustomTestRule(Selector(model => model.Param2))
                    };
                return validator;
            }

            protected override IDbCommandConfig GetConfigCore()
            {
                return Tools.Builder.GetTransactionConfig(
                    DbConnection.AddSimpleData,
                    new Tools.DbParams()
                    {
                        { "Count", Tools.DbParam.Int32(model => model.Param1)},
                        { "Comment", Tools.DbParam.String(model => model.Param2)}
                    },
                    GetValidator()
                );
            }

        }

    }
}
