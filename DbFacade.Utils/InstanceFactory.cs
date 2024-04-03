using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DbFacade.Utils
{
    internal class InstanceFactory
    {

        private static readonly ConcurrentDictionary<Type, InitInstance> InstanceBuilders = new ConcurrentDictionary<Type, InitInstance>();
        public static void RegisterInstanceBuilder<T>(InitInstance builder)
        {
            InstanceBuilders.TryAdd(typeof(T), builder);
        }
        public static bool TryGetInstanceBuilder(Type type, out InitInstance initInstance) 
            => InstanceBuilders.TryGetValue(type, out initInstance);
        
        public static T Build<T>() => Build<T>(Array.Empty<object>());
        public static T Build<T>(DataRow dr, IDataTableParser dtp) 
        where T : class,IDataCollectionModel
        {
            T model = (T)Activator.CreateInstance(typeof(T));
            IDataCollection collection = new DataCollection(((IDataTableParserInternal)dtp).CollectionParser, dr);
            model.InitDataCollection(collection);
            return model;
        }

        public static T Build<T>(IDataCollection collection)
        where T : class, IDataCollectionModel
        {
            T model = (T)Activator.CreateInstance(typeof(T));
            model.InitDataCollection(collection);
            return model;
        }
        public static T Build<T>(params object[] parameters)
        {
            if (typeof(T).TryCreateClass(parameters, out object classInstance))
            {
                return (T)classInstance;
            }
            else if(typeof(T).TryGetValueType(out object valueTypeInstance))
            {
                return (T)valueTypeInstance;
            } 
            return default(T);
        }
        public static bool TryBuild<T>() => TryBuild(out T value, Array.Empty<object>());
        public static bool TryBuild<T>(out T value, params object[] parameters)
        {
            if (typeof(T).TryCreateClass(parameters, out object classInstance))
            {
                value = (T)classInstance;
                return true;
            }
            else if (typeof(T).TryGetValueType(out object valueTypeInstance))
            {
                value = (T)valueTypeInstance;
                return true;
            }
            value =  default(T);
            return false;
        }
        
    }
    

    internal static class ParametersExtensions
    {
        private static bool IsValidInterfaceParam(this object val, ParameterInfo pi) => pi.ParameterType.IsInterface && val.GetType().IsClass && val.GetType().TryGetInterfaces(out IEnumerable<Type>interfaces) && interfaces.Contains(pi.ParameterType);
        private static bool IsValidChildClassParam(this object val, ParameterInfo pi)  => pi.ParameterType.IsClass && val.GetType().IsClass && val.GetType().IsSubclassOf(pi.ParameterType);
        private static bool IsValidValueParam(this object val, ParameterInfo pi) => pi.ParameterType == val.GetType();
        private static bool IsValidValueNullableParam(this object val, ParameterInfo pi) => Nullable.GetUnderlyingType(pi.ParameterType) == val.GetType();
        private static bool IsValidGenericObjectParam(this object val, ParameterInfo pi) => pi.ParameterType == typeof(object);
        private static bool IsValidParam(this object val, ParameterInfo pi)
            => val.IsValidGenericObjectParam(pi) ||
            val.IsValidValueParam(pi) ||
            val.IsValidValueNullableParam(pi) ||
            val.IsValidChildClassParam(pi) ||
            val.IsValidInterfaceParam(pi);

        private static bool TryTransformParameters(this ConstructorInfo ci, object[] parameters, out object[] transformed)
        {
            var constructorParams = ci.GetParameters();
            if (parameters.Length == 0 && parameters.Length == 0)
            {
                transformed = parameters;
                return true;
            }
            if (parameters.Length != constructorParams.Length)
            {
                transformed = parameters;
                return false;
            }
            bool paramsMatch = true;
            List<object> transformedParameters = new List<object>();
            for (int i = 0; i < constructorParams.Length; i++)
            {
                var val = parameters.ElementAt(i);
                var pi = constructorParams.ElementAt(i);
                if (pi.ParameterType.IsInterface &&
                    pi.ParameterType == typeof(IDataCollection) &&
                    CollectionParserFactory.TryGetCollectionParser(val.GetType(), out ICollectionParser parser)
                    ) {
                    transformedParameters.Add(new DataCollection(parser, val));
                }
                else if (parameters.ElementAt(i).IsValidParam(constructorParams.ElementAt(i)))
                {
                    transformedParameters.Add(val);
                }
                else
                {
                    paramsMatch = false;
                    break;
                }
            }
            if (paramsMatch)
            {
                transformed = transformedParameters.ToArray();
                return true;
            }
            transformed = parameters;
            return false;
        }
        private static readonly ConcurrentDictionary<Type, IEnumerable<ConstructorInfo>> ConstructorInfoMap = new ConcurrentDictionary<Type, IEnumerable<ConstructorInfo>>();
        private static readonly ConcurrentDictionary<Type, IEnumerable<Type>> InterfaceMap = new ConcurrentDictionary<Type, IEnumerable<Type>>();
        private static bool TryGetConstructorInfo(this Type classType, out IEnumerable<ConstructorInfo> constructors)
        {
            if (ConstructorInfoMap.TryGetValue(classType, out constructors))
            {
                return true;
            }
            else if (classType.IsClass)
            {
                constructors = classType.GetConstructors();
                ConstructorInfoMap.TryAdd(classType, constructors);
                return true;
            }
            else if (classType.IsValueType)
            {
                constructors = classType.GetTypeInfo().DeclaredConstructors;
                ConstructorInfoMap.TryAdd(classType, constructors);
                return true;
            }
            constructors = Array.Empty<ConstructorInfo>();
            return false;
        }
        public static bool TryGetTransformParameters(this Type classType, object[] parameters, out object[] transformed)
        {
            bool success = false;
            transformed = parameters;
            if (classType.TryGetConstructorInfo(out IEnumerable<ConstructorInfo> constructors))
            {
                
                foreach(ConstructorInfo constructor in constructors)
                {
                    if(constructor.TryTransformParameters(parameters, out object[] newArgs))
                    {
                        success = true;
                        transformed = newArgs;
                        break;
                    }

                }
            }
            return success;
        }

        private static object CreateClass(this Type classType, object[] parameters) { 
            if(parameters.Length == 0 && InstanceFactory.TryGetInstanceBuilder(classType, out var builder))
            {
                return builder();
            }
            return Activator.CreateInstance(classType, parameters);
        }

        private static bool TryGetInterfaces(this Type classType, out IEnumerable<Type> interfaces)
        {
            try
            {
                if(InterfaceMap.TryGetValue(classType,out interfaces))
                {
                    return true;
                }
                interfaces = classType.GetInterfaces();
                InterfaceMap.TryAdd(classType, interfaces);
                return true;
            }
            catch
            {
                interfaces = Array.Empty<Type>();
                return false;
            }
            
        }
        public  static bool TryCreateClass(this Type classType, object[] parameters, out object value)
        {
            if (classType.TryGetInterfaces(out IEnumerable<Type> interfaces) &&
                interfaces.Contains(typeof(IDataCollectionModel)) &&
                classType.TryGetTransformParameters(Array.Empty<object>(), out object[] newArgs) &&
                parameters.Count() > 0 &&
                CollectionParserFactory.TryGetCollectionParser(parameters.FirstOrDefault().GetType(), out ICollectionParser parser)
                )
            {
                try
                {
                    value = classType.CreateClass(newArgs);
                    if(value == null)
                    {
                        return false;
                    }
                    IDataCollection collection = new DataCollection(parser, parameters.FirstOrDefault());
                    ((IDataCollectionModel)value).InitDataCollection(collection);
                    return true;
                }
                catch
                {
                    value = null;
                    return false;
                }
            }
            else if (classType.TryGetInterfaces(out IEnumerable<Type> interfaces2) &&
                interfaces2.Contains(typeof(IDataCollectionModel)) &&
                classType.TryGetTransformParameters(Array.Empty<object>(), out object[] newArgs3) &&
                parameters.Length > 0 &&
                parameters.FirstOrDefault() is IDataCollection collection
                )
            {
                try
                {
                    value = classType.CreateClass(newArgs3);
                    if (value == null)
                    {
                        return false;
                    }
                    ((IDataCollectionModel)value).InitDataCollection(collection);
                    return true;
                }
                catch
                {
                    value = null;
                    return false;
                }
            }
            else if (classType.TryGetTransformParameters(parameters, out object[] newArgs2))
            {
                try
                {
                    value = classType.CreateClass(newArgs2);
                    return value != null;
                }
                catch
                {
                    value = null;
                    return false;
                }
            }
            value = null;
            return false;
        }
        public  static bool TryGetValueType(this Type classType, out object value)
        {
            if (classType.IsValueType)
            {
                try
                {
                    value = Activator.CreateInstance(classType);
                    return value != null;
                }
                catch
                {
                    value = null;
                    return false;
                }
            }
            value = null;
            return false;
        }
    }


    
    
}
