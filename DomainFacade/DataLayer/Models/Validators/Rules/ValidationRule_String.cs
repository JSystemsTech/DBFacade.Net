﻿using System;
using System.Linq.Expressions;
using System.Reflection;
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
            public Match(DbParams paramsModel, Expression<Func<DbParams, object>> selector, string regexMatchStr) : base(selector)
            {
                MatchStr = regexMatchStr;
                RegexOptions = RegexOptions.IgnoreCase;
            }
            public Match(DbParams paramsModel, Expression<Func<DbParams, object>> selector, bool isNullable, string regexMatchStr) : base(selector, isNullable)
            {
                MatchStr = regexMatchStr;
                RegexOptions = RegexOptions.IgnoreCase;
            }
            public Match(DbParams paramsModel, Expression<Func<DbParams, object>> selector, string regexMatchStr, RegexOptions options) : base(selector)
            {
                MatchStr = regexMatchStr;
                RegexOptions = options;
            }
            public Match(DbParams paramsModel, Expression<Func<DbParams, object>> selector, bool isNullable, string regexMatchStr, RegexOptions options) : base(selector, isNullable)
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
            public MinLength(Expression<Func<DbParams, object>> selector, int limit) : base(selector) { LimitValue = limit; }
            public MinLength(Expression<Func<DbParams, object>> selector, bool isNullable, int limit) : base(selector, isNullable) { LimitValue = limit; }
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
            public MaxLength(Expression<Func<DbParams, object>> selector, int limit) : base(selector, limit) { }
            public MaxLength(Expression<Func<DbParams, object>> selector, bool isNullable, int limit) : base(selector, isNullable, limit) { }
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