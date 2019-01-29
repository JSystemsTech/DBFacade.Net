using DomainFacade.DataLayer.DbManifest;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace DomainFacade.DataLayer.Models
{
    public abstract class DbParamsModelBase : IDbParamsModel
    {
        private  dynamic ModelProperties { get; set; }
        public dynamic GetModelProperties()
        {
            if(ModelProperties == null)
            {
                IDictionary<string, object> expando = new ExpandoObject();
                BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
                foreach (var propertyInfo in GetType().GetProperties(flags))
                {
                    if (propertyInfo.PropertyType.IsSubclassOf(typeof(DbParamsModelBase)))
                    {
                        object[] args = new object[0] { };
                        Type[] types = new Type[0] {  };
                        IDbParamsModel nestedValues = (IDbParamsModel)propertyInfo.PropertyType.GetConstructor(types).Invoke(args);
                        expando.Add(propertyInfo.Name, nestedValues.GetModelProperties());
                    }
                    else {
                        expando.Add(propertyInfo.Name, propertyInfo);
                    }                    
                }
                ModelProperties =  expando as ExpandoObject;
            }
            return ModelProperties;
        }
        
        public DbParamsModelBase()
        {
        }
        
    }
    public class DbParamsModel : DbParamsModelBase
    {
        
        public DbParamsModel():base()
        {
        }

    }

    public class SimpleDbParamsModel<T> : DbParamsModel
    {
        public T Param1 { get; private set; }
        public SimpleDbParamsModel(T param1) : base() { Param1 = param1; }
        public SimpleDbParamsModel() : base() { }
    }
    public class SimpleDbParamsModel<T, U> : SimpleDbParamsModel<T>
    {
        public U Param2 { get; private set; }
        public SimpleDbParamsModel(T param1, U param2) : base(param1) { Param2 = param2; }
        public SimpleDbParamsModel() : base() { }
    }
    public class SimpleDbParamsModel<T, U, V> : SimpleDbParamsModel<T, U>
    {
        public V Param3 { get; private set; }
        public SimpleDbParamsModel(T param1, U param2, V param3) : base(param1, param2) { Param3 = param3; }
        public SimpleDbParamsModel() : base() { }
    }
    public class SimpleDbParamsModel<T, U, V, W> : SimpleDbParamsModel<T, U, V>
    {
        public W Param4 { get; private set; }
        public SimpleDbParamsModel(T param1, U param2, V param3, W param4) : base(param1, param2, param3) { Param4 = param4; }
        public SimpleDbParamsModel() : base() { }
    }
    public class SimpleDbParamsModel<T, U, V, W, X> : SimpleDbParamsModel<T, U, V, W>
    {
        public X Param5 { get; private set; }
        public SimpleDbParamsModel(T param1, U param2, V param3, W param4, X param5) : base(param1, param2, param3, param4) { Param5 = param5; }
        public SimpleDbParamsModel() : base() { }
    }
}
