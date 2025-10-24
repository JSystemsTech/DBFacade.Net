namespace DbFacade.UnitTest.DataLayer.Models.Attributes
{
    public class TestColumnAttribute : Attribute
    {
        public TestColumnAttribute() : base() { }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class TestBadAttribute : Attribute
    {
        private readonly string Name;
        public TestBadAttribute(string name) : base() { Name = name; }
    }
}
