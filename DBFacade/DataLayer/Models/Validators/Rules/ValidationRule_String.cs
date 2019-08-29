using DBFacade.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DBFacade.DataLayer.Models.Validators.Rules
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{TDbParams}" />
    public abstract partial class ValidationRule<TDbParams>
        where TDbParams : IDbParamsModel
    {
        public IValidationRule<TDbParams> Match(TDbParams paramsModel, Func<TDbParams, string> selector, string regexMatchStr, bool isNullable = false) => new MatchRule(paramsModel, selector, regexMatchStr, isNullable);
        public IValidationRule<TDbParams> Match(TDbParams paramsModel, Func<TDbParams, string> selector, string regexMatchStr, RegexOptions options, bool isNullable = false) => new MatchRule(paramsModel, selector, regexMatchStr, options, isNullable);

        private class MatchRule : ValidationRule<TDbParams>
        {
            internal string MatchStr { get; private set; }
            
            internal RegexOptions RegexOptions { get; private set; }
            
            public MatchRule(TDbParams paramsModel, Func<TDbParams, string> selector, string regexMatchStr, bool isNullable = false) : base(selector, isNullable)
            {
                MatchStr = regexMatchStr;
                RegexOptions = RegexOptions.IgnoreCase;
            }
           
            public MatchRule(TDbParams paramsModel, Func<TDbParams, string> selector, string regexMatchStr, RegexOptions options, bool isNullable = false) : base(selector, isNullable)
            {
                MatchStr = regexMatchStr;
                RegexOptions = options;
            }
            
            protected override bool ValidateRule()
            {
                System.Text.RegularExpressions.Match MatchRegex = Regex.Match(ParamsValue.ToString(), MatchStr, RegexOptions);
                return MatchRegex.Success;
            }
           
            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} does not match the expression.";
            }

        }

        public IValidationRule<TDbParams> MinLength(Func<TDbParams, string> selector, int limit, bool isNullable = false) => new MinLengthRule(selector, limit, isNullable);

        private class MinLengthRule : ValidationRule<TDbParams>
        {
            
            private int LimitValue { get; set; }
            
            public MinLengthRule(Func<TDbParams, string> selector, int limit, bool isNullable = false) : base(selector, isNullable) { LimitValue = limit; }
            
            protected override bool ValidateRule() => ParamsValue.ToString().Length >= LimitValue;
            
            protected override string GetErrorMessageCore(string propertyName) => $"{propertyName} expecting text length to be greater than or equal to {LimitValue}";
           

        }
        
        public IValidationRule<TDbParams> MaxLength(Func<TDbParams, string> selector, int limit, bool isNullable = false) => new MaxLengthRule(selector, limit, isNullable);

        private class MaxLengthRule : ValidationRule<TDbParams>
        {
            private int LimitValue { get; set; }
            public MaxLengthRule(Func<TDbParams, string> selector, int limit, bool isNullable = false) : base(selector, isNullable) { LimitValue = limit; }

            protected override bool ValidateRule() => ParamsValue.ToString().Length <= LimitValue;

            protected override string GetErrorMessageCore(string propertyName) => $"{propertyName} expecting text length to be less than or equal to {LimitValue}";
            
        }

        public enum EmailDomainMode
        {
            AllowAll = 0,
            Whitelist = 1,
            Blacklist = 2
        }
        public IValidationRule<TDbParams> Email(Func<TDbParams, string> selector, bool isNullable = false) => new EmailRule(selector, isNullable);
        public IValidationRule<TDbParams> Email(Func<TDbParams, string> selector, IEnumerable<string> domains, EmailDomainMode mode, bool isNullable = false) => new EmailRule(selector, domains, mode, isNullable);

        private class EmailRule : ValidationRule<TDbParams>
        {            
            private IEnumerable<string> Domains { get; set; }
            private EmailDomainMode Mode { get; set; }

            public EmailRule(Func<TDbParams, string> selector, bool isNullable = false) : base(selector, isNullable) { Domains = new string[0]; Mode = EmailDomainMode.AllowAll; }
            public EmailRule(Func<TDbParams, string> selector, IEnumerable<string> domains, EmailDomainMode mode, bool isNullable = false) : base(selector, isNullable) { Domains = domains; Mode = mode; }
            
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