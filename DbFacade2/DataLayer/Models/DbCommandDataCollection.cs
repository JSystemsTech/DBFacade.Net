using DbFacade.DataLayer.CommandConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbFacade.DataLayer.Models
{
    internal sealed class DbCommandDataCollection : IDataCollection
    {
        internal static DbCommandDataCollection Empty = new DbCommandDataCollection();
        public string[] Keys { get; private set; }
        internal readonly Dictionary<string, object> Collection;

        public object this[int columnIndex]
        {
            get
            {
                return GetValueByIndex(columnIndex);
            }
        }
        public object this[string columnName]
        {
            get
            {
                string name = GetName(columnName);
                return Collection.TryGetValue(name, out object value) ? value : null;
            }
        }
        private DbCommandDataCollection()
        {
            Collection = new Dictionary<string, object>();
            Keys = Array.Empty<string>();
        }
        internal DbCommandDataCollection(IDbCommand command, DbCommandMethod config)
        {
            Collection = new Dictionary<string, object>();

            foreach (IDbDataParameter parameter in command.Parameters)
            {
                if (parameter.Direction == ParameterDirection.Output || parameter.Direction == ParameterDirection.InputOutput)
                {
                    string name = config.EndpointSettings.DbConnectionProvider.ResolveParameterName(parameter.ParameterName, false);
                    object value = parameter.Value == DBNull.Value ? null : parameter.Value;
                    Collection[name] = value;
                }
            }
            Keys = Collection.Keys.ToArray();
        }

        private object GetValueByIndex(int columnIndex)
            => Keys.Length == 0 || columnIndex < 0 || columnIndex >= Keys.Length ? (object)null :
            Collection[Keys[columnIndex]];
        private string GetName(string columnName)
        {
            string name = Collection.Keys.FirstOrDefault(m => m.Equals(columnName, StringComparison.OrdinalIgnoreCase));
            return name ?? columnName;
        }
    }
}
