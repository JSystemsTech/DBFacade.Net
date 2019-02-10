using DomainFacade.DataLayer.CommandConfig;
using DomainFacade.DataLayer.CommandConfig.Parameters;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;
using DomainFacade.DataLayer.Models.Validators.Rules;
using DomainFacade.Utils;
using System;
using System.Data;
using System.Linq.Expressions;

namespace DomainFacade.DataLayer.Manifest
{
    public abstract class DbMethodUtils<TDbParams>
        where TDbParams : IDbParamsModel
    {
        public abstract class Rule : ValidationRule<TDbParams>
        {
            public Rule(Selector<TDbParams> selector) : base(selector) { }
            public Rule(Selector<TDbParams> selector, bool isNullable) : base(selector, isNullable) { }
        }
        public abstract class DbParam : DbCommandParameterConfig<TDbParams>
        {
            protected DbParam(DbType dbType) : base(dbType) { }
        }
        public sealed class DbParams : DbCommandConfigParams<TDbParams> { }
        public sealed class Builder : DbCommandConfigBuilder<TDbParams> { }
        public sealed class Validator : Validator<TDbParams> { }
        public static Selector<TDbParams> Selector(Expression<Func<TDbParams, object>> selectorExpression)
        {
            return Selector<TDbParams>.Map(selectorExpression);
        }
    }
}
