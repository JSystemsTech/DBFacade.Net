﻿using DomainFacade.DataLayer.CommandConfig.Parameters;
using DomainFacade.DataLayer.ConnectionService;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;

namespace DomainFacade.DataLayer.CommandConfig
{
    public sealed class DbCommandConfigBuilder
    {
        public static IDbCommandConfig GetFetchConfig<Con>(IDbCommandText<Con> dbCommandText)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<IDbParamsModel, Con>(dbCommandText);
        }
        public static IDbCommandConfig GetFetchConfigWithReturn<Con>(IDbCommandText<Con> dbCommandText, string returnParam)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<IDbParamsModel, Con>(dbCommandText, returnParam);
        }
        public static IDbCommandConfig GetTransactionConfig<Con>(IDbCommandText<Con> dbCommandText)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<IDbParamsModel, Con>(dbCommandText).AsTransaction();
        }
        public static IDbCommandConfig GetTransactionConfigWithReturn<Con>(IDbCommandText<Con> dbCommandText, string returnParam)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<IDbParamsModel, Con>(dbCommandText, returnParam).AsTransaction();
        }
    }


    public class DbCommandConfigBuilder<TDbParams> 
        where TDbParams : IDbParamsModel
    {
        
        public static IDbCommandConfig GetFetchConfig<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams)
        where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams);
        }
        public static IDbCommandConfig GetFetchConfig<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, validator);
        }
        public static IDbCommandConfig GetFetchConfigWithReturn<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, string returnParam)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, returnParam);
        }
        public static IDbCommandConfig GetFetchConfigWithReturn<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, string returnParam, Validator<TDbParams> validator)
        where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, returnParam, validator);
        }
         
        public static IDbCommandConfig GetTransactionConfig<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams).AsTransaction();
        }
        public static IDbCommandConfig GetTransactionConfig<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, validator).AsTransaction();
        }
        
        public static IDbCommandConfig GetTransactionConfigWithReturn<Con>(IDbCommandText<Con> dbCommandText, string returnParam, IDbCommandConfigParams<TDbParams> dbParams)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, returnParam).AsTransaction();
        }
        public static IDbCommandConfig GetTransactionConfigWithReturn<Con>(IDbCommandText<Con> dbCommandText, string returnParam, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, returnParam, validator).AsTransaction();
        }
    }
}
