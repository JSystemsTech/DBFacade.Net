using System;
using DBFacade.Facade;
using DBFacade.DataLayer.Models;
using Facade_Sabdbox_Run_Environment.TestFacade.DbMethods;

namespace Facade_Sabdbox_Run_Environment.TestFacade
{
    public class TestManager: DomainManager<TestDbMethods>
    {
        protected override void OnError(IDbResponse response)
        {
            //throw response.GetException();
            Console.WriteLine("Error Detected: " + response.GetException().Name());
        }
        protected override void OnBeforeForward<U, TestDbMethods>(U parameters) {
            Console.WriteLine("stop here");
        }
    }
}
