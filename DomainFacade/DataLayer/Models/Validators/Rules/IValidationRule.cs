namespace DomainFacade.DataLayer.Models.Validators.Rules
{
    public interface IValidationRule<DbParams> where DbParams : IDbParamsModel
    {
        ValidationRuleResult Validate(DbParams paramsModel);
    }
}
