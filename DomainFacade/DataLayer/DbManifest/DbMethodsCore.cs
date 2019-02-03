using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;
using DomainFacade.Utils;
using System;

namespace DomainFacade.DataLayer.DbManifest
{
    public interface IDbMethod
    {
        DbCommandConfig GetConfig();
        Type GetType();
    }
    
    public abstract class DbMethodsCore : IDbMethod
    {
        protected DbCommandConfig Config { get; private set; }
        public DbCommandConfig GetConfig()
        {
            if(Config == null)
            {
                Config = GetConfigCore();
            }
            return Config;
        }
        protected abstract DbCommandConfig GetConfigCore();

    }
}
