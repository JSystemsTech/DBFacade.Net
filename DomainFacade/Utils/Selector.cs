using DomainFacade.DataLayer.Models;
using System;
using System.Linq.Expressions;

namespace DomainFacade.Utils
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
    public sealed class Selector<DbParams> where DbParams : IDbParamsModel
    {
        /// <summary>
        /// Gets the selector expression.
        /// </summary>
        /// <value>
        /// The selector expression.
        /// </value>
        public Expression<Func<DbParams, object>> SelectorExpression { get; private set; }
        /// <summary>
        /// Maps the specified selector expression.
        /// </summary>
        /// <param name="selectorExpression">The selector expression.</param>
        /// <returns></returns>
        public static Selector<DbParams> Map(Expression<Func<DbParams, object>> selectorExpression)
        {
            return new Selector<DbParams>(selectorExpression);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Selector{DbParams}"/> class.
        /// </summary>
        /// <param name="selectorExpression">The selector expression.</param>
        private Selector(Expression<Func<DbParams, object>> selectorExpression)
        {
            SelectorExpression = selectorExpression;
        }
    }
}
