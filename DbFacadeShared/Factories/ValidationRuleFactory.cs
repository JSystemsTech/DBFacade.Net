using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Validators.Rules;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DbFacade.Factories
{
    
    public sealed class ValidationRuleFactory<TDbParams>
    {

        public static async Task<ValidationRuleFactory<TDbParams>> CreateFactoryAsync()
        {
            ValidationRuleFactory<TDbParams> rules = new ValidationRuleFactory<TDbParams>();
            await Task.CompletedTask;
            return rules;
        }
        #region Compare        
        public IValidationRule<TDbParams> Equals<T>(Func<TDbParams, T> selector, T compareValue)
       => ValidationRule<TDbParams>.CompareRule<T>.CreateIsEqual(selector, compareValue);
        public IValidationRule<TDbParams> NotEqualTo<T>(Func<TDbParams, T> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsNotEqual(selector, compareValue);
        public IValidationRule<TDbParams> GreaterThan<T>(Func<TDbParams, T> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsGreaterThan(selector, compareValue);
        public IValidationRule<TDbParams> GreaterThanOrEqual<T>(Func<TDbParams, T> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsGreaterThanOrEqualTo(selector, compareValue);
        public IValidationRule<TDbParams> LessThan<T>(Func<TDbParams, T> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsLessThan(selector, compareValue);
        public IValidationRule<TDbParams> LessThanOrEqual<T>(Func<TDbParams, T> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsLessThanOrEqualTo(selector, compareValue);


        public IValidationRule<TDbParams> Equals<T>(Func<TDbParams, string> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsEqual(selector, compareValue);
        public IValidationRule<TDbParams> NotEqualTo<T>(Func<TDbParams, string> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsNotEqual(selector, compareValue);
        public IValidationRule<TDbParams> GreaterThan<T>(Func<TDbParams, string> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsGreaterThan(selector, compareValue);
        public IValidationRule<TDbParams> GreaterThanOrEqual<T>(Func<TDbParams, string> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsGreaterThanOrEqualTo(selector, compareValue);
        public IValidationRule<TDbParams> LessThan<T>(Func<TDbParams, string> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsLessThan(selector, compareValue);
        public IValidationRule<TDbParams> LessThanOrEqual<T>(Func<TDbParams, string> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsLessThanOrEqualTo(selector, compareValue);


        public async Task<IValidationRule<TDbParams>> EqualsAsync<T>(Func<TDbParams, T> selector, T compareValue)
        => await ValidationRule<TDbParams>.CompareRule<T>.CreateIsEqualAsync(selector, compareValue);
        public async Task<IValidationRule<TDbParams>> NotEqualToAsync<T>(Func<TDbParams, T> selector, T compareValue)
        => await ValidationRule<TDbParams>.CompareRule<T>.CreateIsNotEqualAsync(selector, compareValue);
        public async Task<IValidationRule<TDbParams>> GreaterThanAsync<T>(Func<TDbParams, T> selector, T compareValue)
        => await ValidationRule<TDbParams>.CompareRule<T>.CreateIsGreaterThanAsync(selector, compareValue);
        public async Task<IValidationRule<TDbParams>> GreaterThanOrEqualAsync<T>(Func<TDbParams, T> selector, T compareValue)
        => await ValidationRule<TDbParams>.CompareRule<T>.CreateIsGreaterThanOrEqualToAsync(selector, compareValue);
        public async Task<IValidationRule<TDbParams>> LessThanAsync<T>(Func<TDbParams, T> selector, T compareValue)
        => await ValidationRule<TDbParams>.CompareRule<T>.CreateIsLessThanAsync(selector, compareValue);
        public async Task<IValidationRule<TDbParams>> LessThanOrEqualAsync<T>(Func<TDbParams, T> selector, T compareValue)
        => await ValidationRule<TDbParams>.CompareRule<T>.CreateIsLessThanOrEqualToAsync(selector, compareValue);



        public async Task<IValidationRule<TDbParams>> EqualsAsync<T>(Func<TDbParams, string> selector, T compareValue)
        => await ValidationRule<TDbParams>.CompareRule<T>.CreateIsEqualAsync(selector, compareValue);
        public async Task<IValidationRule<TDbParams>> NotEqualToAsync<T>(Func<TDbParams, string> selector, T compareValue)
        => await ValidationRule<TDbParams>.CompareRule<T>.CreateIsNotEqualAsync(selector, compareValue);
        public async Task<IValidationRule<TDbParams>> GreaterThanAsync<T>(Func<TDbParams, string> selector, T compareValue)
        => await ValidationRule<TDbParams>.CompareRule<T>.CreateIsGreaterThanAsync(selector, compareValue);
        public async Task<IValidationRule<TDbParams>> GreaterThanOrEqualAsync<T>(Func<TDbParams, string> selector, T compareValue)
        => await ValidationRule<TDbParams>.CompareRule<T>.CreateIsGreaterThanOrEqualToAsync(selector, compareValue);
        public async Task<IValidationRule<TDbParams>> LessThanAsync<T>(Func<TDbParams, string> selector, T compareValue)
        => await ValidationRule<TDbParams>.CompareRule<T>.CreateIsLessThanAsync(selector, compareValue);
        public async Task<IValidationRule<TDbParams>> LessThanOrEqualAsync<T>(Func<TDbParams, string> selector, T compareValue)
        => await ValidationRule<TDbParams>.CompareRule<T>.CreateIsLessThanOrEqualToAsync(selector, compareValue);


        #region DateTime Equals
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public IValidationRule<TDbParams> IsOn(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, compareValue, CultureInfo.InvariantCulture);
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, compareValue, cultureInfo);
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, compareValue, cultureInfo, dateTimeStyles);
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);

        public async Task<IValidationRule<TDbParams>> IsOnAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsEqualAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public async Task<IValidationRule<TDbParams>> IsOnAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsEqualAsync(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public async Task<IValidationRule<TDbParams>> IsOnAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsEqualAsync(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public async Task<IValidationRule<TDbParams>> IsOnAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsEqualAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public async Task<IValidationRule<TDbParams>> IsOnAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsEqualAsync(selector, compareValue, CultureInfo.InvariantCulture);
        public async Task<IValidationRule<TDbParams>> IsOnAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsEqualAsync(selector, compareValue, cultureInfo);
        public async Task<IValidationRule<TDbParams>> IsOnAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsEqualAsync(selector, compareValue, cultureInfo, dateTimeStyles);
        public async Task<IValidationRule<TDbParams>> IsOnAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsEqualAsync(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);
        #endregion

        #region DateTime NotEqualTo
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, compareValue, CultureInfo.InvariantCulture);
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, compareValue, cultureInfo);
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, compareValue, cultureInfo, dateTimeStyles);
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);

        public async Task<IValidationRule<TDbParams>> IsNotOnAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqualAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public async Task<IValidationRule<TDbParams>> IsNotOnAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqualAsync(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public async Task<IValidationRule<TDbParams>> IsNotOnAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqualAsync(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public async Task<IValidationRule<TDbParams>> IsNotOnAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqualAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public async Task<IValidationRule<TDbParams>> IsNotOnAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqualAsync(selector, compareValue, CultureInfo.InvariantCulture);
        public async Task<IValidationRule<TDbParams>> IsNotOnAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqualAsync(selector, compareValue, cultureInfo);
        public async Task<IValidationRule<TDbParams>> IsNotOnAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqualAsync(selector, compareValue, cultureInfo, dateTimeStyles);
        public async Task<IValidationRule<TDbParams>> IsNotOnAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqualAsync(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);

        #endregion

        #region DateTime After

        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, compareValue, CultureInfo.InvariantCulture);
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, compareValue, cultureInfo);
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, compareValue, cultureInfo, dateTimeStyles);
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);

        public async Task<IValidationRule<TDbParams>> IsAfterAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsAfterAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public async Task<IValidationRule<TDbParams>> IsAfterAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsAfterAsync(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public async Task<IValidationRule<TDbParams>> IsAfterAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsAfterAsync(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public async Task<IValidationRule<TDbParams>> IsAfterAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsAfterAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public async Task<IValidationRule<TDbParams>> IsAfterAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsAfterAsync(selector, compareValue, CultureInfo.InvariantCulture);
        public async Task<IValidationRule<TDbParams>> IsAfterAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsAfterAsync(selector, compareValue, cultureInfo);
        public async Task<IValidationRule<TDbParams>> IsAfterAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsAfterAsync(selector, compareValue, cultureInfo, dateTimeStyles);
        public async Task<IValidationRule<TDbParams>> IsAfterAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsAfterAsync(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);
        #endregion

        #region DateTime OnOrAfter
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public IValidationRule<TDbParams> OnOrAfter(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, compareValue, CultureInfo.InvariantCulture);
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, compareValue, cultureInfo);
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, compareValue, cultureInfo, dateTimeStyles);
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);


        public async Task<IValidationRule<TDbParams>> IsOnOrAfterAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfterAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public async Task<IValidationRule<TDbParams>> IsOnOrAfterAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfterAsync(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public async Task<IValidationRule<TDbParams>> IsOnOrAfterAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfterAsync(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public async Task<IValidationRule<TDbParams>> IsOnOrAfterAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfterAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public async Task<IValidationRule<TDbParams>> IsOnOrAfterAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfterAsync(selector, compareValue, CultureInfo.InvariantCulture);
        public async Task<IValidationRule<TDbParams>> IsOnOrAfterAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfterAsync(selector, compareValue, cultureInfo);
        public async Task<IValidationRule<TDbParams>> IsOnOrAfterAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfterAsync(selector, compareValue, cultureInfo, dateTimeStyles);
        public async Task<IValidationRule<TDbParams>> IsOnOrAfterAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfterAsync(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);
        #endregion

        #region DateTime IsBefore
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, compareValue, CultureInfo.InvariantCulture);
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, compareValue, cultureInfo);
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, compareValue, cultureInfo, dateTimeStyles);
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);

        public async Task<IValidationRule<TDbParams>> IsBeforeAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsBeforeAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public async Task<IValidationRule<TDbParams>> IsBeforeAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsBeforeAsync(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public async Task<IValidationRule<TDbParams>> IsBeforeAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsBeforeAsync(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public async Task<IValidationRule<TDbParams>> IsBeforeAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsBeforeAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public async Task<IValidationRule<TDbParams>> BeforeAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsBeforeAsync(selector, compareValue, CultureInfo.InvariantCulture);
        public async Task<IValidationRule<TDbParams>> IsBeforeAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsBeforeAsync(selector, compareValue, cultureInfo);
        public async Task<IValidationRule<TDbParams>> IsBeforeAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsBeforeAsync(selector, compareValue, cultureInfo, dateTimeStyles);
        public async Task<IValidationRule<TDbParams>> IsBeforeAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsBeforeAsync(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);
        #endregion

        #region DateTime IsOnOrBefore
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, compareValue, CultureInfo.InvariantCulture);
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, compareValue, cultureInfo);
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, compareValue, cultureInfo, dateTimeStyles);
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);


        public async Task<IValidationRule<TDbParams>> IsOnOrBeforeAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBeforeAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public async Task<IValidationRule<TDbParams>> IsOnOrBeforeAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBeforeAsync(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public async Task<IValidationRule<TDbParams>> IsOnOrBeforeAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBeforeAsync(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public async Task<IValidationRule<TDbParams>> IsOnOrBeforeAsync(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBeforeAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public async Task<IValidationRule<TDbParams>> IsOnOrBeforeAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBeforeAsync(selector, compareValue, CultureInfo.InvariantCulture);
        public async Task<IValidationRule<TDbParams>> IsOnOrBeforeAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBeforeAsync(selector, compareValue, cultureInfo);
        public async Task<IValidationRule<TDbParams>> IsOnOrBeforeAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBeforeAsync(selector, compareValue, cultureInfo, dateTimeStyles);
        public async Task<IValidationRule<TDbParams>> IsOnOrBeforeAsync(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => await ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBeforeAsync(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);
        #endregion
        #endregion

        #region DateTime


        public IValidationRule<TDbParams> IsDateTime(Func<TDbParams, string> selector,
            string dateFormat = null, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.IsDateTimeRule(selector, dateFormat, isNullable);
        }
        public IValidationRule<TDbParams> IsDateTime(Func<TDbParams, string> selector,
            string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.IsDateTimeRule(selector, dateFormat, isNullable, cultureInfo);
        }
        public IValidationRule<TDbParams> IsDateTime(Func<TDbParams, string> selector,
            string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.IsDateTimeRule(selector, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        }
        public IValidationRule<TDbParams> IsDateTime(Func<TDbParams, string> selector,
            string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.IsDateTimeRule(selector, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);
        }

        public async Task<IValidationRule<TDbParams>> IsDateTimeAsync(Func<TDbParams, string> selector,
            string dateFormat = null, bool isNullable = false)
        => await ValidationRule<TDbParams>.IsDateTimeRule.CreateAsync(selector, dateFormat, isNullable);
        public async Task<IValidationRule<TDbParams>> IsDateTimeAsync(Func<TDbParams, string> selector,
            string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => await ValidationRule<TDbParams>.IsDateTimeRule.CreateAsync(selector, dateFormat, isNullable, cultureInfo);
        public async Task<IValidationRule<TDbParams>> IsDateTimeAsync(Func<TDbParams, string> selector,
            string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await ValidationRule<TDbParams>.IsDateTimeRule.CreateAsync(selector, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public async Task<IValidationRule<TDbParams>> IsDateTimeAsync(Func<TDbParams, string> selector,
            string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await ValidationRule<TDbParams>.IsDateTimeRule.CreateAsync(selector, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        #endregion

        #region Delegagte
        public IValidationRule<TDbParams> Delegate<T>(Func<TDbParams, T> selector, Func<T, bool> handler)
        => new ValidationRule<TDbParams>.DelegateRule<T>(selector, handler);

        public async Task<IValidationRule<TDbParams>> DelegateAsync<T>(Func<TDbParams, T> selector,
            Func<T, Task<bool>> handler)
        => await ValidationRule<TDbParams>.DelegateRule<T>.CreateAsync(selector, handler);

        public IValidationRule<TDbParams> Delegate(Func<TDbParams, bool> handler, string errorMessage)
        => new ValidationRule<TDbParams>.DelegateRule(handler, errorMessage);

        public async Task<IValidationRule<TDbParams>> DelegateAsync(Func<TDbParams, Task<bool>> handler, string errorMessage)
        => await ValidationRule<TDbParams>.DelegateRule.CreateAsync(handler, errorMessage);
        
        #endregion

        #region Numeric
        public IValidationRule<TDbParams> IsNumber(Func<TDbParams, string> selector,
            bool isNullable = false)
        =>  new ValidationRule<TDbParams>.IsNumeric(selector, isNullable);
        
        public async Task<IValidationRule<TDbParams>> IsNumberAsync(Func<TDbParams, string> selector,
            bool isNullable = false)
        => await ValidationRule<TDbParams>.IsNumeric.CreateAsync(selector, isNullable);

        #endregion

        #region Required
        public IValidationRule<TDbParams> Required(Func<TDbParams, object> selector)
        => new ValidationRule<TDbParams>.RequiredRule(selector);

        public async Task<IValidationRule<TDbParams>> RequiredAsync(Func<TDbParams, object> selector)
        => await ValidationRule<TDbParams>.RequiredRule.CreateAsync(selector);
        #endregion

        #region String
        public IValidationRule<TDbParams> Match(Func<TDbParams, string> selector,
            string regexMatchStr, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.MatchRule(selector, regexMatchStr, isNullable);
        }

        public IValidationRule<TDbParams> Match(Func<TDbParams, string> selector,
            string regexMatchStr, RegexOptions options, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.MatchRule(selector, regexMatchStr, options, isNullable);
        }
        public async Task<IValidationRule<TDbParams>> MatchAsync(Func<TDbParams, string> selector,
            string regexMatchStr, bool isNullable = false)
        => await ValidationRule<TDbParams>.MatchRule.CreateAsync(selector, regexMatchStr, isNullable);
        public async Task<IValidationRule<TDbParams>> MatchAsync(Func<TDbParams, string> selector,
            string regexMatchStr, RegexOptions options, bool isNullable = false)
        => await ValidationRule<TDbParams>.MatchRule.CreateAsync(selector, regexMatchStr, options, isNullable);


        public IValidationRule<TDbParams> IsNDigitString(Func<TDbParams, string> selector,
            int length, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.IsNDigitStringRule(selector, length, isNullable);
        }
        public async Task<IValidationRule<TDbParams>> IsNDigitStringAsync(Func<TDbParams, string> selector,
            int length, bool isNullable = false)
        => await ValidationRule<TDbParams>.IsNDigitStringRule.CreateAsync(selector, length, isNullable);


        public IValidationRule<TDbParams> IsSocialSecurityNumber(Func<TDbParams, string> selector,
            bool allowDashes = true, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.IsSocialSecurityNumberRule(selector, allowDashes, isNullable);
        }
        public async Task<IValidationRule<TDbParams>> IsSocialSecurityNumberAsync(Func<TDbParams, string> selector,
            bool allowDashes = true, bool isNullable = false)
        => await ValidationRule<TDbParams>.IsSocialSecurityNumberRule.CreateAsync(selector, allowDashes, isNullable);

        public IValidationRule<TDbParams> IsNullOrEmpty(Func<TDbParams, string> selector)
        {
            return new ValidationRule<TDbParams>.IsNullOrEmptyRule(selector);
        }
        public async Task<IValidationRule<TDbParams>> IsNullOrEmptyAsync(Func<TDbParams, string> selector)
        => await ValidationRule<TDbParams>.IsNullOrEmptyRule.CreateAsync(selector);

        public IValidationRule<TDbParams> IsNotNullOrEmpty(Func<TDbParams, string> selector)
        {
            return new ValidationRule<TDbParams>.IsNotNullOrEmptyRule(selector);
        }
        public async Task<IValidationRule<TDbParams>> IsNotNullOrEmptyAsync(Func<TDbParams, string> selector)
        => await ValidationRule<TDbParams>.IsNotNullOrEmptyRule.CreateAsync(selector);

        public IValidationRule<TDbParams> IsNullOrWhiteSpace(Func<TDbParams, string> selector)
        {
            return new ValidationRule<TDbParams>.IsNullOrWhiteSpaceRule(selector);
        }
        public async Task<IValidationRule<TDbParams>> IsNullOrWhiteSpaceAsync(Func<TDbParams, string> selector)
        => await ValidationRule<TDbParams>.IsNullOrWhiteSpaceRule.CreateAsync(selector);

        public IValidationRule<TDbParams> IsNotNullOrWhiteSpace(Func<TDbParams, string> selector)
        {
            return new ValidationRule<TDbParams>.IsNotNullOrWhiteSpaceRule(selector);
        }
        public async Task<IValidationRule<TDbParams>> IsNotNullOrWhiteSpaceAsync(Func<TDbParams, string> selector)
        => await ValidationRule<TDbParams>.IsNotNullOrWhiteSpaceRule.CreateAsync(selector);

        public IValidationRule<TDbParams> LengthEquals(Func<TDbParams, string> selector, int limit,
            bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.LengthEqualsRule(selector, limit, isNullable);
        }
        public async Task<IValidationRule<TDbParams>> LengthEqualsAsync(Func<TDbParams, string> selector, int limit,
            bool isNullable = false)
        => await ValidationRule<TDbParams>.LengthEqualsRule.CreateAsync(selector, limit, isNullable);

        public IValidationRule<TDbParams> MinLength(Func<TDbParams, string> selector, int limit,
            bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.MinLengthRule(selector, limit, isNullable);
        }
        public async Task<IValidationRule<TDbParams>> MinLengthAsync(Func<TDbParams, string> selector, int limit,
            bool isNullable = false)
        => await ValidationRule<TDbParams>.MinLengthRule.CreateAsync(selector, limit, isNullable);

        public IValidationRule<TDbParams> MaxLength(Func<TDbParams, string> selector, int limit,
            bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.MaxLengthRule(selector, limit, isNullable);
        }
        public async Task<IValidationRule<TDbParams>> MaxLengthAsync(Func<TDbParams, string> selector, int limit,
            bool isNullable = false)
        => await ValidationRule<TDbParams>.MaxLengthRule.CreateAsync(selector, limit, isNullable);

        public IValidationRule<TDbParams> Email(Func<TDbParams, string> selector,
            bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.EmailRule(selector, isNullable);
        }

        public IValidationRule<TDbParams> Email(Func<TDbParams, string> selector,
            IEnumerable<string> domains, EmailDomainMode mode, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.EmailRule(selector, domains, mode, isNullable);
        }

        public async Task<IValidationRule<TDbParams>> EmailAsync(Func<TDbParams, string> selector,
            bool isNullable = false)
        => await ValidationRule<TDbParams>.EmailRule.CreateAsync(selector, isNullable);

        public async Task<IValidationRule<TDbParams>> EmailAsync(Func<TDbParams, string> selector,
            IEnumerable<string> domains, EmailDomainMode mode, bool isNullable = false)
        => await ValidationRule<TDbParams>.EmailRule.CreateAsync(selector, domains, mode, isNullable);
        #endregion
    }
}
