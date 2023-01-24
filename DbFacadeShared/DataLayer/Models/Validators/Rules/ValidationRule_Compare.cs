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
            /// Initializes a new instance of the <see cref="CompareRule`1" /> class.
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
            /// Gets the get limit.
            /// </summary>
            /// <value>
            /// The get limit.
            /// </value>
            protected Func<TDbParams, T> GetLimit { get; private set; }

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
            protected static Func<T, T, bool> IsEqualTo = (v, l) => Compare(v, l) == 0;
            /// <summary>
            /// The is not equal
            /// </summary>
            protected static Func<T, T, bool> IsNotEqual = (v, l) => Compare(v, l) != 0;
            /// <summary>
            /// The is greator than
            /// </summary>
            protected static Func<T, T, bool> IsGreatorThan = (v, l) => Compare(v, l) > 0;
            /// <summary>
            /// The is greater than or equal to
            /// </summary>
            protected static Func<T, T, bool> IsGreaterThanOrEqualTo = (v, l) => Compare(v, l) >= 0;
            /// <summary>
            /// The is less than
            /// </summary>
            protected static Func<T, T, bool> IsLessThan = (v, l) => Compare(v, l) < 0;
            /// <summary>
            /// The is less than or equal to
            /// </summary>
            protected static Func<T, T, bool> IsLessThanOrEqualTo = (v, l) => Compare(v, l) <= 0;


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
            /// <param name="getLimit">The get limit.</param>
            /// <param name="comaparitor">The comaparitor.</param>
            /// <param name="comparitorAsync">The comparitor asynchronous.</param>
            /// <param name="compareType">Type of the compare.</param>
            /// <returns></returns>
            private static CompareRule<T> Create(
                Func<TDbParams, T> selector,
                Func<TDbParams, T> getLimit,
                Func<T, T, bool> comaparitor,
                Func<T, T, Task<bool>> comparitorAsync,
                string compareType
                )
            {
                CompareRule<T> rule = new CompareRule<T>();
                rule.Init(getLimit, comaparitor, comparitorAsync, compareType);
                rule.Init(selector, GenericInstance.IsNullableType<T>());
                return rule;
            }
            /// <summary>
            /// Creates the specified selector.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <param name="comaparitor">The comaparitor.</param>
            /// <param name="comparitorAsync">The comparitor asynchronous.</param>
            /// <param name="compareType">Type of the compare.</param>
            /// <returns></returns>
            private static CompareRule<T> Create(
                Func<TDbParams, string> selector,
                Func<TDbParams, T> getLimit,
                Func<T, T, bool> comaparitor,
                Func<T, T, Task<bool>> comparitorAsync,
                string compareType
                )
            {
                CompareRule<T> rule = new CompareRule<T>();
                rule.Init(getLimit, comaparitor, comparitorAsync, compareType);
                rule.Init(selector, true);
                return rule;
            }

            /// <summary>
            /// Initializes the specified limit.
            /// </summary>
            /// <param name="getLimit">The get limit.</param>
            /// <param name="comaparitor">The comaparitor.</param>
            /// <param name="comparitorAsync">The comparitor asynchronous.</param>
            /// <param name="compareType">Type of the compare.</param>
            protected void Init(
                Func<TDbParams, T> getLimit,
                Func<T, T, bool> comaparitor,
                Func<T, T, Task<bool>> comparitorAsync,
                string compareType
                )
            {
                GetLimit = getLimit;
                CompareType = compareType;
                Comaparitor = comaparitor;
                ComaparitorAsync = comparitorAsync;
            }


            /// <summary>
            /// Creates the is equal.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">Gets The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsEqual(Func<TDbParams, T> selector, Func<TDbParams, T> getLimit)
                => Create(selector, getLimit, IsEqualTo, IsEqualToAsync, "to be equal to");
            /// <summary>
            /// Creates the is not equal.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">Gets The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsNotEqual(Func<TDbParams, T> selector, Func<TDbParams, T> getLimit)
                => Create(selector, getLimit, IsNotEqual, IsNotEqualAsync, "to not be equal to");
            /// <summary>
            /// Creates the is greater than.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">Gets The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsGreaterThan(Func<TDbParams, T> selector, Func<TDbParams, T> getLimit)
                => Create(selector, getLimit, IsGreatorThan, IsGreatorThanAsync, "to be greater than");
            /// <summary>
            /// Creates the is greater than or equal to.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">Gets The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsGreaterThanOrEqualTo(Func<TDbParams, T> selector, Func<TDbParams, T> getLimit)
                => Create(selector, getLimit, IsGreaterThanOrEqualTo, IsGreaterThanOrEqualToAsync, "to be greater than or equal to");
            /// <summary>
            /// Creates the is less than.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">Gets The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsLessThan(Func<TDbParams, T> selector, Func<TDbParams, T> getLimit)
                => Create(selector, getLimit, IsLessThan, IsLessThanAsync, "to be less than");
            /// <summary>
            /// Creates the is less than or equal to.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">Gets The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsLessThanOrEqualTo(Func<TDbParams, T> selector, Func<TDbParams, T> getLimit)
                => Create(selector, getLimit, IsLessThanOrEqualTo, IsLessThanOrEqualToAsync, "to be less than or equal to");


            /// <summary>
            /// Creates the is equal.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">Gets The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsEqual(Func<TDbParams, string> selector, Func<TDbParams, T> getLimit)
                => Create(selector, getLimit, IsEqualTo, IsEqualToAsync, "to be equal to");
            /// <summary>
            /// Creates the is not equal.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">Gets The limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsNotEqual(Func<TDbParams, string> selector, Func<TDbParams, T> getLimit)
                => Create(selector, getLimit, IsNotEqual, IsNotEqualAsync, "to not be equal to");
            /// <summary>
            /// Creates the is greater than.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsGreaterThan(Func<TDbParams, string> selector, Func<TDbParams, T> getLimit)
                => Create(selector, getLimit, IsGreatorThan, IsGreatorThanAsync, "to be greater than");
            /// <summary>
            /// Creates the is greater than or equal to.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsGreaterThanOrEqualTo(Func<TDbParams, string> selector, Func<TDbParams, T> getLimit)
                => Create(selector, getLimit, IsGreaterThanOrEqualTo, IsGreaterThanOrEqualToAsync, "to be greater than or equal to");
            /// <summary>
            /// Creates the is less than.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsLessThan(Func<TDbParams, string> selector, Func<TDbParams, T> getLimit)
                => Create(selector, getLimit, IsLessThan, IsLessThanAsync, "to be less than");
            /// <summary>
            /// Creates the is less than or equal to.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <returns></returns>
            public static CompareRule<T> CreateIsLessThanOrEqualTo(Func<TDbParams, string> selector, Func<TDbParams, T> getLimit)
                => Create(selector, getLimit, IsLessThanOrEqualTo, IsLessThanOrEqualToAsync, "to be less than or equal to");


            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override bool ValidateRule(TDbParams paramsModel)
            {
                T limit = GetLimit(paramsModel);
                if (ParamsValue is string val)
                {
                    if (!Converter<T>.CanConvertFromString)
                    {
                        return false;
                    }
                    try
                    {
                        T convertedValue = Converter<T>.FromString(val);
                        return Comaparitor(convertedValue, limit);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return Comaparitor((T)ParamsValue, limit);
            }
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync(TDbParams paramsModel)
            {
                T limit = GetLimit(paramsModel);
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
                        return await ComaparitorAsync(convertedValue, limit);
                    }
                    catch
                    {
                        await Task.CompletedTask;
                        return false;
                    }
                }
                return await ComaparitorAsync((T)ParamsValue, limit);
            }

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(TDbParams paramsModel)
            {
                return $"expecting value {CompareType} {GetLimit(paramsModel)}";
            }

        }
        /// <summary>
        /// 
        /// </summary>
        internal class CompareDateRule : CompareRule<DateTime>
        {
            /// <summary>
            /// Prevents a default instance of the <see cref="CompareDateRule" /> class from being created.
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
            /// <param name="getLimit">The get limit.</param>
            /// <param name="comparitor">The comparitor.</param>
            /// <param name="comparitorAsync">The comparitor asynchronous.</param>
            /// <param name="compareType">Type of the compare.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            private static CompareDateRule Create(
                Func<TDbParams, DateTime?> selector,
                Func<TDbParams, DateTime> getLimit,
                Func<DateTime, DateTime, bool> comparitor,
                Func<DateTime, DateTime, Task<bool>> comparitorAsync,
                string compareType,
                CultureInfo cultureInfo = null,
                DateTimeStyles dateTimeStyles = DateTimeStyles.None
                )
            {
                CompareDateRule rule = new CompareDateRule();
                rule.Init(getLimit, comparitor, comparitorAsync, compareType, cultureInfo, dateTimeStyles);
                rule.Init(selector, true);
                return rule;
            }
            /// <summary>
            /// Creates the specified selector.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <param name="comparitor">The comparitor.</param>
            /// <param name="comparitorAsync">The comparitor asynchronous.</param>
            /// <param name="compareType">Type of the compare.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            private static CompareDateRule Create(
                Func<TDbParams, string> selector,
                Func<TDbParams, DateTime> getLimit,
                Func<DateTime, DateTime, bool> comparitor,
                Func<DateTime, DateTime, Task<bool>> comparitorAsync,
                string compareType,
                CultureInfo cultureInfo = null,
                DateTimeStyles dateTimeStyles = DateTimeStyles.None,
                string dateFormat = null,
                bool isNullable = true
                )
            {
                CompareDateRule rule = new CompareDateRule();
                rule.Init(getLimit, comparitor, comparitorAsync, compareType, cultureInfo, dateTimeStyles, dateFormat);
                rule.Init(selector, isNullable);
                return rule;
            }
            /// <summary>
            /// Initializes the specified limit.
            /// </summary>
            /// <param name="getLimit">The get limit.</param>
            /// <param name="comparitor">The comparitor.</param>
            /// <param name="comparitorAsync">The comparitor asynchronous.</param>
            /// <param name="compareType">Type of the compare.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <param name="dateFormat">The date format.</param>
            private void Init(
                Func<TDbParams, DateTime> getLimit,
                Func<DateTime, DateTime, bool> comparitor,
                Func<DateTime, DateTime, Task<bool>> comparitorAsync,
                string compareType,
                CultureInfo cultureInfo = null,
                DateTimeStyles dateTimeStyles = DateTimeStyles.None,
                string dateFormat = null
                )
            {
                CultureInfo = cultureInfo is CultureInfo ci ? ci : CultureInfo.InvariantCulture;
                DateTimeStyles = dateTimeStyles;
                DateFormat = dateFormat;
                base.Init(getLimit, comparitor, comparitorAsync, compareType);
            }

            /// <summary>
            /// Creates the is equal.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsEqual(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> getLimit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, getLimit, IsEqualTo, IsEqualToAsync, "to be equal to", cultureInfo, dateTimeStyles);
            /// <summary>
            /// Creates the is not equal.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsNotEqual(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> getLimit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, getLimit, IsNotEqual, IsNotEqualAsync, "to not be equal to", cultureInfo, dateTimeStyles);
            /// <summary>
            /// Creates the is after.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsAfter(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> getLimit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, getLimit, IsGreatorThan, IsGreatorThanAsync, "to be after", cultureInfo, dateTimeStyles);
            /// <summary>
            /// Creates the is on or after.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsOnOrAfter(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> getLimit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, getLimit, IsGreaterThanOrEqualTo, IsGreaterThanOrEqualToAsync, "to be greater than or equal to", cultureInfo, dateTimeStyles);
            /// <summary>
            /// Creates the is before.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsBefore(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> getLimit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, getLimit, IsLessThan, IsLessThanAsync, "to be less than", cultureInfo, dateTimeStyles);
            /// <summary>
            /// Creates the is on or before.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsOnOrBefore(Func<TDbParams, DateTime?> selector, Func<TDbParams, DateTime> getLimit, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, getLimit, IsLessThanOrEqualTo, IsLessThanOrEqualToAsync, "to be less than or equal to", cultureInfo, dateTimeStyles);


            /// <summary>
            /// Creates the is equal.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsEqual(Func<TDbParams, string> selector, Func<TDbParams, DateTime> getLimit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, getLimit, IsEqualTo, IsEqualToAsync, "to be equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            /// <summary>
            /// Creates the is not equal.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsNotEqual(Func<TDbParams, string> selector, Func<TDbParams, DateTime> getLimit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, getLimit, IsNotEqual, IsNotEqualAsync, "to not be equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            /// <summary>
            /// Creates the is after.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsAfter(Func<TDbParams, string> selector, Func<TDbParams, DateTime> getLimit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, getLimit, IsGreatorThan, IsGreatorThanAsync, "to be after", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            /// <summary>
            /// Creates the is on or after.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsOnOrAfter(Func<TDbParams, string> selector, Func<TDbParams, DateTime> getLimit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, getLimit, IsGreaterThanOrEqualTo, IsGreaterThanOrEqualToAsync, "to be greater than or equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            /// <summary>
            /// Creates the is before.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsBefore(Func<TDbParams, string> selector, Func<TDbParams, DateTime> getLimit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, getLimit, IsLessThan, IsLessThanAsync, "to be less than", cultureInfo, dateTimeStyles, dateFormat, isNullable);
            /// <summary>
            /// Creates the is on or before.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="getLimit">The get limit.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            /// <returns></returns>
            public static CompareDateRule CreateIsOnOrBefore(Func<TDbParams, string> selector, Func<TDbParams, DateTime> getLimit, string dateFormat,
                bool isNullable = true, CultureInfo cultureInfo = null,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None)
                => Create(selector, getLimit, IsLessThanOrEqualTo, IsLessThanOrEqualToAsync, "to be less than or equal to", cultureInfo, dateTimeStyles, dateFormat, isNullable);




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
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override bool ValidateRule(TDbParams paramsModel)
            => GetDateTime() is DateTime date ? Comaparitor(date, GetLimit(paramsModel)) : false;
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync(TDbParams paramsModel)
            {
                DateTime? dateTime = await GetDateTimeAsync();
                if (dateTime is DateTime date)
                {
                    return await ComaparitorAsync(date, GetLimit(paramsModel));
                }
                return false;
            }

        }


    }
}