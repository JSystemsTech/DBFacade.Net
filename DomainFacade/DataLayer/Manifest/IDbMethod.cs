using DomainFacade.DataLayer.CommandConfig;
using System;

namespace DomainFacade.DataLayer.Manifest
{
    public interface IDbMethod
    {
        IDbCommandConfig GetConfig();
        Type GetType();
    }
}
