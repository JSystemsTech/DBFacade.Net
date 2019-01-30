﻿using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade;
using Facade_Sabdbox_Run_Environment.TestFacade.DbMethods;
using Facade_Sabdbox_Run_Environment.TestFacade.Models;
using System;
using System.Collections.Generic;

namespace Facade_Sabdbox_Run_Environment.TestFacade
{
    public sealed partial class TestDomain: DomainFacade<TestManager, TestDbMethods>
    {
        public IEnumerable<TestDbDataModel> GetAllSimple()
        {            
            return FetchRecords<TestDbDataModel>(TestDbMethods.GetAllSimple).GetResponse();
        }
        public IEnumerable<TestSharedDbDataModel> GetAllMoreShared()
        {
            return FetchRecords<TestSharedDbDataModel>(TestDbMethods.GetAllSimple).GetResponse();
        }
        public IEnumerable<MoreDbDataModel> GetAllMore()
        {
            return FetchRecords<MoreDbDataModel>(TestDbMethods.GetAllMore).GetResponse();
        }
        public IEnumerable<TestSharedDbDataModel> GetAllSimpleShared()
        {
            return FetchRecords<TestSharedDbDataModel>(TestDbMethods.GetAllMore).GetResponse();
        }
        public void AddSimpleRecord(int count, string comment)
        { 
            Transaction(new SimpleDbParamsModel<int, string>(count, comment), TestDbMethods.AddSimple);
        }
        protected override void OnBeforeForward<U>(U parameters, TestDbMethods dbMethod)
        {
            //throw new System.NotImplementedException();
        }
    }
}
