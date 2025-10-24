using System;

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
        internal class DelegateRule<T> : ValidationRule<TDbParams>
        {
            /// <summary>
            /// Gets or sets the delegate rule handler.
            /// </summary>
            /// <value>
            /// The delegate rule handler.
            /// </value>
            private Func<T, bool> DelegateRuleHandler { get; set; }

            /// <summary>
            /// Prevents a default instance of the <see cref="DelegateRule`1" /> class from being created.
            /// </summary>
            private DelegateRule() { }

            /// <summary>
            /// Initializes a new instance of the <see cref="DelegateRule`1" /> class.
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
            /// Validates the rule.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override bool ValidateRule(TDbParams paramsModel)
            => DelegateRuleHandler((T)ParamsValue);

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(TDbParams paramsModel)
            => $"expecting delegate to pass validation";


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
            /// Gets or sets the error message handler.
            /// </summary>
            /// <value>
            /// The error message handler.
            /// </value>
            private Func<string> ErrorMessageHandler { get; set; }
            /// <summary>
            /// Prevents a default instance of the <see cref="DelegateRule" /> class from being created.
            /// </summary>
            private DelegateRule() { }

            /// <summary>
            /// Initializes a new instance of the <see cref="DelegateRule" /> class.
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
            /// Validates the rule.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override bool ValidateRule(TDbParams paramsModel)
            => DelegateRuleHandler((TDbParams)ParamsValue);

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(TDbParams paramsModel)
            => ErrorMessageHandler();
        }
    }
}