using DbFacade.DataLayer.Models;
using DbFacade.Facade;
using DbFacadeUnitTests.Models;
using System;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.TestFacade
{
    class UnitTestManager: DomainManager<UnitTestMethods>
    {
        protected override void OnBeforeNext<TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            IUnitTestDbParamsModel unitTestParams = parameters as IUnitTestDbParamsModel;
            if (!unitTestParams.IsValidModel)
            {
                throw new Exception("UnitTestManager caught invalid model");
            }
        }
        protected override async Task OnBeforeNextAsync<TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            IUnitTestDbParamsModel unitTestParams = parameters as IUnitTestDbParamsModel;
            if (!unitTestParams.IsValidModel)
            {
                throw new Exception("UnitTestManager caught invalid model");
            }
            await Task.Run(() => "Test OnBeforeNextAsync");
        }
    }
    class UnitTestDbFacadeStep1 : DomainManager<UnitTestMethods>
    {
        protected override IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            IUnitTestDbParamsModel unitTestParams = parameters as IUnitTestDbParamsModel;
            if (unitTestParams.StopAtStep1)
            {
                throw new Exception("Stopping at step 1");
            }
            return Next<UnitTestDbFacadeStep2, TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }
        protected override async Task<IDbResponse<TDbDataModel>> ProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            IUnitTestDbParamsModel unitTestParams = parameters as IUnitTestDbParamsModel;
            if (unitTestParams.StopAtStep1)
            {
                throw new Exception("Stopping at step 1");
            }
            return await NextAsync<UnitTestDbFacadeStep2, TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }
    }
    class UnitTestDbFacadeStep2 : DbFacade<UnitTestMethods>
    {
        protected override IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            IUnitTestDbParamsModel unitTestParams = parameters as IUnitTestDbParamsModel;
            if (unitTestParams.StopAtStep2)
            {
                throw new Exception("Stopping at step 2");
            }
            return Next<UnitTestDbFacadeStep3, TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }
        protected override async Task<IDbResponse<TDbDataModel>> ProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            IUnitTestDbParamsModel unitTestParams = parameters as IUnitTestDbParamsModel;
            if (unitTestParams.StopAtStep2)
            {
                throw new Exception("Stopping at step 2");
            }
            return await NextAsync<UnitTestDbFacadeStep3, TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }
    }
    class UnitTestDbFacadeStep3 : DbFacade<UnitTestMethods>
    {
        protected override IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            IUnitTestDbParamsModel unitTestParams = parameters as IUnitTestDbParamsModel;
            if (unitTestParams.StopAtStep3)
            {
                throw new Exception("Stopping at step 3");
            }
            return Next<DefaultDomainManager<UnitTestMethods>, TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }
        protected override async Task<IDbResponse<TDbDataModel>> ProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            IUnitTestDbParamsModel unitTestParams = parameters as IUnitTestDbParamsModel;
            if (unitTestParams.StopAtStep3)
            {
                throw new Exception("Stopping at step 3");
            }
            return await NextAsync<DefaultDomainManager<UnitTestMethods>, TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }
    }
}
