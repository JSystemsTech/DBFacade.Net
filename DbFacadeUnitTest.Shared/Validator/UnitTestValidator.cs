using DbFacade.DataLayer.Models.Validators;
using DbFacade.DataLayer.Models.Validators.Rules;
using DbFacadeUnitTests.Models;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.Validator
{
    class UnitTestValidator : Validator<UnitTestDbParams> { }
    class UnitTestRules : ValidationRule<UnitTestDbParams> { }
}
