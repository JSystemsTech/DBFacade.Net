using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainFacade.Facade;
using Facade_Sabdbox_Run_Environment.TestFacade.DbMethods;

namespace Facade_Sabdbox_Run_Environment.TestFacade
{
    public class TestManager: DomainManager<TestDbMethods>
    {

        protected override void OnBeforeForward<U>(U parameters, TestDbMethods dbMethod) {

            Console.WriteLine("stop here");

        }
    }
}
