using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Attributes;
using Facade_Sabdbox_Run_Environment.TestFacade.DbMethods;
using System;
using System.Collections.Generic;
using System.Data;

namespace Facade_Sabdbox_Run_Environment.TestFacade.Models
{
    public class TestDbDataModel : DbDataModel
    {
        public TestDbDataModel(IDataRecord data, IDbMethod dbMethod) : base(data, dbMethod) { }

        [DbColumn("guid")]
        public Guid GUID { get; private set; }

        [DbColumn("CreateDate")]
        public DateTime CreatedDate { get; private set; }

        [DbColumn("count")]
        public int Count { get; private set; }

        [DbFlagColumn("count",123)]
        public bool Flag { get; private set; }

        [NestedModel()]
        public TestNestedDbDataModel Comments { get; private set; }

    }
    public class TestNestedDbDataModel : DbDataModel
    {
        public TestNestedDbDataModel(IDataRecord data, IDbMethod dbMethod) : base(data, dbMethod) { }
        
        [DbColumn.String("comment","default text in view model")]
        public string Comment { get; private set; }

        [DbDateStringColumn("CreateDate", "dddd, MMMM dd, yyyy h:mm:ss tt")]
        public string CreatedDateStr { get; private set; }
        
    }

    public class TestSharedDbDataModel : DbDataModel
    {
        public TestSharedDbDataModel(IDataRecord data, IDbMethod dbMethod) : base(data, dbMethod) { }

        [DbColumn(typeof(TestDbMethods.GetAllSimpleData), "comment")]
        [DbColumn(typeof(TestDbMethods.GetAllMoreData), "FirstName")]
        public string GeneralText { get; private set; }

    }


    public class MoreDbDataModel : DbDataModel
    {
        public MoreDbDataModel(IDataRecord data, IDbMethod dbMethod) : base(data, dbMethod) { }

        [DbColumn("number")]
        public double Number { get; private set; }
        

        [DbEnumerableColumn.List("ConcatValues")]
        public List<int> Count { get; private set; }

        [NestedModel()]
        public MoreNameDbDataModel Name { get; private set; }

        [DbColumn("ConcatValues")]
        public CustomDbDataModel Custom { get; private set; }
        

    }
    public class MoreNameDbDataModel : DbDataModel
    {
        public MoreNameDbDataModel(IDataRecord data, IDbMethod dbMethod) : base(data, dbMethod) { }

        [DbColumn("FirstName")]
        public string FirstName { get; private set; }
        [DbColumn("LastName")]
        public string LastName { get; private set; }
        [DbColumn("MiddleInitial")]
        public string MiddleInitial { get; private set; }


    }
    public class CustomDbDataModel : DbDataModel
    {
        public CustomDbDataModel(IDataRecord data, IDbMethod dbMethod) : base(data, dbMethod) { }
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
