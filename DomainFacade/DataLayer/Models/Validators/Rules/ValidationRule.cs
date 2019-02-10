using DomainFacade.Utils;
using System;
using System.Linq.Expressions;
using System.Reflection;
using static DomainFacade.DataLayer.Models.Validators.Rules.ValidationRuleResult;

namespace DomainFacade.DataLayer.Models.Validators.Rules
{
    public interface IValidationRule<DbParams> where DbParams : IDbParamsModel
    {
        ValidationRuleResult Validate(DbParams paramsModel);
    }
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
    public abstract partial class ValidationRule<DbParams>: IValidationRule<DbParams>
        where DbParams : IDbParamsModel
    {
        protected object ParamsValue { get; private set; }

        protected Func<DbParams, object> GetParamFunc { get; private set; }
        protected PropertyInfo PropInfo { get; private set; }
        protected bool IsNullable { get; private set; }

        public ValidationRule(Selector<DbParams> selector)
        {
            init(selector, false);
        }
        public ValidationRule(Selector<DbParams> selector, bool isNullable)
        {
            init(selector, isNullable);
        }
        private void init(Selector<DbParams> selector, bool isNullable)
        {
            GetParamFunc = PropertySelector<DbParams>.GetDelegate(selector.SelectorExpression);
            PropInfo = PropertySelector<DbParams>.GetPropertyInfo(selector.SelectorExpression);
            IsNullable = isNullable;
        }
        public ValidationRuleResult Validate(DbParams paramsModel)
        {
            ParamsValue = GetParamFunc(paramsModel);
            if ((IsNullable && ParamsValue == null) || ValidateRule())
            {
                return new ValidationRuleResult(paramsModel, PropInfo, null, ValidationStatus.PASS);
            }
            return new ValidationRuleResult(paramsModel, PropInfo, GetErrorMessage(), ValidationStatus.FAIL);
        }
        protected abstract bool ValidateRule();

        private string GetErrorMessage()
        {
            return GetErrorMessageCore(PropInfo.Name);
        }
        protected abstract string GetErrorMessageCore(string propertyName);               
    }
}
