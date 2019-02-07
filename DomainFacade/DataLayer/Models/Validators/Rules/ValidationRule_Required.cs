using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DomainFacade.DataLayer.Models.Validators.Rules
{
    public abstract partial class ValidationRule<U>
        where U : IDbParamsModel
    {
        public class Required : ValidationRule<U>
        {
            public Required(Expression<Func<U, object>> selector) : base(selector) { }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is required.";
            }

            protected override bool ValidateRule()
            {
                return ParamsValue != null;
            }
        }
    }
}
