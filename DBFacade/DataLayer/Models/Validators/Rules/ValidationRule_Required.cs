using System;

namespace DBFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{DbParams}" />
    public abstract partial class ValidationRule<TDbParams>
        where TDbParams : IDbParamsModel
    {
        public static IValidationRule<TDbParams> Required(Func<TDbParams, object> selector) => new RequiredRule(selector);
        private class RequiredRule : ValidationRule<TDbParams>
        {
            public RequiredRule(Func<TDbParams, object> selector) : base(selector) { }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} is required.";
            }
            protected override bool ValidateRule()
            {
                return ParamsValue != null;
            }
        }
    }
}
