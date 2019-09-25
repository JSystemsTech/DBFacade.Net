using System;
using System.Linq.Expressions;
using System.Web.UI.WebControls;

namespace DBFacade.DataLayer.Models.Validators.Rules
{
    public abstract partial class ValidationRule<TDbParams>
        where TDbParams : IDbParamsModel
    {
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, object>> selector, DelegateRuleHandler<object> handler) => new DelegateRule<object>(selector, handler, false);
        
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, string>> selector, DelegateRuleHandler<string> handler, bool isNullable = false) => new DelegateRule<string>(selector, handler, isNullable);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, short>> selector, DelegateRuleHandler<short> handler)      => new  DelegateRule<short>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, int>> selector, DelegateRuleHandler<int> handler)          => new  DelegateRule<int>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, long>> selector, DelegateRuleHandler<long> handler)        => new  DelegateRule<long>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, double>> selector, DelegateRuleHandler<double> handler)    => new  DelegateRule<double>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, float>> selector, DelegateRuleHandler<float> handler)      => new  DelegateRule<float>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, decimal>> selector, DelegateRuleHandler<decimal> handler)  => new  DelegateRule<decimal>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, ushort>> selector, DelegateRuleHandler<ushort> handler)    => new  DelegateRule<ushort>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, uint>> selector, DelegateRuleHandler<uint> handler)        => new  DelegateRule<uint>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, ulong>> selector, DelegateRuleHandler<ulong> handler)      => new  DelegateRule<ulong>(selector, handler, false);

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, char>> selector, DelegateRuleHandler<char> handler) => new DelegateRule<char>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, TimeSpan>> selector, DelegateRuleHandler<TimeSpan> handler) => new DelegateRule<TimeSpan>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, DateTime>> selector, DelegateRuleHandler<DateTime> handler) => new DelegateRule<DateTime>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, DateTimeOffset>> selector, DelegateRuleHandler<DateTimeOffset> handler) => new DelegateRule<DateTimeOffset>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, bool>> selector, DelegateRuleHandler<bool> handler) => new DelegateRule<bool>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, byte>> selector, DelegateRuleHandler<byte> handler) => new DelegateRule<byte>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, sbyte>> selector, DelegateRuleHandler<sbyte> handler) => new DelegateRule<sbyte>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, byte[]>> selector, DelegateRuleHandler<byte[]> handler) => new DelegateRule<byte[]>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, char[]>> selector, DelegateRuleHandler<char[]> handler) => new DelegateRule<char[]>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, Xml>> selector, DelegateRuleHandler<Xml> handler) => new DelegateRule<Xml>(selector, handler, false);

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, short?>> selector, DelegateRuleHandler<short?> handler)    => new DelegateRule<short?>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, int?>> selector, DelegateRuleHandler<int?> handler)        => new DelegateRule<int?>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, long?>> selector, DelegateRuleHandler<long?> handler)      => new DelegateRule<long?>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, double?>> selector, DelegateRuleHandler<double?> handler)  => new DelegateRule<double?>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, float?>> selector, DelegateRuleHandler<float?> handler)    => new DelegateRule<float?>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, decimal?>> selector, DelegateRuleHandler<decimal?> handler)=> new DelegateRule<decimal?>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, ushort?>> selector, DelegateRuleHandler<ushort?> handler)  => new DelegateRule<ushort?>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, uint?>> selector, DelegateRuleHandler<uint?> handler)      => new DelegateRule<uint?>(selector, handler, false);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, ulong?>> selector, DelegateRuleHandler<ulong?> handler)    => new DelegateRule<ulong?>(selector, handler, false);

        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, char?>> selector, DelegateRuleHandler<char?> handler) => new DelegateRule<char?>(selector, handler, true);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, TimeSpan?>> selector, DelegateRuleHandler<TimeSpan?> handler) => new DelegateRule<TimeSpan?>(selector, handler, true);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, DateTime?>> selector, DelegateRuleHandler<DateTime?> handler) => new DelegateRule<DateTime?>(selector, handler, true);
        public static IValidationRule<TDbParams> Delegate(Expression<Func<TDbParams, DateTimeOffset?>> selector, DelegateRuleHandler<DateTimeOffset?> handler) => new DelegateRule<DateTimeOffset?>(selector, handler, true);

        public delegate bool DelegateRuleHandler<T>(T value);
        private class DelegateRule<T> : ValidationRule<TDbParams>
        {      
            private DelegateRuleHandler<T> DelegateRuleHandler { get; set; }
            public DelegateRule(Expression<Func<TDbParams, T>> selector, DelegateRuleHandler<T> handler, bool isNullable = false) {
                init(selector, isNullable);
                DelegateRuleHandler = handler;
            }

            protected override bool ValidateRule() => DelegateRuleHandler((T)ParamsValue);

            protected override string GetErrorMessageCore(string propertyName) => $"{propertyName} expecting delegate to pass validation";

        }
    }
}
