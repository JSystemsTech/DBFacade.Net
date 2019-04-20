using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DomainFacade.Utils
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal sealed class PropertySelector<T>
    {
        /// <summary>
        /// Gets the delegate.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        public static Func<T, object> GetDelegate(Expression<Func<T, object>> selector)
        {
            return selector.Compile();
        }
        /// <summary>
        /// Gets the property information.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Selector must be lambda expression - selector
        /// or
        /// Selector must be member access expression - selector
        /// </exception>
        /// <exception cref="InvalidOperationException">Property does not have declaring type</exception>
        public static PropertyInfo GetPropertyInfo(Expression<Func<T, object>> selector)
        {
            if (selector.NodeType != ExpressionType.Lambda)
            {
                throw new ArgumentException("Selector must be lambda expression", "selector");
            }

            var lambda = (LambdaExpression)selector;

            var memberExpression = ExtractMemberExpression(lambda.Body);

            if (memberExpression == null)
            {
                throw new ArgumentException("Selector must be member access expression", "selector");
            }

            if (memberExpression.Member.DeclaringType == null)
            {
                throw new InvalidOperationException("Property does not have declaring type");
            }

            return memberExpression.Member.DeclaringType.GetProperty(memberExpression.Member.Name);
        }

        /// <summary>
        /// Extracts the member expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        private static MemberExpression ExtractMemberExpression(Expression expression)
        {
            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                return ((MemberExpression)expression);
            }

            if (expression.NodeType == ExpressionType.Convert)
            {
                var operand = ((UnaryExpression)expression).Operand;
                return ExtractMemberExpression(operand);
            }

            return null;
        }
    }
}
