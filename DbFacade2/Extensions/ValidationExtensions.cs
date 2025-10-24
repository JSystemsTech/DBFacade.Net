using DbFacade.Factories;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DbFacade.Extensions
{
    internal static class ValidationExtensions
    {
        internal static bool IsEqualTo<T>(this T value, T other)
            where T : IComparable
            => value.CompareTo(other) == 0;
        internal static bool IsNotEqual<T>(this T value, T other)
            where T : IComparable
            => value.CompareTo(other) != 0;
        internal static bool IsGreatorThan<T>(this T value, T other)
            where T : IComparable
            => value.CompareTo(other) > 0;
        internal static bool IsGreaterThanOrEqualTo<T>(this T value, T other)
            where T : IComparable
            => value.CompareTo(other) >= 0;
        internal static bool IsLessThan<T>(this T value, T other)
            where T : IComparable
            => value.CompareTo(other) < 0;
        internal static bool IsLessThanOrEqualTo<T>(this T value, T other)
            where T : IComparable
            => value.CompareTo(other) <= 0;

        internal static bool IsDateTime(this string value, string dateFormat, IFormatProvider provider, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => DateTime.TryParseExact(value, dateFormat, provider, dateTimeStyles, out DateTime dt);
        internal static bool IsDateTime(this string value, string dateFormat, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        => DateTime.TryParseExact(value, dateFormat, CultureInfo.InvariantCulture, dateTimeStyles, out DateTime dt);
        internal static bool IsDateTime(this string value)
        => DateTime.TryParse(value, out DateTime dt);
        internal static bool IsDateTime(this DateTime? value)
        => value is DateTime;

        internal static bool IsNumeric(this object value)
        => ConversionFactory.IsNumericValue(value);


        internal static bool IsRegexMatch(this string value, string regexMatchStr, RegexOptions options = RegexOptions.IgnoreCase)
        => Regex.Match(value, regexMatchStr, options).Success;
        internal static bool IsRegexMatch(this string value, Regex regex)
        => regex.Match(value).Success;

        internal static bool IsNDigitString(this string value, int length)
        => length > 0 && value.Length == length && IsRegexMatch(value, $"(?<!\\d)\\d{{{length}}}(?!\\d)");
        internal static bool HasMinLength(this string value, int min)
        => min >= 0  && IsGreaterThanOrEqualTo(value.Length, min);
        internal static bool HasMaxLength(this string value, int max)
        => max >= 0 && IsLessThanOrEqualTo(value.Length, max);

        internal static bool IsNullOrEmpty(this string value)
        => string.IsNullOrEmpty(value);
        internal static bool IsNullOrWhiteSpace(this string value)
        => string.IsNullOrWhiteSpace(value);
#if NET8_0_OR_GREATER
        internal static bool TryParseMailAddress(this string value, out MailAddress mailAddress)
            => MailAddress.TryCreate(value, out mailAddress);

#else
        internal static bool TryParseMailAddress(this string value, out MailAddress mailAddress)
        {
            try
            {
                mailAddress = new MailAddress(value);
                return true;
            }
            catch
            {
                mailAddress = null;
                return false;
            }
        }
#endif

        internal static bool IsInDomain(this string str, params string[] domains)
        => TryParseMailAddress(str, out MailAddress mailAddress) ? IsInDomain(mailAddress, domains) : false;
        internal static bool IsInDomain(this MailAddress mailAddress, params string[] domains)
        => domains.Contains(mailAddress.Host);
        internal static bool IsNotInDomain(this string str, params string[] domains)
        => TryParseMailAddress(str, out MailAddress mailAddress) ? !IsInDomain(mailAddress, domains) : false;
        internal static bool IsNotInDomain(this MailAddress mailAddress, params string[] domains)
        => !IsInDomain(mailAddress, domains);


    }
}
