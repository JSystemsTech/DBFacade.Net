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
    public abstract class DbMethodUtils<T>
        where T : IDbParamsModel
    {
        public abstract class Rule : ValidationRule<T>
        {
            public Rule(Selector<T> selector) : base(selector) { }
            public Rule(Selector<T> selector, bool isNullable) : base(selector, isNullable) { }
        }
        public abstract class DbParam : DbCommandParameterConfig<T>
        {
            protected DbParam(DbType dbType) : base(dbType) { }
        }
        public sealed class DbParams : DbCommandConfigParams<T> { }
        public sealed class Builder : DbCommandConfigBuilder<T> { }
        public sealed class Validator : Validator<T> { }
        public static Selector<T> Selector(Expression<Func<T, object>> selectorExpression)
        {
            return Selector<T>.Map(selectorExpression);
        }
    }
}
