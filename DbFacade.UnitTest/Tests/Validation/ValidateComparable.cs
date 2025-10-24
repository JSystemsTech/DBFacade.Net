namespace DbFacade.UnitTest.Tests.Validation
{
    public class ValidateComparableShort : ValidateIComparable<short>
    {
        protected override short Value => Short10;
        protected override short CompareToLess => Short9;
        protected override short CompareToGreater => Short11;
        public ValidateComparableShort(ServiceFixture services) : base(services) { }
    }
    
    public class ValidateComparableUShort : ValidateIComparable<ushort>
    {
        protected override ushort Value => UShort10;
        protected override ushort CompareToLess => UShort9;
        protected override ushort CompareToGreater => UShort11;
        public ValidateComparableUShort(ServiceFixture services) : base(services) { }
    }
    
    public class ValidateComparableInt : ValidateIComparable<int>
    {
        protected override int Value => Int10;
        protected override int CompareToLess => Int9;
        protected override int CompareToGreater => Int11;
        public ValidateComparableInt(ServiceFixture services) : base(services) { }
    }
    
    public class ValidateComparableUInt : ValidateIComparable<uint>
    {
        protected override uint Value => UInt10;
        protected override uint CompareToLess => UInt9;
        protected override uint CompareToGreater => UInt11;
        public ValidateComparableUInt(ServiceFixture services) : base(services) { }
    }
    
    public class ValidateComparableLong : ValidateIComparable<long>
    {
        protected override long Value => Long10;
        protected override long CompareToLess => Long9;
        protected override long CompareToGreater => Long11;
        public ValidateComparableLong(ServiceFixture services) : base(services) { }
    }
    
    public class ValidateComparableULong : ValidateIComparable<ulong>
    {
        protected override ulong Value => ULong10;
        protected override ulong CompareToLess => ULong9;
        protected override ulong CompareToGreater => ULong11;
        public ValidateComparableULong(ServiceFixture services) : base(services) { }
    }
    
    public class ValidateComparableDouble : ValidateIComparable<double>
    {
        protected override double Value => Double10;
        protected override double CompareToLess => Double9;
        protected override double CompareToGreater => Double11;
        public ValidateComparableDouble(ServiceFixture services) : base(services) { }
    }
    
    public class ValidateComparableFloat : ValidateIComparable<float>
    {
        protected override float Value => Float10;
        protected override float CompareToLess => Float9;
        protected override float CompareToGreater => Float11;
        public ValidateComparableFloat(ServiceFixture services) : base(services) { }
    }
    
    public class ValidateComparableDecimal : ValidateIComparable<decimal>
    {
        protected override decimal Value => Decimal10;
        protected override decimal CompareToLess => Decimal9;
        protected override decimal CompareToGreater => Decimal11;
        public ValidateComparableDecimal(ServiceFixture services) : base(services) { }
    }

    
    public class ValidateComparableDateTime : ValidateIComparable<DateTime>
    {
        protected override DateTime Value => Today;
        protected override DateTime CompareToLess => Yesterday;
        protected override DateTime CompareToGreater => Tomorrow;
        public ValidateComparableDateTime(ServiceFixture services) : base(services) { }
    }
    
    public class ValidateComparableTimeSpan : ValidateIComparable<TimeSpan>
    {
        protected override TimeSpan Value => Noon;
        protected override TimeSpan CompareToLess => Morning;
        protected override TimeSpan CompareToGreater => Afternoon;
        public ValidateComparableTimeSpan(ServiceFixture services) : base(services) { }
    }

    public enum ComparableEnum
    {
        Less,
        Equal,
        Greater
    }
    
    public class ValidateComparableEnum : ValidateIComparable<ComparableEnum>
    {
        protected override ComparableEnum Value => ComparableEnum.Equal;
        protected override ComparableEnum CompareToLess => ComparableEnum.Less;
        protected override ComparableEnum CompareToGreater => ComparableEnum.Greater;
        public ValidateComparableEnum(ServiceFixture services) : base(services) { }
    }

    public sealed class MyCustomComparable : IComparable<MyCustomComparable>, IComparable
    {
        public int PrimaryValue { get; private set; }
        public short SecondaryValue { get; private set; }
        public MyCustomComparable(int primary, short secondary)
        {
            PrimaryValue = primary;
            SecondaryValue = secondary;
        }

        public int CompareTo(MyCustomComparable other)
        {
            int primaryCompare = PrimaryValue.CompareTo(other.PrimaryValue);
            return primaryCompare == 0 ? SecondaryValue.CompareTo(other.SecondaryValue) : primaryCompare;
        }

        public int CompareTo(object obj)
        => obj is MyCustomComparable other ? CompareTo(other) : -1;
    }
    
    public class ValidateComparableMyCustomComparable : ValidateIComparable<MyCustomComparable>
    {
        protected override MyCustomComparable Value => new MyCustomComparable(Int10, Short10);
        protected override MyCustomComparable CompareToLess => new MyCustomComparable(Int10, Short9);
        protected override MyCustomComparable CompareToGreater => new MyCustomComparable(Int10, Short11);
        public ValidateComparableMyCustomComparable(ServiceFixture services) : base(services) { }
    }
}
