using System;
using DBFacade.Facade;
using DBFacade.DataLayer.Models;
using DomainLayerSandbox1.Facades.SampleFacade;

namespace DomainLayerSandbox1.TestFacade
{
    public class TestManager : DomainManager<TestDbMethods>
    {
        protected override void OnError(IDbResponse response)
        {
            //throw response.GetException();
            Console.WriteLine("Error Detected: " + response.GetException().Name());
        }
        protected override void OnBeforeForward<U, TestDbMethods>(U parameters)
        {
            Console.WriteLine("stop here");
        }
    }
}
