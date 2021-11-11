using DbFacade.DataLayer.Models.Validators;
using System;
using System.Threading.Tasks;

namespace DbFacade.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ValidatorFactory
    {
        /// <summary>
        /// Creates the specified validator initializer.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        public static IValidator<TDbParams> Create<TDbParams>(Action<IValidator<TDbParams>> validatorInitializer = null)
        => Validator<TDbParams>.Create(validatorInitializer);
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        public static async Task<IValidator<TDbParams>> CreateAsync<TDbParams>(Func<IValidator<TDbParams>, Task> validatorInitializer = null)
        => await Validator<TDbParams>.CreateAsync(validatorInitializer);
    }
}
