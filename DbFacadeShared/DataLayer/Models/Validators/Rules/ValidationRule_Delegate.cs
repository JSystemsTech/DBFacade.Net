using DbFacade.Utils;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    public abstract partial class ValidationRule<TDbParams>
        where TDbParams : DbParamsModel
    {
        public delegate bool DelegateRuleHandler<T>(T value);
        public delegate Task<bool> DelegateRuleHandlerAsync<T>(T value);        

        internal  class DelegateRule<T> : ValidationRule<TDbParams>
        {
            private Func<T, bool> DelegateRuleHandler{ get; set; }
            private Func<T, Task<bool>> DelegateRuleHandlerAsync { get; set; }

            private DelegateRule(){}

            public DelegateRule(Expression<Func<TDbParams, T>> selector, Func<T, bool> handler)
            {
                bool isNullable = Nullable.GetUnderlyingType(typeof(T)) != null;
                Init(selector, isNullable);
                
                DelegateRuleHandler = handler;
            }
            public static async Task<DelegateRule<T>> CreateAsync(Expression<Func<TDbParams, T>> selector, Func<T, Task<bool>> handler)
            {
                DelegateRule<T> rule = new DelegateRule<T>();
                rule.DelegateRuleHandlerAsync = handler;
                await rule.InitAsync(selector, await GenericInstance.IsNullableTypeAsync<T>());
                return rule;
            }
            protected override bool ValidateRule()
            => DelegateRuleHandler((T) ParamsValue);
            
            protected override async Task<bool> ValidateRuleAsync()
            => await DelegateRuleHandlerAsync((T)ParamsValue);

            protected override string GetErrorMessageCore(string propertyName)
            => $"{propertyName} expecting delegate to pass validation";

            protected override async Task<string> GetErrorMessageCoreAsync(string propertyName)
            {
                string message = $"{propertyName} expecting delegate to pass validation";
                await Task.CompletedTask;
                return message;
            }
            

        }
        internal class DelegateRule : ValidationRule<TDbParams>
        {
            private Func<TDbParams, bool> DelegateRuleHandler { get; set; }
            private Func<TDbParams, Task<bool>> DelegateRuleHandlerAsync { get; set; }

            private Func<string, string> ErrorMessageHandler { get; set; }
            private DelegateRule() { }

            public DelegateRule(Func<TDbParams, bool> handler, Func<string, string> errorMessageHandler)
            {
                Init();
                ErrorMessageHandler = errorMessageHandler;
                DelegateRuleHandler = handler;
            }
            public static async Task<DelegateRule> CreateAsync(Func<TDbParams, Task<bool>> handler, Func<string, string> errorMessageHandler)
            {
                DelegateRule rule = new DelegateRule();
                rule.ErrorMessageHandler = errorMessageHandler;
                rule.DelegateRuleHandlerAsync = handler;
                await rule.InitAsync();
                return rule;
            }
            protected override bool ValidateRule()
            => DelegateRuleHandler((TDbParams)ParamsValue);

            protected override async Task<bool> ValidateRuleAsync()
            => await DelegateRuleHandlerAsync((TDbParams)ParamsValue);

            protected override string GetErrorMessageCore(string propertyName)
            => ErrorMessageHandler(propertyName);

            protected override async Task<string> GetErrorMessageCoreAsync(string propertyName)
            {
                string message = ErrorMessageHandler(propertyName);
                await Task.CompletedTask;
                return message;
            }
        }
    }
}