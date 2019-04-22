using DBFacade.DataLayer.CommandConfig;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.Utils;

namespace DomainLayerSandbox1.Facades.Extensions
{
    public abstract class CustomValidationRules<DbParams> : DbMethodUtils<DbParams>.Rule where DbParams : IDbParamsModel
    {
        public static int test = 6;
        public CustomValidationRules(Selector<DbParams> selector) : base(selector) { }
        public CustomValidationRules(Selector<DbParams> selector, bool isNullable) : base(selector, isNullable) { }

        public class CustomTestRule : CustomValidationRules<DbParams>
        {
            public CustomTestRule(Selector<DbParams> selector) : base(selector) { }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return "I failed";
            }

            protected override bool ValidateRule()
            {
                return true;
            }
        }
    }
}
