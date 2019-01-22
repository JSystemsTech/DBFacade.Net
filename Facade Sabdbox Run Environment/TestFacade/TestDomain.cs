using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade;
using Facade_Sabdbox_Run_Environment.TestFacade.DbMethods;
using Facade_Sabdbox_Run_Environment.TestFacade.Models;
using System;
using System.Collections.Generic;

namespace Facade_Sabdbox_Run_Environment.TestFacade
{
    public partial class TestDomain: DomainFacade<TestManager, TestDbMethods>
    {
        public List<TestDbDataModel> GetAllSimple()
        {
            return FetchRecords<TestDbDataModel>(TestDbMethods.GetAllSimple).GetResponse();
        }
        public List<MoreDbDataModel> GetAllMore()
        {
            return FetchRecords<MoreDbDataModel>(TestDbMethods.GetAllMore).GetResponse();
        }
        public void AddSimpleRecord(int count, string comment)
        { 
            Func<SimpleDbParamsModel<int, string>, DbMethodsCore, bool> validator = (model, dbMethod) => false;
            Transaction(new SimpleDbParamsModel<int, string>(count, comment).AddValidator(validator), TestDbMethods.AddSimple);
        }
        protected override void OnBeforeForward<U>(U parameters, TestDbMethods dbMethod)
        {
            //throw new System.NotImplementedException();
        }
    }
}
