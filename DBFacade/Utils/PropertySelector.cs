using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DBFacade.Utils
{    
    internal sealed class PropertySelector<T>
    {
        public static string GetSelectorNameValue(string name, string type) => $"{name} ({type} Value)".Trim();
        public static string GetPropertyName<TOut>(Expression<Func<T, TOut>> expression)
        {
            if(expression.Body.NodeType == ExpressionType.Call)
            {
                MethodInfo methodInfo = ((MethodCallExpression)expression.Body).Method;
                return GetSelectorNameValue(methodInfo.Name, "Method");
            }
            else if (expression.Body.NodeType == ExpressionType.Constant)
            {
                return GetSelectorNameValue(string.Empty, "Constant");
            }
            MemberExpression memberExpression = ExtractMemberExpression(expression.Body);

            if (memberExpression == null)
            {
                throw new ArgumentException("Selector must be member access expression", "selector");
            }else if (memberExpression.Member.DeclaringType == null)
            {
                throw new InvalidOperationException("Property does not have declaring type");
            }
            string propType = typeof(T).GetProperty(memberExpression.Member.Name) == null ? "Field" : "Property";
            return GetSelectorNameValue(memberExpression.Member.Name, propType);
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
            }else if (expression.NodeType == ExpressionType.Convert)
            {
                var operand = ((UnaryExpression)expression).Operand;
                return ExtractMemberExpression(operand);
            }

            return null;
        }
    }
}
