using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the b parameters.</typeparam>
    public interface IValidationRule<TDbParams>
    {
        /// <summary>
        /// Validates the specified parameters model.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        IValidationRuleResult Validate(TDbParams paramsModel);

        /// <summary>
        /// Validates the asynchronous.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        Task<IValidationRuleResult> ValidateAsync(TDbParams paramsModel);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    internal interface IValidationRuleInternal<TDbParams> : IValidationRule<TDbParams>
    {
        /// <summary>
        /// Sets the name of the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        void SetParameterName(string parameterName);
    }
}