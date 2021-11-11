using DbFacade.Utils;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{TDbParams}" />
    public abstract partial class ValidationRule<TDbParams>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <seealso cref="Rules.IValidationRule{TDbParams}" />
        internal class CompareRule<T> : ValidationRule<TDbParams>
        {

            /// <summary>
            /// Initializes a new instance of the <see cref="CompareRule`1"/> class.
            /// </summary>
            protected CompareRule() { }
            /// <summary>
            /// Gets or sets the type of the compare.
            /// </summary>
            /// <value>
            /// The type of the compare.
            /// </value>
            private string CompareType { get; set; }
            /// <summary>
            /// Gets the limit.
            /// </summary>
            /// <value>
            /// The limit.
            /// </value>
            protected T Limit { get; private set; }

            /// <summary>
            /// Gets the comaparitor.
            /// </summary>
            /// <value>
            /// The comaparitor.
            /// </value>
            protected Func<T, T, bool> Comaparitor { get; private set; }
            /// <summary>
            /// Gets the comaparitor asynchronous.
            /// </summary>
            /// <value>
            /// The comaparitor asynchronous.
            /// </value>
            protected Func<T, T, Task<bool>> ComaparitorAsync { get; private set; }

            /// <summary>
            /// Gets the comparable value.
            /// </summary>
            /// <param name="obj">The object.</param>
            /// <returns></returns>
            protected static IComparable GetComparableValue(T obj)
            {
                if (obj is IComparable comparableObj)
                {
                    return comparableObj;
                }
                Type t = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

                try
                {
                    object convertedObj = Convert.ChangeType(obj, t);
                    if (convertedObj is IComparable convertedConvertedObj)
                    {
                        return convertedConvertedObj;
                    }
                    return 0;
                }
                catch
                {
                    return 0;
                }
            }
            /// <summary>
            /// Gets the comparable value asynchronous.
            /// </summary>
            /// <param name="obj">The object.</param>
            /// <returns></returns>
            protected static async Task<IComparable> GetComparableValueAsync(T obj)
            {
                if (obj is IComparable comparableObj)
                {
                    await Task.CompletedTask;
                    return comparableObj;
                }
                Type t = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

                try
                {
                    object convertedObj = Convert.ChangeType(obj, t);
                    if (convertedObj is IComparable convertedConvertedObj)
                    {
                        await Task.CompletedTask;
                        return convertedConvertedObj;
                    }
                    await Task.CompletedTask;
                    return 0;
                }
                catch
                {
                    await Task.CompletedTask;
                    return 0;
                }
            }
            /// <summary>
            /// Compares the specified value.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            private static int Compare(T value, T limit)
            => GetComparableValue(value).CompareTo(GetComparableValue(limit));



            /// <summary>
            /// Compares the asynchronous.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            private static async Task<int> CompareAsync(T value, T limit)
            {
                IComparable iValue = await GetComparableValueAsync(value);
                IComparable iLimit = await GetComparableValueAsync(limit);
                int compareValue = iValue.CompareTo(iLimit);
                await Task.CompletedTask;
                return compareValue;
            }

            /// <summary>
            /// The is equal to
            /// </summary>
            protected static Func<T,T, bool> IsEqualTo = (v, l) => Compare(v,l) == 0;
            /// <summary>
            /// The is not equal
            /// </summary>
            protected static Func<T,T, bool> IsNotEqual = (v, l) => Compare(v, l) != 0;
            /// <summary>
            /// The is greator than
            /// </summary>
            protected static Func<T,T, bool> IsGreatorThan = (v, l) => Compare(v, l) > 0;
            /// <summary>
            /// The is greater than or equal to
            /// </summary>
            protected static Func<T,T, bool> IsGreaterThanOrEqualTo = (v, l) => Compare(v, l) >= 0;
            /// <summary>
            /// The is less than
            /// </summary>
            protected static Func<T,T, bool> IsLessThan = (v, l) => Compare(v, l) < 0;
            /// <summary>
            /// The is less than or equal to
            /// </summary>
            protected static Func<T,T, bool> IsLessThanOrEqualTo = (v, l) => Compare(v, l) <= 0;


            /// <summary>
            /// The is equal to asynchronous
            /// </summary>
            protected static Func<T, T, Task<bool>> IsEqualToAsync = async (v, l) => await CompareAsync(v, l) == 0;
            /// <summary>
            /// The is not equal asynchronous
            /// </summary>
            protected static Func<T, T, Task<bool>> IsNotEqualAsync = async (v, l) => await CompareAsync(v, l) != 0;
            /// <summary>
            /// The is greator than asynchronous
            /// </summary>
            protected static Func<T, T, Task<bool>> IsGreatorThanAsync = async (v, l) => await CompareAsync(v, l) > 0;
            /// <summary>
            /// The is greater than or equal to asynchronous
            /// </summary>
            protected static Func<T, T, Task<bool>> IsGreaterThanOrEqualToAsync = async (v, l) => await CompareAsync(v, l) >= 0;
            /// <summary>
            /// The is less than asynchronous
            /// </summary>
            protected static Func<T, T, Task<bool>> IsLessThanAsync = async (v, l) => await CompareAsync(v, l) < 0;
            /// <summary>
            /// The is less than or equal to asynchronous
            /// </summary>
            protected static Func<T, T, Task<bool>> IsLessThanOrEqualToAsync = async (v, l) => await CompareAsync(v, l) <= 0;


            /// <summary>
            /// Creates the specified selector.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="comaparitor">The comaparitor.</param>
            /// <param name="compareType">Type of the compare.</param>
            /// <returns></returns>
            private static CompareRule<T> Create(
                Func<TDbParams, T> selector, 
                T limit,
                Func<T, T, bool> comaparitor,
                string compareType
                )
            {
                CompareRule<T> rule = new CompareRule<T>();
                rule.Init(limit, comaparitor, compareType);
                rule.Init(selector, GenericInstance.IsNullableType<T>());
                return rule;
            }
            /// <summary>
            /// Creates the specified selector.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="comaparitor">The comaparitor.</param>
            /// <param name="compareType">Type of the compare.</param>
            /// <returns></returns>
            private static CompareRule<T> Create(
                Func<TDbParams, string> selector,
                T limit,
                Func<T, T, bool> comaparitor,
                string compareType
                )
            {
                CompareRule<T> rule = new CompareRule<T>();
                rule.Init(limit, comaparitor, compareType);
                rule.Init(selector, true);
                return rule;
            }

            /// <summary>
            /// Initializes the specified limit.
            /// </summary>
            /// <param name="limit">The limit.</param>
            /// <param name="comaparitor">The comaparitor.</param>
            /// <param name="compareType">Type of the compare.</param>
            protected void Init(
                T limit,
                Func<T, T, bool> comaparitor,
                string compareType
                )
            {
                Limit = limit;
                CompareType = compareType;
                Comaparitor = comaparitor;
            }


            /// <summary>
            /// Creates the is equal.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsEqual(Func<TDbParams, T> selector, T limit)
                => Create(selector, limit, IsEqualTo, "to be equal to");
            /// <summary>
            /// Creates the is not equal.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsNotEqual(Func<TDbParams, T> selector, T limit)
                => Create(selector, limit, IsNotEqual, "to not be equal to");
            /// <summary>
            /// Creates the is greater than.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsGreaterThan(Func<TDbParams, T> selector, T limit)
                => Create(selector, limit, IsGreatorThan, "to be greater than");
            /// <summary>
            /// Creates the is greater than or equal to.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsGreaterThanOrEqualTo(Func<TDbParams, T> selector, T limit)
                => Create(selector, limit, IsGreaterThanOrEqualTo, "to be greater than or equal to");
            /// <summary>
            /// Creates the is less than.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsLessThan(Func<TDbParams, T> selector, T limit)
                => Create(selector, limit, IsLessThan, "to be less than");
            /// <summary>
            /// Creates the is less than or equal to.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsLessThanOrEqualTo(Func<TDbParams, T> selector, T limit)
                => Create(selector, limit, IsLessThanOrEqualTo, "to be less than or equal to");


            /// <summary>
            /// Creates the is equal.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsEqual(Func<TDbParams, string> selector, T limit)
                => Create(selector, limit, IsEqualTo, "to be equal to");
            /// <summary>
            /// Creates the is not equal.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsNotEqual(Func<TDbParams, string> selector, T limit)
                => Create(selector, limit, IsNotEqual, "to not be equal to");
            /// <summary>
            /// Creates the is greater than.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsGreaterThan(Func<TDbParams, string> selector, T limit)
                => Create(selector, limit, IsGreatorThan, "to be greater than");
            /// <summary>
            /// Creates the is greater than or equal to.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsGreaterThanOrEqualTo(Func<TDbParams, string> selector, T limit)
                => Create(selector, limit, IsGreaterThanOrEqualTo, "to be greater than or equal to");
            /// <summary>
            /// Creates the is less than.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsLessThan(Func<TDbParams, string> selector, T limit)
                => Create(selector, limit, IsLessThan, "to be less than");
            /// <summary>
            /// Creates the is less than or equal to.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsLessThanOrEqualTo(Func<TDbParams, string> selector, T limit)
                => Create(selector, limit, IsLessThanOrEqualTo, "to be less than or equal to");



            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="comaparitor">The comaparitor.</param>
            /// <param name="compareType">Type of the compare.</param>
            /// <returns></returns>
            private static async Task<CompareRule<T>> CreateAsync(
                Func<TDbParams, T> selector,
                T limit,
                Func<T, T, Task<bool>> comaparitor,
                string compareType
                )
            {
                CompareRule<T> rule = new CompareRule<T>();
                await rule.InitAsync(limit, comaparitor, compareType);
                await rule.InitAsync(selector, await GenericInstance.IsNullableTypeAsync<T>());
                return rule;
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="comaparitor">The comaparitor.</param>
            /// <param name="compareType">Type of the compare.</param>
            /// <returns></returns>
            private static async Task<CompareRule<T>> CreateAsync(
                Func<TDbParams, string> selector,
                T limit,
                Func<T, T, Task<bool>> comaparitor,
                string compareType
                )
            {
                CompareRule<T> rule = new CompareRule<T>();
                await rule.InitAsync(limit, comaparitor, compareType);
                await rule.InitAsync(selector, true);
                return rule;
            }
            /// <summary>
            /// Initializes the asynchronous.
            /// </summary>
            /// <param name="limit">The limit.</param>
            /// <param name="comaparitor">The comaparitor.</param>
            /// <param name="compareType">Type of the compare.</param>
            protected virtual async Task InitAsync(
                T limit,
                Func<T, T, Task<bool>> comaparitor,
                string compareType
                )
            {
                Limit = limit;
                CompareType = compareType;
                ComaparitorAsync = comaparitor;
                await Task.CompletedTask;
            }

            /// <summary>
            /// Creates the is equal asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static async Task<CompareRule<T>> CreateIsEqualAsync(Func<TDbParams, T> selector, T limit)
                => await CreateAsync(selector, limit, IsEqualToAsync, "to be equal to");
            /// <summary>
            /// Creates the is not equal asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static async Task<CompareRule<T>> CreateIsNotEqualAsync(Func<TDbParams, T> selector, T limit)
                => await CreateAsync(selector, limit, IsNotEqualAsync, "to not be equal to");
            /// <summary>
            /// Creates the is greater than asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static async Task<CompareRule<T>> CreateIsGreaterThanAsync(Func<TDbParams, T> selector, T limit)
                => await CreateAsync(selector, limit, IsGreatorThanAsync, "to be greater than");
            /// <summary>
            /// Creates the is greater than or equal to asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static async Task<CompareRule<T>> CreateIsGreaterThanOrEqualToAsync(Func<TDbParams, T> selector, T limit)
                => await CreateAsync(selector, limit, IsGreaterThanOrEqualToAsync, "to be greater than or equal to");
            /// <summary>
            /// Creates the is less than asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static async Task<CompareRule<T>> CreateIsLessThanAsync(Func<TDbParams, T> selector, T limit)
                => await CreateAsync(selector, limit, IsLessThanAsync, "to be less than");
            /// <summary>
            /// Creates the is less than or equal to asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static async Task<CompareRule<T>> CreateIsLessThanOrEqualToAsync(Func<TDbParams, T> selector, T limit)
                => await CreateAsync(selector, limit, IsLessThanOrEqualToAsync, "to be less than or equal to");

            /// <summary>
            /// Creates the is equal asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static async Task<CompareRule<T>> CreateIsEqualAsync(Func<TDbParams, string> selector, T limit)
                => await CreateAsync(selector, limit, IsEqualToAsync, "to be equal to");
            /// <summary>
            /// Creates the is not equal asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static async Task<CompareRule<T>> CreateIsNotEqualAsync(Func<TDbParams, string> selector, T limit)
                => await CreateAsync(selector, limit, IsNotEqualAsync, "to not be equal to");
            /// <summary>
            /// Creates the is greater than asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static async Task<CompareRule<T>> CreateIsGreaterThanAsync(Func<TDbParams, string> selector, T limit)
                => await CreateAsync(selector, limit, IsGreatorThanAsync, "to be greater than");
            /// <summary>
            /// Creates the is greater than or equal to asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static async Task<CompareRule<T>> CreateIsGreaterThanOrEqualToAsync(Func<TDbParams, string> selector, T limit)
                => await CreateAsync(selector, limit, IsGreaterThanOrEqualToAsync, "to be greater than or equal to");
            /// <summary>
            /// Creates the is less than asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static async Task<CompareRule<T>> CreateIsLessThanAsync(Func<TDbParams, string> selector, T limit)
                => await CreateAsync(selector, limit, IsLessThanAsync, "to be less than");
            /// <summary>
            /// Creates the is less than or equal to asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <returns></returns>
            public static async Task<CompareRule<T>> CreateIsLessThanOrEqualToAsync(Func<TDbParams, string> selector, T limit)
                => await CreateAsync(selector, limit, IsLessThanOrEqualToAsync, "to be less than or equal to");


            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                if(ParamsValue is string val)
                {
                    if (!Converter<T>.CanConvertFromString)
                    {
                        return false;
                    }
                    try
                    {                        
                        T convertedValue = Converter<T>.FromString(val);
                        return Comaparitor(convertedValue, Limit);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return Comaparitor((T)ParamsValue, Limit);
            }
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync()
            {
                if (ParamsValue is string val)
                {
                    if (!Converter<T>.CanConvertFromString)
                    {
                        await Task.CompletedTask;
                        return false;
                    }
                    try
                    {
                        T convertedValue = await Converter<T>.FromStringAsync(val);
                        return await ComaparitorAsync(convertedValue, Limit);
                    }
                    catch
                    {
                        await Task.CompletedTask;
                        return false;
                    }
                }
                return await ComaparitorAsync((T)ParamsValue, Limit);
            }

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <returns></returns>
            protected override string GetErrorMessageCore()
            {
                return $"expecting value {CompareType} {Limit}";
            }

        }
        /// <summary>
        /// 
        /// </summary>
        internal class CompareDateRule : CompareRule<DateTime> 
        {
            /// <summary>
            /// Prevents a default instance of the <see cref="CompareDateRule"/> class from being created.
            /// </summary>
            private CompareDateRule() { }
            /// <summary>
            /// Gets or sets the culture information.
            /// </summary>
            /// <value>
            /// The culture information.
            /// </value>
            private CultureInfo CultureInfo { get; set; }
            /// <summary>
            /// Gets or sets the date time styles.
            /// </summary>
            /// <value>
            /// The date time styles.
            /// </value>
            private DateTimeStyles DateTimeStyles { get; set; }
            /// <summary>
            /// Gets or sets the date format.
            /// </summary>
            /// <value>
            /// The date format.
            /// </value>
            private string DateFormat { get; set; }

            /// <summary>
            /// Creates the specified selector.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="comparitor">The comparitor.</param>
            /// <param name="compareType">Type of the compare.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            private static CompareDateRule Create(
                Func<TDbParams, DateTime?> selector,
                DateTime limit,
                Func<DateTime, DateTime, bool> comparitor,
                string compareType,
                CultureInfo cultureInfo = null,
                DateTimeStyles dateTimeStyles = DateTimeStyles.None
                )
            {
                CompareDateRule rule = new CompareDateRule();
                rule.Init(limit, comparitor, compareType, cultureInfo, dateTimeStyles);
                rule.Init(selector, true);
                return rule;
            }
            /// <summary>
            /// Creates the specified selector.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="comparitor">The comparitor.</param>
            /// <param name="compareType">Type of the compare.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            private static CompareDateRule Create(
                Func<TDbParams, string> selector,
                DateTime limit,
                Func<DateTime, DateTime, bool> comparitor,
                string compareType,
                CultureInfo cultureInfo = null,
                DateTimeStyles dateTimeStyles = DateTimeStyles.None,
                string dateFormat = null,
                bool isNullable = true
                )
            {
                CompareDateRule rule = new CompareDateRule();
                rule.Init(limit, comparitor, compareType, cultureInfo, dateTimeStyles, dateFormat);
                rule.Init(selector, isNullable);
                return rule;
            }
            /// <summary>
            /// Initializes the specified limit.
            /// </summary>
            /// <param name="limit">The limit.</param>
            /// <param name="comparitor">The comparitor.</param>
            /// <param name="compareType">Type of the compare.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <param name="dateFormat">The date format.</param>
            private void Init(
                DateTime limit,
                Func<DateTime, DateTime, bool> comparitor,
                string compareType, 
                CultureInfo cultureInfo = null,
                DateTimeStyles dateTimeStyles = DateTimeStyles.None,
                string dateFormat = null
                )
            {
                CultureInfo = cultureInfo is CultureInfo ci ? ci : CultureInfo.InvariantCulture;
                DateTimeStyles = dateTimeStyles;
                DateFormat = dateFormat;
                base.Init(limit, comparitor, compareType);
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="comparitor">The comparitor.</param>
            /// <param name="compareType">Type of the compare.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            private static async Task<CompareDateRule> CreateAsync(
                Func<TDbParams, DateTime?> selector,
                DateTime limit,
                Func<DateTime, DateTime, Task<bool>> comparitor,
                string compareType,
                CultureInfo cultureInfo = null,
                DateTimeStyles dateTimeStyles = DateTimeStyles.None
                )
            {
                CompareDateRule rule = new CompareDateRule();
                await rule.InitAsync(limit, comparitor, compareType, cultureInfo, dateTimeStyles);
                await rule.InitAsync(selector, true);
                return rule;
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="comparitor">The comparitor.</param>
            /// <param name="compareType">Type of the compare.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            private static async Task<CompareDateRule> CreateAsync(
                Func<TDbParams, string> selector,
                DateTime limit,
                Func<DateTime, DateTime, Task<bool>> comparitor,
                string compareType,
                CultureInfo cultureInfo = null,
                DateTimeStyles dateTimeStyles = DateTimeStyles.None,
                string dateFormat = null,
                bool isNullable = true
                )
            {
                CompareDateRule rule = new CompareDateRule();
                await rule.InitAsync(limit, comparitor, compareType, cultureInfo, dateTimeStyles, dateFormat);
                await rule.InitAsync(selector, isNullable);
                return rule;
            }
            /// <summary>
            /// Initializes the asynchronous.
            /// </summary>
            /// <param name="limit">The limit.</param>
            /// <param name="comparitor">The comparitor.</param>
            /// <param name="compareType">Type of the compare.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <param name="dateFormat">The date format.</param>
            private async Task InitAsync(
                DateTime limit,
                Func<DateTime, DateTime, Task<bool>> comparitor,
                string compareType,
                CultureInfo cultureInfo = null,
                DateTimeStyles dateTimeStyles = DateTimeStyles.None,
                string dateFormat = null
                )
            {
                CultureInfo = cultureInfo is CultureInfo ci ? ci : CultureInfo.InvariantCulture;
                DateTimeStyles = dateTimeStyles;
                DateFormat = dateFormat;
                await base.InitAsync(limit, comparitor, compareType);
            }


            /// <summary>
            /// Creates the is equal.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsEqual(Func<TDbParams, DateTime?> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsEqualTo, "to be equal to", cultureInfo, dateTimeStyles);
            /// <summary>
            /// Creates the is not equal.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsNotEqual(Func<TDbParams, DateTime?> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsNotEqual, "to not be equal to", cultureInfo, dateTimeStyles);
            /// <summary>
            /// Creates the is after.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsAfter(Func<TDbParams, DateTime?> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsGreatorThan, "to be after", cultureInfo, dateTimeStyles);
            /// <summary>
            /// Creates the is on or after.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsOnOrAfter(Func<TDbParams, DateTime?> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsGreaterThanOrEqualTo, "to be greater than or equal to", cultureInfo, dateTimeStyles);
            /// <summary>
            /// Creates the is before.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsBefore(Func<TDbParams, DateTime?> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsLessThan, "to be less than", cultureInfo, dateTimeStyles);
            /// <summary>
            /// Creates the is on or before.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsOnOrBefore(Func<TDbParams, DateTime?> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsLessThanOrEqualTo, "to be less than or equal to", cultureInfo, dateTimeStyles);


            /// <summary>
            /// Creates the is equal.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsEqual(Func<TDbParams, string> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsEqualTo, "to be equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            /// <summary>
            /// Creates the is not equal.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsNotEqual(Func<TDbParams, string> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsNotEqual, "to not be equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            /// <summary>
            /// Creates the is after.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsAfter(Func<TDbParams, string> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsGreatorThan, "to be after", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            /// <summary>
            /// Creates the is on or after.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsOnOrAfter(Func<TDbParams, string> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsGreaterThanOrEqualTo, "to be greater than or equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            /// <summary>
            /// Creates the is before.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsBefore(Func<TDbParams, string> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsLessThan, "to be less than", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            /// <summary>
            /// Creates the is on or before.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsOnOrBefore(Func<TDbParams, string> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, limit, IsLessThanOrEqualTo, "to be less than or equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);



            /// <summary>
            /// Creates the is equal asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static async Task<CompareDateRule> CreateIsEqualAsync(Func<TDbParams, DateTime?> selector, DateTime limit, CultureInfo cultureInfo = null,
           DateTimeStyles dateTimeStyles = DateTimeStyles.None)
               => await CreateAsync(selector, limit, IsEqualToAsync, "to be equal to", cultureInfo, dateTimeStyles);
            /// <summary>
            /// Creates the is not equal asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static async Task<CompareDateRule> CreateIsNotEqualAsync(Func<TDbParams, DateTime?> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsNotEqualAsync, "to not be equal to", cultureInfo, dateTimeStyles);
            /// <summary>
            /// Creates the is after asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static async Task<CompareDateRule> CreateIsAfterAsync(Func<TDbParams, DateTime?> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsGreatorThanAsync, "to be after", cultureInfo, dateTimeStyles);
            /// <summary>
            /// Creates the is on or after asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static async Task<CompareDateRule> CreateIsOnOrAfterAsync(Func<TDbParams, DateTime?> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsGreaterThanOrEqualToAsync, "to be greater than or equal to", cultureInfo, dateTimeStyles);
            /// <summary>
            /// Creates the is before asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static async Task<CompareDateRule> CreateIsBeforeAsync(Func<TDbParams, DateTime?> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsLessThanAsync, "to be less than", cultureInfo, dateTimeStyles);
            /// <summary>
            /// Creates the is on or before asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static async Task<CompareDateRule> CreateIsOnOrBeforeAsync(Func<TDbParams, DateTime?> selector, DateTime limit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsLessThanOrEqualToAsync, "to be less than or equal to", cultureInfo, dateTimeStyles);


            /// <summary>
            /// Creates the is equal asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static async Task<CompareDateRule> CreateIsEqualAsync(Func<TDbParams, string> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsEqualToAsync, "to be equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            /// <summary>
            /// Creates the is not equal asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static async Task<CompareDateRule> CreateIsNotEqualAsync(Func<TDbParams, string> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsNotEqualAsync, "to not be equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            /// <summary>
            /// Creates the is after asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static async Task<CompareDateRule> CreateIsAfterAsync(Func<TDbParams, string> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsGreatorThanAsync, "to be after", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            /// <summary>
            /// Creates the is on or after asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static async Task<CompareDateRule> CreateIsOnOrAfterAsync(Func<TDbParams, string> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsGreaterThanOrEqualToAsync, "to be greater than or equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            /// <summary>
            /// Creates the is before asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static async Task<CompareDateRule> CreateIsBeforeAsync(Func<TDbParams, string> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsLessThanAsync, "to be less than", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            /// <summary>
            /// Creates the is on or before asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="limit">The limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static async Task<CompareDateRule> CreateIsOnOrBeforeAsync(Func<TDbParams, string> selector, DateTime limit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => await CreateAsync(selector, limit, IsLessThanOrEqualToAsync, "to be less than or equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);

            /// <summary>
            /// Gets the date time from string.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private DateTime? GetDateTimeFromString(string value)
            {
                DateTime? dateTime = null;
                DateTime dateTimeParsed;
                bool isDate = !string.IsNullOrEmpty(DateFormat)
                ? DateTime.TryParseExact(value, DateFormat, CultureInfo, DateTimeStyles, out dateTimeParsed)
                : DateTime.TryParse(value, CultureInfo, DateTimeStyles, out dateTimeParsed);
                if (isDate)
                {
                    dateTime = dateTimeParsed;
                }
                return dateTime;
            }
            /// <summary>
            /// Gets the date time from string asynchronous.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            private async Task<DateTime?> GetDateTimeFromStringAsync(string value)
            {
                DateTime? dateTime = null;
                DateTime dateTimeParsed;
                bool isDate = !string.IsNullOrEmpty(DateFormat)
                ? DateTime.TryParseExact(value, DateFormat, CultureInfo, DateTimeStyles, out dateTimeParsed)
                : DateTime.TryParse(value, CultureInfo, DateTimeStyles, out dateTimeParsed);
                if (isDate)
                {
                    dateTime = dateTimeParsed;
                }
                await Task.CompletedTask;
                return dateTime;
            }
            /// <summary>
            /// Gets the date time.
            /// </summary>
            /// <returns></returns>
            private DateTime? GetDateTime()
            {
                return ParamsValue is DateTime date ? date : ParamsValue is string dateStr ? GetDateTimeFromString(dateStr) : null;
            }
            /// <summary>
            /// Gets the date time asynchronous.
            /// </summary>
            /// <returns></returns>
            private async Task<DateTime?> GetDateTimeAsync()
            {
                if (ParamsValue is string dateStr)
                {
                    return await GetDateTimeFromStringAsync(dateStr);
                }
                DateTime? dateTime = null;
                if (ParamsValue is DateTime date)
                {
                    dateTime = date;
                }
                await Task.CompletedTask;
                return dateTime;
            }
            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            => GetDateTime() is DateTime date ? Comaparitor(date, Limit) : false;
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync()
            {
                DateTime? dateTime = await GetDateTimeAsync();
                if (dateTime is DateTime date)
                {
                    return await ComaparitorAsync(date, Limit);
                }
                return false;
            }

        }


    }
}