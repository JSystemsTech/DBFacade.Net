﻿using DomainFacade.DataLayer.CommandConfig;
using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.SampleDomainFacade.DbMethods;
using DomainFacade.Utils;
using System;
using System.Linq.Expressions;

namespace Facade_Sabdbox_Run_Environment.TestFacade.DbMethods
{
    public abstract class CustomRule<DbParams>: DbMethodUtils<DbParams>.Rule where DbParams : IDbParamsModel
    {
        public static int test = 6;
        public CustomRule(Selector<DbParams> selector) : base(selector) { }
        public CustomRule(Selector<DbParams> selector, bool isNullable) : base(selector, isNullable) { }

        public class CustomTestRule : CustomRule<DbParams>
        {
            public CustomTestRule(Selector<DbParams> selector) : base(selector) { }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return "I failed";
            }

            protected override bool ValidateRule()
            {
                return true;
            }
        }
    }
    public abstract partial class TestDbMethods : DbManifest
    {
        public abstract class DbMethod<T> : TestDbMethods where T : IDbParamsModel
        {
            internal class Tools : DbMethodUtils<T> { }
            internal abstract class CustomRules : CustomRule<T> {
                public CustomRules(Selector<T> selector) : base(selector) { }
                public CustomRules(Selector<T> selector, bool isNullable) : base(selector, isNullable) { }
            }
            internal virtual Tools.Validator GetValidator() { return default(Tools.Validator); }
            internal static Selector<T> Selector(Expression<Func<T, object>> selectorExpression) {
                return Tools.Selector(selectorExpression);
            }
        }
        public sealed class GetAllSimple : TestDbMethods
        {            
            protected override IDbCommandConfig GetConfigCore()
            {
                return DbCommandConfigBuilder.GetFetchConfig(TestDbConnection.GetAllSimpleData);
            }
        }
        
        public sealed class GetAllMore : TestDbMethods
        {
            protected override IDbCommandConfig GetConfigCore()
            {
                return DbCommandConfigBuilder.GetFetchConfig(TestDbConnection.GetAllMoreData);
            }
        }
        public sealed class MissingSproc : TestDbMethods
        {
            protected override IDbCommandConfig GetConfigCore()
            {
                return DbCommandConfigBuilder.GetTransactionConfig(TestDbConnection.MissingSproc2);
            }
        }

        public sealed class AddSimple : DbMethod<SimpleDbParamsModel<int, string>>
        {
            internal override Tools.Validator GetValidator()
            {
                Tools.Validator validator = new Tools.Validator(){
                        new Tools.Rule.Required(Selector(model => model.Param1)),
                        new Tools.Rule.GreaterThanOrEqual(Selector(model => model.Param1), 123),
                        new Tools.Rule.MinLength(Selector(model => model.Param2), true, 10),
                        new CustomRules.CustomTestRule(Selector(model => model.Param2))
                    };                
                return validator;
            }

            protected override IDbCommandConfig GetConfigCore()
            {
                return Tools.Builder.GetTransactionConfig(
                    TestDbConnection.AddSimpleData,
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
