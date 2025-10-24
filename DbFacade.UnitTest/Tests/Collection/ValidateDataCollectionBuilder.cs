using DbFacade.DataLayer.Models;
using DbFacade.Extensions;

namespace DbFacade.UnitTest.Tests.Collection
{
    public abstract class ValidateDataCollectionBuilder<T> : UnitTestBase
        where T : class
    {
        protected readonly T TestClass;
        private readonly IDataCollection DbDataCollection;
        public ValidateDataCollectionBuilder(ServiceFixture services) : base(services)
        {
            TestClass = InitTestClass();
            DbDataCollection = TestClass.ToDataCollection(BuildCollection);
        }
        protected abstract T InitTestClass();

        [Fact]
        public void IsValid()
        {
            Validate();
        }
        protected virtual void BuildCollection(ParameterDataCollection<T> builder) { }

        protected virtual void Validate()
        {
            Assert.True(true);
        }

        private bool DataCollectionHasKey(string name)
        => DbDataCollection.Keys.Contains(name);

        protected void ValidateCollectionEntry(object expected, string name)
        {
            object actualCollectionValue = DbDataCollection[name];
            Assert.True(DataCollectionHasKey(name), $"Unable to find collection key {name}");
            Assert.Equal(expected, actualCollectionValue);
        }
        protected void ValidateCollectionEntryByOrdinal(object expected, int index)
        {
            object actualCollectionValue = DbDataCollection[index];
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
