using DbFacade.DataLayer.Models;
using DbFacade.Extensions;
using DbFacade.UnitTest.DataLayer.Models.Attributes;
using DbFacade.UnitTest.DataLayer.Models.Parameters;

namespace DbFacade.UnitTest.Tests.Validation
{
    public class ValidateAccessorUnitTestDbParamsForAccessor : ValidateAccessor<UnitTestDbParamsForAccessor>
    {
        public ValidateAccessorUnitTestDbParamsForAccessor(ServiceFixture services) : base(services) { }
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

            
            var data = TestClass.ToDataCollection<UnitTestDbParamsForAccessor, TestBadAttribute>();
            Assert.Empty(data.Keys);
        }
    }
}
