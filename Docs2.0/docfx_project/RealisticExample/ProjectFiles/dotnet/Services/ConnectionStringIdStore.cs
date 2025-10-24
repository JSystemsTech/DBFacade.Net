using System.Collections.Generic;
using System.Dynamic;

namespace RealisticExampleProject.Services
{
    internal sealed class ConnectionStringIdStore : DynamicObject
    {
        private readonly Dictionary<string, object> _properties;
        public ConnectionStringIdStore(string[] connectionStringIds)
        {
            _properties = new Dictionary<string, object>();
            foreach (string name in connectionStringIds)
            {
                _properties.Add(name, name);
            }
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _properties.Keys;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_properties.ContainsKey(binder.Name))
            {
                result = _properties[binder.Name];
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }
        //Do not allow values to be set outside constructor
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            return false;
        }
    }
}