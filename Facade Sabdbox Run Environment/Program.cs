using Facade_Sabdbox_Run_Environment.TestFacade;
using Facade_Sabdbox_Run_Environment.TestFacade.Models;
using System.Collections.Generic;

namespace Facade_Sabdbox_Run_Environment
{
    class Program
    {
        static void Main(string[] args)
        {
           ServiceConfig.DomainFacade.AddSimpleRecord(78, null);
            List<TestDbDataModel> test = ServiceConfig.DomainFacade.GetAllSimple();
            List<MoreDbDataModel> test2 = ServiceConfig.DomainFacade.GetAllMore();

            
            int i = 5;
        }
        
    }

    public class ServiceConfig {
        
        public static TestDomain DomainFacade = new TestDomain();

    }
    

}
