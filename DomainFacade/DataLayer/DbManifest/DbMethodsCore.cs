using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;
using DomainFacade.DataLayer.Models.Validators.Rules;
using DomainFacade.DataLayer.CommandConfig;
using System;
using System.Data;
using System.Linq.Expressions;
using DomainFacade.DataLayer.CommandConfig.Parameters;
using DomainFacade.Utils;

namespace DomainFacade.DataLayer.DbManifest
{
    public interface IDbMethod
    {
        IDbCommandConfig GetConfig();
        Type GetType();
    }
    
    public abstract class DbMethodsCore : IDbMethod
    {
        protected IDbCommandConfig Config { get; private set; }
        public IDbCommandConfig GetConfig()
        {
            if(Config == null)
            {
                Config = GetConfigCore();
            }
            return Config;
        }
        protected abstract IDbCommandConfig GetConfigCore();
    }
    public abstract class DbMethodTools<T>
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
        public static Selector<T> Map(Expression<Func<T, object>> selectorExpression)
        {
            return Selector<T>.Map(selectorExpression);
        }
    }

}
