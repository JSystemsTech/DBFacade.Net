using DbFacade.DataLayer.Models;
using DbFacade.Extensions;

namespace DbFacade.UnitTest.Tests.Collection
{
    public class ValidateDataCollection : UnitTestBase
    {
        private readonly DataCollection DataCollection;
        private readonly ParameterDataCollection ParmDataCollection;
        private readonly ParameterDataCollection ParmDataCollectionEmpty;
        private const string FullName = "FullName";
        private const int Age = 23;
        private static DateTime Updated = DateTime.Parse("01/01/2025");
        public ValidateDataCollection(ServiceFixture services) : base(services)
        {
            DataCollection = new DataCollection();
            DataCollection["FullName"] = FullName;
            DataCollection["Age"] = Age;
            DataCollection["Updated"] = Updated;
            DataCollection["DateStr"] = "01/01/2025";
            DataCollection["Null"] = null;

            ParmDataCollection = ParameterDataCollection.Create(collection =>
            {
                collection.AddInput("FullName", FullName);
                collection.AddInput("Age", Age);
                collection.AddInput("Updated", Updated);
            });
            ParmDataCollectionEmpty = ParameterDataCollection.Create(collection => {});
        }

        [Fact]
        public void ValidateCollection()
        {
            Assert.Equal(FullName, DataCollection["FullName"]);
            Assert.Equal(Age, DataCollection["Age"]);
            Assert.Equal(Updated, DataCollection["Updated"]);
            Assert.Equal(FullName, DataCollection[0]);
            Assert.Equal(Age, DataCollection[1]);
            Assert.Equal(Updated, DataCollection[2]);


            Assert.Null(DataCollection["BadRef"]);
            Assert.Null(DataCollection[100]);

            Assert.Equal(5, DataCollection.Keys.Count());
            Assert.Equal("FullName", DataCollection.Keys[0]);
            Assert.Equal("Age", DataCollection.Keys[1]);
            Assert.Equal("Updated", DataCollection.Keys[2]);
        }
        [Fact]
        public void ValidateCollectionExtensions()
        {
            Assert.NotNull(DataCollection.ToDateTime("DateStr", "dd/MM/yyyy"));
            Assert.Null(DataCollection.ToDateTime("Null", "dd/MM/yyyy"));
            Assert.Null(DataCollection.ToDateTime("InvalidKey", "dd/MM/yyyy"));
            Assert.Null(DataCollection.ToDateTime("DateStr", "1234"));
            Assert.Equal("01/01/2025", DataCollection.ToDateTimeString("Updated", "dd/MM/yyyy"));
            Assert.Null(DataCollection.ToDateTimeString("Null", "dd/MM/yyyy"));

            Assert.True(DataCollection.ToBoolean("Age", Age));
            Assert.False(DataCollection.ToBoolean("Age", 100));
            Assert.False(DataCollection.ToBoolean("Null", 100));
            Assert.False(DataCollection.ToBoolean("InvalidKey", "test"));
        }
        [Fact]
        public void ValidateEmptyCollection()
        {
            Assert.Empty(DataCollection.Empty.Keys);
            Assert.Null(DataCollection.Empty["FullName"]);
            Assert.Null(DataCollection.Empty[0]);
        }

        [Fact]
        public void ValidateParameterDataCollection()
        {

            Assert.Equal(FullName, ParmDataCollection["FullName"]);
            Assert.Equal(Age, ParmDataCollection["Age"]);
            Assert.Equal(Updated, ParmDataCollection["Updated"]);

            Assert.Equal(FullName, ParmDataCollection[0]);
            Assert.Equal(Age, ParmDataCollection[1]);
            Assert.Equal(Updated, ParmDataCollection[2]);

            Assert.Null(ParmDataCollection[-1]);
            Assert.Null(ParmDataCollection[100]);
            Assert.Null(ParmDataCollection["BadKey"]);

            Assert.Empty(ParmDataCollectionEmpty.Keys);
            Assert.Null(ParmDataCollectionEmpty[-1]);
            Assert.Null(ParmDataCollectionEmpty[100]);
            Assert.Null(ParmDataCollectionEmpty["BadKey"]);
        }
    }
}
