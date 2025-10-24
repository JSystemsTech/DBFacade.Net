using DbFacade;
using DbFacade.DataLayer.Models.Validators;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(DbFacadeConstants.UnitTestAssembly)]
namespace DbFacade.Factories
{
    internal sealed class ValidatorFactory
    {
        /// <summary>
        /// Creates the specified validator initializer.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        internal static Validator<TDbParams> Create<TDbParams>(Action<IValidator<TDbParams>> validatorInitializer)
        {
            var validator = new Validator<TDbParams>();
            if(validatorInitializer != null)
            {
                validatorInitializer(validator);
            }
            return validator;
        }
    }
}
