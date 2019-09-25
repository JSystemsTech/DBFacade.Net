using System;
using System.Linq.Expressions;

namespace DBFacade.DataLayer.Models.Validators.Rules
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{TDbParams}" />
    public abstract partial class ValidationRule<TDbParams>
        where TDbParams : IDbParamsModel
    {   
        private bool IsNumeric<T>(string value, Func<string,T> converter)
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
            else if (value.GetType() == typeof(string))
            {
                string str = value.ToString();
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

        public static IValidationRule<TDbParams> IsNumber(Expression<Func<TDbParams, string>> selector, bool isNullable = false) => new IsNummeric(selector, isNullable);
        private class IsNummeric : ValidationRule<TDbParams>
        {
            
            public IsNummeric(Expression<Func<TDbParams, string>> selector, bool isNullable = false) : base(selector, isNullable) { }
            public IsNummeric(Expression<Func<TDbParams, short>> selector) : base(selector) { }
            public IsNummeric(Expression<Func<TDbParams, int>> selector) : base(selector) { }
            public IsNummeric(Expression<Func<TDbParams, long>> selector) : base(selector) { }
            public IsNummeric(Expression<Func<TDbParams, double>> selector) : base(selector) { }
            public IsNummeric(Expression<Func<TDbParams, float>> selector) : base(selector) { }
            public IsNummeric(Expression<Func<TDbParams, decimal>> selector) : base(selector) { }
            public IsNummeric(Expression<Func<TDbParams, ushort>> selector) : base(selector) { }
            public IsNummeric(Expression<Func<TDbParams, uint>> selector) : base(selector) { }
            public IsNummeric(Expression<Func<TDbParams, ulong>> selector) : base(selector) { }

            public IsNummeric(Expression<Func<TDbParams, short?>> selector) : base(selector) { }
            public IsNummeric(Expression<Func<TDbParams, int?>> selector) : base(selector) { }
            public IsNummeric(Expression<Func<TDbParams, long?>> selector) : base(selector) { }
            public IsNummeric(Expression<Func<TDbParams, double?>> selector) : base(selector) { }
            public IsNummeric(Expression<Func<TDbParams, float?>> selector) : base(selector) { }
            public IsNummeric(Expression<Func<TDbParams, decimal?>> selector) : base(selector) { }
            public IsNummeric(Expression<Func<TDbParams, ushort?>> selector) : base(selector) { }
            public IsNummeric(Expression<Func<TDbParams, uint?>> selector) : base(selector) { }
            public IsNummeric(Expression<Func<TDbParams, ulong?>> selector) : base(selector) { }

            protected override bool ValidateRule()
            {
                if (IsNumeric(ParamsValue))
                {
                    double num;
                    if (ParamsValue.GetType() == typeof(string))
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
        /// 
        /// </summary>
        /// <seealso cref="Rules.IValidationRule{TDbParams}" />
        private class NumericCompare : IsNummeric
        {
            internal short LimitValueShort { get; set; }
            internal int LimitValueInt { get; set; }
            internal long LimitValueLong { get; set; }
            internal double LimitValueDouble { get; set; }
            internal float LimitValueFloat { get; set; }
            internal decimal LimitValueDecimal { get; set; }
            internal ushort LimitValueUShort { get; set; }
            internal uint LimitValueUInt { get; set; }
            internal ulong LimitValueULong { get; set; }

            public NumericCompare(Expression<Func<TDbParams, string>> selector, double limit, bool isNullable = false) : base(selector, isNullable) { LimitValueDouble = limit; }
            public NumericCompare(Expression<Func<TDbParams, short>> selector, short limit) : base(selector) { LimitValueShort = limit; }
            public NumericCompare(Expression<Func<TDbParams, int>> selector, int limit) : base(selector) { LimitValueInt = limit; }
            public NumericCompare(Expression<Func<TDbParams, long>> selector, long limit) : base(selector) { LimitValueLong = limit; }
            public NumericCompare(Expression<Func<TDbParams, double>> selector, double limit) : base(selector) { LimitValueDouble = limit; }
            public NumericCompare(Expression<Func<TDbParams, float>> selector, float limit) : base(selector) { LimitValueFloat = limit;}
            public NumericCompare(Expression<Func<TDbParams, decimal>> selector, decimal limit) : base(selector) { LimitValueDecimal = limit; }
            public NumericCompare(Expression<Func<TDbParams, ushort>> selector, ushort limit) : base(selector) { LimitValueUShort = limit; }
            public NumericCompare(Expression<Func<TDbParams, uint>> selector, uint limit) : base(selector) { LimitValueUInt = limit;}
            public NumericCompare(Expression<Func<TDbParams, ulong>> selector, ulong limit) : base(selector) { LimitValueULong = limit;}

            public NumericCompare(Expression<Func<TDbParams, short?>> selector, short limit) : base(selector) { LimitValueShort = limit; }
            public NumericCompare(Expression<Func<TDbParams, int?>> selector, int limit) : base(selector) { LimitValueInt = limit; }
            public NumericCompare(Expression<Func<TDbParams, long?>> selector, long limit) : base(selector) { LimitValueLong = limit; }
            public NumericCompare(Expression<Func<TDbParams, double?>> selector, double limit) : base(selector) { LimitValueDouble = limit; }
            public NumericCompare(Expression<Func<TDbParams, float?>> selector, float limit) : base(selector) { LimitValueFloat = limit; }
            public NumericCompare(Expression<Func<TDbParams, decimal?>> selector, decimal limit) : base(selector) { LimitValueDecimal = limit; }
            public NumericCompare(Expression<Func<TDbParams, ushort?>> selector, ushort limit) : base(selector) { LimitValueUShort = limit; }
            public NumericCompare(Expression<Func<TDbParams, uint?>> selector, uint limit) : base(selector) { LimitValueUInt = limit; }
            public NumericCompare(Expression<Func<TDbParams, ulong?>> selector, ulong limit) : base(selector) { LimitValueULong = limit; }

            internal override bool ValidateNumberRule(object value)
            {
                Type numType = value.GetType();
                
                return
                    numType == typeof(short) ?
                        CompareShort((short)value, LimitValueShort) :
                    numType == typeof(int) ?
                        CompareInt((int)value, LimitValueInt) :
                    numType == typeof(long) ?
                        CompareLong((long)value, LimitValueLong) :
                    numType == typeof(double) ?
                        CompareDouble((double)value, LimitValueDouble) :
                    numType == typeof(float) ?
                        CompareFloat((float)value, LimitValueFloat) :
                    numType == typeof(decimal) ?
                        CompareDecimal((decimal)value, LimitValueDecimal) :
                    numType == typeof(ushort) ?
                        CompareUShort((ushort)value, LimitValueUShort) :
                    numType == typeof(uint) ?
                        CompareUInt((uint)value, LimitValueUInt) :
                    numType == typeof(ulong) ?
                        CompareULong((ulong)value, LimitValueULong) : false;
            }
            protected virtual bool CompareShort(short value, short compareValue) => false;
            protected virtual bool CompareInt(int value, int compareValue) => false;
            protected virtual bool CompareLong(long value, long compareValue) => false;
            protected virtual bool CompareDouble(double value, double compareValue) => false;
            protected virtual bool CompareFloat(float value, float compareValue) => false;
            protected virtual bool CompareDecimal(decimal value, decimal compareValue) => false;
            protected virtual bool CompareUShort(ushort value, ushort compareValue) => false;
            protected virtual bool CompareUInt(uint value, uint compareValue) => false;
            protected virtual bool CompareULong(ulong value, ulong compareValue) => false;

            protected virtual string GetCompareTypeLabel() => "";
            protected override string GetErrorMessageCore(string propertyName)
            {
                string compareType = GetCompareTypeLabel();
                if (string.IsNullOrEmpty(compareType))
                {
                    return $"{propertyName} is invalid ";
                }
                string value = !string.IsNullOrEmpty(LimitValueShort.ToString()) ?
                        LimitValueShort.ToString():
                    !string.IsNullOrEmpty(LimitValueInt.ToString()) ?
                        LimitValueInt.ToString() :
                    !string.IsNullOrEmpty(LimitValueLong.ToString()) ?
                        LimitValueLong.ToString() :
                    !string.IsNullOrEmpty(LimitValueDouble.ToString()) ?
                        LimitValueDouble.ToString() :
                    !string.IsNullOrEmpty(LimitValueFloat.ToString()) ?
                        LimitValueFloat.ToString() :
                    !string.IsNullOrEmpty(LimitValueDecimal.ToString()) ?
                        LimitValueDecimal.ToString() :
                    !string.IsNullOrEmpty(LimitValueUShort.ToString()) ?
                        LimitValueUShort.ToString() :
                    !string.IsNullOrEmpty(LimitValueUInt.ToString()) ?
                        LimitValueUInt.ToString() :
                        LimitValueULong.ToString();

                return propertyName + $"{propertyName} expecting value length to {compareType} {value}";
            }
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, string>> selector, double compareValue, bool isNullable = false) => new EqualToRule(selector, compareValue,isNullable);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, short>> selector, short compareValue) => new EqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, int>> selector, int compareValue) => new EqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, long>> selector, long compareValue) => new EqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, double>> selector, double compareValue) => new EqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, float>> selector, float compareValue) => new EqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, decimal>> selector, decimal compareValue) => new EqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, ushort>> selector, ushort compareValue) => new EqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, uint>> selector, uint compareValue) => new EqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) => new EqualToRule(selector, compareValue);

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, short?>> selector, short compareValue) => new EqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, int?>> selector, int compareValue) => new EqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, long?>> selector, long compareValue) => new EqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, double?>> selector, double compareValue) => new EqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, float?>> selector, float compareValue) => new EqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) => new EqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, ushort?>> selector, ushort compareValue) => new EqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, uint?>> selector, uint compareValue) => new EqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) => new EqualToRule(selector, compareValue);

        private class EqualToRule : NumericCompare
        {
            public EqualToRule(Expression<Func<TDbParams, string>> selector, double compareValue, bool isNullable = false) : base(selector, compareValue, isNullable) { }
            public EqualToRule(Expression<Func<TDbParams, short>> selector, short compareValue) : base(selector, compareValue) { }
            public EqualToRule(Expression<Func<TDbParams, int>> selector, int compareValue) : base(selector, compareValue) { }
            public EqualToRule(Expression<Func<TDbParams, long>> selector, long compareValue) : base(selector, compareValue) { }
            public EqualToRule(Expression<Func<TDbParams, double>> selector, double compareValue) : base(selector, compareValue) { }
            public EqualToRule(Expression<Func<TDbParams, float>> selector, float compareValue) : base(selector, compareValue) { }
            public EqualToRule(Expression<Func<TDbParams, decimal>> selector, decimal compareValue) : base(selector, compareValue) { }
            public EqualToRule(Expression<Func<TDbParams, ushort>> selector, ushort compareValue) : base(selector, compareValue) { }
            public EqualToRule(Expression<Func<TDbParams, uint>> selector, uint compareValue) : base(selector, compareValue) { }
            public EqualToRule(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) : base(selector, compareValue) { }

            public EqualToRule(Expression<Func<TDbParams, short?>> selector, short compareValue) : base(selector, compareValue) { }
            public EqualToRule(Expression<Func<TDbParams, int?>> selector, int compareValue) : base(selector, compareValue) { }
            public EqualToRule(Expression<Func<TDbParams, long?>> selector, long compareValue) : base(selector, compareValue) { }
            public EqualToRule(Expression<Func<TDbParams, double?>> selector, double compareValue) : base(selector, compareValue) { }
            public EqualToRule(Expression<Func<TDbParams, float?>> selector, float compareValue) : base(selector, compareValue) { }
            public EqualToRule(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) : base(selector, compareValue) { }
            public EqualToRule(Expression<Func<TDbParams, ushort?>> selector, ushort compareValue) : base(selector, compareValue) { }
            public EqualToRule(Expression<Func<TDbParams, uint?>> selector, uint compareValue) : base(selector, compareValue) { }
            public EqualToRule(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) : base(selector, compareValue) { }

            protected override bool CompareShort(short value, short compareValue)       => value == compareValue;
            protected override bool CompareInt(int value, int compareValue)             => value == compareValue;
            protected override bool CompareLong(long value, long compareValue)          => value == compareValue;
            protected override bool CompareDouble(double value, double compareValue)    => value == compareValue;
            protected override bool CompareFloat(float value, float compareValue)       => value == compareValue;
            protected override bool CompareDecimal(decimal value, decimal compareValue) => value == compareValue;
            protected override bool CompareUShort(ushort value, ushort compareValue)    => value == compareValue;
            protected override bool CompareUInt(uint value, uint compareValue)          => value == compareValue;
            protected override bool CompareULong(ulong value, ulong compareValue)       => value == compareValue;

            protected override string GetCompareTypeLabel() => "to be equal to";
        }

        public static IValidationRule<TDbParams>  NotEqualTo(Expression<Func<TDbParams, string>> selector, double compareValue, bool isNullable = false) => new NotEqualToRule(selector, compareValue, isNullable);
        public static IValidationRule<TDbParams>  NotEqualTo(Expression<Func<TDbParams, short>> selector, short compareValue) => new NotEqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams>  NotEqualTo(Expression<Func<TDbParams, int>> selector, int compareValue) => new NotEqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams>  NotEqualTo(Expression<Func<TDbParams, long>> selector, long compareValue) => new NotEqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams>  NotEqualTo(Expression<Func<TDbParams, double>> selector, double compareValue) => new NotEqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams>  NotEqualTo(Expression<Func<TDbParams, float>> selector, float compareValue) => new NotEqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams>  NotEqualTo(Expression<Func<TDbParams, decimal>> selector, decimal compareValue) => new NotEqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams>  NotEqualTo(Expression<Func<TDbParams, ushort>> selector, ushort compareValue) => new NotEqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams>  NotEqualTo(Expression<Func<TDbParams, uint>> selector, uint compareValue) => new NotEqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams>  NotEqualTo(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) => new NotEqualToRule(selector, compareValue);

        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, short?>> selector, short compareValue) => new NotEqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, int?>> selector, int compareValue) => new NotEqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, long?>> selector, long compareValue) => new NotEqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, double?>> selector, double compareValue) => new NotEqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, float?>> selector, float compareValue) => new NotEqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) => new NotEqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, ushort?>> selector, ushort compareValue) => new NotEqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, uint?>> selector, uint compareValue) => new NotEqualToRule(selector, compareValue);
        public static IValidationRule<TDbParams> NotEqualTo(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) => new NotEqualToRule(selector, compareValue);

        private class NotEqualToRule : NumericCompare
        {
            public NotEqualToRule(Expression<Func<TDbParams, string>> selector  , double compareValue, bool isNullable = false) : base(selector, compareValue, isNullable) { }
            public NotEqualToRule(Expression<Func<TDbParams, short>> selector   , short compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Expression<Func<TDbParams, int>> selector     , int compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Expression<Func<TDbParams, long>> selector    , long compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Expression<Func<TDbParams, double>> selector  , double compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Expression<Func<TDbParams, float>> selector   , float compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Expression<Func<TDbParams, decimal>> selector , decimal compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Expression<Func<TDbParams, ushort>> selector  , ushort compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Expression<Func<TDbParams, uint>> selector    , uint compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Expression<Func<TDbParams, ulong>> selector   , ulong compareValue) : base(selector, compareValue) { }

            public NotEqualToRule(Expression<Func<TDbParams, short?>> selector  , short compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Expression<Func<TDbParams, int?>> selector    , int compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Expression<Func<TDbParams, long?>> selector   , long compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Expression<Func<TDbParams, double?>> selector , double compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Expression<Func<TDbParams, float?>> selector  , float compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Expression<Func<TDbParams, ushort?>> selector , ushort compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Expression<Func<TDbParams, uint?>> selector   , uint compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Expression<Func<TDbParams, ulong?>> selector  , ulong compareValue) : base(selector, compareValue) { }

            protected override bool CompareShort(short value, short compareValue)       => value != compareValue;
            protected override bool CompareInt(int value, int compareValue)             => value != compareValue;
            protected override bool CompareLong(long value, long compareValue)          => value != compareValue;
            protected override bool CompareDouble(double value, double compareValue)    => value != compareValue;
            protected override bool CompareFloat(float value, float compareValue)       => value != compareValue;
            protected override bool CompareDecimal(decimal value, decimal compareValue) => value != compareValue;
            protected override bool CompareUShort(ushort value, ushort compareValue)    => value != compareValue;
            protected override bool CompareUInt(uint value, uint compareValue)          => value != compareValue;
            protected override bool CompareULong(ulong value, ulong compareValue)       => value != compareValue;
            protected override string GetCompareTypeLabel() => "to not be equal to";
        }

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, string>> selector   , double compareValue, bool isNullable = false) => new GreaterThanRule(selector, compareValue, isNullable);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, short>> selector    , short compareValue) => new GreaterThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, int>> selector      , int compareValue) => new GreaterThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, long>> selector     , long compareValue) => new GreaterThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, double>> selector   , double compareValue) => new GreaterThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, float>> selector    , float compareValue) => new GreaterThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, decimal>> selector  , decimal compareValue) => new GreaterThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, ushort>> selector   , ushort compareValue) => new GreaterThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, uint>> selector     , uint compareValue) => new GreaterThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) => new GreaterThanRule(selector, compareValue);

        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, short?>> selector  , short compareValue) => new GreaterThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, int?>> selector    , int compareValue) => new GreaterThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, long?>> selector   , long compareValue) => new GreaterThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, double?>> selector , double compareValue) => new GreaterThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, float?>> selector  , float compareValue) => new GreaterThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) => new GreaterThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, ushort?>> selector , ushort compareValue) => new GreaterThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, uint?>> selector   , uint compareValue) => new GreaterThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThan(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) => new GreaterThanRule(selector, compareValue);

        private class GreaterThanRule : NumericCompare
        {
            public GreaterThanRule(Expression<Func<TDbParams, string>> selector , double compareValue, bool isNullable = false) : base(selector, compareValue,isNullable) { }
            public GreaterThanRule(Expression<Func<TDbParams, short>> selector  , short compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Expression<Func<TDbParams, int>> selector    , int compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Expression<Func<TDbParams, long>> selector   , long compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Expression<Func<TDbParams, double>> selector , double compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Expression<Func<TDbParams, float>> selector  , float compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Expression<Func<TDbParams, decimal>> selector, decimal compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Expression<Func<TDbParams, ushort>> selector , ushort compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Expression<Func<TDbParams, uint>> selector   , uint compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) : base(selector, compareValue) { }

            public GreaterThanRule(Expression<Func<TDbParams, short?>> selector  , short compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Expression<Func<TDbParams, int?>> selector    , int compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Expression<Func<TDbParams, long?>> selector   , long compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Expression<Func<TDbParams, double?>> selector , double compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Expression<Func<TDbParams, float?>> selector  , float compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Expression<Func<TDbParams, ushort?>> selector , ushort compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Expression<Func<TDbParams, uint?>> selector   , uint compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) : base(selector, compareValue) { }

            protected override bool CompareShort(short value, short compareValue)       => value > compareValue;
            protected override bool CompareInt(int value, int compareValue)             => value > compareValue;
            protected override bool CompareLong(long value, long compareValue)          => value > compareValue;
            protected override bool CompareDouble(double value, double compareValue)    => value > compareValue;
            protected override bool CompareFloat(float value, float compareValue)       => value > compareValue;
            protected override bool CompareDecimal(decimal value, decimal compareValue) => value > compareValue;
            protected override bool CompareUShort(ushort value, ushort compareValue)    => value > compareValue;
            protected override bool CompareUInt(uint value, uint compareValue)          => value > compareValue;
            protected override bool CompareULong(ulong value, ulong compareValue)       => value > compareValue;
            protected override string GetCompareTypeLabel() => "to be greater than";
            
        }

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, string>> selector  , double compareValue, bool isNullable = false) => new GreaterThanOrEqualRule(selector, compareValue,isNullable);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, short>> selector   , short compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, int>> selector     , int compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, long>> selector    , long compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, double>> selector  , double compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, float>> selector   , float compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, decimal>> selector , decimal compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, ushort>> selector  , ushort compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, uint>> selector    , uint compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) => new GreaterThanOrEqualRule(selector, compareValue);

        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, short?>> selector  , short compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, int?>> selector    , int compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, long?>> selector   , long compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, double?>> selector , double compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, float?>> selector  , float compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, ushort?>> selector , ushort compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, uint?>> selector   , uint compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> GreaterThanOrEqual(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) => new GreaterThanOrEqualRule(selector, compareValue);

        private class GreaterThanOrEqualRule : NumericCompare
        {
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, string>> selector , double compareValue, bool isNullable = false) : base(selector, compareValue, isNullable) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, short>> selector  , short compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, int>> selector    , int compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, long>> selector   , long compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, double>> selector , double compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, float>> selector  , float compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, decimal>> selector, decimal compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, ushort>> selector , ushort compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, uint>> selector   , uint compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) : base(selector, compareValue) { }

            public GreaterThanOrEqualRule(Expression<Func<TDbParams, short?>> selector  , short compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, int?>> selector    , int compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, long?>> selector   , long compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, double?>> selector , double compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, float?>> selector  , float compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, ushort?>> selector , ushort compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, uint?>> selector   , uint compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) : base(selector, compareValue) { }

            protected override bool CompareShort(short value, short compareValue)       => value >= compareValue;
            protected override bool CompareInt(int value, int compareValue)             => value >= compareValue;
            protected override bool CompareLong(long value, long compareValue)          => value >= compareValue;
            protected override bool CompareDouble(double value, double compareValue)    => value >= compareValue;
            protected override bool CompareFloat(float value, float compareValue)       => value >= compareValue;
            protected override bool CompareDecimal(decimal value, decimal compareValue) => value >= compareValue;
            protected override bool CompareUShort(ushort value, ushort compareValue)    => value >= compareValue;
            protected override bool CompareUInt(uint value, uint compareValue)          => value >= compareValue;
            protected override bool CompareULong(ulong value, ulong compareValue)       => value >= compareValue;
            protected override string GetCompareTypeLabel() => "to be greater than or equal to";
        }

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, string>> selector , double compareValue, bool isNullable = false) => new LessThanRule(selector, compareValue, isNullable);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, short>> selector  , short compareValue) => new LessThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, int>> selector    , int compareValue) => new LessThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, long>> selector   , long compareValue) => new LessThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, double>> selector , double compareValue) => new LessThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, float>> selector  , float compareValue) => new LessThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, decimal>> selector, decimal compareValue) => new LessThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, ushort>> selector , ushort compareValue) => new LessThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, uint>> selector   , uint compareValue) => new LessThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) => new LessThanRule(selector, compareValue);

        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, short?>> selector  , short compareValue) => new LessThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, int?>> selector    , int compareValue) => new LessThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, long?>> selector   , long compareValue) => new LessThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, double?>> selector , double compareValue) => new LessThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, float?>> selector  , float compareValue) => new LessThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) => new LessThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, ushort?>> selector , ushort compareValue) => new LessThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, uint?>> selector   , uint compareValue) => new LessThanRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThan(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) => new LessThanRule(selector, compareValue);

        private class LessThanRule : NumericCompare
        {
            public LessThanRule(Expression<Func<TDbParams, string>> selector , double compareValue, bool isNullable = false) : base(selector, compareValue, isNullable) { }
            public LessThanRule(Expression<Func<TDbParams, short>> selector  , short compareValue) : base(selector, compareValue) { }
            public LessThanRule(Expression<Func<TDbParams, int>> selector    , int compareValue) : base(selector, compareValue) { }
            public LessThanRule(Expression<Func<TDbParams, long>> selector   , long compareValue) : base(selector, compareValue) { }
            public LessThanRule(Expression<Func<TDbParams, double>> selector , double compareValue) : base(selector, compareValue) { }
            public LessThanRule(Expression<Func<TDbParams, float>> selector  , float compareValue) : base(selector, compareValue) { }
            public LessThanRule(Expression<Func<TDbParams, decimal>> selector, decimal compareValue) : base(selector, compareValue) { }
            public LessThanRule(Expression<Func<TDbParams, ushort>> selector , ushort compareValue) : base(selector, compareValue) { }
            public LessThanRule(Expression<Func<TDbParams, uint>> selector   , uint compareValue) : base(selector, compareValue) { }
            public LessThanRule(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) : base(selector, compareValue) { }

            public LessThanRule(Expression<Func<TDbParams, short?>> selector  , short compareValue) : base(selector, compareValue) { }
            public LessThanRule(Expression<Func<TDbParams, int?>> selector    , int compareValue) : base(selector, compareValue) { }
            public LessThanRule(Expression<Func<TDbParams, long?>> selector   , long compareValue) : base(selector, compareValue) { }
            public LessThanRule(Expression<Func<TDbParams, double?>> selector , double compareValue) : base(selector, compareValue) { }
            public LessThanRule(Expression<Func<TDbParams, float?>> selector  , float compareValue) : base(selector, compareValue) { }
            public LessThanRule(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) : base(selector, compareValue) { }
            public LessThanRule(Expression<Func<TDbParams, ushort?>> selector , ushort compareValue) : base(selector, compareValue) { }
            public LessThanRule(Expression<Func<TDbParams, uint?>> selector   , uint compareValue) : base(selector, compareValue) { }
            public LessThanRule(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) : base(selector, compareValue) { }

            protected override bool CompareShort(short value, short compareValue)           => value < compareValue;
            protected override bool CompareInt(int value, int compareValue)                 => value < compareValue;
            protected override bool CompareLong(long value, long compareValue)              => value < compareValue;
            protected override bool CompareDouble(double value, double compareValue)        => value < compareValue;
            protected override bool CompareFloat(float value, float compareValue)           => value < compareValue;
            protected override bool CompareDecimal(decimal value, decimal compareValue)     => value < compareValue;
            protected override bool CompareUShort(ushort value, ushort compareValue)        => value < compareValue;
            protected override bool CompareUInt(uint value, uint compareValue)              => value < compareValue;
            protected override bool CompareULong(ulong value, ulong compareValue)           => value < compareValue;
            protected override string GetCompareTypeLabel() => "to be less than";
        }

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, string>> selector , double compareValue, bool isNullable = false) => new LessThanOrEqualRule(selector, compareValue, isNullable);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, short>> selector  , short compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, int>> selector    , int compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, long>> selector   , long compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, double>> selector , double compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, float>> selector  , float compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, decimal>> selector, decimal compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, ushort>> selector , ushort compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, uint>> selector   , uint compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) => new LessThanOrEqualRule(selector, compareValue);

        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, short?>> selector  , short compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, int?>> selector    , int compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, long?>> selector   , long compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, double?>> selector , double compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, float?>> selector  , float compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, ushort?>> selector , ushort compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, uint?>> selector   , uint compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public static IValidationRule<TDbParams> LessThanOrEqual(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) => new LessThanOrEqualRule(selector, compareValue);

        private class LessThanOrEqualRule : NumericCompare
        {
            public LessThanOrEqualRule(Expression<Func<TDbParams, string>> selector , double compareValue, bool isNullable = false) : base(selector, compareValue, isNullable) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, short>> selector  , short compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, int>> selector    , int compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, long>> selector   , long compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, double>> selector , double compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, float>> selector  , float compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, decimal>> selector, decimal compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, ushort>> selector , ushort compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, uint>> selector   , uint compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, ulong>> selector, ulong compareValue) : base(selector, compareValue) { }

            public LessThanOrEqualRule(Expression<Func<TDbParams, short?>> selector  , short compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, int?>> selector    , int compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, long?>> selector   , long compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, double?>> selector , double compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, float?>> selector  , float compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, decimal?>> selector, decimal compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, ushort?>> selector , ushort compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, uint?>> selector   , uint compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Expression<Func<TDbParams, ulong?>> selector, ulong compareValue) : base(selector, compareValue) { }

            protected override bool CompareShort(short value, short compareValue)           => value <= compareValue;
            protected override bool CompareInt(int value, int compareValue)                 => value <= compareValue;
            protected override bool CompareLong(long value, long compareValue)              => value <= compareValue;
            protected override bool CompareDouble(double value, double compareValue)        => value <= compareValue;
            protected override bool CompareFloat(float value, float compareValue)           => value <= compareValue;
            protected override bool CompareDecimal(decimal value, decimal compareValue)     => value <= compareValue;
            protected override bool CompareUShort(ushort value, ushort compareValue)        => value <= compareValue;
            protected override bool CompareUInt(uint value, uint compareValue)              => value <= compareValue;
            protected override bool CompareULong(ulong value, ulong compareValue)           => value <= compareValue;
            protected override string GetCompareTypeLabel() => "to be less than or equal to";
        }
    }
}