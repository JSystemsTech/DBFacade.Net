using DbFacade.Utils;
using DbFacadeShared.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TDbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{TDbParams}" />
    public abstract partial class ValidationRule<TDbParams>
        where TDbParams : IDbParamsModel
    {
        

        public static IValidationRule<TDbParams> Equals<T>(Expression<Func<TDbParams, T>> selector, T compareValue)
        => CompareRule<T>.CreateIsEqual(selector, compareValue);
        public static IValidationRule<TDbParams> NotEqualTo<T>(Expression<Func<TDbParams, T>> selector, T compareValue)
        => CompareRule<T>.CreateIsNotEqual(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan<T>(Expression<Func<TDbParams, T>> selector, T compareValue)
        => CompareRule<T>.CreateIsGreaterThan(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual<T>(Expression<Func<TDbParams, T>> selector, T compareValue)
        => CompareRule<T>.CreateIsGreaterThanOrEqualTo(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan<T>(Expression<Func<TDbParams, T>> selector, T compareValue)
        => CompareRule<T>.CreateIsLessThan(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual<T>(Expression<Func<TDbParams, T>> selector, T compareValue)
        => CompareRule<T>.CreateIsLessThanOrEqualTo(selector, compareValue);


        public static IValidationRule<TDbParams> Equals<T>(Expression<Func<TDbParams, string>> selector, T compareValue)
        => CompareRule<T>.CreateIsEqual(selector, compareValue);
        public static IValidationRule<TDbParams> NotEqualTo<T>(Expression<Func<TDbParams, string>> selector, T compareValue)
        => CompareRule<T>.CreateIsNotEqual(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan<T>(Expression<Func<TDbParams, string>> selector, T compareValue)
        => CompareRule<T>.CreateIsGreaterThan(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual<T>(Expression<Func<TDbParams, string>> selector, T compareValue)
        => CompareRule<T>.CreateIsGreaterThanOrEqualTo(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan<T>(Expression<Func<TDbParams, string>> selector, T compareValue)
        => CompareRule<T>.CreateIsLessThan(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual<T>(Expression<Func<TDbParams, string>> selector, T compareValue)
        => CompareRule<T>.CreateIsLessThanOrEqualTo(selector, compareValue);


        public static async Task<IValidationRule<TDbParams>> EqualsAsync<T>(Expression<Func<TDbParams, T>> selector, T compareValue)
        => await CompareRule<T>.CreateIsEqualAsync(selector, compareValue);
        public static async Task<IValidationRule<TDbParams>> NotEqualToAsync<T>(Expression<Func<TDbParams, T>> selector, T compareValue)            
        => await CompareRule<T>.CreateIsNotEqualAsync(selector, compareValue);
        public static async Task<IValidationRule<TDbParams>> GreaterThanAsync<T>(Expression<Func<TDbParams, T>> selector, T compareValue)        
        => await CompareRule<T>.CreateIsGreaterThanAsync(selector, compareValue);
        public static async Task<IValidationRule<TDbParams>> GreaterThanOrEqualAsync<T>(Expression<Func<TDbParams, T>> selector, T compareValue)       
        => await CompareRule<T>.CreateIsGreaterThanOrEqualToAsync(selector, compareValue);
        public static async Task<IValidationRule<TDbParams>> LessThanAsync<T>(Expression<Func<TDbParams, T>> selector, T compareValue)    
        => await CompareRule<T>.CreateIsLessThanAsync(selector, compareValue);
        public static async Task<IValidationRule<TDbParams>> LessThanOrEqualAsync<T>(Expression<Func<TDbParams, T>> selector, T compareValue)           
        => await CompareRule<T>.CreateIsLessThanOrEqualToAsync(selector, compareValue);



        public static async Task<IValidationRule<TDbParams>> EqualsAsync<T>(Expression<Func<TDbParams, string>> selector, T compareValue)
        => await CompareRule<T>.CreateIsEqualAsync(selector, compareValue);
        public static async Task<IValidationRule<TDbParams>> NotEqualToAsync<T>(Expression<Func<TDbParams, string>> selector, T compareValue)
        => await CompareRule<T>.CreateIsNotEqualAsync(selector, compareValue);
        public static async Task<IValidationRule<TDbParams>> GreaterThanAsync<T>(Expression<Func<TDbParams, string>> selector, T compareValue)
        => await CompareRule<T>.CreateIsGreaterThanAsync(selector, compareValue);
        public static async Task<IValidationRule<TDbParams>> GreaterThanOrEqualAsync<T>(Expression<Func<TDbParams, string>> selector, T compareValue)
        => await CompareRule<T>.CreateIsGreaterThanOrEqualToAsync(selector, compareValue);
        public static async Task<IValidationRule<TDbParams>> LessThanAsync<T>(Expression<Func<TDbParams, string>> selector, T compareValue)
        => await CompareRule<T>.CreateIsLessThanAsync(selector, compareValue);
        public static async Task<IValidationRule<TDbParams>> LessThanOrEqualAsync<T>(Expression<Func<TDbParams, string>> selector, T compareValue)
        => await CompareRule<T>.CreateIsLessThanOrEqualToAsync(selector, compareValue);


        #region DateTime Equals
        public static IValidationRule<TDbParams> IsOn(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => CompareDateRule.CreateIsEqual(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public static IValidationRule<TDbParams> IsOn(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => CompareDateRule.CreateIsEqual(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public static IValidationRule<TDbParams> IsOn(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => CompareDateRule.CreateIsEqual(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public static IValidationRule<TDbParams> IsOn(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => CompareDateRule.CreateIsEqual(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public static IValidationRule<TDbParams> IsOn(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue)
        => CompareDateRule.CreateIsEqual(selector, compareValue, CultureInfo.InvariantCulture);
        public static IValidationRule<TDbParams> IsOn(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo)
        => CompareDateRule.CreateIsEqual(selector, compareValue, cultureInfo);
        public static IValidationRule<TDbParams> IsOn(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => CompareDateRule.CreateIsEqual(selector, compareValue, cultureInfo, dateTimeStyles);
        public static IValidationRule<TDbParams> IsOn(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => CompareDateRule.CreateIsEqual(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);

        public static async Task<IValidationRule<TDbParams>> IsOnAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => await CompareDateRule.CreateIsEqualAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public static async Task<IValidationRule<TDbParams>> IsOnAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => await CompareDateRule.CreateIsEqualAsync(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public static async Task<IValidationRule<TDbParams>> IsOnAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await CompareDateRule.CreateIsEqualAsync(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public static async Task<IValidationRule<TDbParams>> IsOnAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await CompareDateRule.CreateIsEqualAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public static async Task<IValidationRule<TDbParams>> IsOnAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue)
        => await CompareDateRule.CreateIsEqualAsync(selector, compareValue, CultureInfo.InvariantCulture);
        public static async Task<IValidationRule<TDbParams>> IsOnAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo)
        => await CompareDateRule.CreateIsEqualAsync(selector, compareValue, cultureInfo);
        public static async Task<IValidationRule<TDbParams>> IsOnAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => await CompareDateRule.CreateIsEqualAsync(selector, compareValue, cultureInfo, dateTimeStyles);
        public static async Task<IValidationRule<TDbParams>> IsOnAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => await CompareDateRule.CreateIsEqualAsync(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);
        #endregion

        #region DateTime NotEqualTo
        public static IValidationRule<TDbParams> IsNotOn(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => CompareDateRule.CreateIsNotEqual(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public static IValidationRule<TDbParams> IsNotOn(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => CompareDateRule.CreateIsNotEqual(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public static IValidationRule<TDbParams> IsNotOn(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => CompareDateRule.CreateIsNotEqual(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public static IValidationRule<TDbParams> IsNotOn(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => CompareDateRule.CreateIsNotEqual(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public static IValidationRule<TDbParams> IsNotOn(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue)
        => CompareDateRule.CreateIsNotEqual(selector, compareValue, CultureInfo.InvariantCulture);
        public static IValidationRule<TDbParams> IsNotOn(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo)
        => CompareDateRule.CreateIsNotEqual(selector, compareValue, cultureInfo);
        public static IValidationRule<TDbParams> IsNotOn(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => CompareDateRule.CreateIsNotEqual(selector, compareValue, cultureInfo, dateTimeStyles);
        public static IValidationRule<TDbParams> IsNotOn(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => CompareDateRule.CreateIsNotEqual(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);

        public static async Task<IValidationRule<TDbParams>> IsNotOnAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => await CompareDateRule.CreateIsNotEqualAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public static async Task<IValidationRule<TDbParams>> IsNotOnAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => await CompareDateRule.CreateIsNotEqualAsync(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public static async Task<IValidationRule<TDbParams>> IsNotOnAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await CompareDateRule.CreateIsNotEqualAsync(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public static async Task<IValidationRule<TDbParams>> IsNotOnAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await CompareDateRule.CreateIsNotEqualAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public static async Task<IValidationRule<TDbParams>> IsNotOnAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue)
        => await CompareDateRule.CreateIsNotEqualAsync(selector, compareValue, CultureInfo.InvariantCulture);
        public static async Task<IValidationRule<TDbParams>> IsNotOnAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo)
        => await CompareDateRule.CreateIsNotEqualAsync(selector, compareValue, cultureInfo);
        public static async Task<IValidationRule<TDbParams>> IsNotOnAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => await CompareDateRule.CreateIsNotEqualAsync(selector, compareValue, cultureInfo, dateTimeStyles);
        public static async Task<IValidationRule<TDbParams>> IsNotOnAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => await CompareDateRule.CreateIsNotEqualAsync(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);

        #endregion

        #region DateTime After

        public static IValidationRule<TDbParams> IsAfter(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => CompareDateRule.CreateIsAfter(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public static IValidationRule<TDbParams> IsAfter(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => CompareDateRule.CreateIsAfter(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public static IValidationRule<TDbParams> IsAfter(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => CompareDateRule.CreateIsAfter(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public static IValidationRule<TDbParams> IsAfter(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => CompareDateRule.CreateIsAfter(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public static IValidationRule<TDbParams> IsAfter(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue)
        => CompareDateRule.CreateIsAfter(selector, compareValue, CultureInfo.InvariantCulture);
        public static IValidationRule<TDbParams> IsAfter(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo)
        => CompareDateRule.CreateIsAfter(selector, compareValue, cultureInfo);
        public static IValidationRule<TDbParams> IsAfter(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => CompareDateRule.CreateIsAfter(selector, compareValue, cultureInfo, dateTimeStyles);
        public static IValidationRule<TDbParams> IsAfter(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => CompareDateRule.CreateIsAfter(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);

        public static async Task<IValidationRule<TDbParams>> IsAfterAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => await CompareDateRule.CreateIsAfterAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public static async Task<IValidationRule<TDbParams>> IsAfterAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => await CompareDateRule.CreateIsAfterAsync(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public static async Task<IValidationRule<TDbParams>> IsAfterAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await CompareDateRule.CreateIsAfterAsync(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public static async Task<IValidationRule<TDbParams>> IsAfterAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await CompareDateRule.CreateIsAfterAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public static async Task<IValidationRule<TDbParams>> IsAfterAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue)
        => await CompareDateRule.CreateIsAfterAsync(selector, compareValue, CultureInfo.InvariantCulture);
        public static async Task<IValidationRule<TDbParams>> IsAfterAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo)
        => await CompareDateRule.CreateIsAfterAsync(selector, compareValue, cultureInfo);
        public static async Task<IValidationRule<TDbParams>> IsAfterAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => await CompareDateRule.CreateIsAfterAsync(selector, compareValue, cultureInfo, dateTimeStyles);
        public static async Task<IValidationRule<TDbParams>> IsAfterAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => await CompareDateRule.CreateIsAfterAsync(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);
        #endregion

        #region DateTime OnOrAfter
        public static IValidationRule<TDbParams> IsOnOrAfter(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => CompareDateRule.CreateIsOnOrAfter(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public static IValidationRule<TDbParams> IsOnOrAfter(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => CompareDateRule.CreateIsOnOrAfter(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public static IValidationRule<TDbParams> IsOnOrAfter(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => CompareDateRule.CreateIsOnOrAfter(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public static IValidationRule<TDbParams> OnOrAfter(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => CompareDateRule.CreateIsOnOrAfter(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public static IValidationRule<TDbParams> IsOnOrAfter(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue)
        => CompareDateRule.CreateIsOnOrAfter(selector, compareValue, CultureInfo.InvariantCulture);
        public static IValidationRule<TDbParams> IsOnOrAfter(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo)
        => CompareDateRule.CreateIsOnOrAfter(selector, compareValue, cultureInfo);
        public static IValidationRule<TDbParams> IsOnOrAfter(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => CompareDateRule.CreateIsOnOrAfter(selector, compareValue, cultureInfo, dateTimeStyles);
        public static IValidationRule<TDbParams> IsOnOrAfter(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => CompareDateRule.CreateIsOnOrAfter(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);


        public static async Task<IValidationRule<TDbParams>> IsOnOrAfterAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => await CompareDateRule.CreateIsOnOrAfterAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public static async Task<IValidationRule<TDbParams>> IsOnOrAfterAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => await CompareDateRule.CreateIsOnOrAfterAsync(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public static async Task<IValidationRule<TDbParams>> IsOnOrAfterAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await CompareDateRule.CreateIsOnOrAfterAsync(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public static async Task<IValidationRule<TDbParams>> IsOnOrAfterAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await CompareDateRule.CreateIsOnOrAfterAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public static async Task<IValidationRule<TDbParams>> IsOnOrAfterAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue)
        => await CompareDateRule.CreateIsOnOrAfterAsync(selector, compareValue, CultureInfo.InvariantCulture);
        public static async Task<IValidationRule<TDbParams>> IsOnOrAfterAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo)
        => await CompareDateRule.CreateIsOnOrAfterAsync(selector, compareValue, cultureInfo);
        public static async Task<IValidationRule<TDbParams>> IsOnOrAfterAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => await CompareDateRule.CreateIsOnOrAfterAsync(selector, compareValue, cultureInfo, dateTimeStyles);
        public static async Task<IValidationRule<TDbParams>> IsOnOrAfterAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => await CompareDateRule.CreateIsOnOrAfterAsync(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);
        #endregion

        #region DateTime IsBefore
        public static IValidationRule<TDbParams> IsBefore(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => CompareDateRule.CreateIsBefore(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public static IValidationRule<TDbParams> IsBefore(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => CompareDateRule.CreateIsBefore(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public static IValidationRule<TDbParams> IsBefore(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => CompareDateRule.CreateIsBefore(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public static IValidationRule<TDbParams> IsBefore(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => CompareDateRule.CreateIsBefore(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public static IValidationRule<TDbParams> IsBefore(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue)
        => CompareDateRule.CreateIsBefore(selector, compareValue, CultureInfo.InvariantCulture);
        public static IValidationRule<TDbParams> IsBefore(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo)
        => CompareDateRule.CreateIsBefore(selector, compareValue, cultureInfo);
        public static IValidationRule<TDbParams> IsBefore(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => CompareDateRule.CreateIsBefore(selector, compareValue, cultureInfo, dateTimeStyles);
        public static IValidationRule<TDbParams> IsBefore(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => CompareDateRule.CreateIsBefore(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);

        public static async Task<IValidationRule<TDbParams>> IsBeforeAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => await CompareDateRule.CreateIsBeforeAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public static async Task<IValidationRule<TDbParams>> IsBeforeAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => await CompareDateRule.CreateIsBeforeAsync(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public static async Task<IValidationRule<TDbParams>> IsBeforeAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await CompareDateRule.CreateIsBeforeAsync(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public static async Task<IValidationRule<TDbParams>> IsBeforeAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await CompareDateRule.CreateIsBeforeAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public static async Task<IValidationRule<TDbParams>> BeforeAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue)
        => await CompareDateRule.CreateIsBeforeAsync(selector, compareValue, CultureInfo.InvariantCulture);
        public static async Task<IValidationRule<TDbParams>> IsBeforeAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo)
        => await CompareDateRule.CreateIsBeforeAsync(selector, compareValue, cultureInfo);
        public static async Task<IValidationRule<TDbParams>> IsBeforeAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => await CompareDateRule.CreateIsBeforeAsync(selector, compareValue, cultureInfo, dateTimeStyles);
        public static async Task<IValidationRule<TDbParams>> IsBeforeAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => await CompareDateRule.CreateIsBeforeAsync(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);
        #endregion

        #region DateTime IsOnOrBefore
        public static IValidationRule<TDbParams> IsOnOrBefore(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => CompareDateRule.CreateIsOnOrBefore(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public static IValidationRule<TDbParams> IsOnOrBefore(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => CompareDateRule.CreateIsOnOrBefore(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public static IValidationRule<TDbParams> IsOnOrBefore(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => CompareDateRule.CreateIsOnOrBefore(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public static IValidationRule<TDbParams> IsOnOrBefore(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => CompareDateRule.CreateIsOnOrBefore(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public static IValidationRule<TDbParams> IsOnOrBefore(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue)
        => CompareDateRule.CreateIsOnOrBefore(selector, compareValue, CultureInfo.InvariantCulture);
        public static IValidationRule<TDbParams> IsOnOrBefore(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo)
        => CompareDateRule.CreateIsOnOrBefore(selector, compareValue, cultureInfo);
        public static IValidationRule<TDbParams> IsOnOrBefore(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => CompareDateRule.CreateIsOnOrBefore(selector, compareValue, cultureInfo, dateTimeStyles);
        public static IValidationRule<TDbParams> IsOnOrBefore(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => CompareDateRule.CreateIsOnOrBefore(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);


        public static async Task<IValidationRule<TDbParams>> IsOnOrBeforeAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => await CompareDateRule.CreateIsOnOrBeforeAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        public static async Task<IValidationRule<TDbParams>> IsOnOrBeforeAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => await CompareDateRule.CreateIsOnOrBeforeAsync(selector, compareValue, dateFormat, isNullable, cultureInfo);
        public static async Task<IValidationRule<TDbParams>> IsOnOrBeforeAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await CompareDateRule.CreateIsOnOrBeforeAsync(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        public static async Task<IValidationRule<TDbParams>> IsOnOrBeforeAsync(Expression<Func<TDbParams, string>> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => await CompareDateRule.CreateIsOnOrBeforeAsync(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        public static async Task<IValidationRule<TDbParams>> IsOnOrBeforeAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue)
        => await CompareDateRule.CreateIsOnOrBeforeAsync(selector, compareValue, CultureInfo.InvariantCulture);
        public static async Task<IValidationRule<TDbParams>> IsOnOrBeforeAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo)
        => await CompareDateRule.CreateIsOnOrBeforeAsync(selector, compareValue, cultureInfo);
        public static async Task<IValidationRule<TDbParams>> IsOnOrBeforeAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => await CompareDateRule.CreateIsOnOrBeforeAsync(selector, compareValue, cultureInfo, dateTimeStyles);
        public static async Task<IValidationRule<TDbParams>> IsOnOrBeforeAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => await CompareDateRule.CreateIsOnOrBeforeAsync(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);
        #endregion




        /// <summary>
        /// </summary>
        /// <seealso cref="Rules.IValidationRule{TDbParams}" />
        private class CompareRule<T> : ValidationRule<TDbParams>
        {

            protected CompareRule() { }
            private string CompareType { get; set; }
            protected T Limit { get; private set; } 

            protected Func<T, T, bool> Comaparitor { get; private set; }
            protected Func<T, T, Task<bool>> ComaparitorAsync { get; private set; }

            protected static IComparable GetComparableValue(T obj)
            {
                if (obj is IComparable comparableObj)
                {
                    return comparableObj;
                }
                Type t = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

                try
                {
                    object convertedObj = Convert.ChangeType(obj, t);
                    if (convertedObj is IComparable convertedConvertedObj)
                    {
                        return convertedConvertedObj;
                    }
                    return 0;
                }
                catch
                {
                    return 0;
                }
            }
            protected static async Task<IComparable> GetComparableValueAsync(T obj)
            {
                if (obj is IComparable comparableObj)
                {
                    await Task.CompletedTask;
                    return comparableObj;
                }
                Type t = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

                try
                {
                    object convertedObj = Convert.ChangeType(obj, t);
                    if (convertedObj is IComparable convertedConvertedObj)
                    {
                        await Task.CompletedTask;
                        return convertedConvertedObj;
                    }
                    await Task.CompletedTask;
                    return 0;
                }
                catch
                {
                    await Task.CompletedTask;
                    return 0;
                }
            }
            private static int Compare(T value, T limit)
            => GetComparableValue(value).CompareTo(GetComparableValue(limit));
            
            

            private static async Task<int> CompareAsync(T value, T limit)
            {
                IComparable iValue = await GetComparableValueAsync(value);
                IComparable iLimit = await GetComparableValueAsync(limit);
                int compareValue = iValue.CompareTo(iLimit);
                await Task.CompletedTask;
                return compareValue;
            }

            protected static Func<T,T, bool> IsEqualTo = (v, l) => Compare(v,l) == 0;
            protected static Func<T,T, bool> IsNotEqual = (v, l) => Compare(v, l) != 0;
            protected static Func<T,T, bool> IsGreatorThan = (v, l) => Compare(v, l) > 0;
            protected static Func<T,T, bool> IsGreaterThanOrEqualTo = (v, l) => Compare(v, l) >= 0;
            protected static Func<T,T, bool> IsLessThan = (v, l) => Compare(v, l) < 0;
            protected static Func<T,T, bool> IsLessThanOrEqualTo = (v, l) => Compare(v, l) <= 0;


            protected static Func<T, T, Task<bool>> IsEqualToAsync = async (v, l) => await CompareAsync(v, l) == 0;
            protected static Func<T, T, Task<bool>> IsNotEqualAsync = async (v, l) => await CompareAsync(v, l) != 0;
            protected static Func<T, T, Task<bool>> IsGreatorThanAsync = async (v, l) => await CompareAsync(v, l) > 0;
            protected static Func<T, T, Task<bool>> IsGreaterThanOrEqualToAsync = async (v, l) => await CompareAsync(v, l) >= 0;
            protected static Func<T, T, Task<bool>> IsLessThanAsync = async (v, l) => await CompareAsync(v, l) < 0;
            protected static Func<T, T, Task<bool>> IsLessThanOrEqualToAsync = async (v, l) => await CompareAsync(v, l) <= 0;


            private static CompareRule<T> Create(
                Expression<Func<TDbParams, T>> selector, 
                T limit,
                Func<T, T, bool> comaparitor,
                string compareType
                )
            {
                CompareRule<T> rule = new CompareRule<T>();
                rule.Init(limit, comaparitor, compareType);
                rule.Init(selector, GenericInstance.IsNullableType<T>());
                return rule;
            }
            private static CompareRule<T> Create(
                Expression<Func<TDbParams, string>> selector,
                T limit,
                Func<T, T, bool> comaparitor,
                string compareType
                )
            {
                CompareRule<T> rule = new CompareRule<T>();
                rule.Init(limit, comaparitor, compareType);
                rule.Init(selector, true);
                return rule;
            }

            protected void Init(
                T limit,
                Func<T, T, bool> comaparitor,
                string compareType
                )
            {
                Limit = limit;
                CompareType = compareType;
                Comaparitor = comaparitor;
            }
            
            
            public static CompareRule<T> CreateIsEqual(Expression<Func<TDbParams, T>> selector, T limit)
                => Create(selector, limit, IsEqualTo, "to be equal to");
            public static CompareRule<T> CreateIsNotEqual(Expression<Func<TDbParams, T>> selector, T limit)
                => Create(selector, limit, IsNotEqual, "to not be equal to");
            public static CompareRule<T> CreateIsGreaterThan(Expression<Func<TDbParams, T>> selector, T limit)
                => Create(selector, limit, IsGreatorThan, "to be greater than");
            public static CompareRule<T> CreateIsGreaterThanOrEqualTo(Expression<Func<TDbParams, T>> selector, T limit)
                => Create(selector, limit, IsGreaterThanOrEqualTo, "to be greater than or equal to");
            public static CompareRule<T> CreateIsLessThan(Expression<Func<TDbParams, T>> selector, T limit)
                => Create(selector, limit, IsLessThan, "to be less than");
            public static CompareRule<T> CreateIsLessThanOrEqualTo(Expression<Func<TDbParams, T>> selector, T limit)
                => Create(selector, limit, IsLessThanOrEqualTo, "to be less than or equal to");


            public static CompareRule<T> CreateIsEqual(Expression<Func<TDbParams, string>> selector, T limit)
                => Create(selector, limit, IsEqualTo, "to be equal to");
            public static CompareRule<T> CreateIsNotEqual(Expression<Func<TDbParams, string>> selector, T limit)
                => Create(selector, limit, IsNotEqual, "to not be equal to");
            public static CompareRule<T> CreateIsGreaterThan(Expression<Func<TDbParams, string>> selector, T limit)
                => Create(selector, limit, IsGreatorThan, "to be greater than");
            public static CompareRule<T> CreateIsGreaterThanOrEqualTo(Expression<Func<TDbParams, string>> selector, T limit)
                => Create(selector, limit, IsGreaterThanOrEqualTo, "to be greater than or equal to");
            public static CompareRule<T> CreateIsLessThan(Expression<Func<TDbParams, string>> selector, T limit)
                => Create(selector, limit, IsLessThan, "to be less than");
            public static CompareRule<T> CreateIsLessThanOrEqualTo(Expression<Func<TDbParams, string>> selector, T limit)
                => Create(selector, limit, IsLessThanOrEqualTo, "to be less than or equal to");



            private static async Task<CompareRule<T>> CreateAsync(
                Expression<Func<TDbParams, T>> selector,
                T limit,
                Func<T, T, Task<bool>> comaparitor,
                string compareType
                )
            {
                CompareRule<T> rule = new CompareRule<T>();
                await rule.InitAsync(limit, comaparitor, compareType);
                await rule.InitAsync(selector, await GenericInstance.IsNullableTypeAsync<T>());
                return rule;
            }
            private static async Task<CompareRule<T>> CreateAsync(
                Expression<Func<TDbParams, string>> selector,
                T limit,
                Func<T, T, Task<bool>> comaparitor,
                string compareType
                )
            {
                CompareRule<T> rule = new CompareRule<T>();
                await rule.InitAsync(limit, comaparitor, compareType);
                await rule.InitAsync(selector, true);
                return rule;
            }
            protected virtual async Task InitAsync(
                T limit,
                Func<T, T, Task<bool>> comaparitor,
                string compareType
                )
            {
                Limit = limit;
                CompareType = compareType;
                ComaparitorAsync = comaparitor;
                await Task.CompletedTask;
            }

            public static async Task<CompareRule<T>> CreateIsEqualAsync(Expression<Func<TDbParams, T>> selector, T limit)
                => await CreateAsync(selector, limit, IsEqualToAsync, "to be equal to");
            public static async Task<CompareRule<T>> CreateIsNotEqualAsync(Expression<Func<TDbParams, T>> selector, T limit)
                => await CreateAsync(selector, limit, IsNotEqualAsync, "to not be equal to");
            public static async Task<CompareRule<T>> CreateIsGreaterThanAsync(Expression<Func<TDbParams, T>> selector, T limit)
                => await CreateAsync(selector, limit, IsGreatorThanAsync, "to be greater than");
            public static async Task<CompareRule<T>> CreateIsGreaterThanOrEqualToAsync(Expression<Func<TDbParams, T>> selector, T limit)
                => await CreateAsync(selector, limit, IsGreaterThanOrEqualToAsync, "to be greater than or equal to");
            public static async Task<CompareRule<T>> CreateIsLessThanAsync(Expression<Func<TDbParams, T>> selector, T limit)
                => await CreateAsync(selector, limit, IsLessThanAsync, "to be less than");
            public static async Task<CompareRule<T>> CreateIsLessThanOrEqualToAsync(Expression<Func<TDbParams, T>> selector, T limit)
                => await CreateAsync(selector, limit, IsLessThanOrEqualToAsync, "to be less than or equal to");

            public static async Task<CompareRule<T>> CreateIsEqualAsync(Expression<Func<TDbParams, string>> selector, T limit)
                => await CreateAsync(selector, limit, IsEqualToAsync, "to be equal to");
            public static async Task<CompareRule<T>> CreateIsNotEqualAsync(Expression<Func<TDbParams, string>> selector, T limit)
                => await CreateAsync(selector, limit, IsNotEqualAsync, "to not be equal to");
            public static async Task<CompareRule<T>> CreateIsGreaterThanAsync(Expression<Func<TDbParams, string>> selector, T limit)
                => await CreateAsync(selector, limit, IsGreatorThanAsync, "to be greater than");
            public static async Task<CompareRule<T>> CreateIsGreaterThanOrEqualToAsync(Expression<Func<TDbParams, string>> selector, T limit)
                => await CreateAsync(selector, limit, IsGreaterThanOrEqualToAsync, "to be greater than or equal to");
            public static async Task<CompareRule<T>> CreateIsLessThanAsync(Expression<Func<TDbParams, string>> selector, T limit)
                => await CreateAsync(selector, limit, IsLessThanAsync, "to be less than");
            public static async Task<CompareRule<T>> CreateIsLessThanOrEqualToAsync(Expression<Func<TDbParams, string>> selector, T limit)
                => await CreateAsync(selector, limit, IsLessThanOrEqualToAsync, "to be less than or equal to");


            protected override bool ValidateRule()
            {
                if(ParamsValue is string val)
                {
                    if (!Converter<T>.CanConvertFromString)
                    {
                        return false;
                    }
                    try
                    {                        
                        T convertedValue = Converter<T>.FromString(val);
                        return Comaparitor(convertedValue, Limit);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return Comaparitor((T)ParamsValue, Limit);
            }
            protected override async Task<bool> ValidateRuleAsync()
            {
                if (ParamsValue is string val)
                {
                    if (!Converter<T>.CanConvertFromString)
                    {
                        await Task.CompletedTask;
                        return false;
                    }
                    try
                    {
                        T convertedValue = await Converter<T>.FromStringAsync(val);
                        return await ComaparitorAsync(convertedValue, Limit);
                    }
                    catch
                    {
                        await Task.CompletedTask;
                        return false;
                    }
                }
                return await ComaparitorAsync((T)ParamsValue, Limit);
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} expecting value {CompareType} {Limit}";
            }

        }
        private class CompareDateRule : CompareRule<DateTime> 
        {
            private CompareDateRule() { }
            private CultureInfo CultureInfo { get; set; }
            private DateTimeStyles DateTimeStyles { get; set; }
            private string DateFormat { get; set; }
           
            private static CompareDateRule Create(
                Expression<Func<TDbParams, DateTime?>> selector,
                DateTime limit,
                Func<DateTime, DateTime, bool> comparitor,
                string compareType,
                CultureInfo cultureInfo = null,
                DateTimeStyles dateTimeStyles = DateTimeStyles.None
                )
            {
                CompareDateRule rule = new CompareDateRule();
                rule.Init(limit, comparitor, compareType, cultureInfo, dateTimeStyles);
                rule.Init(selector, true);
                return rule;
            }
            private static CompareDateRule Create(
                Expression<Func<TDbParams, string>> selector,
                DateTime limit,
                Func<DateTime, DateTime, bool> comparitor,
                string compareType,
                CultureInfo cultureInfo = null,
                DateTimeStyles dateTimeStyles = DateTimeStyles.None,
                string dateFormat = null,
                bool isNullable = true
                )
            {
                CompareDateRule rule = new CompareDateRule();
                rule.Init(limit, comparitor, compareType, cultureInfo, dateTimeStyles, dateFormat);
                rule.Init(selector, isNullable);
                return rule;
            }
            private void Init(
                DateTime limit,
                Func<DateTime, DateTime, bool> comparitor,
                string compareType, 
                CultureInfo cultureInfo = null,
                DateTimeStyles dateTimeStyles = DateTimeStyles.None,
                string dateFormat = null
                )
            {
                CultureInfo = cultureInfo is CultureInfo ci ? ci : CultureInfo.InvariantCulture;
                DateTimeStyles = dateTimeStyles;
                DateFormat = dateFormat;
                base.Init(limit, comparitor, compareType);
            }
            private static async Task<CompareDateRule> CreateAsync(
                Expression<Func<TDbParams, DateTime?>> selector,
                DateTime limit,
                Func<DateTime, DateTime, Task<bool>> comparitor,
                string compareType,
                CultureInfo cultureInfo = null,
                DateTimeStyles dateTimeStyles = DateTimeStyles.None
                )
            {
                CompareDateRule rule = new CompareDateRule();
                await rule.InitAsync(limit, comparitor, compareType, cultureInfo, dateTimeStyles);
                await rule.InitAsync(selector, true);
                return rule;
            }
            private static async Task<CompareDateRule> CreateAsync(
                Expression<Func<TDbParams, string>> selector,
                DateTime limit,
                Func<DateTime, DateTime, Task<bool>> comparitor,
                string compareType,
                CultureInfo cultureInfo = null,
                DateTimeStyles dateTimeStyles = DateTimeStyles.None,
                string dateFormat = null,
                bool isNullable = true
                )
            {
                CompareDateRule rule = new CompareDateRule();
                await rule.InitAsync(limit, comparitor, compareType, cultureInfo, dateTimeStyles, dateFormat);
                await rule.InitAsync(selector, isNullable);
                return rule;
            }
            private async Task InitAsync(
                DateTime limit,
                Func<DateTime, DateTime, Task<bool>> comparitor,
                string compareType,
                CultureInfo cultureInfo = null,
                DateTimeStyles dateTimeStyles = DateTimeStyles.None,
                string dateFormat = null
                )
            {
                CultureInfo = cultureInfo is CultureInfo ci ? ci : CultureInfo.InvariantCulture;
                DateTimeStyles = dateTimeStyles;
                DateFormat = dateFormat;
                await base.InitAsync(limit, comparitor, compareType);
            }


            public static CompareDateRule CreateIsEqual(Expression<Func<TDbParams, DateTime?>> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsEqualTo, "to be equal to", cultureInfo, dateTimeStyles);
            public static CompareDateRule CreateIsNotEqual(Expression<Func<TDbParams, DateTime?>> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsNotEqual, "to not be equal to", cultureInfo, dateTimeStyles);
            public static CompareDateRule CreateIsAfter(Expression<Func<TDbParams, DateTime?>> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsGreatorThan, "to be after", cultureInfo, dateTimeStyles);
            public static CompareDateRule CreateIsOnOrAfter(Expression<Func<TDbParams, DateTime?>> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsGreaterThanOrEqualTo, "to be greater than or equal to", cultureInfo, dateTimeStyles);
            public static CompareDateRule CreateIsBefore(Expression<Func<TDbParams, DateTime?>> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsLessThan, "to be less than", cultureInfo, dateTimeStyles);
            public static CompareDateRule CreateIsOnOrBefore(Expression<Func<TDbParams, DateTime?>> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsLessThanOrEqualTo, "to be less than or equal to", cultureInfo, dateTimeStyles);


            public static CompareDateRule CreateIsEqual(Expression<Func<TDbParams, string>> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsEqualTo, "to be equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            public static CompareDateRule CreateIsNotEqual(Expression<Func<TDbParams, string>> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsNotEqual, "to not be equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            public static CompareDateRule CreateIsAfter(Expression<Func<TDbParams, string>> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsGreatorThan, "to be after", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            public static CompareDateRule CreateIsOnOrAfter(Expression<Func<TDbParams, string>> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsGreaterThanOrEqualTo, "to be greater than or equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            public static CompareDateRule CreateIsBefore(Expression<Func<TDbParams, string>> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsLessThan, "to be less than", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            public static CompareDateRule CreateIsOnOrBefore(Expression<Func<TDbParams, string>> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsLessThanOrEqualTo, "to be less than or equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);



            public static async Task<CompareDateRule> CreateIsEqualAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime limit, CultureInfo cultureInfo = null,
           DateTimeStyles dateTimeStyles = DateTimeStyles.None)
               => await CreateAsync(selector, limit, IsEqualToAsync, "to be equal to", cultureInfo, dateTimeStyles);
            public static async Task<CompareDateRule> CreateIsNotEqualAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsNotEqualAsync, "to not be equal to", cultureInfo, dateTimeStyles);
            public static async Task<CompareDateRule> CreateIsAfterAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsGreatorThanAsync, "to be after", cultureInfo, dateTimeStyles);
            public static async Task<CompareDateRule> CreateIsOnOrAfterAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsGreaterThanOrEqualToAsync, "to be greater than or equal to", cultureInfo, dateTimeStyles);
            public static async Task<CompareDateRule> CreateIsBeforeAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsLessThanAsync, "to be less than", cultureInfo, dateTimeStyles);
            public static async Task<CompareDateRule> CreateIsOnOrBeforeAsync(Expression<Func<TDbParams, DateTime?>> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsLessThanOrEqualToAsync, "to be less than or equal to", cultureInfo, dateTimeStyles);


            public static async Task<CompareDateRule> CreateIsEqualAsync(Expression<Func<TDbParams, string>> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsEqualToAsync, "to be equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            public static async Task<CompareDateRule> CreateIsNotEqualAsync(Expression<Func<TDbParams, string>> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsNotEqualAsync, "to not be equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            public static async Task<CompareDateRule> CreateIsAfterAsync(Expression<Func<TDbParams, string>> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsGreatorThanAsync, "to be after", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            public static async Task<CompareDateRule> CreateIsOnOrAfterAsync(Expression<Func<TDbParams, string>> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsGreaterThanOrEqualToAsync, "to be greater than or equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            public static async Task<CompareDateRule> CreateIsBeforeAsync(Expression<Func<TDbParams, string>> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsLessThanAsync, "to be less than", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            public static async Task<CompareDateRule> CreateIsOnOrBeforeAsync(Expression<Func<TDbParams, string>> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsLessThanOrEqualToAsync, "to be less than or equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);

            private DateTime? GetDateTimeFromString(string value)
            {
                DateTime? dateTime = null;
                DateTime dateTimeParsed;
                bool isDate = !string.IsNullOrEmpty(DateFormat)
                ? DateTime.TryParseExact(value, DateFormat, CultureInfo, DateTimeStyles, out dateTimeParsed)
                : DateTime.TryParse(value, CultureInfo, DateTimeStyles, out dateTimeParsed);
                if (isDate)
                {
                    dateTime = dateTimeParsed;
                }
                return dateTime;
            }
            private async Task<DateTime?> GetDateTimeFromStringAsync(string value)
            {
                DateTime? dateTime = null;
                DateTime dateTimeParsed;
                bool isDate = !string.IsNullOrEmpty(DateFormat)
                ? DateTime.TryParseExact(value, DateFormat, CultureInfo, DateTimeStyles, out dateTimeParsed)
                : DateTime.TryParse(value, CultureInfo, DateTimeStyles, out dateTimeParsed);
                if (isDate)
                {
                    dateTime = dateTimeParsed;
                }
                await Task.CompletedTask;
                return dateTime;
            }
            private DateTime? GetDateTime()
            {
                return ParamsValue is DateTime date ? date : ParamsValue is string dateStr ? GetDateTimeFromString(dateStr) : null;
            }
            private async Task<DateTime?> GetDateTimeAsync()
            {
                if (ParamsValue is string dateStr)
                {
                    return await GetDateTimeFromStringAsync(dateStr);
                }
                DateTime? dateTime = null;
                if (ParamsValue is DateTime date)
                {
                    dateTime = date;
                }
                await Task.CompletedTask;
                return dateTime;
            }
            protected override bool ValidateRule()
            => GetDateTime() is DateTime date ? Comaparitor(date, Limit) : false;
            protected override async Task<bool> ValidateRuleAsync()
            {
                DateTime? dateTime = await GetDateTimeAsync();
                if (dateTime is DateTime date)
                {
                    return await ComaparitorAsync(date, Limit);
                }
                return false;
            }

        }


    }
}