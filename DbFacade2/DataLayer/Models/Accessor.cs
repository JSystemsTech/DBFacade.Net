using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DbFacade.DataLayer.Models
{
    internal class Accessor<T>
    {
        private static Accessor<T> Instance = new Accessor<T>();
        internal static Accessor<T> GetInstance() {
            Instance.CheckInit();
            return Instance;
        }

        private bool IsInitialized { get; set; }
        internal Type Type { get; set; }
        internal Type UnderlyingType { get; private set; }
        internal bool IsNullable { get; private set; }
        internal PropertyInfo[] Properties { get; private set; }
        internal FieldInfo[] Fields { get; private set; }
        internal ConcurrentDictionary<string, VariableReference> VariableReferences { get; private set; }
        internal IEnumerable<string> Keys { get; private set; }
        internal string Name { get; private set; }

        private Accessor() { }
        private void CheckInit()
        {
            if (!IsInitialized)
            {
                Type = typeof(T);                 
                UnderlyingType = Nullable.GetUnderlyingType(Type);
                IsNullable = Type == typeof(object) || Type == typeof(string) || UnderlyingType != null;
                Name = UnderlyingType != null ? UnderlyingType.Name : Type.Name;
                Properties = Type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                Fields = Type.GetFields(BindingFlags.Instance | BindingFlags.Public);
                VariableReferences = new ConcurrentDictionary<string, VariableReference>();
                foreach (PropertyInfo property in Properties)
                {
                    VariableReferences.TryAdd(property.Name, new VariableReference(property));
                }
                foreach (FieldInfo field in Fields)
                {
                    VariableReferences.TryAdd(field.Name, new VariableReference(field));
                }
                Keys = VariableReferences.Keys;
                IsInitialized = true;
            }
            
        }
        internal IEnumerable<string> FindKeys(Func<VariableReference, bool> predicate)
        {
            return VariableReferences.Where(kv => predicate(kv.Value)).Select(kv => kv.Key);
        }
        
        internal bool TryGetValue(T source, string name, out object value, out Type type)
        {
            if (!string.IsNullOrWhiteSpace(name) && VariableReferences.TryGetValue(name, out VariableReference varRef))
            {
                value = varRef.Get(source);
                type = varRef.VariableType;
                return true;
            }
            value = null;
            type = null;
            return true;
        }
    }
}
