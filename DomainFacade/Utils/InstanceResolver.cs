using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainFacade.Utils
{
    public class InstanceResolver<T>
    {
        public static Dictionary<Type, T> Instances = new Dictionary<Type, T>();
        public static C GetInstance<C>()
            where C : T
        {
            if (!Instances.ContainsKey(typeof(C)))
            {
                Instances.Add(typeof(C), GenericInstance<C>.GetInstance());
            }
            return (C)Instances[typeof(C)];
        }
    }
}
