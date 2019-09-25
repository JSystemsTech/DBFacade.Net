using DBFacade.DataLayer.Models;
using DBFacade.Facade;
using DBFacade.Facade.Core;
using DbFacadeUnitTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.TestFacade
{
    class UnitTestManager: DomainManager<UnitTestMethods>
    {
        protected override void OnBeforeNext<TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            IUnitTestDbParamsModel unitTestParams = parameters as IUnitTestDbParamsModel;
            if (!unitTestParams.IsValidModel)
            {
                throw new Exception("UnitTestManager caught invalid model");
            }
        }
    }
    class UnitTestDbFacadeStep1 : DomainManager<UnitTestMethods>
    {
        protected override IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            IUnitTestDbParamsModel unitTestParams = parameters as IUnitTestDbParamsModel;
            if (unitTestParams.StopAtStep1)
            {
                throw new Exception("Stopping at step 1");
            }
            return Next<UnitTestDbFacadeStep2, TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }
        
    }
    class UnitTestDbFacadeStep2 : DbFacade<UnitTestMethods>
    {
        protected override IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            IUnitTestDbParamsModel unitTestParams = parameters as IUnitTestDbParamsModel;
            if (unitTestParams.StopAtStep2)
            {
                throw new Exception("Stopping at step 2");
            }
            return Next<UnitTestDbFacadeStep3, TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }

    }
    class UnitTestDbFacadeStep3 : DbFacade<UnitTestMethods>
    {
        protected override IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            IUnitTestDbParamsModel unitTestParams = parameters as IUnitTestDbParamsModel;
            if (unitTestParams.StopAtStep3)
            {
                throw new Exception("Stopping at step 3");
            }
            return Next<DefaultDomainManager<UnitTestMethods>, TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }

    }
}
