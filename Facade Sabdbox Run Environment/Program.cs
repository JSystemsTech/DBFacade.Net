using DomainFacade.DataLayer.Models;
using Facade_Sabdbox_Run_Environment.TestFacade;
using Facade_Sabdbox_Run_Environment.TestFacade.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Facade_Sabdbox_Run_Environment
{
    class Program
    {
        static void Main(string[] args)
        {
            


            //ServiceConfig.DomainFacade.AddSimpleRecord(55, null);
            //Console.WriteLine(GetName(typeof(ServiceConfigChild)));
            IEnumerable<TestDbDataModel> test = ServiceConfig.DomainFacade.GetAllSimple();
            IEnumerable<MoreDbDataModel> test2 = ServiceConfig.DomainFacade.GetAllMore();

            IEnumerable<TestSharedDbDataModel> test3 = ServiceConfig.DomainFacade.GetAllSimpleShared();
            IEnumerable<TestSharedDbDataModel> test4 = ServiceConfig.DomainFacade.GetAllMoreShared();


            int i = 5;
        }
        static string GetName(object test)
        {
            return test.GetType().Name;
        }
        static string GetName(Type t)
        {
            return t.Name;
        }

    }

    public class ServiceConfig {
        
        public static TestDomain DomainFacade = new TestDomain();

    }
    public class ServiceConfigChild: ServiceConfig
    {

    }


}
