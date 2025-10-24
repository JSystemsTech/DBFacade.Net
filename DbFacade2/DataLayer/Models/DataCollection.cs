using System;
using System.Collections.Generic;
using System.Linq;

namespace DbFacade.DataLayer.Models
{
    /// <summary>
    ///   <br />
    /// </summary>
    public sealed class DataCollection : IDataCollection
    {
        internal static IDataCollection Empty = new DataCollection();
        /// <summary>Gets the keys.</summary>
        /// <value>The keys.</value>
        public string[] Keys { get; private set; }
        internal readonly Dictionary<string, object> Collection;

        /// <summary>Gets the <see cref="System.Object" /> with the specified column index.</summary>
        /// <param name="columnIndex">Index of the column.</param>
        /// <value>The <see cref="System.Object" />.</value>
        /// <returns>
        ///   <br />
        /// </returns>
        public object this[int columnIndex]
        {
            get
            {
                return GetValueByIndex(columnIndex);
            }
        }
        /// <summary>Gets the <see cref="System.Object" /> with the specified column name.</summary>
        /// <param name="columnName">Name of the column.</param>
        /// <value>The <see cref="System.Object" />.</value>
        /// <returns>
        ///   <br />
        /// </returns>
        public object this[string columnName]
        {
            get
            {
                string name = GetName(columnName);
                return Collection.TryGetValue(name, out object value) ? value : null;
            }
            set
            {
                string name = GetName(columnName);
                Collection[columnName] = value;
                Keys = Collection.Keys.ToArray();
            }
        }
        internal void Clear()
        {
            Collection.Clear();
            Keys = Collection.Keys.ToArray();
        }
        internal DataCollection()
        {
            Collection = new Dictionary<string, object>();
            Keys = Array.Empty<string>();
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
