using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Attributes;
using System;
using System.Data;

namespace Facade_Sabdbox_Run_Environment.TestFacade.Models
{
    public class TestDbDataModel : DbDataModel
    {
        public TestDbDataModel(IDataRecord data) : base(data) { }

        [DbColumn("guid")]
        public Guid GUID { get; private set; }

        [DbColumn("CreateDate")]
        public DateTime CreatedDate { get; private set; }

        [DbColumn("count")]
        public int Count { get; private set; }

        [NestedModel()]
        public TestNestedDbDataModel Comments { get; private set; }

    }
    public class TestNestedDbDataModel : DbDataModel
    {
        public TestNestedDbDataModel(IDataRecord data) : base(data) { }
        
        [DbColumn("comment")]
        public string Comment { get; private set; }

        [DbColumn("CreateDate", "dddd, MMMM dd, yyyy h:mm:ss tt", true)]
        public string CreatedDateStr { get; private set; }
        
    }


    public class MoreDbDataModel : DbDataModel
    {
        public MoreDbDataModel(IDataRecord data) : base(data) { }

        [DbColumn("number")]
        public double Number { get; private set; }
        

        [DbColumn("ConcatValues")]
        public int[] Count { get; private set; }

        [NestedModel()]
        public MoreNameDbDataModel Name { get; private set; }

        [DbColumn("ConcatValues")]
        public CustomDbDataModel Custom { get; private set; }
        

    }
    public class MoreNameDbDataModel : DbDataModel
    {
        public MoreNameDbDataModel(IDataRecord data) : base(data) { }

        [DbColumn("FirstName")]
        public string FirstName { get; private set; }
        [DbColumn("LastName")]
        public string LastName { get; private set; }
        [DbColumn("MiddleInitial")]
        public string MiddleInitial { get; private set; }

    }
    public class CustomDbDataModel : DbDataModel
    {
        public CustomDbDataModel(IDataRecord data) : base(data) { }
        public CustomDbDataModel(object columnValue) :base(columnValue)
        {
            string[] values = (columnValue as string).Split(',');
            int[] intValues = Array.ConvertAll(values, int.Parse);
            double value = 0;

            foreach (int val in intValues)
            {
                value += val;
            }
            Value = value;
        }
        public double Value { get; private set; }


    }
}
