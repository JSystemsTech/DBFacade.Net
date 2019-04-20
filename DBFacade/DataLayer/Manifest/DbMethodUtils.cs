using DBFacade.DataLayer.CommandConfig;
using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using DBFacade.DataLayer.Models.Validators.Rules;
using DBFacade.Utils;
using System;
using System.Data;
using System.Linq.Expressions;

namespace DBFacade.DataLayer.Manifest
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public abstract class DbMethodUtils<TDbParams>
        where TDbParams : IDbParamsModel
    {
        /// <summary>
        /// 
        /// </summary>
        public abstract class Rule : ValidationRule<TDbParams>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Rule"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            public Rule(Selector<TDbParams> selector) : base(selector) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="Rule"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            public Rule(Selector<TDbParams> selector, bool isNullable) : base(selector, isNullable) { }
        }
        /// <summary>
        /// 
        /// </summary>
        public abstract class DbParam : DbCommandParameterConfig<TDbParams>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DbParam"/> class.
            /// </summary>
            /// <param name="dbType">Type of the database.</param>
            protected DbParam(DbType dbType) : base(dbType) { }
        }
        /// <summary>
        /// 
        /// </summary>
        public sealed class DbParams : DbCommandConfigParams<TDbParams> { }
        /// <summary>
        /// 
        /// </summary>
        public sealed class Builder : DbCommandConfigBuilder<TDbParams> { }
        /// <summary>
        /// 
        /// </summary>
        public sealed class Validator : Validator<TDbParams> { }
        /// <summary>
        /// Selectors the specified selector expression.
        /// </summary>
        /// <param name="selectorExpression">The selector expression.</param>
        /// <returns></returns>
        public static Selector<TDbParams> Selector(Expression<Func<TDbParams, object>> selectorExpression)
        {
            return Selector<TDbParams>.Map(selectorExpression);
        }
    }
}
