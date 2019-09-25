using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DBFacade.DataLayer.Models.Validators.Rules
{
    public enum EmailDomainMode
    {
        AllowAll = 0,
        Whitelist = 1,
        Blacklist = 2
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{TDbParams}" />
    public abstract partial class ValidationRule<TDbParams>
        where TDbParams : IDbParamsModel
    {
        public static IValidationRule<TDbParams> Match(Expression<Func<TDbParams, string>> selector, string regexMatchStr, bool isNullable = false) => new MatchRule(selector, regexMatchStr, isNullable);
        public static IValidationRule<TDbParams> Match(Expression<Func<TDbParams, string>> selector, string regexMatchStr, RegexOptions options, bool isNullable = false) => new MatchRule(selector, regexMatchStr, options, isNullable);

        private class MatchRule : ValidationRule<TDbParams>
        {
            private string MatchStr { get;  set; }

            internal RegexOptions RegexOptions { get; private set; }

            public MatchRule(Expression<Func<TDbParams, string>> selector, string regexMatchStr, bool isNullable = false) : this(selector, regexMatchStr, RegexOptions.IgnoreCase, isNullable) { }
            
            public MatchRule(Expression<Func<TDbParams, string>> selector, string regexMatchStr, RegexOptions options, bool isNullable = false) : base(selector, isNullable)
            {
                MatchStr = regexMatchStr;
                RegexOptions = options;
            }

            protected override bool ValidateRule()
            {
                Match MatchRegex = Regex.Match(ParamsValue.ToString(), MatchStr, RegexOptions);
                return MatchRegex.Success;
            }
           
            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} does not match the expression.";
            }

        }
        public static IValidationRule<TDbParams> IsNullOrEmpty(Expression<Func<TDbParams, string>> selector) => new IsNullOrEmptyRule(selector);

        private class IsNullOrEmptyRule : ValidationRule<TDbParams>
        {
            public IsNullOrEmptyRule(Expression<Func<TDbParams, string>> selector) : base(selector, true) { }

            protected override bool ValidateRule() => string.IsNullOrEmpty(ParamsValue.ToString());

            protected override string GetErrorMessageCore(string propertyName) => $"{propertyName} expecting value to be null or empty";


        }
        public static IValidationRule<TDbParams> IsNotNullOrEmpty(Expression<Func<TDbParams, string>> selector) => new IsNotNullOrEmptyRule(selector);

        private class IsNotNullOrEmptyRule : ValidationRule<TDbParams>
        {
            public IsNotNullOrEmptyRule(Expression<Func<TDbParams, string>> selector) : base(selector, true) { }

            protected override bool ValidateRule() => !string.IsNullOrEmpty(ParamsValue.ToString());

            protected override string GetErrorMessageCore(string propertyName) => $"{propertyName} expecting value to not be null or empty";

        }
        public static IValidationRule<TDbParams> IsNullOrWhiteSpace(Expression<Func<TDbParams, string>> selector) => new IsNullOrWhiteSpaceRule(selector);

        private class IsNullOrWhiteSpaceRule : ValidationRule<TDbParams>
        {
            public IsNullOrWhiteSpaceRule(Expression<Func<TDbParams, string>> selector) : base(selector, true) { }

            protected override bool ValidateRule() => string.IsNullOrWhiteSpace(ParamsValue.ToString());

            protected override string GetErrorMessageCore(string propertyName) => $"{propertyName} expecting value to be null or white space";


        }
        public static IValidationRule<TDbParams> IsNotNullOrWhiteSpace(Expression<Func<TDbParams, string>> selector) => new IsNotNullOrWhiteSpaceRule(selector);

        private class IsNotNullOrWhiteSpaceRule : ValidationRule<TDbParams>
        {
            public IsNotNullOrWhiteSpaceRule(Expression<Func<TDbParams, string>> selector) : base(selector, true) { }

            protected override bool ValidateRule() => !string.IsNullOrWhiteSpace(ParamsValue.ToString());

            protected override string GetErrorMessageCore(string propertyName) => $"{propertyName} expecting value to not be null or white space";

        }

        public static IValidationRule<TDbParams> MinLength(Expression<Func<TDbParams, string>> selector, int limit, bool isNullable = false) => new MinLengthRule(selector, limit, isNullable);

        private class MinLengthRule : ValidationRule<TDbParams>
        {
            
            private int LimitValue { get; set; }
            
            public MinLengthRule(Expression<Func<TDbParams, string>> selector, int limit, bool isNullable = false) : base(selector, isNullable) { LimitValue = limit; }
            
            protected override bool ValidateRule() => ParamsValue.ToString().Length >= LimitValue;
            
            protected override string GetErrorMessageCore(string propertyName) => $"{propertyName} expecting text length to be greater than or equal to {LimitValue}";
           

        }
        
        public static IValidationRule<TDbParams> MaxLength(Expression<Func<TDbParams, string>> selector, int limit, bool isNullable = false) => new MaxLengthRule(selector, limit, isNullable);

        private class MaxLengthRule : ValidationRule<TDbParams>
        {
            private int LimitValue { get; set; }
            public MaxLengthRule(Expression<Func<TDbParams, string>> selector, int limit, bool isNullable = false) : base(selector, isNullable) { LimitValue = limit; }

            protected override bool ValidateRule() => ParamsValue.ToString().Length <= LimitValue;

            protected override string GetErrorMessageCore(string propertyName) => $"{propertyName} expecting text length to be less than or equal to {LimitValue}";
            
        }

        
        public static IValidationRule<TDbParams> Email(Expression<Func<TDbParams, string>> selector, bool isNullable = false) => new EmailRule(selector, isNullable);
        public static IValidationRule<TDbParams> Email(Expression<Func<TDbParams, string>> selector, IEnumerable<string> domains, EmailDomainMode mode, bool isNullable = false) => new EmailRule(selector, domains, mode, isNullable);

        private class EmailRule : ValidationRule<TDbParams>
        {            
            private IEnumerable<string> Domains { get; set; }
            private EmailDomainMode Mode { get; set; }

            public EmailRule(Expression<Func<TDbParams, string>> selector, bool isNullable = false) : base(selector, isNullable) { Domains = new string[0]; Mode = EmailDomainMode.AllowAll; }
            public EmailRule(Expression<Func<TDbParams, string>> selector, IEnumerable<string> domains, EmailDomainMode mode, bool isNullable = false) : base(selector, isNullable) { Domains = domains; Mode = mode; }
            
            protected override bool ValidateRule()
            {
                try
                {
                    MailAddress email = new MailAddress(ParamsValue.ToString());
                    bool hasDomail = Domains.Contains(email.Host);
                    return Mode == EmailDomainMode.Whitelist ? hasDomail :
                        Mode == EmailDomainMode.Blacklist ? !hasDomail :
                        true;
                }
                catch
                {
                    return false;
                }
            }
           
            protected override string GetErrorMessageCore(string propertyName) => $"{propertyName} is an invalid email address";
           
        }
    }
}