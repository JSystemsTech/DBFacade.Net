using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbFacade.DataLayer.Models.Validators.Rules;
using DbFacade.Factories;

namespace DbFacade.DataLayer.Models.Validators
{
    /// <summary>
    /// </summary>
    public interface IValidationResult
    {
        bool IsValid { get; }
        IEnumerable<IValidationRuleResult> Errors { get; }
    }

    internal class ValidationResult : IValidationResult
    {
        public ValidationResult(IEnumerable<IValidationRuleResult> errors)
        {
            Errors = errors;
        }

        public IEnumerable<IValidationRuleResult> Errors { get; }

        public bool IsValid => !Errors.Any();

        public static ValidationResult PassingValidation = new ValidationResult(new List<ValidationRuleResult>());
    }
    public interface IValidator<TDbParams>
        where TDbParams : DbParamsModel
    {
        int Count { get; }
        ValidationRuleFactory<TDbParams> Rules { get; }
        void Add(IValidationRule<TDbParams> rule);
        Task AddAsync(IValidationRule<TDbParams> rule);
        IValidationResult Validate(TDbParams paramsModel);
        Task<IValidationResult> ValidateAsync(TDbParams paramsModel);
    }

    internal class Validator<TDbParams> : List<IValidationRule<TDbParams>>, IValidator<TDbParams>
        where TDbParams : DbParamsModel
    {
        public ValidationRuleFactory<TDbParams> Rules { get; private set; }
        public async Task AddAsync(IValidationRule<TDbParams> rule)
        {
            Add(rule);
            await Task.CompletedTask;
        }
        public static Validator<TDbParams> Create(Action<IValidator<TDbParams>> validatorInitializer = null)
        {
            Validator<TDbParams> validator = new Validator<TDbParams>();            
            validator.Rules = new ValidationRuleFactory<TDbParams>();
            Action<IValidator<TDbParams>> _validatorInitializer =
                validatorInitializer != null ? validatorInitializer : v => { };
            _validatorInitializer(validator);
            return validator;
        }
        public static async Task<Validator<TDbParams>> CreateAsync(Func<IValidator<TDbParams>, Task> validatorInitializer = null)
        {
            Validator<TDbParams> validator = new Validator<TDbParams>();
            validator.Rules = await ValidationRuleFactory<TDbParams>.CreateFactoryAsync();
            Func<IValidator<TDbParams>, Task> _validatorInitializer =
                validatorInitializer != null ? validatorInitializer : async v => { await Task.CompletedTask; };
            await _validatorInitializer(validator);           
            return validator;
        }

        public IValidationResult Validate(TDbParams paramsModel)
        => new ValidationResult(this.Select(rule => rule.Validate(paramsModel))
                .Where(result => result.Status == ValidationStatus.FAIL));


        public async Task<IValidationResult> ValidateAsync(TDbParams paramsModel)
        {
            var validationTasks = this.Select(rule => rule.ValidateAsync(paramsModel)).ToArray();
            await Task.WhenAll(validationTasks);
            return new ValidationResult(validationTasks.Where(task => task.Result.Status == ValidationStatus.FAIL)
                .Select(task => task.Result));
        }
    }
}