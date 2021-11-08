using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Validators;
using System;
using System.Threading.Tasks;

namespace DbFacade.Factories
{
    public sealed class ValidatorFactory
    {
        public static IValidator<TDbParams> Create<TDbParams>(Action<IValidator<TDbParams>> validatorInitializer = null)
        => Validator<TDbParams>.Create(validatorInitializer);
        public static async Task<IValidator<TDbParams>> CreateAsync<TDbParams>(Func<IValidator<TDbParams>, Task> validatorInitializer = null)
        => await Validator<TDbParams>.CreateAsync(validatorInitializer);
    }
}
