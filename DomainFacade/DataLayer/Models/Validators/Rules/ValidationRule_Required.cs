using DomainFacade.Utils;

namespace DomainFacade.DataLayer.Models.Validators.Rules
{
    public abstract partial class ValidationRule<DbParams>
        where DbParams : IDbParamsModel
    {
        public class Required : ValidationRule<DbParams>
        {
            public Required(Selector<DbParams> selector) : base(selector) { }
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
