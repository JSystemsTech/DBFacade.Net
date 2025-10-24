using DbFacade.DataLayer.Models;
using DbFacade.UnitTest.DataLayer.Models.Parameters;

namespace DbFacade.UnitTest.Tests.Collection
{
    public class ValidateCollectionBuilder : ValidateDataCollectionBuilder<UnitTestDbParamsForAccessor>
    {
        public ValidateCollectionBuilder(ServiceFixture services) : base(services) { }
        protected override UnitTestDbParamsForAccessor InitTestClass()
        => new UnitTestDbParamsForAccessor();
        protected override void BuildCollection(ParameterDataCollection<UnitTestDbParamsForAccessor> builder)
        {
            builder.AddInput("FullName", m => m.FullName);
            builder.AddInput("Age", m => m.Age);
            builder.AddInput("Updated", m => m.Updated);


            builder.AddOutput<string>("OutputStr");
            builder.AddOutput<string>("OutputStrWithSize",23);

            builder.AddInputOutput("InputOutputStr", "test");
            builder.AddInputOutput("InputOutputStrWithSize","testInOut", 23);
            builder.AddInputOutput("InputOutputStr2", m => m.FullName);
            builder.AddInputOutput("InputOutputStrWithSize2", m => m.FullName, 23);
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
