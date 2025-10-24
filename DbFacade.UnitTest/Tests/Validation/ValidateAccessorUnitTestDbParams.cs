using DbFacade.UnitTest.DataLayer.Models.Parameters;

namespace DbFacade.UnitTest.Tests.Validation
{
    public class ValidateAccessorUnitTestDbParams : ValidateAccessor<UnitTestDbParams>
    {
        public ValidateAccessorUnitTestDbParams(ServiceFixture services) : base(services) { }
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
}
