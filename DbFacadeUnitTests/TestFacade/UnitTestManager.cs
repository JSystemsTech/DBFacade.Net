﻿using DBFacade.DataLayer.Models;
using DBFacade.Facade;
using DBFacade.Facade.Core;
using DbFacadeUnitTests.Models;
using System;

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

    }
}
