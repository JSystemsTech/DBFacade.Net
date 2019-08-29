using DBFacade.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

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
            if (value.GetType() == typeof(string))
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
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Rules.IValidationRule{TDbParams}" />
        public class IsNummeric : ValidationRule<TDbParams>
        {
            
            public IsNummeric(Func<TDbParams, string> selector) : base(selector) { }
            public IsNummeric(Func<TDbParams, short> selector) : base(selector) { }
            public IsNummeric(Func<TDbParams, int> selector) : base(selector) { }
            public IsNummeric(Func<TDbParams, long> selector) : base(selector) { }
            public IsNummeric(Func<TDbParams, double> selector) : base(selector) { }
            public IsNummeric(Func<TDbParams, float> selector) : base(selector) { }
            public IsNummeric(Func<TDbParams, decimal> selector) : base(selector) { }
            public IsNummeric(Func<TDbParams, ushort> selector) : base(selector) { }
            public IsNummeric(Func<TDbParams, uint> selector) : base(selector) { }
            public IsNummeric(Func<TDbParams, ulong> selector) : base(selector) { }

            protected override bool ValidateRule()
            {
                if (IsNumeric(ParamsValue))
                {
                    double num;
                    if (ParamsValue.GetType() == typeof(string))
                    {
                        double.TryParse(ParamsValue.ToString(), out num);
                    }
                    else
                    {
                        num = Convert.ToDouble(ParamsValue);
                    }
                    return ValidateNumberRule(num);
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
        public class NumericCompare : IsNummeric
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

            public NumericCompare(Func<TDbParams, string> selector, double limit) : base(selector) { LimitValueDouble = limit; }
            public NumericCompare(Func<TDbParams, short> selector, short limit) : base(selector) { LimitValueShort = limit; }
            public NumericCompare(Func<TDbParams, int> selector, int limit) : base(selector) { LimitValueInt = limit; }
            public NumericCompare(Func<TDbParams, long> selector, long limit) : base(selector) { LimitValueLong = limit; }
            public NumericCompare(Func<TDbParams, double> selector, double limit) : base(selector) { LimitValueDouble = limit; }
            public NumericCompare(Func<TDbParams, float> selector, float limit) : base(selector) { LimitValueFloat = limit;}
            public NumericCompare(Func<TDbParams, decimal> selector, decimal limit) : base(selector) { LimitValueDecimal = limit; }
            public NumericCompare(Func<TDbParams, ushort> selector, ushort limit) : base(selector) { LimitValueUShort = limit; }
            public NumericCompare(Func<TDbParams, uint> selector, uint limit) : base(selector) { LimitValueUInt = limit;}
            public NumericCompare(Func<TDbParams, ulong> selector, ulong limit) : base(selector) { LimitValueULong = limit;}
            
            
            internal override bool ValidateNumberRule(object value)
            {
                Type numType = value.GetType();
                return
                    numType == typeof(short) ?
                        CompareShort((short)ParamsValue, LimitValueShort) :
                    numType == typeof(int) ?
                        CompareInt((int)ParamsValue, LimitValueInt) :
                    numType == typeof(long) ?
                        CompareLong((long)ParamsValue, LimitValueLong) :
                    numType == typeof(double) ?
                        CompareDouble((double)ParamsValue, LimitValueDouble) :
                    numType == typeof(float) ?
                        CompareFloat((float)ParamsValue, LimitValueFloat) :
                    numType == typeof(decimal) ?
                        CompareDecimal((decimal)ParamsValue, LimitValueDecimal) :
                    numType == typeof(ushort) ?
                        CompareUShort((ushort)ParamsValue, LimitValueUShort) :
                    numType == typeof(uint) ?
                        CompareUInt((uint)ParamsValue,LimitValueUInt) :
                    numType == typeof(ulong) ?
                        CompareULong((ulong)ParamsValue,LimitValueULong) : false;
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

        public EqualToRule EqualTo(Func<TDbParams, string> selector, double compareValue) => new EqualToRule(selector, compareValue);
        public EqualToRule EqualTo(Func<TDbParams, short> selector, short compareValue) => new EqualToRule(selector, compareValue);
        public EqualToRule EqualTo(Func<TDbParams, int> selector, int compareValue) => new EqualToRule(selector, compareValue);
        public EqualToRule EqualTo(Func<TDbParams, long> selector, long compareValue) => new EqualToRule(selector, compareValue);
        public EqualToRule EqualTo(Func<TDbParams, double> selector, double compareValue) => new EqualToRule(selector, compareValue);
        public EqualToRule EqualTo(Func<TDbParams, float> selector, float compareValue) => new EqualToRule(selector, compareValue);
        public EqualToRule EqualTo(Func<TDbParams, decimal> selector, decimal compareValue) => new EqualToRule(selector, compareValue);
        public EqualToRule EqualTo(Func<TDbParams, ushort> selector, ushort compareValue) => new EqualToRule(selector, compareValue);
        public EqualToRule EqualTo(Func<TDbParams, uint> selector, uint compareValue) => new EqualToRule(selector, compareValue);
        public EqualToRule EqualTo(Func<TDbParams, ulong> selector, ulong compareValue) => new EqualToRule(selector, compareValue);
        public class EqualToRule : NumericCompare
        {
            public EqualToRule(Func<TDbParams, string> selector, double compareValue) : base(selector, compareValue) { }
            public EqualToRule(Func<TDbParams, short> selector, short compareValue) : base(selector, compareValue) { }
            public EqualToRule(Func<TDbParams, int> selector, int compareValue) : base(selector, compareValue) { }
            public EqualToRule(Func<TDbParams, long> selector, long compareValue) : base(selector, compareValue) { }
            public EqualToRule(Func<TDbParams, double> selector, double compareValue) : base(selector, compareValue) { }
            public EqualToRule(Func<TDbParams, float> selector, float compareValue) : base(selector, compareValue) { }
            public EqualToRule(Func<TDbParams, decimal> selector, decimal compareValue) : base(selector, compareValue) { }
            public EqualToRule(Func<TDbParams, ushort> selector, ushort compareValue) : base(selector, compareValue) { }
            public EqualToRule(Func<TDbParams, uint> selector, uint compareValue) : base(selector, compareValue) { }
            public EqualToRule(Func<TDbParams, ulong> selector, ulong compareValue) : base(selector, compareValue) { }

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

        public NotEqualToRule NotEqualTo(Func<TDbParams, string> selector, double compareValue) => new NotEqualToRule(selector, compareValue);
        public NotEqualToRule NotEqualTo(Func<TDbParams, short> selector, short compareValue) => new NotEqualToRule(selector, compareValue);
        public NotEqualToRule NotEqualTo(Func<TDbParams, int> selector, int compareValue) => new NotEqualToRule(selector, compareValue);
        public NotEqualToRule NotEqualTo(Func<TDbParams, long> selector, long compareValue) => new NotEqualToRule(selector, compareValue);
        public NotEqualToRule NotEqualTo(Func<TDbParams, double> selector, double compareValue) => new NotEqualToRule(selector, compareValue);
        public NotEqualToRule NotEqualTo(Func<TDbParams, float> selector, float compareValue) => new NotEqualToRule(selector, compareValue);
        public NotEqualToRule NotEqualTo(Func<TDbParams, decimal> selector, decimal compareValue) => new NotEqualToRule(selector, compareValue);
        public NotEqualToRule NotEqualTo(Func<TDbParams, ushort> selector, ushort compareValue) => new NotEqualToRule(selector, compareValue);
        public NotEqualToRule NotEqualTo(Func<TDbParams, uint> selector, uint compareValue) => new NotEqualToRule(selector, compareValue);
        public NotEqualToRule NotEqualTo(Func<TDbParams, ulong> selector, ulong compareValue) => new NotEqualToRule(selector, compareValue);
        public class NotEqualToRule : NumericCompare
        {
            public NotEqualToRule(Func<TDbParams, string> selector, double compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Func<TDbParams, short> selector, short compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Func<TDbParams, int> selector, int compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Func<TDbParams, long> selector, long compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Func<TDbParams, double> selector, double compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Func<TDbParams, float> selector, float compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Func<TDbParams, decimal> selector, decimal compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Func<TDbParams, ushort> selector, ushort compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Func<TDbParams, uint> selector, uint compareValue) : base(selector, compareValue) { }
            public NotEqualToRule(Func<TDbParams, ulong> selector, ulong compareValue) : base(selector, compareValue) { }

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

        public GreaterThanRule GreaterThan(Func<TDbParams, string> selector, double compareValue) => new GreaterThanRule(selector, compareValue);
        public GreaterThanRule GreaterThan(Func<TDbParams, short> selector, short compareValue) => new GreaterThanRule(selector, compareValue);
        public GreaterThanRule GreaterThan(Func<TDbParams, int> selector, int compareValue) => new GreaterThanRule(selector, compareValue);
        public GreaterThanRule GreaterThan(Func<TDbParams, long> selector, long compareValue) => new GreaterThanRule(selector, compareValue);
        public GreaterThanRule GreaterThan(Func<TDbParams, double> selector, double compareValue) => new GreaterThanRule(selector, compareValue);
        public GreaterThanRule GreaterThan(Func<TDbParams, float> selector, float compareValue) => new GreaterThanRule(selector, compareValue);
        public GreaterThanRule GreaterThan(Func<TDbParams, decimal> selector, decimal compareValue) => new GreaterThanRule(selector, compareValue);
        public GreaterThanRule GreaterThan(Func<TDbParams, ushort> selector, ushort compareValue) => new GreaterThanRule(selector, compareValue);
        public GreaterThanRule GreaterThan(Func<TDbParams, uint> selector, uint compareValue) => new GreaterThanRule(selector, compareValue);
        public GreaterThanRule GreaterThan(Func<TDbParams, ulong> selector, ulong compareValue) => new GreaterThanRule(selector, compareValue);

        public class GreaterThanRule : NumericCompare
        {
            public GreaterThanRule(Func<TDbParams, string> selector, double compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Func<TDbParams, short> selector, short compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Func<TDbParams, int> selector, int compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Func<TDbParams, long> selector, long compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Func<TDbParams, double> selector, double compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Func<TDbParams, float> selector, float compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Func<TDbParams, decimal> selector, decimal compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Func<TDbParams, ushort> selector, ushort compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Func<TDbParams, uint> selector, uint compareValue) : base(selector, compareValue) { }
            public GreaterThanRule(Func<TDbParams, ulong> selector, ulong compareValue) : base(selector, compareValue) { }

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

        public GreaterThanOrEqualRule GreaterThanOrEqual(Func<TDbParams, string> selector, double compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public GreaterThanOrEqualRule GreaterThanOrEqual(Func<TDbParams, short> selector, short compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public GreaterThanOrEqualRule GreaterThanOrEqual(Func<TDbParams, int> selector, int compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public GreaterThanOrEqualRule GreaterThanOrEqual(Func<TDbParams, long> selector, long compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public GreaterThanOrEqualRule GreaterThanOrEqual(Func<TDbParams, double> selector, double compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public GreaterThanOrEqualRule GreaterThanOrEqual(Func<TDbParams, float> selector, float compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public GreaterThanOrEqualRule GreaterThanOrEqual(Func<TDbParams, decimal> selector, decimal compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public GreaterThanOrEqualRule GreaterThanOrEqual(Func<TDbParams, ushort> selector, ushort compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public GreaterThanOrEqualRule GreaterThanOrEqual(Func<TDbParams, uint> selector, uint compareValue) => new GreaterThanOrEqualRule(selector, compareValue);
        public GreaterThanOrEqualRule GreaterThanOrEqual(Func<TDbParams, ulong> selector, ulong compareValue) => new GreaterThanOrEqualRule(selector, compareValue);

        public class GreaterThanOrEqualRule : NumericCompare
        {
            public GreaterThanOrEqualRule(Func<TDbParams, string> selector, double compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Func<TDbParams, short> selector, short compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Func<TDbParams, int> selector, int compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Func<TDbParams, long> selector, long compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Func<TDbParams, double> selector, double compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Func<TDbParams, float> selector, float compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Func<TDbParams, decimal> selector, decimal compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Func<TDbParams, ushort> selector, ushort compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Func<TDbParams, uint> selector, uint compareValue) : base(selector, compareValue) { }
            public GreaterThanOrEqualRule(Func<TDbParams, ulong> selector, ulong compareValue) : base(selector, compareValue) { }

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

        public LessThanRule LessThan(Func<TDbParams, string> selector, double compareValue) => new LessThanRule(selector, compareValue);
        public LessThanRule LessThan(Func<TDbParams, short> selector, short compareValue) => new LessThanRule(selector, compareValue);
        public LessThanRule LessThan(Func<TDbParams, int> selector, int compareValue) => new LessThanRule(selector, compareValue);
        public LessThanRule LessThan(Func<TDbParams, long> selector, long compareValue) => new LessThanRule(selector, compareValue);
        public LessThanRule LessThan(Func<TDbParams, double> selector, double compareValue) => new LessThanRule(selector, compareValue);
        public LessThanRule LessThan(Func<TDbParams, float> selector, float compareValue) => new LessThanRule(selector, compareValue);
        public LessThanRule LessThan(Func<TDbParams, decimal> selector, decimal compareValue) => new LessThanRule(selector, compareValue);
        public LessThanRule LessThan(Func<TDbParams, ushort> selector, ushort compareValue) => new LessThanRule(selector, compareValue);
        public LessThanRule LessThan(Func<TDbParams, uint> selector, uint compareValue) => new LessThanRule(selector, compareValue);
        public LessThanRule LessThan(Func<TDbParams, ulong> selector, ulong compareValue) => new LessThanRule(selector, compareValue);

        public class LessThanRule : NumericCompare
        {
            public LessThanRule(Func<TDbParams, string> selector, double compareValue) : base(selector, compareValue) { }
            public LessThanRule(Func<TDbParams, short> selector, short compareValue) : base(selector, compareValue) { }
            public LessThanRule(Func<TDbParams, int> selector, int compareValue) : base(selector, compareValue) { }
            public LessThanRule(Func<TDbParams, long> selector, long compareValue) : base(selector, compareValue) { }
            public LessThanRule(Func<TDbParams, double> selector, double compareValue) : base(selector, compareValue) { }
            public LessThanRule(Func<TDbParams, float> selector, float compareValue) : base(selector, compareValue) { }
            public LessThanRule(Func<TDbParams, decimal> selector, decimal compareValue) : base(selector, compareValue) { }
            public LessThanRule(Func<TDbParams, ushort> selector, ushort compareValue) : base(selector, compareValue) { }
            public LessThanRule(Func<TDbParams, uint> selector, uint compareValue) : base(selector, compareValue) { }
            public LessThanRule(Func<TDbParams, ulong> selector, ulong compareValue) : base(selector, compareValue) { }

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

        public LessThanOrEqualRule LessThanOrEqual(Func<TDbParams, string> selector, double compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public LessThanOrEqualRule LessThanOrEqual(Func<TDbParams, short> selector, short compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public LessThanOrEqualRule LessThanOrEqual(Func<TDbParams, int> selector, int compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public LessThanOrEqualRule LessThanOrEqual(Func<TDbParams, long> selector, long compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public LessThanOrEqualRule LessThanOrEqual(Func<TDbParams, double> selector, double compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public LessThanOrEqualRule LessThanOrEqual(Func<TDbParams, float> selector, float compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public LessThanOrEqualRule LessThanOrEqual(Func<TDbParams, decimal> selector, decimal compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public LessThanOrEqualRule LessThanOrEqual(Func<TDbParams, ushort> selector, ushort compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public LessThanOrEqualRule LessThanOrEqual(Func<TDbParams, uint> selector, uint compareValue) => new LessThanOrEqualRule(selector, compareValue);
        public LessThanOrEqualRule LessThanOrEqual(Func<TDbParams, ulong> selector, ulong compareValue) => new LessThanOrEqualRule(selector, compareValue);

        public class LessThanOrEqualRule : NumericCompare
        {
            public LessThanOrEqualRule(Func<TDbParams, string> selector, double compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Func<TDbParams, short> selector, short compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Func<TDbParams, int> selector, int compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Func<TDbParams, long> selector, long compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Func<TDbParams, double> selector, double compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Func<TDbParams, float> selector, float compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Func<TDbParams, decimal> selector, decimal compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Func<TDbParams, ushort> selector, ushort compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Func<TDbParams, uint> selector, uint compareValue) : base(selector, compareValue) { }
            public LessThanOrEqualRule(Func<TDbParams, ulong> selector, ulong compareValue) : base(selector, compareValue) { }

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