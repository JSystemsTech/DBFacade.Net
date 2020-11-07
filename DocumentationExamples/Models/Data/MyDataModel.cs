using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Attributes;
using System;

namespace DocumentationExamples.Models.Data
{
    public class MyDataModel: DbDataModel
    {
        [DbColumn("MyString")]
        public string MyString { get; private set; }
        [DbColumn("MyInt")]
        public int MyInt { get; private set; }
        [DbColumn("MyDateTime")]
        public DateTime? MyDateTime { get; private set; }

        public MyDataModel() { }
    }
}
