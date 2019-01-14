using DomainFacade.DataLayer.DbManifest;

namespace DomainFacade.DataLayer.Models
{
    public interface IDbParamsModel
    {
        bool Validate<E>(E dbMethod) where E : DbMethodsCore;
    }
}
