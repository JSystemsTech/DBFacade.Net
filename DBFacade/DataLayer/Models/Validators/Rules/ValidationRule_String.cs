using DBFacade.Utils;
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
    /// <seealso cref="Rules.IValidationRule{DbParams}" />
    public abstract partial class ValidationRule<DbParams>
        where DbParams : IDbParamsModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Rules.IValidationRule{DbParams}" />
        public class Match : ValidationRule<DbParams>
        {
            /// <summary>
            /// Gets the match string.
            /// </summary>
            /// <value>
            /// The match string.
            /// </value>
            internal string MatchStr { get; private set; }
            /// <summary>
            /// Gets the regex options.
            /// </summary>
            /// <value>
            /// The regex options.
            /// </value>
            internal RegexOptions RegexOptions { get; private set; }
            /// <summary>
            /// Initializes a new instance of the <see cref="Match"/> class.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <param name="selector">The selector.</param>
            /// <param name="regexMatchStr">The regex match string.</param>
            public Match(DbParams paramsModel, Selector<DbParams> selector, string regexMatchStr) : base(selector)
            {
                MatchStr = regexMatchStr;
                RegexOptions = RegexOptions.IgnoreCase;
            }
            /// <summary>
            /// Initializes a new instance of the <see cref="Match"/> class.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <param name="selector">The selector.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="regexMatchStr">The regex match string.</param>
            public Match(DbParams paramsModel, Selector<DbParams> selector, bool isNullable, string regexMatchStr) : base(selector, isNullable)
            {
                MatchStr = regexMatchStr;
                RegexOptions = RegexOptions.IgnoreCase;
            }
            /// <summary>
            /// Initializes a new instance of the <see cref="Match"/> class.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <param name="selector">The selector.</param>
            /// <param name="regexMatchStr">The regex match string.</param>
            /// <param name="options">The options.</param>
            public Match(DbParams paramsModel, Selector<DbParams> selector, string regexMatchStr, RegexOptions options) : base(selector)
            {
                MatchStr = regexMatchStr;
                RegexOptions = options;
            }
            /// <summary>
            /// Initializes a new instance of the <see cref="Match"/> class.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <param name="selector">The selector.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="regexMatchStr">The regex match string.</param>
            /// <param name="options">The options.</param>
            public Match(DbParams paramsModel, Selector<DbParams> selector, bool isNullable, string regexMatchStr, RegexOptions options) : base(selector, isNullable)
            {
                MatchStr = regexMatchStr;
                RegexOptions = options;
            }
            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                System.Text.RegularExpressions.Match MatchRegex = Regex.Match(ParamsValue.ToString(), MatchStr, RegexOptions);
                return MatchRegex.Success;
            }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " does not match the expression.";
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Rules.IValidationRule{DbParams}" />
        public class MinLength : ValidationRule<DbParams>
        {
            /// <summary>
            /// Gets or sets the limit value.
            /// </summary>
            /// <value>
            /// The limit value.
            /// </value>
            internal int LimitValue { get; set; }
            /// <summary>
            /// Initializes a new instance of the <see cref="MinLength"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            public MinLength(Selector<DbParams> selector, int limit) : base(selector) { LimitValue = limit; }
            /// <summary>
            /// Initializes a new instance of the <see cref="MinLength"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="limit">The limit.</param>
            public MinLength(Selector<DbParams> selector, bool isNullable, int limit) : base(selector, isNullable) { LimitValue = limit; }
            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                return ParamsValue.ToString().Length >= LimitValue;
            }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to be >= " + LimitValue;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Rules.IValidationRule{DbParams}" />
        public class MaxLength : MinLength
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="MaxLength"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            public MaxLength(Selector<DbParams> selector, int limit) : base(selector, limit) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="MaxLength"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="limit">The limit.</param>
            public MaxLength(Selector<DbParams> selector, bool isNullable, int limit) : base(selector, isNullable, limit) { }
            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                return ParamsValue.ToString().Length <= LimitValue;
            }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to be <= " + LimitValue;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Rules.IValidationRule{DbParams}" />
        public class Email : ValidationRule<DbParams>
        {
            /// <summary>
            /// Gets or sets the domains.
            /// </summary>
            /// <value>
            /// The domains.
            /// </value>
            private IEnumerable<string> Domains { get; set; }
            /// <summary>
            /// Gets or sets a value indicating whether [allow all].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [allow all]; otherwise, <c>false</c>.
            /// </value>
            private bool AllowAll { get; set; }
            /// <summary>
            /// Gets or sets a value indicating whether [forbid some].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [forbid some]; otherwise, <c>false</c>.
            /// </value>
            private bool ForbidSome { get; set; }
            /// <summary>
            /// Initializes a new instance of the <see cref="Email"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            public Email(Selector<DbParams> selector) : base(selector) { Domains = new string[0]; AllowAll = true; }
            /// <summary>
            /// Initializes a new instance of the <see cref="Email"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            public Email(Selector<DbParams> selector, bool isNullable) : base(selector, isNullable) { Domains = new string[0]; AllowAll = true; }
            /// <summary>
            /// Initializes a new instance of the <see cref="Email"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="domains">The domains.</param>
            public Email(Selector<DbParams> selector, IEnumerable<string> domains) : base(selector) { Domains = domains; }
            /// <summary>
            /// Initializes a new instance of the <see cref="Email"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="domains">The domains.</param>
            public Email(Selector<DbParams> selector, bool isNullable, IEnumerable<string> domains) : base(selector, isNullable) { Domains = domains; }
            /// <summary>
            /// Initializes a new instance of the <see cref="Email"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="domains">The domains.</param>
            /// <param name="forbidSome">if set to <c>true</c> [forbid some].</param>
            public Email(Selector<DbParams> selector, IEnumerable<string> domains, bool forbidSome) : base(selector) { Domains = domains; ForbidSome = forbidSome; }
            /// <summary>
            /// Initializes a new instance of the <see cref="Email"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="domains">The domains.</param>
            /// <param name="forbidSome">if set to <c>true</c> [forbid some].</param>
            public Email(Selector<DbParams> selector, bool isNullable, IEnumerable<string> domains, bool forbidSome) : base(selector, isNullable) { Domains = domains; ForbidSome = forbidSome; }
            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                try
                {
                    MailAddress email = new MailAddress(ParamsValue.ToString());
                    if (AllowAll)
                    {
                        return true;
                    }
                    else if (ForbidSome)
                    {
                        return !Domains.Contains(email.Host);
                    }
                    else
                    {
                        return Domains.Contains(email.Host);
                    }
                }
                catch
                {
                    return false;
                }
            }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is an invalid email address";
            }

        }
    }
}