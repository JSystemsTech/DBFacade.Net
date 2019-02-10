using DomainFacade.DataLayer.Models;
using System;
using System.Linq.Expressions;

namespace DomainFacade.Utils
{
    public sealed class Selector<DbParams> where DbParams : IDbParamsModel
    {
        public Expression<Func<DbParams, object>> SelectorExpression { get; private set; }
        public static Selector<DbParams> Map(Expression<Func<DbParams, object>> selectorExpression)
        {
            return new Selector<DbParams>(selectorExpression);
        }
        private Selector(Expression<Func<DbParams, object>> selectorExpression)
        {
            SelectorExpression = selectorExpression;
        }
    }
}
