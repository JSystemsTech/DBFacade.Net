using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TDbParams">The type of the b parameters.</typeparam>
    public interface IValidationRule<TDbParams>
    {
        /// <summary>
        ///     Validates the specified parameters model.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        IValidationRuleResult Validate(TDbParams paramsModel);

        /// <summary>
        ///     Validates the asynchronous.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        Task<IValidationRuleResult> ValidateAsync(TDbParams paramsModel);
    }
}