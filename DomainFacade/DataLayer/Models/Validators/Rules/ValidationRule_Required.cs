﻿using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DomainFacade.DataLayer.Models.Validators.Rules
{
    public abstract partial class ValidationRule<DbParams>
        where DbParams : IDbParamsModel
    {
        public class Required : ValidationRule<DbParams>
        {
            public Required(Expression<Func<DbParams, object>> selector) : base(selector) { }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is required.";
            }

            protected override bool ValidateRule()
            {
                return ParamsValue != null;
            }
        }
    }
}