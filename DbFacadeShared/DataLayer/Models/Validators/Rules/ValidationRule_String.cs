using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
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
        public static async Task<IValidationRule<TDbParams>> MatchAsync(Expression<Func<TDbParams, string>> selector,
            string regexMatchStr, bool isNullable = false)
        => await MatchRule.CreateAsync(selector, regexMatchStr, isNullable);
        public static async Task<IValidationRule<TDbParams>> MatchAsync(Expression<Func<TDbParams, string>> selector,
            string regexMatchStr, RegexOptions options, bool isNullable = false)
        => await MatchRule.CreateAsync(selector, regexMatchStr, options, isNullable);


        public static IValidationRule<TDbParams> IsNDigitString(Expression<Func<TDbParams, string>> selector,
            int length, bool isNullable = false)
        {
            return new IsNDigitStringRule(selector, length, isNullable);
        }
        public static async Task<IValidationRule<TDbParams>> IsNDigitStringAsync(Expression<Func<TDbParams, string>> selector,
            int length, bool isNullable = false)
        => await IsNDigitStringRule.CreateAsync(selector, length, isNullable);


        public static IValidationRule<TDbParams> IsSocialSecurityNumber(Expression<Func<TDbParams, string>> selector,
            bool allowDashes = true, bool isNullable = false)
        {
            return new IsSocialSecurityNumberRule(selector, allowDashes, isNullable);
        }
        public static async Task<IValidationRule<TDbParams>> IsSocialSecurityNumberAsync(Expression<Func<TDbParams, string>> selector,
            bool allowDashes = true, bool isNullable = false)
        => await IsSocialSecurityNumberRule.CreateAsync(selector, allowDashes, isNullable);

        public static IValidationRule<TDbParams> IsNullOrEmpty(Expression<Func<TDbParams, string>> selector)
        {
            return new IsNullOrEmptyRule(selector);
        }
        public static async Task<IValidationRule<TDbParams>> IsNullOrEmptyAsync(Expression<Func<TDbParams, string>> selector)
        => await IsNullOrEmptyRule.CreateAsync(selector);

        public static IValidationRule<TDbParams> IsNotNullOrEmpty(Expression<Func<TDbParams, string>> selector)
        {
            return new IsNotNullOrEmptyRule(selector);
        }
        public static async Task<IValidationRule<TDbParams>> IsNotNullOrEmptyAsync(Expression<Func<TDbParams, string>> selector)
        => await IsNotNullOrEmptyRule.CreateAsync(selector);

        public static IValidationRule<TDbParams> IsNullOrWhiteSpace(Expression<Func<TDbParams, string>> selector)
        {
            return new IsNullOrWhiteSpaceRule(selector);
        }
        public static async Task<IValidationRule<TDbParams>> IsNullOrWhiteSpaceAsync(Expression<Func<TDbParams, string>> selector)
        => await IsNullOrWhiteSpaceRule.CreateAsync(selector);

        public static IValidationRule<TDbParams> IsNotNullOrWhiteSpace(Expression<Func<TDbParams, string>> selector)
        {
            return new IsNotNullOrWhiteSpaceRule(selector);
        }
        public static async Task<IValidationRule<TDbParams>> IsNotNullOrWhiteSpaceAsync(Expression<Func<TDbParams, string>> selector)
        => await IsNotNullOrWhiteSpaceRule.CreateAsync(selector);

        public static IValidationRule<TDbParams> LengthEquals(Expression<Func<TDbParams, string>> selector, int limit,
            bool isNullable = false)
        {
            return new LengthEqualsRule(selector, limit, isNullable);
        }
        public static async Task<IValidationRule<TDbParams>> LengthEqualsAsync(Expression<Func<TDbParams, string>> selector, int limit,
            bool isNullable = false)
        => await LengthEqualsRule.CreateAsync(selector, limit, isNullable);

        public static IValidationRule<TDbParams> MinLength(Expression<Func<TDbParams, string>> selector, int limit,
            bool isNullable = false)
        {
            return new MinLengthRule(selector, limit, isNullable);
        }
        public static async Task<IValidationRule<TDbParams>> MinLengthAsync(Expression<Func<TDbParams, string>> selector, int limit,
            bool isNullable = false)
        => await MinLengthRule.CreateAsync(selector, limit, isNullable);

        public static IValidationRule<TDbParams> MaxLength(Expression<Func<TDbParams, string>> selector, int limit,
            bool isNullable = false)
        {
            return new MaxLengthRule(selector, limit, isNullable);
        }
        public static async Task<IValidationRule<TDbParams>> MaxLengthAsync(Expression<Func<TDbParams, string>> selector, int limit,
            bool isNullable = false)
        => await MaxLengthRule.CreateAsync(selector, limit, isNullable);

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

        public static async Task<IValidationRule<TDbParams>> EmailAsync(Expression<Func<TDbParams, string>> selector,
            bool isNullable = false)
        => await EmailRule.CreateAsync(selector, isNullable);

        public static async Task<IValidationRule<TDbParams>> EmailAsync(Expression<Func<TDbParams, string>> selector,
            IEnumerable<string> domains, EmailDomainMode mode, bool isNullable = false)
        => await EmailRule.CreateAsync(selector, domains, mode, isNullable);



        private class MatchRule : ValidationRule<TDbParams>
        {
            protected MatchRule(){}
            public MatchRule(Expression<Func<TDbParams, string>> selector, string regexMatchStr,
                bool isNullable = false) : this(selector, regexMatchStr, RegexOptions.IgnoreCase, isNullable) { }
            public MatchRule(Expression<Func<TDbParams, string>> selector, string regexMatchStr, RegexOptions options,
                bool isNullable = false)
            {
                Init(selector, isNullable);
                MatchStr = regexMatchStr;
                RegexOptions = options;
            }
            public static async Task<MatchRule> CreateAsync(Expression<Func<TDbParams, string>> selector, string regexMatchStr, RegexOptions options,
                bool isNullable = false)
            {
                MatchRule rule = new MatchRule();
                await rule.InitAsync(selector, regexMatchStr, options, isNullable);                
                return rule;
            }
            public static async Task<MatchRule> CreateAsync(Expression<Func<TDbParams, string>> selector, string regexMatchStr,
                bool isNullable = false)
            => await CreateAsync(selector, regexMatchStr, RegexOptions.IgnoreCase, isNullable);

            protected async Task InitAsync(Expression<Func<TDbParams, string>> selector, string regexMatchStr, RegexOptions options,
                bool isNullable = false)
            {
                MatchStr = regexMatchStr;
                RegexOptions = options;
                await InitAsync(selector, isNullable);
            }
            protected async Task InitAsync(Expression<Func<TDbParams, string>> selector, string regexMatchStr,
                bool isNullable = false)
            {
                await InitAsync(selector, regexMatchStr, RegexOptions.IgnoreCase, isNullable);
            }


            private string MatchStr { get; set; }

            internal RegexOptions RegexOptions { get; private set; }

            protected override bool ValidateRule()
            => Regex.Match(ParamsValue.ToString(), MatchStr, RegexOptions).Success;
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool val = Regex.Match(ParamsValue.ToString(), MatchStr, RegexOptions).Success;
                await Task.CompletedTask;
                return val;
            }

            protected override string GetErrorMessageCore(string propertyName)
            => $"{propertyName} does not match the expression.";

            protected override async Task<string> GetErrorMessageCoreAsync(string propertyName)
            {
                await Task.CompletedTask;
                return $"{propertyName} does not match the expression."; 
            }

        }

        private sealed class IsNDigitStringRule : MatchRule
        {
            private IsNDigitStringRule() { }
            public IsNDigitStringRule(Expression<Func<TDbParams, string>> selector, int length, bool isNullable = false)
                : base(selector, $"(?<!\\d)\\d{{{length}}}(?!\\d)", isNullable)
            {
                Length = length;
            }
            public static async Task<IsNDigitStringRule> CreateAsync(Expression<Func<TDbParams, string>> selector, int length, bool isNullable = false)
            {
                IsNDigitStringRule rule = new IsNDigitStringRule();
                await rule.InitAsync(selector, length, isNullable);
                return rule;
            }
            private async Task InitAsync(Expression<Func<TDbParams, string>> selector, int length, bool isNullable = false)
            {
                Length = length;
                await InitAsync(selector, $"(?<!\\d)\\d{{{length}}}(?!\\d)", isNullable);
            }
            private int Length { get; set; }


            protected override bool ValidateRule()
            {
                return Length > 0 && ParamsValue.ToString().Length == Length && base.ValidateRule();
            }
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isLengthN = Length > 0 && ParamsValue.ToString().Length == Length;
                bool baseValidation = await base.ValidateRuleAsync();                
                return isLengthN && baseValidation;
            }

            protected override string GetErrorMessageCore(string propertyName)
            => (Length <= 0 || ParamsValue.ToString().Length != Length) ?
                $"Length of {propertyName} is not equal to {Length}":
                $"{propertyName} is not a {Length} digit string";

            protected override async Task<string> GetErrorMessageCoreAsync(string propertyName)
            {
                string message = (Length <= 0 || ParamsValue.ToString().Length != Length) ?
                $"Length of {propertyName} is not equal to {Length}" :
                $"{propertyName} is not a {Length} digit string";
                await Task.CompletedTask;
                return message;
            }
        }

        private sealed class IsSocialSecurityNumberRule : MatchRule
        {
            private static readonly string SSNMatch =
                "^(?!219-09-9999|078-05-1120)((?!666|000|9\\d{2})\\d{3}-?(?!00)\\d{2}-?(?!0{4})\\d{4})$";

            private static readonly string SSNMatchNoDashes =
                "^(?!219099999|078051120)((?!666|000|9\\d{2})\\d{3}?(?!00)\\d{2}?(?!0{4})\\d{4})$";

            private IsSocialSecurityNumberRule(){}
            public IsSocialSecurityNumberRule(Expression<Func<TDbParams, string>> selector, bool allowDashes = true,
                bool isNullable = false) : base(selector, SSNMatchNoDashes, isNullable) {
                AllowDashes = allowDashes;
            }
            public static async Task<IsSocialSecurityNumberRule> CreateAsync(Expression<Func<TDbParams, string>> selector, bool allowDashes = true,
                bool isNullable = false)
            {
                IsSocialSecurityNumberRule rule = new IsSocialSecurityNumberRule();
                rule.AllowDashes = allowDashes;
                await rule.InitAsync(selector, SSNMatchNoDashes, isNullable);
                return rule;
            }
            private bool AllowDashes { get; set; }

            protected override bool ValidateRule()
            {
                var baseValidation = base.ValidateRule();
                if (AllowDashes)
                {
                    var sSnMatchResult = Regex.Match(ParamsValue.ToString(), SSNMatch, RegexOptions.IgnoreCase);
                    return baseValidation || sSnMatchResult.Success;
                }

                return baseValidation;
            }
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isValid = await base.ValidateRuleAsync();
                if (AllowDashes)
                {
                    isValid = isValid || Regex.Match(ParamsValue.ToString(), SSNMatch, RegexOptions.IgnoreCase).Success;
                }
                await Task.CompletedTask;
                return isValid;
            }

            protected override string GetErrorMessageCore(string propertyName)
            => $"{propertyName} is not a valid Social Security Number";
            protected override async Task<string> GetErrorMessageCoreAsync(string propertyName)
            {
                await Task.CompletedTask;
                return $"{propertyName} is not a valid Social Security Number";
            }
            
        }

        private class IsNullOrEmptyRule : ValidationRule<TDbParams>
        {
            private IsNullOrEmptyRule(){ }
            public IsNullOrEmptyRule(Expression<Func<TDbParams, string>> selector)
            {
                Init(selector, true);
            }
            public static async Task<IsNullOrEmptyRule> CreateAsync(Expression<Func<TDbParams, string>> selector)
            {
                IsNullOrEmptyRule rule = new IsNullOrEmptyRule();
                await rule.InitAsync(selector, true);
                return rule;
            }


            protected override bool ValidateRule()
            {
                return string.IsNullOrEmpty(ParamsValue.ToString());
            }
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isValid = string.IsNullOrEmpty(ParamsValue.ToString());
                await Task.CompletedTask;
                return isValid;
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} expecting value to be null or empty";
            }
            protected override async Task<string> GetErrorMessageCoreAsync(string propertyName)
            {
                await Task.CompletedTask;
                return $"{propertyName} expecting value to be null or empty";
            }
        }

        private class IsNotNullOrEmptyRule : ValidationRule<TDbParams>
        {
            private IsNotNullOrEmptyRule() { }
            public IsNotNullOrEmptyRule(Expression<Func<TDbParams, string>> selector)
            {
                Init(selector, true);
            }
            public static async Task<IsNotNullOrEmptyRule> CreateAsync(Expression<Func<TDbParams, string>> selector)
            {
                IsNotNullOrEmptyRule rule = new IsNotNullOrEmptyRule();
                await rule.InitAsync(selector, true);
                return rule;
            }
            protected override bool ValidateRule()
            {
                return !string.IsNullOrEmpty(ParamsValue.ToString());
            }
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isValid = !string.IsNullOrEmpty(ParamsValue.ToString());
                await Task.CompletedTask;
                return isValid;
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} expecting value to not be null or empty";
            }
            protected override async Task<string> GetErrorMessageCoreAsync(string propertyName)
            {
                await Task.CompletedTask;
                return $"{propertyName} expecting value to not be null or empty";
            }
        }

        private class IsNullOrWhiteSpaceRule : ValidationRule<TDbParams>
        {
            private IsNullOrWhiteSpaceRule() { }
            public IsNullOrWhiteSpaceRule(Expression<Func<TDbParams, string>> selector)
            {
                Init(selector, true);
            }
            public static async Task<IsNullOrWhiteSpaceRule> CreateAsync(Expression<Func<TDbParams, string>> selector)
            {
                IsNullOrWhiteSpaceRule rule = new IsNullOrWhiteSpaceRule();
                await rule.InitAsync(selector, true);
                return rule;
            }
            protected override bool ValidateRule()
            {
                return string.IsNullOrWhiteSpace(ParamsValue.ToString());
            }
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isValid = string.IsNullOrWhiteSpace(ParamsValue.ToString());
                await Task.CompletedTask;
                return isValid;
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} expecting value to be null or white space";
            }
            protected override async Task<string> GetErrorMessageCoreAsync(string propertyName)
            {
                await Task.CompletedTask;
                return $"{propertyName} expecting value to be null or white space";
            }
        }

        private class IsNotNullOrWhiteSpaceRule : ValidationRule<TDbParams>
        {
            private IsNotNullOrWhiteSpaceRule() { }
            public IsNotNullOrWhiteSpaceRule(Expression<Func<TDbParams, string>> selector)
            {
                Init(selector, true);
            }
            public static async Task<IsNotNullOrWhiteSpaceRule> CreateAsync(Expression<Func<TDbParams, string>> selector)
            {
                IsNotNullOrWhiteSpaceRule rule = new IsNotNullOrWhiteSpaceRule();
                await rule.InitAsync(selector, true);
                return rule;
            }
            protected override bool ValidateRule()
            {
                return !string.IsNullOrWhiteSpace(ParamsValue.ToString());
            }
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isValid = !string.IsNullOrWhiteSpace(ParamsValue.ToString());
                await Task.CompletedTask;
                return isValid;
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} expecting value to not be null or white space";
            }
            protected override async Task<string> GetErrorMessageCoreAsync(string propertyName)
            {
                await Task.CompletedTask;
                return $"{propertyName} expecting value to not be null or white space";
            }

        }

        private class LengthEqualsRule : ValidationRule<TDbParams>
        {
            protected LengthEqualsRule() { }
            public LengthEqualsRule(Expression<Func<TDbParams, string>> selector, int limit, bool isNullable = false)
            {
                Init(selector, isNullable);
                LimitValue = limit;
            }
            public static async Task<LengthEqualsRule> CreateAsync(Expression<Func<TDbParams, string>> selector, int limit, bool isNullable = false)
            {
                LengthEqualsRule rule = new LengthEqualsRule();
                await rule.InitAsync(selector, limit, isNullable);
                return rule;
            }
            public async Task InitAsync(Expression<Func<TDbParams, string>> selector, int limit, bool isNullable = false)
            {
                LimitValue = limit;
                await InitAsync(selector, isNullable);
            }
            private int LimitValue { get; set; }

            protected override bool ValidateRule()
            {
                return ParamsValue.ToString().Length == LimitValue;
            }
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isValid = ParamsValue.ToString().Length == LimitValue;
                await Task.CompletedTask;
                return isValid;
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} expecting text length to  equal to {LimitValue}";
            }
            protected override async Task<string> GetErrorMessageCoreAsync(string propertyName)
            {
                await Task.CompletedTask;
                return $"{propertyName} expecting text length to  equal to {LimitValue}";
            }
        }

        private class MinLengthRule : ValidationRule<TDbParams>
        {
            protected MinLengthRule() { }
            public MinLengthRule(Expression<Func<TDbParams, string>> selector, int limit, bool isNullable = false)
            {
                Init(selector, isNullable);
                LimitValue = limit;
            }
            public static async Task<MinLengthRule> CreateAsync(Expression<Func<TDbParams, string>> selector, int limit, bool isNullable = false)
            {
                MinLengthRule rule = new MinLengthRule();
                await rule.InitAsync(selector, limit, isNullable);
                return rule;
            }
            public async Task InitAsync(Expression<Func<TDbParams, string>> selector, int limit, bool isNullable = false)
            {
                LimitValue = limit;
                await InitAsync(selector, isNullable);
            }
            private int LimitValue { get; set; }


            protected override bool ValidateRule()
            {
                return ParamsValue.ToString().Length >= LimitValue;
            }
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isValid = ParamsValue.ToString().Length >= LimitValue;
                await Task.CompletedTask;
                return isValid;
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} expecting text length to be greater than or equal to {LimitValue}";
            }
            protected override async Task<string> GetErrorMessageCoreAsync(string propertyName)
            {
                await Task.CompletedTask;
                return $"{propertyName} expecting text length to be greater than or equal to {LimitValue}";
            }
        }

        private class MaxLengthRule : ValidationRule<TDbParams>
        {
            protected MaxLengthRule() { }
            public MaxLengthRule(Expression<Func<TDbParams, string>> selector, int limit, bool isNullable = false)
            {
                Init(selector, isNullable);
                LimitValue = limit;
            }
            public static async Task<MaxLengthRule> CreateAsync(Expression<Func<TDbParams, string>> selector, int limit, bool isNullable = false)
            {
                MaxLengthRule rule = new MaxLengthRule();
                await rule.InitAsync(selector, limit, isNullable);
                return rule;
            }
            public async Task InitAsync(Expression<Func<TDbParams, string>> selector, int limit, bool isNullable = false)
            {
                LimitValue = limit;
                await InitAsync(selector, isNullable);
            }
            private int LimitValue { get; set; }


            protected override bool ValidateRule()
            {
                return ParamsValue.ToString().Length <= LimitValue;
            }
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isValid = ParamsValue.ToString().Length <= LimitValue;
                await Task.CompletedTask;
                return isValid;
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} expecting text length to be less than or equal to {LimitValue}";
            }
            protected override async Task<string> GetErrorMessageCoreAsync(string propertyName)
            {
                await Task.CompletedTask;
                return $"{propertyName} expecting text length to be less than or equal to {LimitValue}";
            }
        }

        private class EmailRule : ValidationRule<TDbParams>
        {
            protected EmailRule() { }
            public EmailRule(Expression<Func<TDbParams, string>> selector, bool isNullable = false)
                : this(selector, new string[0], EmailDomainMode.AllowAll, isNullable) { }

            public EmailRule(Expression<Func<TDbParams, string>> selector, IEnumerable<string> domains,
                EmailDomainMode mode, bool isNullable = false)
            {
                Init(selector, isNullable);
                Domains = domains;
                Mode = mode;
            }
            public static async Task<EmailRule> CreateAsync(Expression<Func<TDbParams, string>> selector, bool isNullable = false)
            => await CreateAsync(selector, new string[0], EmailDomainMode.AllowAll, isNullable);
            public static async Task<EmailRule> CreateAsync(Expression<Func<TDbParams, string>> selector, IEnumerable<string> domains,
                EmailDomainMode mode, bool isNullable = false) {
                EmailRule rule = new EmailRule();
                rule.Domains = domains;
                rule.Mode = mode;
                await rule.InitAsync(selector, isNullable);
                return rule;
            }

            private IEnumerable<string> Domains { get; set; }
            private EmailDomainMode Mode { get; set; }

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
            private async Task<MailAddress> CreateMailAddressAsync()
            {
                MailAddress mailAddress = new MailAddress(ParamsValue.ToString());
                await Task.CompletedTask;
                return mailAddress;
            }
            private async Task<bool> ValidateMailAddressAsync(MailAddress email)
            {
                bool hasDomain = Domains.Contains(email.Host);
                bool isValid = Mode == EmailDomainMode.Whitelist ? hasDomain :
                        Mode == EmailDomainMode.Blacklist ? !hasDomain :
                        true;
                await Task.CompletedTask;
                return isValid;
            }
            protected override async Task<bool> ValidateRuleAsync()
            {
                try
                {
                    MailAddress email = await CreateMailAddressAsync();
                    return await ValidateMailAddressAsync(email);
                }
                catch
                {
                    await Task.CompletedTask;
                    return false;
                }
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} is an invalid email address";
            }
            protected override async Task<string> GetErrorMessageCoreAsync(string propertyName)
            {
                await Task.CompletedTask;
                return $"{propertyName} is an invalid email address";
            }
        }
    }
}