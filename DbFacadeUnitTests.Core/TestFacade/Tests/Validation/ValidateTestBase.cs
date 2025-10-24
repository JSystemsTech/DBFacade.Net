using DbFacade.DataLayer.Models;
using DbFacade.Extensions;
using DbFacadeUnitTests.Models;
using DbFacadeUnitTests.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DbFacadeUnitTests.Core.TestFacade.Tests.Validation
{

    public abstract class ValidateIComparable<T> : UnitTestBase
        where T : IComparable
    {
        protected abstract T Value { get; }
        protected abstract T CompareToLess { get; }
        protected abstract T CompareToGreater { get; }

        [TestMethod]
        public void IsEqualTo()
        {
            Assert.IsTrue(Value.IsEqualTo(Value));
            Assert.IsFalse(CompareToLess.IsEqualTo(Value));
            Assert.IsFalse(CompareToGreater.IsEqualTo(Value));
        }
        [TestMethod]
        public void IsNotEqual()
        {
            Assert.IsFalse(Value.IsNotEqual(Value));
            Assert.IsTrue(CompareToLess.IsNotEqual(Value));
            Assert.IsTrue(CompareToGreater.IsNotEqual(Value));
        }
        [TestMethod]
        public void IsGreatorThan()
        {
            Assert.IsTrue(CompareToGreater.IsGreatorThan(Value));
            Assert.IsFalse(Value.IsGreatorThan(Value));
            Assert.IsFalse(CompareToLess.IsGreatorThan(Value));
        }
        [TestMethod]
        public void IsGreaterThanOrEqualTo()
        {
            Assert.IsTrue(Value.IsGreaterThanOrEqualTo(Value));
            Assert.IsTrue(CompareToGreater.IsGreaterThanOrEqualTo(Value));
            Assert.IsFalse(CompareToLess.IsGreaterThanOrEqualTo(Value));
        }
        [TestMethod]
        public void IsLessThan()
        {
            Assert.IsTrue(CompareToLess.IsLessThan(Value));
            Assert.IsFalse(CompareToGreater.IsLessThan(Value));
            Assert.IsFalse(Value.IsLessThan(Value));
        }
        [TestMethod]
        public void IsLessThanOrEqualTo()
        {
            Assert.IsTrue(Value.IsLessThanOrEqualTo(Value));
            Assert.IsTrue(CompareToLess.IsLessThanOrEqualTo(Value));
            Assert.IsFalse(CompareToGreater.IsLessThanOrEqualTo(Value));
        }
    }

    [TestClass]
    public class ValidateComparableShort : ValidateIComparable<short>
    {
        protected override short Value => Short10;
        protected override short CompareToLess => Short9;
        protected override short CompareToGreater => Short11;
    }
    [TestClass]
    public class ValidateComparableUShort : ValidateIComparable<ushort>
    {
        protected override ushort Value => UShort10;
        protected override ushort CompareToLess => UShort9;
        protected override ushort CompareToGreater => UShort11;
    }
    [TestClass]
    public class ValidateComparableInt : ValidateIComparable<int>
    {
        protected override int Value => Int10;
        protected override int CompareToLess => Int9;
        protected override int CompareToGreater => Int11;
    }
    [TestClass]
    public class ValidateComparableUInt : ValidateIComparable<uint>
    {
        protected override uint Value => UInt10;
        protected override uint CompareToLess => UInt9;
        protected override uint CompareToGreater => UInt11;
    }
    [TestClass]
    public class ValidateComparableLong : ValidateIComparable<long>
    {
        protected override long Value => Long10;
        protected override long CompareToLess => Long9;
        protected override long CompareToGreater => Long11;
    }
    [TestClass]
    public class ValidateComparableULong : ValidateIComparable<ulong>
    {
        protected override ulong Value => ULong10;
        protected override ulong CompareToLess => ULong9;
        protected override ulong CompareToGreater => ULong11;
    }
    [TestClass]
    public class ValidateComparableDouble : ValidateIComparable<double>
    {
        protected override double Value => Double10;
        protected override double CompareToLess => Double9;
        protected override double CompareToGreater => Double11;
    }
    [TestClass]
    public class ValidateComparableFloat : ValidateIComparable<float>
    {
        protected override float Value => Float10;
        protected override float CompareToLess => Float9;
        protected override float CompareToGreater => Float11;
    }
    [TestClass]
    public class ValidateComparableDecimal : ValidateIComparable<decimal>
    {
        protected override decimal Value => Decimal10;
        protected override decimal CompareToLess => Decimal9;
        protected override decimal CompareToGreater => Decimal11;
    }

    [TestClass]
    public class ValidateComparableDateTime : ValidateIComparable<DateTime>
    {
        protected override DateTime Value => Today;
        protected override DateTime CompareToLess => Yesterday;
        protected override DateTime CompareToGreater => Tomorrow;
    }
    [TestClass]
    public class ValidateComparableTimeSpan : ValidateIComparable<TimeSpan>
    {
        protected override TimeSpan Value => Noon;
        protected override TimeSpan CompareToLess => Morning;
        protected override TimeSpan CompareToGreater => Afternoon;
    }

    public enum ComparableEnum
    {
        Less,
        Equal,        
        Greater
    }
    [TestClass]
    public class ValidateComparableEnum : ValidateIComparable<ComparableEnum>
    {
        protected override ComparableEnum Value => ComparableEnum.Equal;
        protected override ComparableEnum CompareToLess => ComparableEnum.Less;
        protected override ComparableEnum CompareToGreater => ComparableEnum.Greater;
    }

    public sealed class MyCustomComparable : IComparable<MyCustomComparable>, IComparable
    {
        public int PrimaryValue { get; private set; }
        public short SecondaryValue { get; private set; }
        public MyCustomComparable(int primary, short secondary) { 
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
    [TestClass]
    public class ValidateComparableMyCustomComparable : ValidateIComparable<MyCustomComparable>
    {
        protected override MyCustomComparable Value => new MyCustomComparable(Int10, Short10);
        protected override MyCustomComparable CompareToLess => new MyCustomComparable(Int10, Short9);
        protected override MyCustomComparable CompareToGreater => new MyCustomComparable(Int10, Short11);
    }


    public abstract class ValidateAccessor<T> : UnitTestBase
        where T : class
    {
        protected readonly T TestClass;
        private readonly Accessor<T> Accessor; //= Accessor<T>.Instance;
        private readonly IDataCollection DbDataCollection; 
        public ValidateAccessor() {
            TestClass = InitTestClass();
            Accessor = Accessor<T>.Instance;
            DbDataCollection = GetDataCollection();
        }
        protected abstract T InitTestClass();

        [TestMethod]
        public void IsValid()
        {
            Validate();
        }
        protected virtual IDataCollection GetDataCollection()
        => TestClass.ToDataCollection();

        protected virtual void Validate()
        {
            Assert.IsTrue(true);
        }
        protected void ValidatePropertyGet(object expected, string name)
        {
            ValidateAccessorEntry(expected, name);
            ValidateCollectionEntry(expected, name);
        }
        private bool DataCollectionHasKey(string name)
        => DbDataCollection.Keys.Contains(name);
        protected void ValidateAccessorEntry(object expected, string name)
        {
            bool success = TestClass.TryGetValue(name, out var actual);
            Assert.IsTrue(success, $"Unable to find property or field {name}");
            Assert.AreEqual(expected, actual, $"expected {name} to be {expected}, got {actual}");
        }
        protected void ValidateCollectionEntry(object expected, string name)
        {
            object actualCollectionValue = DbDataCollection[name];
            Assert.IsTrue(DataCollectionHasKey(name), $"Unable to find collection key {name}");
            Assert.AreEqual(expected, actualCollectionValue, $"expected collection entry {name} to be {expected}, got {actualCollectionValue}");
        }
        protected void ValidateCollectionHasKey(string name)
        {
            Assert.IsTrue(DataCollectionHasKey(name), $"Unable to find collection key {name}");
        }
        protected void ValidateCollectionDoesNotHaveKey(string name)
        {
            Assert.IsFalse(DataCollectionHasKey(name), $"Collection contains unexpected key {name}");
        }
    }
    [TestClass]
    public class ValidateAccessorUnitTestDbParams : ValidateAccessor<UnitTestDbParams>
    {
        protected override UnitTestDbParams InitTestClass()
        => Parameters;

        protected override void Validate()
        {
            ValidatePropertyGet(TestClass.Null, "Null");
            ValidatePropertyGet(TestClass.EmptyString, "EmptyString");
            ValidatePropertyGet(TestClass.String, "String");
            ValidatePropertyGet(TestClass.FiveDigitString, "FiveDigitString");
            ValidatePropertyGet(TestClass.TenDigitString, "TenDigitString");
            ValidatePropertyGet(TestClass.SSN, "SSN");
            ValidatePropertyGet(TestClass.SSNNoDashes, "SSNNoDashes");
            ValidatePropertyGet(TestClass.InvalidSSN, "InvalidSSN");
            ValidatePropertyGet(TestClass.InvalidSSNNoDashes, "InvalidSSNNoDashes");

            ValidatePropertyGet(TestClass.StringInvalidNum, "StringInvalidNum");
            ValidatePropertyGet(TestClass.StringInvalidDate, "StringInvalidDate");
            ValidatePropertyGet(TestClass.StringNum, "StringNum");
            ValidatePropertyGet(TestClass.StringNumNull, "StringNumNull");
            ValidatePropertyGet(TestClass.Email, "Email");
            ValidatePropertyGet(TestClass.EmailNull, "EmailNull");
            ValidatePropertyGet(TestClass.InvalidEmail, "InvalidEmail");
            ValidatePropertyGet(TestClass.ForbiddenEmail, "ForbiddenEmail");
            ValidatePropertyGet(TestClass.FormatedString, "FormatedString");

            ValidatePropertyGet(TestClass.Short, "Short");
            ValidatePropertyGet(TestClass.Int, "Int");
            ValidatePropertyGet(TestClass.Long, "Long");
            ValidatePropertyGet(TestClass.UShort, "UShort");
            ValidatePropertyGet(TestClass.UInt, "UInt");
            ValidatePropertyGet(TestClass.ULong, "ULong");
            ValidatePropertyGet(TestClass.Double, "Double");
            ValidatePropertyGet(TestClass.Float, "Float");
            ValidatePropertyGet(TestClass.Decimal, "Decimal");
            //ValidatePropertyGet(TestClass.DateTime, "DateTime");
            ValidatePropertyGet(TestClass.Today, "Today");
            ValidatePropertyGet(TestClass.TodayOptional, "TodayOptional");
            ValidatePropertyGet(TestClass.Yesterday, "Yesterday");
            ValidatePropertyGet(TestClass.YesterdayOptional, "YesterdayOptional");
            ValidatePropertyGet(TestClass.Tomorrow, "Tomorrow");
            ValidatePropertyGet(TestClass.TomorrowOptional, "TomorrowOptional");
            ValidatePropertyGet(TestClass.DateTimeNull, "DateTimeNull");
            ValidatePropertyGet(TestClass.DateTimeString, "DateTimeString");
            ValidatePropertyGet(TestClass.DateTimeStringAlt, "DateTimeStringAlt");


            ValidatePropertyGet(TestClass.ShortOptional, "ShortOptional");
            ValidatePropertyGet(TestClass.IntOptional, "IntOptional");
            ValidatePropertyGet(TestClass.LongOptional, "LongOptional");
            ValidatePropertyGet(TestClass.UShortOptional, "UShortOptional");
            ValidatePropertyGet(TestClass.UIntOptional, "UIntOptional");
            ValidatePropertyGet(TestClass.ULongOptional, "ULongOptional");
            ValidatePropertyGet(TestClass.DoubleOptional, "DoubleOptional");
            ValidatePropertyGet(TestClass.FloatOptional, "FloatOptional");
            ValidatePropertyGet(TestClass.DecimalOptional, "DecimalOptional");
            //ValidatePropertyGet(TestClass.DateTimeOptional, "DateTimeOptional");

            ValidatePropertyGet(TestClass.ShortOptionalNull, "ShortOptionalNull");
            ValidatePropertyGet(TestClass.IntOptionalNull, "IntOptionalNull");
            ValidatePropertyGet(TestClass.LongOptionalNull, "LongOptionalNull");
            ValidatePropertyGet(TestClass.UShortOptionalNull, "UShortOptionalNull");
            ValidatePropertyGet(TestClass.UIntOptionalNull, "UIntOptionalNull");
            ValidatePropertyGet(TestClass.ULongOptionalNull, "ULongOptionalNull");
            ValidatePropertyGet(TestClass.DoubleOptionalNull, "DoubleOptionalNull");
            ValidatePropertyGet(TestClass.FloatOptionalNull, "FloatOptionalNull");
            ValidatePropertyGet(TestClass.DecimalOptionalNull, "DecimalOptionalNull");
            ValidatePropertyGet(TestClass.DateTimeOptionalNull, "DateTimeOptionalNull");

        }
    }
    [TestClass]
    public class ValidateAccessorUnitTestDbParamsForAccessor : ValidateAccessor<UnitTestDbParamsForAccessor>
    {
        protected override UnitTestDbParamsForAccessor InitTestClass()
        => new UnitTestDbParamsForAccessor();
        protected override IDataCollection GetDataCollection()
        => TestClass.ToDataCollection<UnitTestDbParamsForAccessor, TestColumnAttribute>();

        protected override void Validate()
        {
            ValidatePropertyGet(TestClass.FullName, "FullName");
            ValidatePropertyGet(TestClass.Age, "Age");
            ValidatePropertyGet(TestClass.Updated, "Updated");

            ValidateAccessorEntry(TestClass.UpdatedInternal, "UpdatedInternal");
            ValidateAccessorEntry(TestClass.FirstName, "FirstName");
            ValidateAccessorEntry(TestClass.MiddleName, "MiddleName");
            ValidateAccessorEntry(TestClass.LastName, "LastName");

            ValidateCollectionDoesNotHaveKey("UpdatedInternal");
            ValidateCollectionDoesNotHaveKey("FirstName");
            ValidateCollectionDoesNotHaveKey("MiddleName");
            ValidateCollectionDoesNotHaveKey("LastName");

        }
    }


    public abstract class ValidatDataCollectionBuilder<T> : UnitTestBase
        where T : class
    {
        protected readonly T TestClass;
        private readonly IDataCollection DbDataCollection;
        public ValidatDataCollectionBuilder()
        {
            TestClass = InitTestClass();
            DbDataCollection = TestClass.ToDataCollection(BuildCollection);
        }
        protected abstract T InitTestClass();

        [TestMethod]
        public void IsValid()
        {
            Validate();
        }
        protected virtual void BuildCollection(ParameterDataCollection<T> builder) { }

        protected virtual void Validate()
        {
            Assert.IsTrue(true);
        }
        
        private bool DataCollectionHasKey(string name)
        => DbDataCollection.Keys.Contains(name);
        
        protected void ValidateCollectionEntry(object expected, string name)
        {
            object actualCollectionValue = DbDataCollection[name];
            Assert.IsTrue(DataCollectionHasKey(name), $"Unable to find collection key {name}");
            Assert.AreEqual(expected, actualCollectionValue, $"expected collection entry {name} to be {expected}, got {actualCollectionValue}");
        }
        protected void ValidateCollectionEntryByOrdinal(object expected, int index)
        {
            object actualCollectionValue = DbDataCollection[index];
            Assert.AreEqual(expected, actualCollectionValue, $"expected collection entry at index {index} to be {expected}, got {actualCollectionValue}");
        }

        protected void ValidateCollectionHasKey(string name)
        {
            Assert.IsTrue(DataCollectionHasKey(name), $"Unable to find collection key {name}");
        }
        protected void ValidateCollectionDoesNotHaveKey(string name)
        {
            Assert.IsFalse(DataCollectionHasKey(name), $"Collection contains unexpected key {name}");
        }
    }

    [TestClass]
    public class ValidateCollectionBuilder : ValidatDataCollectionBuilder<UnitTestDbParamsForAccessor>
    {

        protected override UnitTestDbParamsForAccessor InitTestClass()
        => new UnitTestDbParamsForAccessor();
        protected override void BuildCollection(ParameterDataCollection<UnitTestDbParamsForAccessor> builder)
        {
            builder.AddInput("FullName", m => m.FullName);
            builder.AddInput("Age", m => m.Age);
            builder.AddInput("Updated", m => m.Updated);
        }

        protected override void Validate()
        {
            ValidateCollectionEntry(TestClass.FullName, "FullName");
            ValidateCollectionEntry(TestClass.Age, "Age");
            ValidateCollectionEntry(TestClass.Updated, "Updated");

            ValidateCollectionEntryByOrdinal(TestClass.FullName, 0);
            ValidateCollectionEntryByOrdinal(TestClass.Age, 1);
            ValidateCollectionEntryByOrdinal(TestClass.Updated, 2);

            ValidateCollectionDoesNotHaveKey("UpdatedInternal");
            ValidateCollectionDoesNotHaveKey("FirstName");
            ValidateCollectionDoesNotHaveKey("MiddleName");
            ValidateCollectionDoesNotHaveKey("LastName");
        }
    }
}
