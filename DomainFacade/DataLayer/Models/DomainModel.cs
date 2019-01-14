using DomainFacade.DataLayer.DbManifest;
using System.Data;

namespace DomainFacade.DataLayer.Models
{
    public abstract class DomainModel: DbDataModel, IDbParamsModel
    {        
        protected DomainModel(IDataRecord data) : base(data) { }        
        
        public bool Validate<E>(E dbMethod)
            where E : DbMethodsCore
        {
            return ValidateCore(dbMethod);
        }
        protected abstract bool ValidateCore<E>(E dbMethod) where E : DbMethodsCore;
    }  

}
