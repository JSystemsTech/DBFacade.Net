using DbFacade.DataLayer.Models;
using System;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DbFacade.Extensions
{
    public static class ValidatorExtensions
    {
        /// <summary>Adds the is equal to.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="other">The other.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsEqualTo<T, TValue>(this Validator<T> validator, Func<T, TValue> getValue, TValue other, string errorMessage)
            where T : class
            where TValue : IComparable
        => validator.Add(m => getValue(m).IsEqualTo(other), errorMessage);

        /// <summary>Adds the is equal to.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="getOtherValue">The get other value.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsEqualTo<T, TValue>(this Validator<T> validator, Func<T, TValue> getValue, Func<T, TValue> getOtherValue, string errorMessage)
            where T : class
            where TValue : IComparable
        => validator.Add(m => getValue(m).IsEqualTo(getOtherValue(m)), errorMessage);

        /// <summary>Adds the is not equal.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="other">The other.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsNotEqual<T, TValue>(this Validator<T> validator, Func<T, TValue> getValue, TValue other, string errorMessage)
            where T : class
            where TValue : IComparable
        => validator.Add(m => getValue(m).IsNotEqual(other), errorMessage);

        /// <summary>Adds the is not equal.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="getOtherValue">The get other value.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsNotEqual<T, TValue>(this Validator<T> validator, Func<T, TValue> getValue, Func<T, TValue> getOtherValue, string errorMessage)
            where T : class
            where TValue : IComparable
        => validator.Add(m => getValue(m).IsNotEqual(getOtherValue(m)), errorMessage);

        /// <summary>Adds the is greator than.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="other">The other.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsGreatorThan<T, TValue>(this Validator<T> validator, Func<T, TValue> getValue, TValue other, string errorMessage)
            where T : class
            where TValue : IComparable
        => validator.Add(m => getValue(m).IsGreatorThan(other), errorMessage);

        /// <summary>Adds the is greator than.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="getOtherValue">The get other value.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsGreatorThan<T, TValue>(this Validator<T> validator, Func<T, TValue> getValue, Func<T, TValue> getOtherValue, string errorMessage)
            where T : class
            where TValue : IComparable
        => validator.Add(m => getValue(m).IsGreatorThan(getOtherValue(m)), errorMessage);

        /// <summary>Adds the is greater than or equal to.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="other">The other.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsGreaterThanOrEqualTo<T, TValue>(this Validator<T> validator, Func<T, TValue> getValue, TValue other, string errorMessage)
            where T : class
            where TValue : IComparable
        => validator.Add(m => getValue(m).IsGreaterThanOrEqualTo(other), errorMessage);

        /// <summary>Adds the is greater than or equal to.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="getOtherValue">The get other value.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsGreaterThanOrEqualTo<T, TValue>(this Validator<T> validator, Func<T, TValue> getValue, Func<T, TValue> getOtherValue, string errorMessage)
            where T : class
            where TValue : IComparable
        => validator.Add(m => getValue(m).IsGreaterThanOrEqualTo(getOtherValue(m)), errorMessage);

        /// <summary>Adds the is less than.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="other">The other.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsLessThan<T, TValue>(this Validator<T> validator, Func<T, TValue> getValue, TValue other, string errorMessage)
            where T : class
            where TValue : IComparable
        => validator.Add(m => getValue(m).IsLessThan(other), errorMessage);

        /// <summary>Adds the is less than.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="getOtherValue">The get other value.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsLessThan<T, TValue>(this Validator<T> validator, Func<T, TValue> getValue, Func<T, TValue> getOtherValue, string errorMessage)
            where T : class
            where TValue : IComparable
        => validator.Add(m => getValue(m).IsLessThan(getOtherValue(m)), errorMessage);

        /// <summary>Adds the is less than or equal to.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="other">The other.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsLessThanOrEqualTo<T, TValue>(this Validator<T> validator, Func<T, TValue> getValue, TValue other, string errorMessage)
            where T : class
            where TValue : IComparable
        => validator.Add(m => getValue(m).IsLessThanOrEqualTo(other), errorMessage);

        /// <summary>Adds the is less than or equal to.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="getOtherValue">The get other value.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsLessThanOrEqualTo<T, TValue>(this Validator<T> validator, Func<T, TValue> getValue, Func<T, TValue> getOtherValue, string errorMessage)
            where T : class
            where TValue : IComparable
        => validator.Add(m => getValue(m).IsLessThanOrEqualTo(getOtherValue(m)), errorMessage);

        /// <summary>Adds the is date time.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsDateTime<T>(this Validator<T> validator, Func<T, string> getValue, string dateFormat, IFormatProvider provider, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsDateTime(dateFormat, provider), errorMessage);
        /// <summary>Adds the is date time.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsDateTime<T>(this Validator<T> validator, Func<T, string> getValue, string dateFormat, IFormatProvider provider, DateTimeStyles dateTimeStyles, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsDateTime(dateFormat, provider, dateTimeStyles), errorMessage);
        /// <summary>Adds the is date time.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="dateTimeStyles">The date time styles.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsDateTime<T>(this Validator<T> validator, Func<T, string> getValue, string dateFormat, DateTimeStyles dateTimeStyles, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsDateTime(dateFormat, dateTimeStyles), errorMessage);
        /// <summary>Adds the is date time.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsDateTime<T>(this Validator<T> validator, Func<T, string> getValue, string dateFormat, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsDateTime(dateFormat), errorMessage);
        /// <summary>Adds the is date time.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsDateTime<T>(this Validator<T> validator, Func<T, string> getValue, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsDateTime(), errorMessage);
        /// <summary>Adds the is date time.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsDateTime<T>(this Validator<T> validator, Func<T, DateTime?> getValue, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsDateTime(), errorMessage);

        /// <summary>Adds the is numeric.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsNumeric<T>(this Validator<T> validator, Func<T, object> getValue, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsNumeric(), errorMessage);

        /// <summary>Adds the is regex match.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="regexMatchStr">The regex match string.</param>
        /// <param name="options">The options.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsRegexMatch<T>(this Validator<T> validator, Func<T, string> getValue, string regexMatchStr, RegexOptions options, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsRegexMatch(regexMatchStr, options), errorMessage);
        /// <summary>Adds the is regex match.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="regexMatchStr">The regex match string.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsRegexMatch<T>(this Validator<T> validator, Func<T, string> getValue, string regexMatchStr, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsRegexMatch(regexMatchStr), errorMessage);
        /// <summary>Adds the is regex match.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="regex">The regex.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsRegexMatch<T>(this Validator<T> validator, Func<T, string> getValue, Regex regex, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsRegexMatch(regex), errorMessage);


        /// <summary>Adds the is n digit string.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="length">The length.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsNDigitString<T>(this Validator<T> validator, Func<T, string> getValue, int length, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsNDigitString(length), errorMessage);

        /// <summary>Adds the minimum length of the has.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddHasMinLength<T>(this Validator<T> validator, Func<T, string> getValue, int min, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).HasMinLength(min), errorMessage);
        /// <summary>Adds the maximum length of the has.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddHasMaxLength<T>(this Validator<T> validator, Func<T, string> getValue, int max, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).HasMaxLength(max), errorMessage);

        /// <summary>Adds the is null.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsNull<T>(this Validator<T> validator, Func<T, string> getValue, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m) == null, errorMessage);
        /// <summary>Adds the is not null.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsNotNull<T>(this Validator<T> validator, Func<T, string> getValue, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m) != null, errorMessage);
        /// <summary>Adds the is null or empty.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsNullOrEmpty<T>(this Validator<T> validator, Func<T, string> getValue, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsNullOrEmpty(), errorMessage);
        /// <summary>Adds the is not null or empty.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsNotNullOrEmpty<T>(this Validator<T> validator, Func<T, string> getValue, string errorMessage)
            where T : class
        => validator.Add(m => !getValue(m).IsNullOrEmpty(), errorMessage);
        /// <summary>Adds the is null or white space.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsNullOrWhiteSpace<T>(this Validator<T> validator, Func<T, string> getValue, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsNullOrWhiteSpace(), errorMessage);
        /// <summary>Adds the is not null or white space.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsNotNullOrWhiteSpace<T>(this Validator<T> validator, Func<T, string> getValue, string errorMessage)
            where T : class
        => validator.Add(m => !getValue(m).IsNullOrWhiteSpace(), errorMessage);

        /// <summary>Adds the is mail address.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsMailAddress<T>(this Validator<T> validator, Func<T, string> getValue, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).TryParseMailAddress(out MailAddress ma), errorMessage);
        /// <summary>Adds the is in domain.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="domains">The domains.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsInDomain<T>(this Validator<T> validator, Func<T, string> getValue, string[] domains, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsInDomain(domains), errorMessage);
        /// <summary>Adds the is in domain.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="domains">The domains.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsInDomain<T>(this Validator<T> validator, Func<T, MailAddress> getValue, string[] domains, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsInDomain(domains), errorMessage);
        /// <summary>Adds the is not in domain.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="domains">The domains.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsNotInDomain<T>(this Validator<T> validator, Func<T, string> getValue, string[] domains, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsNotInDomain(domains), errorMessage);
        /// <summary>Adds the is not in domain.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator">The validator.</param>
        /// <param name="getValue">The get value.</param>
        /// <param name="domains">The domains.</param>
        /// <param name="errorMessage">The error message.</param>
        public static void AddIsNotInDomain<T>(this Validator<T> validator, Func<T, MailAddress> getValue, string[] domains, string errorMessage)
            where T : class
        => validator.Add(m => getValue(m).IsNotInDomain(domains), errorMessage);
    }
}
