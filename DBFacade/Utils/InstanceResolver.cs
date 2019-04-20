using System;
using System.Collections.Generic;

namespace DBFacade.Utils
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InstanceResolver<T>
    {
        /// <summary>
        /// The instances
        /// </summary>
        public static Dictionary<Type, T> Instances = new Dictionary<Type, T>();
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="C"></typeparam>
        /// <returns></returns>
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
