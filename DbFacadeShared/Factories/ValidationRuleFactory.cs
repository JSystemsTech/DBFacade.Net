using DbFacade.DataLayer.Models.Validators.Rules;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DbFacade.Factories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public sealed class ValidationRuleFactory<TDbParams>
    {
        internal static ValidationRuleFactory<TDbParams> Instance = new ValidationRuleFactory<TDbParams>();

        #region Compare        
        /// <summary>
        /// Equalses the specified selector.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> Equals<T>(Func<TDbParams, T> selector, T compareValue)
       => ValidationRule<TDbParams>.CompareRule<T>.CreateIsEqual(selector, m => compareValue);
        /// <summary>
        /// Equalses the specified selector.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> Equals<T>(Func<TDbParams, T> selector, Func<TDbParams, T> compareValue)
       => ValidationRule<TDbParams>.CompareRule<T>.CreateIsEqual(selector, compareValue);
        /// <summary>
        /// Nots the equal to.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> NotEqualTo<T>(Func<TDbParams, T> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsNotEqual(selector, m => compareValue);
        /// <summary>
        /// Nots the equal to.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> NotEqualTo<T>(Func<TDbParams, T> selector, Func<TDbParams, T> compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsNotEqual(selector, compareValue);
        /// <summary>
        /// Greaters the than.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> GreaterThan<T>(Func<TDbParams, T> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsGreaterThan(selector, m => compareValue);
        /// <summary>
        /// Greaters the than.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> GreaterThan<T>(Func<TDbParams, T> selector, Func<TDbParams, T> compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsGreaterThan(selector, compareValue);
        /// <summary>
        /// Greaters the than or equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> GreaterThanOrEqual<T>(Func<TDbParams, T> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsGreaterThanOrEqualTo(selector, m => compareValue);
        /// <summary>
        /// Greaters the than or equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> GreaterThanOrEqual<T>(Func<TDbParams, T> selector, Func<TDbParams, T> compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsGreaterThanOrEqualTo(selector, compareValue);
        /// <summary>
        /// Lesses the than.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> LessThan<T>(Func<TDbParams, T> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsLessThan(selector, m => compareValue);
        /// <summary>
        /// Lesses the than.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> LessThan<T>(Func<TDbParams, T> selector, Func<TDbParams, T> compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsLessThan(selector, compareValue);
        /// <summary>
        /// Lesses the than or equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> LessThanOrEqual<T>(Func<TDbParams, T> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsLessThanOrEqualTo(selector, m => compareValue);
        /// <summary>
        /// Lesses the than or equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> LessThanOrEqual<T>(Func<TDbParams, T> selector, Func<TDbParams, T> compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsLessThanOrEqualTo(selector, compareValue);


        /// <summary>
        /// Equalses the specified selector.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> Equals<T>(Func<TDbParams, string> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsEqual(selector, m => compareValue);
        /// <summary>
        /// Equalses the specified selector.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> Equals<T>(Func<TDbParams, string> selector, Func<TDbParams, T> compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsEqual(selector, compareValue);
        /// <summary>
        /// Nots the equal to.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> NotEqualTo<T>(Func<TDbParams, string> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsNotEqual(selector, m => compareValue);
        /// <summary>
        /// Nots the equal to.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> NotEqualTo<T>(Func<TDbParams, string> selector, Func<TDbParams, T> compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsNotEqual(selector, compareValue);
        /// <summary>
        /// Greaters the than.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> GreaterThan<T>(Func<TDbParams, string> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsGreaterThan(selector, m => compareValue);
        /// <summary>
        /// Greaters the than.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> GreaterThan<T>(Func<TDbParams, string> selector, Func<TDbParams, T> compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsGreaterThan(selector, compareValue);
        /// <summary>
        /// Greaters the than or equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> GreaterThanOrEqual<T>(Func<TDbParams, string> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsGreaterThanOrEqualTo(selector, m => compareValue);
        /// <summary>
        /// Greaters the than or equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> GreaterThanOrEqual<T>(Func<TDbParams, string> selector, Func<TDbParams, T> compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsGreaterThanOrEqualTo(selector, compareValue);
        /// <summary>
        /// Lesses the than.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> LessThan<T>(Func<TDbParams, string> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsLessThan(selector, m => compareValue);
        /// <summary>
        /// Lesses the than.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> LessThan<T>(Func<TDbParams, string> selector, Func<TDbParams, T> compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsLessThan(selector, compareValue);
        /// <summary>
        /// Lesses the than or equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> LessThanOrEqual<T>(Func<TDbParams, string> selector, T compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsLessThanOrEqualTo(selector, m => compareValue);

        /// <summary>
        /// Lesses the than or equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> LessThanOrEqual<T>(Func<TDbParams, string> selector, Func<TDbParams, T> compareValue)
        => ValidationRule<TDbParams>.CompareRule<T>.CreateIsLessThanOrEqualTo(selector, compareValue);


        #region DateTime Equals
        /// <summary>
        /// Determines whether the specified selector is on.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, m => compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether the specified selector is on.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether the specified selector is on.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, m => compareValue, dateFormat, isNullable, cultureInfo);
        /// <summary>
        /// Determines whether the specified selector is on.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, compareValue, dateFormat, isNullable, cultureInfo);
        /// <summary>
        /// Determines whether the specified selector is on.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, m => compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is on.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is on.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, m => compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is on.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        /// <summary>
        /// Determines whether the specified selector is on.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, m => compareValue, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether the specified selector is on.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, compareValue, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether the specified selector is on.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, m => compareValue, cultureInfo);
        /// <summary>
        /// Determines whether the specified selector is on.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, compareValue, cultureInfo);
        /// <summary>
        /// Determines whether the specified selector is on.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, m => compareValue, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is on.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, compareValue, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is on.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, m => compareValue, CultureInfo.InvariantCulture, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is on.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOn(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsEqual(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);


        #endregion

        #region DateTime NotEqualTo
        /// <summary>
        /// Determines whether [is not on] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, m => compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether [is not on] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether [is not on] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, m => compareValue, dateFormat, isNullable, cultureInfo);
        /// <summary>
        /// Determines whether [is not on] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, compareValue, dateFormat, isNullable, cultureInfo);
        /// <summary>
        /// Determines whether [is not on] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, m => compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether [is not on] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether [is not on] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, m => compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);
        /// <summary>
        /// Determines whether [is not on] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);
        /// <summary>
        /// Determines whether [is not on] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, m => compareValue, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether [is not on] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, compareValue, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether [is not on] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, m => compareValue, cultureInfo);
        /// <summary>
        /// Determines whether [is not on] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, compareValue, cultureInfo);
        /// <summary>
        /// Determines whether [is not on] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, m => compareValue, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether [is not on] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, compareValue, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether [is not on] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, m => compareValue, CultureInfo.InvariantCulture, dateTimeStyles);
        /// <summary>
        /// Determines whether [is not on] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotOn(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsNotEqual(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);

        #endregion

        #region DateTime After

        /// <summary>
        /// Determines whether the specified selector is after.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, m => compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether the specified selector is after.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether the specified selector is after.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, m => compareValue, dateFormat, isNullable, cultureInfo);
        /// <summary>
        /// Determines whether the specified selector is after.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, compareValue, dateFormat, isNullable, cultureInfo);
        /// <summary>
        /// Determines whether the specified selector is after.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, m => compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is after.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is after.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, m => compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is after.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);

        /// <summary>
        /// Determines whether the specified selector is after.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, m => compareValue, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether the specified selector is after.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, compareValue, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether the specified selector is after.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, m => compareValue, cultureInfo);
        /// <summary>
        /// Determines whether the specified selector is after.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, compareValue, cultureInfo);
        /// <summary>
        /// Determines whether the specified selector is after.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, m => compareValue, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is after.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, compareValue, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is after.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, m => compareValue, CultureInfo.InvariantCulture, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is after.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsAfter(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsAfter(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);

        #endregion

        #region DateTime OnOrAfter
        /// <summary>
        /// Determines whether [is on or after] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, m => compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether [is on or after] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether [is on or after] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, m => compareValue, dateFormat, isNullable, cultureInfo);
        /// <summary>
        /// Determines whether [is on or after] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, compareValue, dateFormat, isNullable, cultureInfo);
        /// <summary>
        /// Determines whether [is on or after] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, m => compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether [is on or after] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Called when [or after].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> OnOrAfter(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, m => compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);
        /// <summary>
        /// Called when [or after].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> OnOrAfter(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);
        /// <summary>
        /// Determines whether [is on or after] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, m => compareValue, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether [is on or after] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, compareValue, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether [is on or after] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, m => compareValue, cultureInfo);
        /// <summary>
        /// Determines whether [is on or after] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, compareValue, cultureInfo);
        /// <summary>
        /// Determines whether [is on or after] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, m => compareValue, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether [is on or after] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, compareValue, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether [is on or after] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, m => compareValue, CultureInfo.InvariantCulture, dateTimeStyles);
        /// <summary>
        /// Determines whether [is on or after] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrAfter(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrAfter(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);

        #endregion

        #region DateTime IsBefore
        /// <summary>
        /// Determines whether the specified selector is before.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, m => compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether the specified selector is before.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether the specified selector is before.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, m => compareValue, dateFormat, isNullable, cultureInfo);
        /// <summary>
        /// Determines whether the specified selector is before.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, compareValue, dateFormat, isNullable, cultureInfo);
        /// <summary>
        /// Determines whether the specified selector is before.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, m => compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is before.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is before.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, m => compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is before.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is before.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, m => compareValue, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether the specified selector is before.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, compareValue, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether the specified selector is before.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, m => compareValue, cultureInfo);
        /// <summary>
        /// Determines whether the specified selector is before.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, compareValue, cultureInfo);
        /// <summary>
        /// Determines whether the specified selector is before.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, m => compareValue, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is before.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, compareValue, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is before.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, m => compareValue, CultureInfo.InvariantCulture, dateTimeStyles);
        /// <summary>
        /// Determines whether the specified selector is before.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsBefore(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsBefore(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);

        #endregion

        #region DateTime IsOnOrBefore
        /// <summary>
        /// Determines whether [is on or before] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, m => compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether [is on or before] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether [is on or before] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, m => compareValue, dateFormat, isNullable, cultureInfo);
        /// <summary>
        /// Determines whether [is on or before] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
       => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, compareValue, dateFormat, isNullable, cultureInfo);
        /// <summary>
        /// Determines whether [is on or before] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, m => compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether [is on or before] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, compareValue, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether [is on or before] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, string> selector, DateTime compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, m => compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);
        /// <summary>
        /// Determines whether [is on or before] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, string> selector, Func<TDbParams, DateTime> compareValue, string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, compareValue, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);
        /// <summary>
        /// Determines whether [is on or before] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, DateTime?> selector, DateTime compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, m => compareValue, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether [is on or before] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, compareValue, CultureInfo.InvariantCulture);
        /// <summary>
        /// Determines whether [is on or before] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, m => compareValue, cultureInfo);
        /// <summary>
        /// Determines whether [is on or before] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, CultureInfo cultureInfo)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, compareValue, cultureInfo);
        /// <summary>
        /// Determines whether [is on or before] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, DateTime?> selector, DateTime compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, m => compareValue, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether [is on or before] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, compareValue, cultureInfo, dateTimeStyles);
        /// <summary>
        /// Determines whether [is on or before] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, DateTime?> selector, DateTime compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, m => compareValue, CultureInfo.InvariantCulture, dateTimeStyles);
        /// <summary>
        /// Determines whether [is on or before] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="compareValue">The compare value.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsOnOrBefore(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> compareValue, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => ValidationRule<TDbParams>.CompareDateRule.CreateIsOnOrBefore(selector, compareValue, CultureInfo.InvariantCulture, dateTimeStyles);

        #endregion
        #endregion

        #region DateTime


        /// <summary>
        /// Determines whether [is date time] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsDateTime(Func<TDbParams, string> selector,
            string dateFormat = null, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.IsDateTimeRule(selector, dateFormat, isNullable);
        }
        /// <summary>
        /// Determines whether [is date time] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsDateTime(Func<TDbParams, string> selector,
            string dateFormat, CultureInfo cultureInfo, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.IsDateTimeRule(selector, dateFormat, isNullable, cultureInfo);
        }
        /// <summary>
        /// Determines whether [is date time] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsDateTime(Func<TDbParams, string> selector,
            string dateFormat, CultureInfo cultureInfo, DateTimeStyles dateTimeStyles, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.IsDateTimeRule(selector, dateFormat, isNullable, cultureInfo, dateTimeStyles);
        }
        /// <summary>
        /// Determines whether [is date time] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsDateTime(Func<TDbParams, string> selector,
            string dateFormat, DateTimeStyles dateTimeStyles, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.IsDateTimeRule(selector, dateFormat, isNullable, CultureInfo.InvariantCulture, dateTimeStyles);
        }



        #endregion

        #region Delegagte
        /// <summary>
        /// Delegates the specified selector.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> Delegate<T>(Func<TDbParams, T> selector, Func<T, bool> handler)
        => new ValidationRule<TDbParams>.DelegateRule<T>(selector, handler);


        /// <summary>
        /// Delegates the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> Delegate(Func<TDbParams, bool> handler, string errorMessage)
        => new ValidationRule<TDbParams>.DelegateRule(handler, errorMessage);

        #endregion

        #region Numeric
        /// <summary>
        /// Determines whether the specified selector is number.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNumber(Func<TDbParams, string> selector,
            bool isNullable = false)
        => new ValidationRule<TDbParams>.IsNumeric(selector, isNullable);



        #endregion

        #region Required
        /// <summary>
        /// Requireds the specified selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> Required(Func<TDbParams, object> selector)
        => new ValidationRule<TDbParams>.RequiredRule(selector);


        #endregion

        #region String
        /// <summary>
        /// Matches the specified selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="regexMatchStr">The regex match string.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> Match(Func<TDbParams, string> selector,
            string regexMatchStr, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.MatchRule(selector, regexMatchStr, isNullable);
        }

        /// <summary>
        /// Matches the specified selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="regexMatchStr">The regex match string.</param>
        /// <param name="options">The options.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> Match(Func<TDbParams, string> selector,
            string regexMatchStr, RegexOptions options, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.MatchRule(selector, regexMatchStr, options, isNullable);
        }


        /// <summary>
        /// Determines whether [is n digit string] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="length">The length.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNDigitString(Func<TDbParams, string> selector,
            int length, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.IsNDigitStringRule(selector, length, isNullable);
        }


        /// <summary>
        /// Determines whether [is social security number] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="allowDashes">if set to <c>true</c> [allow dashes].</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsSocialSecurityNumber(Func<TDbParams, string> selector,
            bool allowDashes = true, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.IsSocialSecurityNumberRule(selector, allowDashes, isNullable);
        }

        /// <summary>
        /// Determines whether [is null or empty] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNullOrEmpty(Func<TDbParams, string> selector)
        {
            return new ValidationRule<TDbParams>.IsNullOrEmptyRule(selector);
        }


        /// <summary>
        /// Determines whether [is not null or empty] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotNullOrEmpty(Func<TDbParams, string> selector)
        {
            return new ValidationRule<TDbParams>.IsNotNullOrEmptyRule(selector);
        }


        /// <summary>
        /// Determines whether [is null or white space] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNullOrWhiteSpace(Func<TDbParams, string> selector)
        {
            return new ValidationRule<TDbParams>.IsNullOrWhiteSpaceRule(selector);
        }


        /// <summary>
        /// Determines whether [is not null or white space] [the specified selector].
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> IsNotNullOrWhiteSpace(Func<TDbParams, string> selector)
        {
            return new ValidationRule<TDbParams>.IsNotNullOrWhiteSpaceRule(selector);
        }


        /// <summary>
        /// Lengthes the equals.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> LengthEquals(Func<TDbParams, string> selector, int limit,
            bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.LengthEqualsRule(selector, limit, isNullable);
        }


        /// <summary>
        /// Minimums the length.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> MinLength(Func<TDbParams, string> selector, int limit,
            bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.MinLengthRule(selector, limit, isNullable);
        }


        /// <summary>
        /// Maximums the length.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> MaxLength(Func<TDbParams, string> selector, int limit,
            bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.MaxLengthRule(selector, limit, isNullable);
        }


        /// <summary>
        /// Emails the specified selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> Email(Func<TDbParams, string> selector,
            bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.EmailRule(selector, isNullable);
        }

        /// <summary>
        /// Emails the specified selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="domains">The domains.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IValidationRule<TDbParams> Email(Func<TDbParams, string> selector,
            IEnumerable<string> domains, EmailDomainMode mode, bool isNullable = false)
        {
            return new ValidationRule<TDbParams>.EmailRule(selector, domains, mode, isNullable);
        }


        #endregion
    }
}
