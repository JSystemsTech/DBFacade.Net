using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DomainFacade.DataLayer.Models.Validators.Rules
{

    public abstract partial class ValidationRule<U>
        where U : IDbParamsModel
    {
        public class Match : ValidationRule<U>
        {
            internal string MatchStr { get; private set; }
            internal RegexOptions RegexOptions { get; private set; }
            public Match(U paramsModel, Func<dynamic, PropertyInfo> getPropInfo, string regexMatchStr) : base(getPropInfo)
            {
                MatchStr = regexMatchStr;
                RegexOptions = RegexOptions.IgnoreCase;
            }
            public Match(U paramsModel, Func<dynamic, PropertyInfo> getPropInfo, bool isNullable, string regexMatchStr) : base(getPropInfo, isNullable)
            {
                MatchStr = regexMatchStr;
                RegexOptions = RegexOptions.IgnoreCase;
            }
            public Match(U paramsModel, Func<dynamic, PropertyInfo> getPropInfo, string regexMatchStr, RegexOptions options) : base(getPropInfo)
            {
                MatchStr = regexMatchStr;
                RegexOptions = options;
            }
            public Match(U paramsModel, Func<dynamic, PropertyInfo> getPropInfo, bool isNullable, string regexMatchStr, RegexOptions options) : base(getPropInfo, isNullable)
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
        public class MinLength : ValidationRule<U>
        {
            internal int LimitValue { get; set; }
            public MinLength(Func<dynamic, PropertyInfo> getPropInfo, int limit) : base(getPropInfo) { LimitValue = limit; }
            public MinLength(Func<dynamic, PropertyInfo> getPropInfo, bool isNullable, int limit) : base(getPropInfo, isNullable) { LimitValue = limit; }
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
            public MaxLength(Func<dynamic, PropertyInfo> getPropInfo, int limit) : base(getPropInfo, limit) { }
            public MaxLength(Func<dynamic, PropertyInfo> getPropInfo, bool isNullable, int limit) : base(getPropInfo, isNullable, limit) { }
            protected override bool ValidateRule()
            {
                return ParamsValue.ToString().Length <= LimitValue;
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to be <= " + LimitValue;
            }

        }
    }
}