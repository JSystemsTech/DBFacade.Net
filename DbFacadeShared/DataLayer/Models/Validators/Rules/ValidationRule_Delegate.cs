using DbFacade.Utils;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    public abstract partial class ValidationRule<TDbParams>
    {
        public delegate bool DelegateRuleHandler<T>(T value);
        public delegate Task<bool> DelegateRuleHandlerAsync<T>(T value);        

        internal  class DelegateRule<T> : ValidationRule<TDbParams>
        {
            private Func<T, bool> DelegateRuleHandler{ get; set; }
            private Func<T, Task<bool>> DelegateRuleHandlerAsync { get; set; }

            private DelegateRule(){}

            public DelegateRule(Func<TDbParams, T> selector, Func<T, bool> handler)
            {
                bool isNullable = Nullable.GetUnderlyingType(typeof(T)) != null;
                Init(selector, isNullable);
                
                DelegateRuleHandler = handler;
            }
            public static async Task<DelegateRule<T>> CreateAsync(Func<TDbParams, T> selector, Func<T, Task<bool>> handler)
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

            protected override string GetErrorMessageCore()
            => $"expecting delegate to pass validation";

            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                string message = $"expecting delegate to pass validation";
                await Task.CompletedTask;
                return message;
            }
            

        }
        internal class DelegateRule : ValidationRule<TDbParams>
        {
            private Func<TDbParams, bool> DelegateRuleHandler { get; set; }
            private Func<TDbParams, Task<bool>> DelegateRuleHandlerAsync { get; set; }

            private Func<string> ErrorMessageHandler { get; set; }
            private DelegateRule() { }

            public DelegateRule(Func<TDbParams, bool> handler, string errorMessage)
            {
                Init();
                ErrorMessageHandler = () => errorMessage;
                DelegateRuleHandler = handler;
            }
            public static async Task<DelegateRule> CreateAsync(Func<TDbParams, Task<bool>> handler, string errorMessage)
            {
                DelegateRule rule = new DelegateRule();
                rule.ErrorMessageHandler = () => errorMessage;
                rule.DelegateRuleHandlerAsync = handler;
                await rule.InitAsync();
                return rule;
            }
            protected override bool ValidateRule()
            => DelegateRuleHandler((TDbParams)ParamsValue);

            protected override async Task<bool> ValidateRuleAsync()
            => await DelegateRuleHandlerAsync((TDbParams)ParamsValue);

            protected override string GetErrorMessageCore()
            => ErrorMessageHandler();

            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                string message = ErrorMessageHandler();
                await Task.CompletedTask;
                return message;
            }
        }
    }
}