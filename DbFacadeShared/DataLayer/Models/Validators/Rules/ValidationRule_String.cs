using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// 
    /// </summary>
    public enum EmailDomainMode
    {
        /// <summary>
        /// The allow all
        /// </summary>
        AllowAll = 0,
        /// <summary>
        /// The whitelist
        /// </summary>
        Whitelist = 1,
        /// <summary>
        /// The blacklist
        /// </summary>
        Blacklist = 2
    }

    /// <summary>
    ///   <br />
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public abstract partial class ValidationRule<TDbParams>
    {
        /// <summary>
        /// 
        /// </summary>
        internal class MatchRule : ValidationRule<TDbParams>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="MatchRule"/> class.
            /// </summary>
            protected MatchRule(){}
            /// <summary>
            /// Initializes a new instance of the <see cref="MatchRule"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="regexMatchStr">The regex match string.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            public MatchRule(Func<TDbParams, string> selector, string regexMatchStr,
                bool isNullable = false) : this(selector, regexMatchStr, RegexOptions.IgnoreCase, isNullable) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="MatchRule"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="regexMatchStr">The regex match string.</param>
            /// <param name="options">The options.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            public MatchRule(Func<TDbParams, string> selector, string regexMatchStr, RegexOptions options,
                bool isNullable = false)
            {
                Init(selector, isNullable);
                MatchStr = regexMatchStr;
                RegexOptions = options;
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="regexMatchStr">The regex match string.</param>
            /// <param name="options">The options.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            public static async Task<MatchRule> CreateAsync(Func<TDbParams, string> selector, string regexMatchStr, RegexOptions options,
                bool isNullable = false)
            {
                MatchRule rule = new MatchRule();
                await rule.InitAsync(selector, regexMatchStr, options, isNullable);                
                return rule;
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="regexMatchStr">The regex match string.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            public static async Task<MatchRule> CreateAsync(Func<TDbParams, string> selector, string regexMatchStr,
                bool isNullable = false)
            => await CreateAsync(selector, regexMatchStr, RegexOptions.IgnoreCase, isNullable);

            /// <summary>
            /// Initializes the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="regexMatchStr">The regex match string.</param>
            /// <param name="options">The options.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            protected async Task InitAsync(Func<TDbParams, string> selector, string regexMatchStr, RegexOptions options,
                bool isNullable = false)
            {
                MatchStr = regexMatchStr;
                RegexOptions = options;
                await InitAsync(selector, isNullable);
            }
            /// <summary>
            /// Initializes the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="regexMatchStr">The regex match string.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            protected async Task InitAsync(Func<TDbParams, string> selector, string regexMatchStr,
                bool isNullable = false)
            {
                await InitAsync(selector, regexMatchStr, RegexOptions.IgnoreCase, isNullable);
            }


            /// <summary>
            /// Gets or sets the match string.
            /// </summary>
            /// <value>
            /// The match string.
            /// </value>
            private string MatchStr { get; set; }

            /// <summary>
            /// Gets the regex options.
            /// </summary>
            /// <value>
            /// The regex options.
            /// </value>
            internal RegexOptions RegexOptions { get; private set; }

            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            => Regex.Match(ParamsValue.ToString(), MatchStr, RegexOptions).Success;
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool val = Regex.Match(ParamsValue.ToString(), MatchStr, RegexOptions).Success;
                await Task.CompletedTask;
                return val;
            }

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <returns></returns>
            protected override string GetErrorMessageCore()
            => $"does not match the expression.";

            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                await Task.CompletedTask;
                return $"does not match the expression."; 
            }

        }

        /// <summary>
        /// 
        /// </summary>
        internal sealed class IsNDigitStringRule : MatchRule
        {
            /// <summary>
            /// Prevents a default instance of the <see cref="IsNDigitStringRule"/> class from being created.
            /// </summary>
            private IsNDigitStringRule() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="IsNDigitStringRule"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="length">The length.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            public IsNDigitStringRule(Func<TDbParams, string> selector, int length, bool isNullable = false)
                : base(selector, $"(?<!\\d)\\d{{{length}}}(?!\\d)", isNullable)
            {
                Length = length;
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="length">The length.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            public static async Task<IsNDigitStringRule> CreateAsync(Func<TDbParams, string> selector, int length, bool isNullable = false)
            {
                IsNDigitStringRule rule = new IsNDigitStringRule();
                await rule.InitAsync(selector, length, isNullable);
                return rule;
            }
            /// <summary>
            /// Initializes the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="length">The length.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            private async Task InitAsync(Func<TDbParams, string>  selector, int length, bool isNullable = false)
            {
                Length = length;
                await InitAsync(selector, $"(?<!\\d)\\d{{{length}}}(?!\\d)", isNullable);
            }
            /// <summary>
            /// Gets or sets the length.
            /// </summary>
            /// <value>
            /// The length.
            /// </value>
            private int Length { get; set; }


            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                return Length > 0 && ParamsValue.ToString().Length == Length && base.ValidateRule();
            }
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isLengthN = Length > 0 && ParamsValue.ToString().Length == Length;
                bool baseValidation = await base.ValidateRuleAsync();                
                return isLengthN && baseValidation;
            }

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <returns></returns>
            protected override string GetErrorMessageCore()
            => (Length <= 0 || ParamsValue.ToString().Length != Length) ?
                $"Length is not equal to {Length}":
                $"not a {Length} digit string";

            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                string message = (Length <= 0 || ParamsValue.ToString().Length != Length) ?
                $"Length is not equal to {Length}" :
                $"not a {Length} digit string";
                await Task.CompletedTask;
                return message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal sealed class IsSocialSecurityNumberRule : MatchRule
        {
            /// <summary>
            /// The SSN match
            /// </summary>
            private static readonly string SSNMatch =
                "^(?!219-09-9999|078-05-1120)((?!666|000|9\\d{2})\\d{3}-?(?!00)\\d{2}-?(?!0{4})\\d{4})$";

            /// <summary>
            /// The SSN match no dashes
            /// </summary>
            private static readonly string SSNMatchNoDashes =
                "^(?!219099999|078051120)((?!666|000|9\\d{2})\\d{3}?(?!00)\\d{2}?(?!0{4})\\d{4})$";

            /// <summary>
            /// Prevents a default instance of the <see cref="IsSocialSecurityNumberRule"/> class from being created.
            /// </summary>
            private IsSocialSecurityNumberRule(){}
            /// <summary>
            /// Initializes a new instance of the <see cref="IsSocialSecurityNumberRule"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="allowDashes">if set to <c>true</c> [allow dashes].</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            public IsSocialSecurityNumberRule(Func<TDbParams, string> selector, bool allowDashes = true,
                bool isNullable = false) : base(selector, SSNMatchNoDashes, isNullable) {
                AllowDashes = allowDashes;
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="allowDashes">if set to <c>true</c> [allow dashes].</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            public static async Task<IsSocialSecurityNumberRule> CreateAsync(Func<TDbParams, string> selector, bool allowDashes = true,
                bool isNullable = false)
            {
                IsSocialSecurityNumberRule rule = new IsSocialSecurityNumberRule();
                rule.AllowDashes = allowDashes;
                await rule.InitAsync(selector, SSNMatchNoDashes, isNullable);
                return rule;
            }
            /// <summary>
            /// Gets or sets a value indicating whether [allow dashes].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [allow dashes]; otherwise, <c>false</c>.
            /// </value>
            private bool AllowDashes { get; set; }

            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
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
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <returns></returns>
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

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <returns></returns>
            protected override string GetErrorMessageCore()
            => $"not a valid Social Security Number";
            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                await Task.CompletedTask;
                return $"not a valid Social Security Number";
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        internal sealed class IsNullOrEmptyRule : ValidationRule<TDbParams>
        {
            /// <summary>
            /// Prevents a default instance of the <see cref="IsNullOrEmptyRule"/> class from being created.
            /// </summary>
            private IsNullOrEmptyRule(){ }
            /// <summary>
            /// Initializes a new instance of the <see cref="IsNullOrEmptyRule"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            public IsNullOrEmptyRule(Func<TDbParams, string> selector)
            {
                Init(selector, true);
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <returns></returns>
            public static async Task<IsNullOrEmptyRule> CreateAsync(Func<TDbParams, string> selector)
            {
                IsNullOrEmptyRule rule = new IsNullOrEmptyRule();
                await rule.InitAsync(selector, true);
                return rule;
            }


            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                return string.IsNullOrEmpty(ParamsValue.ToString());
            }
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isValid = string.IsNullOrEmpty(ParamsValue.ToString());
                await Task.CompletedTask;
                return isValid;
            }

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <returns></returns>
            protected override string GetErrorMessageCore()
            {
                return $"expecting value to be null or empty";
            }
            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                await Task.CompletedTask;
                return $"expecting value to be null or empty";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal sealed class IsNotNullOrEmptyRule : ValidationRule<TDbParams>
        {
            /// <summary>
            /// Prevents a default instance of the <see cref="IsNotNullOrEmptyRule"/> class from being created.
            /// </summary>
            private IsNotNullOrEmptyRule() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="IsNotNullOrEmptyRule"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            public IsNotNullOrEmptyRule(Func<TDbParams, string> selector)
            {
                Init(selector, true);
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <returns></returns>
            public static async Task<IsNotNullOrEmptyRule> CreateAsync(Func<TDbParams, string> selector)
            {
                IsNotNullOrEmptyRule rule = new IsNotNullOrEmptyRule();
                await rule.InitAsync(selector, true);
                return rule;
            }
            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                return !string.IsNullOrEmpty(ParamsValue.ToString());
            }
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isValid = !string.IsNullOrEmpty(ParamsValue.ToString());
                await Task.CompletedTask;
                return isValid;
            }

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <returns></returns>
            protected override string GetErrorMessageCore()
            {
                return $"expecting value to not be null or empty";
            }
            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                await Task.CompletedTask;
                return $"expecting value to not be null or empty";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal sealed class IsNullOrWhiteSpaceRule : ValidationRule<TDbParams>
        {
            /// <summary>
            /// Prevents a default instance of the <see cref="IsNullOrWhiteSpaceRule"/> class from being created.
            /// </summary>
            private IsNullOrWhiteSpaceRule() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="IsNullOrWhiteSpaceRule"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            public IsNullOrWhiteSpaceRule(Func<TDbParams, string> selector)
            {
                Init(selector, true);
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <returns></returns>
            public static async Task<IsNullOrWhiteSpaceRule> CreateAsync(Func<TDbParams, string> selector)
            {
                IsNullOrWhiteSpaceRule rule = new IsNullOrWhiteSpaceRule();
                await rule.InitAsync(selector, true);
                return rule;
            }
            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                return string.IsNullOrWhiteSpace(ParamsValue.ToString());
            }
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isValid = string.IsNullOrWhiteSpace(ParamsValue.ToString());
                await Task.CompletedTask;
                return isValid;
            }

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <returns></returns>
            protected override string GetErrorMessageCore()
            {
                return $"expecting value to be null or white space";
            }
            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                await Task.CompletedTask;
                return $"expecting value to be null or white space";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal sealed class IsNotNullOrWhiteSpaceRule : ValidationRule<TDbParams>
        {
            /// <summary>
            /// Prevents a default instance of the <see cref="IsNotNullOrWhiteSpaceRule"/> class from being created.
            /// </summary>
            private IsNotNullOrWhiteSpaceRule() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="IsNotNullOrWhiteSpaceRule"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            public IsNotNullOrWhiteSpaceRule(Func<TDbParams, string> selector)
            {
                Init(selector, true);
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <returns></returns>
            public static async Task<IsNotNullOrWhiteSpaceRule> CreateAsync(Func<TDbParams, string> selector)
            {
                IsNotNullOrWhiteSpaceRule rule = new IsNotNullOrWhiteSpaceRule();
                await rule.InitAsync(selector, true);
                return rule;
            }
            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                return !string.IsNullOrWhiteSpace(ParamsValue.ToString());
            }
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isValid = !string.IsNullOrWhiteSpace(ParamsValue.ToString());
                await Task.CompletedTask;
                return isValid;
            }

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <returns></returns>
            protected override string GetErrorMessageCore()
            {
                return $"expecting value to not be null or white space";
            }
            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                await Task.CompletedTask;
                return $"expecting value to not be null or white space";
            }

        }

        /// <summary>
        /// 
        /// </summary>
        internal sealed class LengthEqualsRule : ValidationRule<TDbParams>
        {
            /// <summary>
            /// Prevents a default instance of the <see cref="LengthEqualsRule"/> class from being created.
            /// </summary>
            private LengthEqualsRule() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="LengthEqualsRule"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            public LengthEqualsRule(Func<TDbParams, string> selector, int limit, bool isNullable = false)
            {
                Init(selector, isNullable);
                LimitValue = limit;
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            public static async Task<LengthEqualsRule> CreateAsync(Func<TDbParams, string> selector, int limit, bool isNullable = false)
            {
                LengthEqualsRule rule = new LengthEqualsRule();
                await rule.InitAsync(selector, limit, isNullable);
                return rule;
            }
            /// <summary>
            /// Initializes the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            public async Task InitAsync(Func<TDbParams, string> selector, int limit, bool isNullable = false)
            {
                LimitValue = limit;
                await InitAsync(selector, isNullable);
            }
            /// <summary>
            /// Gets or sets the limit value.
            /// </summary>
            /// <value>
            /// The limit value.
            /// </value>
            private int LimitValue { get; set; }

            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                return ParamsValue.ToString().Length == LimitValue;
            }
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isValid = ParamsValue.ToString().Length == LimitValue;
                await Task.CompletedTask;
                return isValid;
            }

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <returns></returns>
            protected override string GetErrorMessageCore()
            {
                return $"expecting text length to  equal to {LimitValue}";
            }
            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                await Task.CompletedTask;
                return $"expecting text length to  equal to {LimitValue}";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal sealed class MinLengthRule : ValidationRule<TDbParams>
        {
            /// <summary>
            /// Prevents a default instance of the <see cref="MinLengthRule"/> class from being created.
            /// </summary>
            private MinLengthRule() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="MinLengthRule"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            public MinLengthRule(Func<TDbParams, string> selector, int limit, bool isNullable = false)
            {
                Init(selector, isNullable);
                LimitValue = limit;
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            public static async Task<MinLengthRule> CreateAsync(Func<TDbParams, string> selector, int limit, bool isNullable = false)
            {
                MinLengthRule rule = new MinLengthRule();
                await rule.InitAsync(selector, limit, isNullable);
                return rule;
            }
            /// <summary>
            /// Initializes the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            public async Task InitAsync(Func<TDbParams, string> selector, int limit, bool isNullable = false)
            {
                LimitValue = limit;
                await InitAsync(selector, isNullable);
            }
            /// <summary>
            /// Gets or sets the limit value.
            /// </summary>
            /// <value>
            /// The limit value.
            /// </value>
            private int LimitValue { get; set; }


            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                return ParamsValue.ToString().Length >= LimitValue;
            }
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isValid = ParamsValue.ToString().Length >= LimitValue;
                await Task.CompletedTask;
                return isValid;
            }

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <returns></returns>
            protected override string GetErrorMessageCore()
            {
                return $"expecting text length to be greater than or equal to {LimitValue}";
            }
            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                await Task.CompletedTask;
                return $"expecting text length to be greater than or equal to {LimitValue}";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal sealed class MaxLengthRule : ValidationRule<TDbParams>
        {
            /// <summary>
            /// Prevents a default instance of the <see cref="MaxLengthRule"/> class from being created.
            /// </summary>
            private MaxLengthRule() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="MaxLengthRule"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            public MaxLengthRule(Func<TDbParams, string> selector, int limit, bool isNullable = false)
            {
                Init(selector, isNullable);
                LimitValue = limit;
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            public static async Task<MaxLengthRule> CreateAsync(Func<TDbParams, string> selector, int limit, bool isNullable = false)
            {
                MaxLengthRule rule = new MaxLengthRule();
                await rule.InitAsync(selector, limit, isNullable);
                return rule;
            }
            /// <summary>
            /// Initializes the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            public async Task InitAsync(Func<TDbParams, string> selector, int limit, bool isNullable = false)
            {
                LimitValue = limit;
                await InitAsync(selector, isNullable);
            }
            /// <summary>
            /// Gets or sets the limit value.
            /// </summary>
            /// <value>
            /// The limit value.
            /// </value>
            private int LimitValue { get; set; }


            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                return ParamsValue.ToString().Length <= LimitValue;
            }
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync()
            {
                bool isValid = ParamsValue.ToString().Length <= LimitValue;
                await Task.CompletedTask;
                return isValid;
            }

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <returns></returns>
            protected override string GetErrorMessageCore()
            {
                return $"expecting text length to be less than or equal to {LimitValue}";
            }
            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                await Task.CompletedTask;
                return $"expecting text length to be less than or equal to {LimitValue}";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal sealed class EmailRule : ValidationRule<TDbParams>
        {
            /// <summary>
            /// Prevents a default instance of the <see cref="EmailRule"/> class from being created.
            /// </summary>
            private EmailRule() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="EmailRule"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            public EmailRule(Func<TDbParams, string> selector, bool isNullable = false)
                : this(selector, new string[0], EmailDomainMode.AllowAll, isNullable) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="EmailRule"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="domains">The domains.</param>
            /// <param name="mode">The mode.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            public EmailRule(Func<TDbParams, string> selector, IEnumerable<string> domains,
                EmailDomainMode mode, bool isNullable = false)
            {
                Init(selector, isNullable);
                Domains = domains;
                Mode = mode;
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            public static async Task<EmailRule> CreateAsync(Func<TDbParams, string> selector, bool isNullable = false)
            => await CreateAsync(selector, new string[0], EmailDomainMode.AllowAll, isNullable);
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="domains">The domains.</param>
            /// <param name="mode">The mode.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            public static async Task<EmailRule> CreateAsync(Func<TDbParams, string> selector, IEnumerable<string> domains,
                EmailDomainMode mode, bool isNullable = false) {
                EmailRule rule = new EmailRule();
                rule.Domains = domains;
                rule.Mode = mode;
                await rule.InitAsync(selector, isNullable);
                return rule;
            }

            /// <summary>
            /// Gets or sets the domains.
            /// </summary>
            /// <value>
            /// The domains.
            /// </value>
            private IEnumerable<string> Domains { get; set; }
            /// <summary>
            /// Gets or sets the mode.
            /// </summary>
            /// <value>
            /// The mode.
            /// </value>
            private EmailDomainMode Mode { get; set; }

            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
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
            /// <summary>
            /// Creates the mail address asynchronous.
            /// </summary>
            /// <returns></returns>
            private async Task<MailAddress> CreateMailAddressAsync()
            {
                MailAddress mailAddress = new MailAddress(ParamsValue.ToString());
                await Task.CompletedTask;
                return mailAddress;
            }
            /// <summary>
            /// Validates the mail address asynchronous.
            /// </summary>
            /// <param name="email">The email.</param>
            /// <returns></returns>
            private async Task<bool> ValidateMailAddressAsync(MailAddress email)
            {
                bool hasDomain = Domains.Contains(email.Host);
                bool isValid = Mode == EmailDomainMode.Whitelist ? hasDomain :
                        Mode == EmailDomainMode.Blacklist ? !hasDomain :
                        true;
                await Task.CompletedTask;
                return isValid;
            }
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <returns></returns>
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

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <returns></returns>
            protected override string GetErrorMessageCore()
            {
                return $"invalid email address";
            }
            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                await Task.CompletedTask;
                return $"invalid email address";
            }
        }
    }
}