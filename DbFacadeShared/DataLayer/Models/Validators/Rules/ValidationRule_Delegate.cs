using DbFacade.Utils;
using System;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public abstract partial class ValidationRule<TDbParams>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public delegate bool DelegateRuleHandler<T>(T value);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public delegate Task<bool> DelegateRuleHandlerAsync<T>(T value);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        internal class DelegateRule<T> : ValidationRule<TDbParams>
        {
            /// <summary>
            /// Gets or sets the delegate rule handler.
            /// </summary>
            /// <value>
            /// The delegate rule handler.
            /// </value>
            private Func<T, bool> DelegateRuleHandler{ get; set; }
            /// <summary>
            /// Gets or sets the delegate rule handler asynchronous.
            /// </summary>
            /// <value>
            /// The delegate rule handler asynchronous.
            /// </value>
            private Func<T, Task<bool>> DelegateRuleHandlerAsync { get; set; }

            /// <summary>
            /// Prevents a default instance of the <see cref="DelegateRule`1"/> class from being created.
            /// </summary>
            private DelegateRule(){}

            /// <summary>
            /// Initializes a new instance of the <see cref="DelegateRule`1"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="handler">The handler.</param>
            public DelegateRule(Func<TDbParams, T> selector, Func<T, bool> handler)
            {
                bool isNullable = Nullable.GetUnderlyingType(typeof(T)) != null;
                Init(selector, isNullable);
                
                DelegateRuleHandler = handler;
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="handler">The handler.</param>
            /// <returns></returns>
            public static async Task<DelegateRule<T>> CreateAsync(Func<TDbParams, T> selector, Func<T, Task<bool>> handler)
            {
                DelegateRule<T> rule = new DelegateRule<T>();
                rule.DelegateRuleHandlerAsync = handler;
                await rule.InitAsync(selector, await GenericInstance.IsNullableTypeAsync<T>());
                return rule;
            }
            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            => DelegateRuleHandler((T) ParamsValue);

            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync()
            => await DelegateRuleHandlerAsync((T)ParamsValue);

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <returns></returns>
            protected override string GetErrorMessageCore()
            => $"expecting delegate to pass validation";

            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                string message = $"expecting delegate to pass validation";
                await Task.CompletedTask;
                return message;
            }
            

        }
        /// <summary>
        /// 
        /// </summary>
        internal class DelegateRule : ValidationRule<TDbParams>
        {
            /// <summary>
            /// Gets or sets the delegate rule handler.
            /// </summary>
            /// <value>
            /// The delegate rule handler.
            /// </value>
            private Func<TDbParams, bool> DelegateRuleHandler { get; set; }
            /// <summary>
            /// Gets or sets the delegate rule handler asynchronous.
            /// </summary>
            /// <value>
            /// The delegate rule handler asynchronous.
            /// </value>
            private Func<TDbParams, Task<bool>> DelegateRuleHandlerAsync { get; set; }

            /// <summary>
            /// Gets or sets the error message handler.
            /// </summary>
            /// <value>
            /// The error message handler.
            /// </value>
            private Func<string> ErrorMessageHandler { get; set; }
            /// <summary>
            /// Prevents a default instance of the <see cref="DelegateRule"/> class from being created.
            /// </summary>
            private DelegateRule() { }

            /// <summary>
            /// Initializes a new instance of the <see cref="DelegateRule"/> class.
            /// </summary>
            /// <param name="handler">The handler.</param>
            /// <param name="errorMessage">The error message.</param>
            public DelegateRule(Func<TDbParams, bool> handler, string errorMessage)
            {
                Init();
                ErrorMessageHandler = () => errorMessage;
                DelegateRuleHandler = handler;
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="handler">The handler.</param>
            /// <param name="errorMessage">The error message.</param>
            /// <returns></returns>
            public static async Task<DelegateRule> CreateAsync(Func<TDbParams, Task<bool>> handler, string errorMessage)
            {
                DelegateRule rule = new DelegateRule();
                rule.ErrorMessageHandler = () => errorMessage;
                rule.DelegateRuleHandlerAsync = handler;
                await rule.InitAsync();
                return rule;
            }
            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            => DelegateRuleHandler((TDbParams)ParamsValue);

            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync()
            => await DelegateRuleHandlerAsync((TDbParams)ParamsValue);

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <returns></returns>
            protected override string GetErrorMessageCore()
            => ErrorMessageHandler();

            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                string message = ErrorMessageHandler();
                await Task.CompletedTask;
                return message;
            }
        }
    }
}