using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Attributes;
using Facade_Sabdbox_Run_Environment.TestFacade.DbMethods;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Facade_Sabdbox_Run_Environment.TestFacade.Models
{
    public class TestDbDataModel : DbDataModel
    {

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
        
        [DbColumn.String("comment","default text in view model")]
       
        public string Comment { get; private set; }

        [DbDateStringColumn("CreateDate", "dddd, MMMM dd, yyyy h:mm:ss tt")]
        
        public string CreatedDateStr { get; private set; }

        [DbColumn.String(typeof(TestDbMethods.GetAllSimple),"comment", "default text in view model")]
        [DbDateStringColumn(typeof(TestDbMethods.GetAllSimple),"CreateDate", "dddd, MMMM dd, yyyy h:mm:ss tt")]
        public TestNestedDbDataModel( string commnet)
        {
            Comment = commnet;
        }
        public TestNestedDbDataModel() { }

    }

    public class TestSharedDbDataModel : DbDataModel
    {

        [DbColumn(typeof(TestDbMethods.GetAllSimple), "comment")]
        [DbColumn(typeof(TestDbMethods.GetAllMore), "FirstName")]
        public string GeneralText { get; private set; }

    }


    public class MoreDbDataModel : DbDataModel
    {

        [DbColumn("number")]
        public double Number { get; private set; }
        

        [DbEnumerableColumn.List("ConcatValues")]
        public List<int> Count { get; private set; }

        [NestedModel()]
        public MoreNameDbDataModel Name { get; private set; }  
        

    }
    public class MoreNameDbDataModel : DbDataModel
    {

        [DbColumn("FirstName")]
        public string FirstName { get; private set; }
        [DbColumn("LastName")]
        public string LastName { get; private set; }
        [DbColumn("MiddleInitial")]
        public string MiddleInitial { get; private set; }


    }
   
    public class TestConstructorModel: DbDataModel
    {
        [DbColumn(typeof(TestDbMethods.GetAllSimple),"guid")]
        [DbColumn(typeof(TestDbMethods.GetAllSimple), "CreateDate")]
        [DbColumn(typeof(TestDbMethods.GetAllSimple), "count")]
        [DbFlagColumn(typeof(TestDbMethods.GetAllSimple), "count", 123)]
        [DbColumn(typeof(TestDbMethods.GetAllSimple), "comment")]
        public TestConstructorModel(Guid guid, DateTime createdDate, int count, bool flag, string comment)
        {
            GUID = guid;
            CreatedDate = createdDate;
            Count = count;
            Flag = flag;
            Comment = comment;
        }
        
        public Guid GUID { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public int Count { get; private set; }
        public bool Flag { get; private set; }
        public string Comment { get; private set; }

    }
}
