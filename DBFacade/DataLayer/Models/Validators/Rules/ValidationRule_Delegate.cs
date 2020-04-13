using System;
using System.Linq.Expressions;
using System.Web.UI.WebControls;

namespace DBFacade.DataLayer.Models.Validators.Rules
{
    public abstract partial class ValidationRule<TDbParams>
        where TDbParams : IDbParamsModel
    {
        public delegate bool DelegateRuleHandler<T>(T value);

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, object>> selector,
            DelegateRuleHandler<object> handler)
        {
            return new DelegateRule<object>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, string>> selector,
            DelegateRuleHandler<string> handler, bool isNullable = false)
        {
            return new DelegateRule<string>(selector, handler, isNullable);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, short>> selector,
            DelegateRuleHandler<short> handler)
        {
            return new DelegateRule<short>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, int>> selector,
            DelegateRuleHandler<int> handler)
        {
            return new DelegateRule<int>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, long>> selector,
            DelegateRuleHandler<long> handler)
        {
            return new DelegateRule<long>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, double>> selector,
            DelegateRuleHandler<double> handler)
        {
            return new DelegateRule<double>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, float>> selector,
            DelegateRuleHandler<float> handler)
        {
            return new DelegateRule<float>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, decimal>> selector,
            DelegateRuleHandler<decimal> handler)
        {
            return new DelegateRule<decimal>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, ushort>> selector,
            DelegateRuleHandler<ushort> handler)
        {
            return new DelegateRule<ushort>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, uint>> selector,
            DelegateRuleHandler<uint> handler)
        {
            return new DelegateRule<uint>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, ulong>> selector,
            DelegateRuleHandler<ulong> handler)
        {
            return new DelegateRule<ulong>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, char>> selector,
            DelegateRuleHandler<char> handler)
        {
            return new DelegateRule<char>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, TimeSpan>> selector,
            DelegateRuleHandler<TimeSpan> handler)
        {
            return new DelegateRule<TimeSpan>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, DateTime>> selector,
            DelegateRuleHandler<DateTime> handler)
        {
            return new DelegateRule<DateTime>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, DateTimeOffset>> selector,
            DelegateRuleHandler<DateTimeOffset> handler)
        {
            return new DelegateRule<DateTimeOffset>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, bool>> selector,
            DelegateRuleHandler<bool> handler)
        {
            return new DelegateRule<bool>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, byte>> selector,
            DelegateRuleHandler<byte> handler)
        {
            return new DelegateRule<byte>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, sbyte>> selector,
            DelegateRuleHandler<sbyte> handler)
        {
            return new DelegateRule<sbyte>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, byte[]>> selector,
            DelegateRuleHandler<byte[]> handler)
        {
            return new DelegateRule<byte[]>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, char[]>> selector,
            DelegateRuleHandler<char[]> handler)
        {
            return new DelegateRule<char[]>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, Xml>> selector,
            DelegateRuleHandler<Xml> handler)
        {
            return new DelegateRule<Xml>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, short?>> selector,
            DelegateRuleHandler<short?> handler)
        {
            return new DelegateRule<short?>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, int?>> selector,
            DelegateRuleHandler<int?> handler)
        {
            return new DelegateRule<int?>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, long?>> selector,
            DelegateRuleHandler<long?> handler)
        {
            return new DelegateRule<long?>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, double?>> selector,
            DelegateRuleHandler<double?> handler)
        {
            return new DelegateRule<double?>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, float?>> selector,
            DelegateRuleHandler<float?> handler)
        {
            return new DelegateRule<float?>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, decimal?>> selector,
            DelegateRuleHandler<decimal?> handler)
        {
            return new DelegateRule<decimal?>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, ushort?>> selector,
            DelegateRuleHandler<ushort?> handler)
        {
            return new DelegateRule<ushort?>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, uint?>> selector,
            DelegateRuleHandler<uint?> handler)
        {
            return new DelegateRule<uint?>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, ulong?>> selector,
            DelegateRuleHandler<ulong?> handler)
        {
            return new DelegateRule<ulong?>(selector, handler);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, char?>> selector,
            DelegateRuleHandler<char?> handler)
        {
            return new DelegateRule<char?>(selector, handler, true);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, TimeSpan?>> selector,
            DelegateRuleHandler<TimeSpan?> handler)
        {
            return new DelegateRule<TimeSpan?>(selector, handler, true);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, DateTime?>> selector,
            DelegateRuleHandler<DateTime?> handler)
        {
            return new DelegateRule<DateTime?>(selector, handler, true);
        }

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, DateTimeOffset?>> selector,
            DelegateRuleHandler<DateTimeOffset?> handler)
        {
            return new DelegateRule<DateTimeOffset?>(selector, handler, true);
        }

        private class DelegateRule<T> : ValidationRule<TDbParams>
        {
            public DelegateRule(Expression<Func<TDbParams, T>> selector, DelegateRuleHandler<T> handler,
                bool isNullable = false)
            {
                init(selector, isNullable);
                DelegateRuleHandler = handler;
            }

            private DelegateRuleHandler<T> DelegateRuleHandler { get; }

            protected override bool ValidateRule()
            {
                return DelegateRuleHandler((T) ParamsValue);
            }

            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} expecting delegate to pass validation";
            }
        }
    }
}