using DomainFacade.DataLayer.DbManifest;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Reflection;

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

        private dynamic ModelProperties { get; set; }
        public dynamic GetModelProperties()
        {
            if (ModelProperties == null)
            {
                IDictionary<string, object> expando = new ExpandoObject();
                BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
                foreach (var propertyInfo in GetType().GetProperties(flags))
                {
                    expando.Add(propertyInfo.Name, propertyInfo);
                }
                ModelProperties = expando as ExpandoObject;
            }
            return ModelProperties;
        }
    }  

}
