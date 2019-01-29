using System.Reflection;

namespace DomainFacade.Utils
{
    public sealed class PropertyInfoFactory<T>
    {
        public PropertyInfo GetPropertyInfo(string name)
        {
            return typeof(T).GetProperty(name);
        }
    }
}
