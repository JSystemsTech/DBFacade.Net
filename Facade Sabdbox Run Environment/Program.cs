using DBFacade.DataLayer.Models;
using Facade_Sabdbox_Run_Environment.TestFacade;
using Facade_Sabdbox_Run_Environment.TestFacade.Models;
using System.Collections.Generic;
using System.Linq;

namespace Facade_Sabdbox_Run_Environment
{
    class Program
    {

        static void Main(string[] args)
        {
            //ServiceConfig.DomainFacade.CallMissingSproc();
            //IEnumerable<TestDbDataModel> UnitTestData = ServiceConfig.UnitTestFacade.GetAllSimple();
            //ServiceConfig.DomainFacade.AddSimpleRecord(55, null);
            //Console.WriteLine(GetName(typeof(ServiceConfigChild)));
            IEnumerable<TestDbDataModel> test = ServiceConfig.DomainFacade.GetAllSimple();
            IDbResponse<TestDbDataModel> testResponse = ServiceConfig.DomainFacade.GetAllSimpleResponse();
            IEnumerable<TestConstructorModel> testCon = ServiceConfig.DomainFacade.GetAllSimple2();
            IEnumerable<MoreDbDataModel> test2 = ServiceConfig.DomainFacade.GetAllMore();
            TestDbDataModel first = test.First();
            string json = first.ToJson();
            TestDbDataModel parsed = DbDataModel.ParseJson<TestDbDataModel>(json);
            IEnumerable<TestSharedDbDataModel> test3 = ServiceConfig.DomainFacade.GetAllSimpleShared();
            IEnumerable<TestSharedDbDataModel> test4 = ServiceConfig.DomainFacade.GetAllMoreShared();


            int i = 5;
        }
    }

    public class ServiceConfig {
        private static ITestDomain domainFacade = new TestDomain();
        private static ITestDomain UnitTestFacade = new MockTestDomain();
        
        public static ITestDomain DomainFacade { get{return domainFacade; } private set{} }
    }
}
