using DbFacade.DataLayer.Models;
using DbFacade.Extensions;

namespace DbFacade.UnitTest.Tests.Validation
{
    public abstract class ValidateAccessor<T> : UnitTestBase
       where T : class
    {
        protected readonly T TestClass;
        private readonly Accessor<T> Accessor; //= Accessor<T>.Instance;
        private readonly IDataCollection DbDataCollection;
        public ValidateAccessor(ServiceFixture services) : base(services)
        {
            TestClass = InitTestClass();
            Accessor = Accessor<T>.GetInstance();
            DbDataCollection = GetDataCollection();
        }
        protected abstract T InitTestClass();

        [Fact]
        public void IsValid()
        {
            Validate();
        }
        protected virtual IDataCollection GetDataCollection()
        => TestClass.ToDataCollection();

        protected virtual void Validate()
        {
            Assert.True(true);
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
            Assert.True(success, $"Unable to find property or field {name}");
            Assert.Equal(expected, actual);
        }
        protected void ValidateCollectionEntry(object expected, string name)
        {
            object actualCollectionValue = DbDataCollection[name];
            Assert.True(DataCollectionHasKey(name), $"Unable to find collection key {name}");
            Assert.Equal(expected, actualCollectionValue);
        }
        protected void ValidateCollectionHasKey(string name)
        {
            Assert.True(DataCollectionHasKey(name), $"Unable to find collection key {name}");
        }
        protected void ValidateCollectionDoesNotHaveKey(string name)
        {
            Assert.False(DataCollectionHasKey(name), $"Collection contains unexpected key {name}");
        }
    }
}
