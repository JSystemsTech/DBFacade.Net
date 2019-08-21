using System.Threading.Tasks;

namespace DBFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
    public interface IValidationRule<DbParams> where DbParams : IDbParamsModel
    {
        /// <summary>
        /// Validates the specified parameters model.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        ValidationRuleResult Validate(DbParams paramsModel);
        /// <summary>
        /// Validates the asynchronous.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        Task<ValidationRuleResult> ValidateAsync(DbParams paramsModel);
    }
}
