using System;
using System.Linq.Expressions;

namespace DBFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TDbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{TDbParams}" />
    public abstract partial class ValidationRule<TDbParams>
        where TDbParams : IDbParamsModel
    {
        private bool IsNumeric<T>(string value, Func<string, T> converter)
        {
            try
            {
                converter(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool IsNumeric(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (value is string)
            {
                var str = value.ToString();
                return
                    IsNumeric(str, short.Parse) ||
                    IsNumeric(str, int.Parse) ||
                    IsNumeric(str, long.Parse) ||
                    IsNumeric(str, double.Parse) ||
                    IsNumeric(str, float.Parse) ||
                    IsNumeric(str, decimal.Parse) ||
                    IsNumeric(str, ushort.Parse) ||
                    IsNumeric(str, uint.Parse) ||
                    IsNumeric(str, ulong.Parse);
            }

            return true;
        }

        public static IValidationRule<TDbParams> IsNumber(Expression<Func<TDbParams, string>> selector,
            bool isNullable = false)
        {
            return new IsNummeric(selector, isNullable);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, string>> selector,
            double compareValue, bool isNullable = false)
        {
            return new EqualToRule(selector, compareValue, isNullable);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, short>> selector, short compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, int>> selector, int compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, long>> selector, long compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, double>> selector,
            double compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, float>> selector, float compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, decimal>> selector,
            decimal compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, ushort>> selector,
            ushort compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, uint>> selector, uint compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, ulong>> selector, ulong compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, short?>> selector,
            short compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, int?>> selector, int compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, long?>> selector, long compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, double?>> selector,
            double compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, float?>> selector,
            float compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, decimal?>> selector,
            decimal compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, ushort?>> selector,
            ushort compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, uint?>> selector, uint compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, ulong?>> selector,
            ulong compareValue)
        {
            return new EqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, string>> selector,
            double compareValue, bool isNullable = false)
        {
            return new NotEqualToRule(selector, compareValue, isNullable);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, short>> selector,
            short compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, int>> selector, int compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, long>> selector,
            long compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, double>> selector,
            double compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, float>> selector,
            float compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, decimal>> selector,
            decimal compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, ushort>> selector,
            ushort compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, uint>> selector,
            uint compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, ulong>> selector,
            ulong compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, short?>> selector,
            short compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, int?>> selector,
            int compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, long?>> selector,
            long compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, double?>> selector,
            double compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, float?>> selector,
            float compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, decimal?>> selector,
            decimal compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, ushort?>> selector,
            ushort compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, uint?>> selector,
            uint compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, ulong?>> selector,
            ulong compareValue)
        {
            return new NotEqualToRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, string>> selector,
            double compareValue, bool isNullable = false)
        {
            return new GreaterThanRule(selector, compareValue, isNullable);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, short>> selector,
            short compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, int>> selector,
            int compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, long>> selector,
            long compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, double>> selector,
            double compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, float>> selector,
            float compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, decimal>> selector,
            decimal compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, ushort>> selector,
            ushort compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, uint>> selector,
            uint compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, ulong>> selector,
            ulong compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, short?>> selector,
            short compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, int?>> selector,
            int compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, long?>> selector,
            long compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, double?>> selector,
            double compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, float?>> selector,
            float compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, decimal?>> selector,
            decimal compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, ushort?>> selector,
            ushort compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, uint?>> selector,
            uint compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, ulong?>> selector,
            ulong compareValue)
        {
            return new GreaterThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, string>> selector,
            double compareValue, bool isNullable = false)
        {
            return new GreaterThanOrEqualRule(selector, compareValue, isNullable);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, short>> selector,
            short compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, int>> selector,
            int compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, long>> selector,
            long compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, double>> selector,
            double compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, float>> selector,
            float compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, decimal>> selector,
            decimal compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, ushort>> selector,
            ushort compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, uint>> selector,
            uint compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, ulong>> selector,
            ulong compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, short?>> selector,
            short compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, int?>> selector,
            int compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, long?>> selector,
            long compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, double?>> selector,
            double compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, float?>> selector,
            float compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, decimal?>> selector,
            decimal compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, ushort?>> selector,
            ushort compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, uint?>> selector,
            uint compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, ulong?>> selector,
            ulong compareValue)
        {
            return new GreaterThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, string>> selector,
            double compareValue, bool isNullable = false)
        {
            return new LessThanRule(selector, compareValue, isNullable);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, short>> selector,
            short compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, int>> selector, int compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, long>> selector, long compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, double>> selector,
            double compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, float>> selector,
            float compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, decimal>> selector,
            decimal compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, ushort>> selector,
            ushort compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, uint>> selector, uint compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, ulong>> selector,
            ulong compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, short?>> selector,
            short compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, int?>> selector, int compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, long?>> selector,
            long compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, double?>> selector,
            double compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, float?>> selector,
            float compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, decimal?>> selector,
            decimal compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, ushort?>> selector,
            ushort compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, uint?>> selector,
            uint compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, ulong?>> selector,
            ulong compareValue)
        {
            return new LessThanRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, string>> selector,
            double compareValue, bool isNullable = false)
        {
            return new LessThanOrEqualRule(selector, compareValue, isNullable);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, short>> selector,
            short compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, int>> selector,
            int compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, long>> selector,
            long compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, double>> selector,
            double compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, float>> selector,
            float compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, decimal>> selector,
            decimal compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, ushort>> selector,
            ushort compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, uint>> selector,
            uint compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, ulong>> selector,
            ulong compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, short?>> selector,
            short compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, int?>> selector,
            int compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, long?>> selector,
            long compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, double?>> selector,
            double compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, float?>> selector,
            float compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, decimal?>> selector,
            decimal compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, ushort?>> selector,
            ushort compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, uint?>> selector,
            uint compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, ulong?>> selector,
            ulong compareValue)
        {
            return new LessThanOrEqualRule(selector, compareValue);
        }

        private class IsNummeric : ValidationRule<TDbParams>
        {
            public IsNummeric(Expression<Func<TDbParams, string>> selector, bool isNullable = false) : base(selector,
                isNullable)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, short>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, int>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, long>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, double>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, float>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, decimal>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, ushort>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, uint>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, ulong>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, short?>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, int?>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, long?>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, double?>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, float?>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, decimal?>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, ushort?>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, uint?>> selector) : base(selector)
            {
            }

            public IsNummeric(Expression<Func<TDbParams, ulong?>> selector) : base(selector)
            {
            }

            protected override bool ValidateRule()
            {
                if (IsNumeric(ParamsValue))
                {
                    double num;
                    if (ParamsValue is string)
                    {
                        double.TryParse(ParamsValue.ToString(), out num);
                        return ValidateNumberRule(num);
                    }

                    //else
                    //{
                    //    num = Convert.ToDouble(ParamsValue);
                    //}
                    return ValidateNumberRule(ParamsValue);
                }

                return false;
            }

            internal virtual bool ValidateNumberRule(object value)
            {
                return true;
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is not a number.";
            }
        }

        /// <summary>
        /// </summary>
        /// <seealso cref="Rules.IValidationRule{TDbParams}" />
        private class NumericCompare : IsNummeric
        {
            public NumericCompare(Expression<Func<TDbParams, string>> selector, double limit, bool isNullable = false) :
                base(selector, isNullable)
            {
                LimitValueDouble = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, short>> selector, short limit) : base(selector)
            {
                LimitValueShort = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, int>> selector, int limit) : base(selector)
            {
                LimitValueInt = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, long>> selector, long limit) : base(selector)
            {
                LimitValueLong = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, double>> selector, double limit) : base(selector)
            {
                LimitValueDouble = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, float>> selector, float limit) : base(selector)
            {
                LimitValueFloat = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, decimal>> selector, decimal limit) : base(selector)
            {
                LimitValueDecimal = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, ushort>> selector, ushort limit) : base(selector)
            {
                LimitValueUShort = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, uint>> selector, uint limit) : base(selector)
            {
                LimitValueUInt = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, ulong>> selector, ulong limit) : base(selector)
            {
                LimitValueULong = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, short?>> selector, short limit) : base(selector)
            {
                LimitValueShort = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, int?>> selector, int limit) : base(selector)
            {
                LimitValueInt = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, long?>> selector, long limit) : base(selector)
            {
                LimitValueLong = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, double?>> selector, double limit) : base(selector)
            {
                LimitValueDouble = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, float?>> selector, float limit) : base(selector)
            {
                LimitValueFloat = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, decimal?>> selector, decimal limit) : base(selector)
            {
                LimitValueDecimal = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, ushort?>> selector, ushort limit) : base(selector)
            {
                LimitValueUShort = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, uint?>> selector, uint limit) : base(selector)
            {
                LimitValueUInt = limit;
            }

            public NumericCompare(Expression<Func<TDbParams, ulong?>> selector, ulong limit) : base(selector)
            {
                LimitValueULong = limit;
            }

            internal short LimitValueShort { get; }
            internal int LimitValueInt { get; }
            internal long LimitValueLong { get; }
            internal double LimitValueDouble { get; }
            internal float LimitValueFloat { get; }
            internal decimal LimitValueDecimal { get; }
            internal ushort LimitValueUShort { get; }
            internal uint LimitValueUInt { get; }
            internal ulong LimitValueULong { get; }

            internal override bool ValidateNumberRule(object value)
            {
                var numType = value.GetType();

                return
                    numType == typeof(short) ? CompareShort((short) value, LimitValueShort) :
                    numType == typeof(int) ? CompareInt((int) value, LimitValueInt) :
                    numType == typeof(long) ? CompareLong((long) value, LimitValueLong) :
                    numType == typeof(double) ? CompareDouble((double) value, LimitValueDouble) :
                    numType == typeof(float) ? CompareFloat((float) value, LimitValueFloat) :
                    numType == typeof(decimal) ? CompareDecimal((decimal) value, LimitValueDecimal) :
                    numType == typeof(ushort) ? CompareUShort((ushort) value, LimitValueUShort) :
                    numType == typeof(uint) ? CompareUInt((uint) value, LimitValueUInt) :
                    numType == typeof(ulong) ? CompareULong((ulong) value, LimitValueULong) : false;
            }

            protected virtual bool CompareShort(short value, short compareValue)
            {
                return false;
            }

            protected virtual bool CompareInt(int value, int compareValue)
            {
                return false;
            }

            protected virtual bool CompareLong(long value, long compareValue)
            {
                return false;
            }

            protected virtual bool CompareDouble(double value, double compareValue)
            {
                return false;
            }

            protected virtual bool CompareFloat(float value, float compareValue)
            {
                return false;
            }

            protected virtual bool CompareDecimal(decimal value, decimal compareValue)
            {
                return false;
            }

            protected virtual bool CompareUShort(ushort value, ushort compareValue)
            {
                return false;
            }

            protected virtual bool CompareUInt(uint value, uint compareValue)
            {
                return false;
            }

            protected virtual bool CompareULong(ulong value, ulong compareValue)
            {
                return false;
            }

            protected virtual string GetCompareTypeLabel()
            {
                return "";
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                var compareType = GetCompareTypeLabel();
                if (string.IsNullOrEmpty(compareType)) return $"{propertyName} is invalid ";
                var value = !string.IsNullOrEmpty(LimitValueShort.ToString()) ? LimitValueShort.ToString() :
                    !string.IsNullOrEmpty(LimitValueInt.ToString()) ? LimitValueInt.ToString() :
                    !string.IsNullOrEmpty(LimitValueLong.ToString()) ? LimitValueLong.ToString() :
                    !string.IsNullOrEmpty(LimitValueDouble.ToString()) ? LimitValueDouble.ToString() :
                    !string.IsNullOrEmpty(LimitValueFloat.ToString()) ? LimitValueFloat.ToString() :
                    !string.IsNullOrEmpty(LimitValueDecimal.ToString()) ? LimitValueDecimal.ToString() :
                    !string.IsNullOrEmpty(LimitValueUShort.ToString()) ? LimitValueUShort.ToString() :
                    !string.IsNullOrEmpty(LimitValueUInt.ToString()) ? LimitValueUInt.ToString() :
                    LimitValueULong.ToString();

                return propertyName + $"{propertyName} expecting value length to {compareType} {value}";
            }
        }

        private class EqualToRule : NumericCompare
        {
            public EqualToRule(Expression<Func<TDbParams, string>> selector, double compareValue,
                bool isNullable = false) : base(selector, compareValue, isNullable)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, short>> selector, short compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, int>> selector, int compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, long>> selector, long compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, double>> selector, double compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, float>> selector, float compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, decimal>> selector, decimal compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, ushort>> selector, ushort compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, uint>> selector, uint compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, short?>> selector, short compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, int?>> selector, int compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, long?>> selector, long compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, double?>> selector, double compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, float?>> selector, float compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, ushort?>> selector, ushort compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, uint?>> selector, uint compareValue) : base(selector,
                compareValue)
            {
            }

            public EqualToRule(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) : base(selector,
                compareValue)
            {
            }

            protected override bool CompareShort(short value, short compareValue)
            {
                return value.Equals(compareValue);
            }

            protected override bool CompareInt(int value, int compareValue)
            {
                return value.Equals(compareValue);
            }

            protected override bool CompareLong(long value, long compareValue)
            {
                return value.Equals(compareValue);
            }

            protected override bool CompareDouble(double value, double compareValue)
            {
                return value.Equals(compareValue);
            }

            protected override bool CompareFloat(float value, float compareValue)
            {
                return value.Equals(compareValue);
            }

            protected override bool CompareDecimal(decimal value, decimal compareValue)
            {
                return value.Equals(compareValue);
            }

            protected override bool CompareUShort(ushort value, ushort compareValue)
            {
                return value.Equals(compareValue);
            }

            protected override bool CompareUInt(uint value, uint compareValue)
            {
                return value.Equals(compareValue);
            }

            protected override bool CompareULong(ulong value, ulong compareValue)
            {
                return value.Equals(compareValue);
            }

            protected override string GetCompareTypeLabel()
            {
                return "to be equal to";
            }
        }

        private class NotEqualToRule : NumericCompare
        {
            public NotEqualToRule(Expression<Func<TDbParams, string>> selector, double compareValue,
                bool isNullable = false) : base(selector, compareValue, isNullable)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, short>> selector, short compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, int>> selector, int compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, long>> selector, long compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, double>> selector, double compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, float>> selector, float compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, decimal>> selector, decimal compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, ushort>> selector, ushort compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, uint>> selector, uint compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, short?>> selector, short compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, int?>> selector, int compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, long?>> selector, long compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, double?>> selector, double compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, float?>> selector, float compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, ushort?>> selector, ushort compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, uint?>> selector, uint compareValue) : base(selector,
                compareValue)
            {
            }

            public NotEqualToRule(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) : base(selector,
                compareValue)
            {
            }

            protected override bool CompareShort(short value, short compareValue)
            {
                return !value.Equals(compareValue);
            }

            protected override bool CompareInt(int value, int compareValue)
            {
                return !value.Equals(compareValue);
            }

            protected override bool CompareLong(long value, long compareValue)
            {
                return !value.Equals(compareValue);
            }

            protected override bool CompareDouble(double value, double compareValue)
            {
                return !value.Equals(compareValue);
            }

            protected override bool CompareFloat(float value, float compareValue)
            {
                return !value.Equals(compareValue);
            }

            protected override bool CompareDecimal(decimal value, decimal compareValue)
            {
                return !value.Equals(compareValue);
            }

            protected override bool CompareUShort(ushort value, ushort compareValue)
            {
                return !value.Equals(compareValue);
            }

            protected override bool CompareUInt(uint value, uint compareValue)
            {
                return !value.Equals(compareValue);
            }

            protected override bool CompareULong(ulong value, ulong compareValue)
            {
                return !value.Equals(compareValue);
            }

            protected override string GetCompareTypeLabel()
            {
                return "to not be equal to";
            }
        }

        private class GreaterThanRule : NumericCompare
        {
            public GreaterThanRule(Expression<Func<TDbParams, string>> selector, double compareValue,
                bool isNullable = false) : base(selector, compareValue, isNullable)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, short>> selector, short compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, int>> selector, int compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, long>> selector, long compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, double>> selector, double compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, float>> selector, float compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, decimal>> selector, decimal compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, ushort>> selector, ushort compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, uint>> selector, uint compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, short?>> selector, short compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, int?>> selector, int compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, long?>> selector, long compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, double?>> selector, double compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, float?>> selector, float compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) : base(
                selector, compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, ushort?>> selector, ushort compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, uint?>> selector, uint compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanRule(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) : base(selector,
                compareValue)
            {
            }

            protected override bool CompareShort(short value, short compareValue)
            {
                return value > compareValue;
            }

            protected override bool CompareInt(int value, int compareValue)
            {
                return value > compareValue;
            }

            protected override bool CompareLong(long value, long compareValue)
            {
                return value > compareValue;
            }

            protected override bool CompareDouble(double value, double compareValue)
            {
                return value > compareValue;
            }

            protected override bool CompareFloat(float value, float compareValue)
            {
                return value > compareValue;
            }

            protected override bool CompareDecimal(decimal value, decimal compareValue)
            {
                return value > compareValue;
            }

            protected override bool CompareUShort(ushort value, ushort compareValue)
            {
                return value > compareValue;
            }

            protected override bool CompareUInt(uint value, uint compareValue)
            {
                return value > compareValue;
            }

            protected override bool CompareULong(ulong value, ulong compareValue)
            {
                return value > compareValue;
            }

            protected override string GetCompareTypeLabel()
            {
                return "to be greater than";
            }
        }

        private class GreaterThanOrEqualRule : NumericCompare
        {
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, string>> selector, double compareValue,
                bool isNullable = false) : base(selector, compareValue, isNullable)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, short>> selector, short compareValue) : base(
                selector, compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, int>> selector, int compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, long>> selector, long compareValue) : base(
                selector, compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, double>> selector, double compareValue) : base(
                selector, compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, float>> selector, float compareValue) : base(
                selector, compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, decimal>> selector, decimal compareValue) : base(
                selector, compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, ushort>> selector, ushort compareValue) : base(
                selector, compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, uint>> selector, uint compareValue) : base(
                selector, compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) : base(
                selector, compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, short?>> selector, short compareValue) : base(
                selector, compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, int?>> selector, int compareValue) : base(selector,
                compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, long?>> selector, long compareValue) : base(
                selector, compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, double?>> selector, double compareValue) : base(
                selector, compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, float?>> selector, float compareValue) : base(
                selector, compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) : base(
                selector, compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, ushort?>> selector, ushort compareValue) : base(
                selector, compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, uint?>> selector, uint compareValue) : base(
                selector, compareValue)
            {
            }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) : base(
                selector, compareValue)
            {
            }

            protected override bool CompareShort(short value, short compareValue)
            {
                return value >= compareValue;
            }

            protected override bool CompareInt(int value, int compareValue)
            {
                return value >= compareValue;
            }

            protected override bool CompareLong(long value, long compareValue)
            {
                return value >= compareValue;
            }

            protected override bool CompareDouble(double value, double compareValue)
            {
                return value >= compareValue;
            }

            protected override bool CompareFloat(float value, float compareValue)
            {
                return value >= compareValue;
            }

            protected override bool CompareDecimal(decimal value, decimal compareValue)
            {
                return value >= compareValue;
            }

            protected override bool CompareUShort(ushort value, ushort compareValue)
            {
                return value >= compareValue;
            }

            protected override bool CompareUInt(uint value, uint compareValue)
            {
                return value >= compareValue;
            }

            protected override bool CompareULong(ulong value, ulong compareValue)
            {
                return value >= compareValue;
            }

            protected override string GetCompareTypeLabel()
            {
                return "to be greater than or equal to";
            }
        }

        private class LessThanRule : NumericCompare
        {
            public LessThanRule(Expression<Func<TDbParams, string>> selector, double compareValue,
                bool isNullable = false) : base(selector, compareValue, isNullable)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, short>> selector, short compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, int>> selector, int compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, long>> selector, long compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, double>> selector, double compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, float>> selector, float compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, decimal>> selector, decimal compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, ushort>> selector, ushort compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, uint>> selector, uint compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, short?>> selector, short compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, int?>> selector, int compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, long?>> selector, long compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, double?>> selector, double compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, float?>> selector, float compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, ushort?>> selector, ushort compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, uint?>> selector, uint compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanRule(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) : base(selector,
                compareValue)
            {
            }

            protected override bool CompareShort(short value, short compareValue)
            {
                return value < compareValue;
            }

            protected override bool CompareInt(int value, int compareValue)
            {
                return value < compareValue;
            }

            protected override bool CompareLong(long value, long compareValue)
            {
                return value < compareValue;
            }

            protected override bool CompareDouble(double value, double compareValue)
            {
                return value < compareValue;
            }

            protected override bool CompareFloat(float value, float compareValue)
            {
                return value < compareValue;
            }

            protected override bool CompareDecimal(decimal value, decimal compareValue)
            {
                return value < compareValue;
            }

            protected override bool CompareUShort(ushort value, ushort compareValue)
            {
                return value < compareValue;
            }

            protected override bool CompareUInt(uint value, uint compareValue)
            {
                return value < compareValue;
            }

            protected override bool CompareULong(ulong value, ulong compareValue)
            {
                return value < compareValue;
            }

            protected override string GetCompareTypeLabel()
            {
                return "to be less than";
            }
        }

        private class LessThanOrEqualRule : NumericCompare
        {
            public LessThanOrEqualRule(Expression<Func<TDbParams, string>> selector, double compareValue,
                bool isNullable = false) : base(selector, compareValue, isNullable)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, short>> selector, short compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, int>> selector, int compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, long>> selector, long compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, double>> selector, double compareValue) : base(
                selector, compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, float>> selector, float compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, decimal>> selector, decimal compareValue) : base(
                selector, compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, ushort>> selector, ushort compareValue) : base(
                selector, compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, uint>> selector, uint compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, short?>> selector, short compareValue) : base(
                selector, compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, int?>> selector, int compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, long?>> selector, long compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, double?>> selector, double compareValue) : base(
                selector, compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, float?>> selector, float compareValue) : base(
                selector, compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) : base(
                selector, compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, ushort?>> selector, ushort compareValue) : base(
                selector, compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, uint?>> selector, uint compareValue) : base(selector,
                compareValue)
            {
            }

            public LessThanOrEqualRule(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) : base(
                selector, compareValue)
            {
            }

            protected override bool CompareShort(short value, short compareValue)
            {
                return value <= compareValue;
            }

            protected override bool CompareInt(int value, int compareValue)
            {
                return value <= compareValue;
            }

            protected override bool CompareLong(long value, long compareValue)
            {
                return value <= compareValue;
            }

            protected override bool CompareDouble(double value, double compareValue)
            {
                return value <= compareValue;
            }

            protected override bool CompareFloat(float value, float compareValue)
            {
                return value <= compareValue;
            }

            protected override bool CompareDecimal(decimal value, decimal compareValue)
            {
                return value <= compareValue;
            }

            protected override bool CompareUShort(ushort value, ushort compareValue)
            {
                return value <= compareValue;
            }

            protected override bool CompareUInt(uint value, uint compareValue)
            {
                return value <= compareValue;
            }

            protected override bool CompareULong(ulong value, ulong compareValue)
            {
                return value <= compareValue;
            }

            protected override string GetCompareTypeLabel()
            {
                return "to be less than or equal to";
            }
        }
    }
}