using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace DBFacade.DataLayer.Models.Validators.Rules
{
    public enum EmailDomainMode
    {
        AllowAll = 0,
        Whitelist = 1,
        Blacklist = 2
    }

    public abstract partial class ValidationRule<TDbParams>
        where TDbParams : IDbParamsModel
    {
        public static IValidationRule<TDbParams> Match(Expression<Func<TDbParams, string>> selector,
            string regexMatchStr, bool isNullable = false)
        {
            return new MatchRule(selector, regexMatchStr, isNullable);
        }

        public static IValidationRule<TDbParams> Match(Expression<Func<TDbParams, string>> selector,
            string regexMatchStr, RegexOptions options, bool isNullable = false)
        {
            return new MatchRule(selector, regexMatchStr, options, isNullable);
        }

        public static IValidationRule<TDbParams> IsNDigitString(Expression<Func<TDbParams, string>> selector,
            int length, bool isNullable = false)
        {
            return new IsNDigitStringRule(selector, length, isNullable);
        }

        public static IValidationRule<TDbParams> IsSocialSecurityNumber(Expression<Func<TDbParams, string>> selector,
            bool allowDashes = true, bool isNullable = false)
        {
            return new IsSocialSecurityNumberRule(selector, allowDashes, isNullable);
        }


        public static IValidationRule<TDbParams> IsNullOrEmpty(Expression<Func<TDbParams, string>> selector)
        {
            return new IsNullOrEmptyRule(selector);
        }

        public static IValidationRule<TDbParams> IsNotNullOrEmpty(Expression<Func<TDbParams, string>> selector)
        {
            return new IsNotNullOrEmptyRule(selector);
        }

        public static IValidationRule<TDbParams> IsNullOrWhiteSpace(Expression<Func<TDbParams, string>> selector)
        {
            return new IsNullOrWhiteSpaceRule(selector);
        }

        public static IValidationRule<TDbParams> IsNotNullOrWhiteSpace(Expression<Func<TDbParams, string>> selector)
        {
            return new IsNotNullOrWhiteSpaceRule(selector);
        }

        public static IValidationRule<TDbParams> LengthEquals(Expression<Func<TDbParams, string>> selector, int limit,
            bool isNullable = false)
        {
            return new LengthEqualsRule(selector, limit, isNullable);
        }

        public static IValidationRule<TDbParams> MinLength(Expression<Func<TDbParams, string>> selector, int limit,
            bool isNullable = false)
        {
            return new MinLengthRule(selector, limit, isNullable);
        }

        public static IValidationRule<TDbParams> MaxLength(Expression<Func<TDbParams, string>> selector, int limit,
            bool isNullable = false)
        {
            return new MaxLengthRule(selector, limit, isNullable);
        }


        public static IValidationRule<TDbParams> Email(Expression<Func<TDbParams, string>> selector,
            bool isNullable = false)
        {
            return new EmailRule(selector, isNullable);
        }

        public static IValidationRule<TDbParams> Email(Expression<Func<TDbParams, string>> selector,
            IEnumerable<string> domains, EmailDomainMode mode, bool isNullable = false)
        {
            return new EmailRule(selector, domains, mode, isNullable);
        }

        private class MatchRule : ValidationRule<TDbParams>
        {
            public MatchRule(Expression<Func<TDbParams, string>> selector, string regexMatchStr,
                bool isNullable = false) : this(selector, regexMatchStr, RegexOptions.IgnoreCase, isNullable)
            {
            }

            public MatchRule(Expression<Func<TDbParams, string>> selector, string regexMatchStr, RegexOptions options,
                bool isNullable = false) : base(selector, isNullable)
            {
                MatchStr = regexMatchStr;
                RegexOptions = options;
            }

            private string MatchStr { get; }

            internal RegexOptions RegexOptions { get; }

            protected override bool ValidateRule()
            {
                var matchRegex = Regex.Match(ParamsValue.ToString(), MatchStr, RegexOptions);
                return matchRegex.Success;
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} does not match the expression.";
            }
        }

        private sealed class IsNDigitStringRule : MatchRule
        {
            public IsNDigitStringRule(Expression<Func<TDbParams, string>> selector, int length, bool isNullable = false)
                : base(selector, BuildRegexString(length), isNullable)
            {
                Length = length;
            }

            private int Length { get; }

            private static string BuildRegexString(int length)
            {
                return new StringBuilder("(?<!\\d)\\d{").Append(length).Append("}(?!\\d)").ToString();
            }

            protected override bool ValidateRule()
            {
                return Length > 0 && ParamsValue.ToString().Length == Length && base.ValidateRule();
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                if (Length <= 0 || ParamsValue.ToString().Length != Length)
                    return $"Length of {propertyName} is not equal to {Length}";
                return $"{propertyName} is not a {Length} digit string";
            }
        }

        private sealed class IsSocialSecurityNumberRule : ValidationRule<TDbParams>
        {
            private static readonly string SSNMatch =
                "^(?!219-09-9999|078-05-1120)((?!666|000|9\\d{2})\\d{3}-?(?!00)\\d{2}-?(?!0{4})\\d{4})$";

            private static readonly string SSNMatchNoDashes =
                "^(?!219099999|078051120)((?!666|000|9\\d{2})\\d{3}?(?!00)\\d{2}?(?!0{4})\\d{4})$";

            public IsSocialSecurityNumberRule(Expression<Func<TDbParams, string>> selector, bool allowDashes = true,
                bool isNullable = false) : base(selector, isNullable)
            {
                AllowDashes = allowDashes;
            }

            private bool AllowDashes { get; }

            protected override bool ValidateRule()
            {
                var sSnMatchNoDashesResult =
                    Regex.Match(ParamsValue.ToString(), SSNMatchNoDashes, RegexOptions.IgnoreCase);
                if (AllowDashes)
                {
                    var sSnMatchResult = Regex.Match(ParamsValue.ToString(), SSNMatch, RegexOptions.IgnoreCase);
                    return sSnMatchNoDashesResult.Success || sSnMatchResult.Success;
                }

                return sSnMatchNoDashesResult.Success;
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} is not a valid Social Security Number";
            }
        }

        private class IsNullOrEmptyRule : ValidationRule<TDbParams>
        {
            public IsNullOrEmptyRule(Expression<Func<TDbParams, string>> selector) : base(selector, true)
            {
            }

            protected override bool ValidateRule()
            {
                return string.IsNullOrEmpty(ParamsValue.ToString());
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} expecting value to be null or empty";
            }
        }

        private class IsNotNullOrEmptyRule : ValidationRule<TDbParams>
        {
            public IsNotNullOrEmptyRule(Expression<Func<TDbParams, string>> selector) : base(selector, true)
            {
            }

            protected override bool ValidateRule()
            {
                return !string.IsNullOrEmpty(ParamsValue.ToString());
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} expecting value to not be null or empty";
            }
        }

        private class IsNullOrWhiteSpaceRule : ValidationRule<TDbParams>
        {
            public IsNullOrWhiteSpaceRule(Expression<Func<TDbParams, string>> selector) : base(selector, true)
            {
            }

            protected override bool ValidateRule()
            {
                return string.IsNullOrWhiteSpace(ParamsValue.ToString());
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} expecting value to be null or white space";
            }
        }

        private class IsNotNullOrWhiteSpaceRule : ValidationRule<TDbParams>
        {
            public IsNotNullOrWhiteSpaceRule(Expression<Func<TDbParams, string>> selector) : base(selector, true)
            {
            }

            protected override bool ValidateRule()
            {
                return !string.IsNullOrWhiteSpace(ParamsValue.ToString());
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} expecting value to not be null or white space";
            }
        }

        private class LengthEqualsRule : ValidationRule<TDbParams>
        {
            public LengthEqualsRule(Expression<Func<TDbParams, string>> selector, int limit, bool isNullable = false) :
                base(selector, isNullable)
            {
                LimitValue = limit;
            }

            private int LimitValue { get; }

            protected override bool ValidateRule()
            {
                return ParamsValue.ToString().Length == LimitValue;
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} expecting text length to  equal to {LimitValue}";
            }
        }

        private class MinLengthRule : ValidationRule<TDbParams>
        {
            public MinLengthRule(Expression<Func<TDbParams, string>> selector, int limit, bool isNullable = false) :
                base(selector, isNullable)
            {
                LimitValue = limit;
            }

            private int LimitValue { get; }

            protected override bool ValidateRule()
            {
                return ParamsValue.ToString().Length >= LimitValue;
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} expecting text length to be greater than or equal to {LimitValue}";
            }
        }

        private class MaxLengthRule : ValidationRule<TDbParams>
        {
            public MaxLengthRule(Expression<Func<TDbParams, string>> selector, int limit, bool isNullable = false) :
                base(selector, isNullable)
            {
                LimitValue = limit;
            }

            private int LimitValue { get; }

            protected override bool ValidateRule()
            {
                return ParamsValue.ToString().Length <= LimitValue;
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} expecting text length to be less than or equal to {LimitValue}";
            }
        }

        private class EmailRule : ValidationRule<TDbParams>
        {
            public EmailRule(Expression<Func<TDbParams, string>> selector, bool isNullable = false) : base(selector,
                isNullable)
            {
                Domains = new string[0];
                Mode = EmailDomainMode.AllowAll;
            }

            public EmailRule(Expression<Func<TDbParams, string>> selector, IEnumerable<string> domains,
                EmailDomainMode mode, bool isNullable = false) : base(selector, isNullable)
            {
                Domains = domains;
                Mode = mode;
            }

            private IEnumerable<string> Domains { get; }
            private EmailDomainMode Mode { get; }

            protected override bool ValidateRule()
            {
                try
                {
                    var email = new MailAddress(ParamsValue.ToString());
                    var hasDomain = Domains.Contains(email.Host);
                    return Mode == EmailDomainMode.Whitelist ? hasDomain :
                        Mode == EmailDomainMode.Blacklist ? !hasDomain :
                        true;
                }
                catch
                {
                    return false;
                }
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} is an invalid email address";
            }
        }
    }
}