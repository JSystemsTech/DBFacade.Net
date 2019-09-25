using DBFacade.DataLayer.Models.Validators;
using DBFacade.DataLayer.Models.Validators.Rules;
using DbFacadeUnitTests.Models;

namespace DbFacadeUnitTests.Validator
{
    class UnitTestValidator : Validator<UnitTestDbParams> { }
    class UnitTestRules : ValidationRule<UnitTestDbParams> { }
}
