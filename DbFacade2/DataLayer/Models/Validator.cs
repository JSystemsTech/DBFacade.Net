using System;
using System.Collections.Generic;

namespace DbFacade.DataLayer.Models
{
    /// <summary>
    ///   <br />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Validator<T> where T : class
    {
        private readonly List<ValidationRule> Rules;
        internal Validator()
        {
            Rules = new List<ValidationRule>();
        }
        /// <summary>Adds the specified validate.</summary>
        /// <param name="validate">The validate.</param>
        /// <param name="errorMessage">The error message.</param>
        public void Add(Func<T, bool> validate, string errorMessage)
            => Rules.Add(new ValidationRule(validate, errorMessage));
        internal bool Validate(T value, out string[] errorMessages)
        {
            bool isValid = true;
            List<string> errors = new List<string>();
            foreach (var rule in Rules)
            {
                if (!rule.IsValid(value, out string errorMessage))
                {
                    errors.Add(errorMessage);
                    isValid = false;
                }
            }
            errorMessages = errors.ToArray();
            return isValid;
        }
        private class ValidationRule
        {
            private readonly string ErrorMessage;
            private Func<T, bool> Validate;
            internal ValidationRule(Func<T, bool> validate, string errorMessage)
            {
                ErrorMessage = errorMessage;
                Validate = validate;
            }
            internal bool IsValid(T value, out string errorMessage)
            {
                bool isValid = Validate(value);
                errorMessage = isValid ? null : ErrorMessage;
                return isValid;
            }
        }
    }
}
