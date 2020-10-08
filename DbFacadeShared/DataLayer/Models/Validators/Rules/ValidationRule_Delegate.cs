using DbFacade.Utils;
using System;
using System.Data.SqlTypes;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    public abstract partial class ValidationRule<TDbParams>
        where TDbParams : IDbParamsModel
    {
        public delegate bool DelegateRuleHandler<T>(T value);
        public delegate Task<bool> DelegateRuleHandlerAsync<T>(T value);

        public static IValidationRule<TDbParams> Delegate<T>(Expression<Func<TDbParams, T>> selector,DelegateRuleHandler<T> handler)
        =>  new DelegateRule<T>(selector, handler);

        public static async Task<IValidationRule<TDbParams>> DelegateAsync<T>(Expression<Func<TDbParams, T>> selector,
            DelegateRuleHandlerAsync<T> handler)
        => await DelegateRule<T>.CreateAsync(selector, handler);

        private class DelegateRule<T> : ValidationRule<TDbParams>
        {
            private DelegateRule(){}
            public DelegateRule(Expression<Func<TDbParams, T>> selector, DelegateRuleHandler<T> handler)
            {
                bool isNullable = Nullable.GetUnderlyingType(typeof(T)) != null;
                Init(selector, isNullable);
                
                DelegateRuleHandler = handler;
            }
            public static async Task<DelegateRule<T>> CreateAsync(Expression<Func<TDbParams, T>> selector, DelegateRuleHandlerAsync<T> handler)
            {
                DelegateRule<T> rule = new DelegateRule<T>();
                rule.DelegateRuleHandlerAsync = handler;
                await rule.InitAsync(selector, await GenericInstance.IsNullableTypeAsync<T>());
                return rule;
            }

            private DelegateRuleHandler<T> DelegateRuleHandler { get; set; }
            private DelegateRuleHandlerAsync<T> DelegateRuleHandlerAsync { get; set; }

            protected override bool ValidateRule()
            => DelegateRuleHandler((T) ParamsValue);
            
            protected override async Task<bool> ValidateRuleAsync()
            => await DelegateRuleHandlerAsync((T)ParamsValue);

            protected override string GetErrorMessageCore(string propertyName)
            => $"{propertyName} expecting delegate to pass validation";

            protected override async Task<string> GetErrorMessageCoreAsync(string propertyName)
            {
                await Task.CompletedTask;
                return $"{propertyName} expecting delegate to pass validation";
            }
            

        }
    }
}