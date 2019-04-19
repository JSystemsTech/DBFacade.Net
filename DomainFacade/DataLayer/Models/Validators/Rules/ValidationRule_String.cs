using DomainFacade.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DomainFacade.DataLayer.Models.Validators.Rules
{

    public abstract partial class ValidationRule<DbParams>
        where DbParams : IDbParamsModel
    {
        public class Match : ValidationRule<DbParams>
        {
            internal string MatchStr { get; private set; }
            internal RegexOptions RegexOptions { get; private set; }
            public Match(DbParams paramsModel, Selector<DbParams> selector, string regexMatchStr) : base(selector)
            {
                MatchStr = regexMatchStr;
                RegexOptions = RegexOptions.IgnoreCase;
            }
            public Match(DbParams paramsModel, Selector<DbParams> selector, bool isNullable, string regexMatchStr) : base(selector, isNullable)
            {
                MatchStr = regexMatchStr;
                RegexOptions = RegexOptions.IgnoreCase;
            }
            public Match(DbParams paramsModel, Selector<DbParams> selector, string regexMatchStr, RegexOptions options) : base(selector)
            {
                MatchStr = regexMatchStr;
                RegexOptions = options;
            }
            public Match(DbParams paramsModel, Selector<DbParams> selector, bool isNullable, string regexMatchStr, RegexOptions options) : base(selector, isNullable)
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
                return propertyName + " does not match the expression.";
            }

        }
        public class MinLength : ValidationRule<DbParams>
        {
            internal int LimitValue { get; set; }
            public MinLength(Selector<DbParams> selector, int limit) : base(selector) { LimitValue = limit; }
            public MinLength(Selector<DbParams> selector, bool isNullable, int limit) : base(selector, isNullable) { LimitValue = limit; }
            protected override bool ValidateRule()
            {
                return ParamsValue.ToString().Length >= LimitValue;
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to be >= " + LimitValue;
            }

        }
        public class MaxLength : MinLength
        {
            public MaxLength(Selector<DbParams> selector, int limit) : base(selector, limit) { }
            public MaxLength(Selector<DbParams> selector, bool isNullable, int limit) : base(selector, isNullable, limit) { }
            protected override bool ValidateRule()
            {
                return ParamsValue.ToString().Length <= LimitValue;
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to be <= " + LimitValue;
            }

        }
        public class Email : ValidationRule<DbParams>
        {
            private IEnumerable<string> Domains { get; set; }
            private bool AllowAll { get; set; }
            private bool ForbidSome { get; set; }
            public Email(Selector<DbParams> selector) : base(selector) { Domains = new string[0]; AllowAll = true; }
            public Email(Selector<DbParams> selector, bool isNullable) : base(selector, isNullable) { Domains = new string[0]; AllowAll = true; }
            public Email(Selector<DbParams> selector, IEnumerable<string> domains) : base(selector) { Domains = domains; }
            public Email(Selector<DbParams> selector, bool isNullable, IEnumerable<string> domains) : base(selector, isNullable) { Domains = domains;}
            public Email(Selector<DbParams> selector, IEnumerable<string> domains, bool forbidSome) : base(selector) { Domains = domains; ForbidSome = forbidSome; }
            public Email(Selector<DbParams> selector, bool isNullable, IEnumerable<string> domains, bool forbidSome) : base(selector, isNullable) { Domains = domains; ForbidSome = forbidSome; }
            protected override bool ValidateRule()
            {
                try
                {
                    MailAddress email = new MailAddress(ParamsValue.ToString());
                    if(AllowAll)
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
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is an invalid email address";
            }

        }
    }
}