using DbFacade.DataLayer.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbFacade.DataLayer.Models
{
    public sealed class ParameterDataCollection : IDataCollection
    {

        internal readonly Dictionary<string, ParameterInfo> Collection;
        private ParameterDataCollection()
        {
            Collection = new Dictionary<string, ParameterInfo>();
            Keys = Array.Empty<string>();
        }
        internal static ParameterDataCollection Create(Action<ParameterDataCollection> resolver)
        {
            ParameterDataCollection collection = new ParameterDataCollection();
            resolver(collection);
            return collection;
        }
        internal static ParameterDataCollection Create()
        => Create(pc => { });
        public string[] Keys { get; private set; }
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
                return Collection.TryGetValue(name, out ParameterInfo pi) ? pi.Value : null;
            }
        }
        private static Type GetType(object value) => value == null ? typeof(object) : value.GetType();
        private void Add(string columnName, object value, Type type, ParameterDirection direction, int? size = null)
        {
            string name = GetName(columnName);
            Collection[name] = new ParameterInfo(name, value, type, direction, size);
            Keys = Collection.Keys.ToArray();
        }
        public void AddInput(string columnName, object value)
        => Add(columnName, value, GetType(value), ParameterDirection.Input);
        public void AddInput<TModel>(string columnName, TModel data, Func<TModel, object> getter) where TModel : class
        {
            object value = getter(data);
            Add(columnName, value, GetType(value), ParameterDirection.Input);
        }

        public void AddOutput<TCol>(string columnName, int? size = null)
        => Add(columnName, default(TCol), typeof(TCol), ParameterDirection.Output, size);

        public void AddInputOutput(string columnName, object value, int? size = null)
        => Add(columnName, value, GetType(value), ParameterDirection.InputOutput, size);
        public void AddInputOutput<TModel>(string columnName, TModel data, Func<TModel, object> getter, int? size = null) where TModel : class
        {
            object value = getter(data);
            Add(columnName, value, GetType(value), ParameterDirection.InputOutput, size);
        }

        private object GetValueByIndex(int columnIndex)
            => Keys.Length == 0 || columnIndex < 0 || columnIndex >= Keys.Length ? (object)null :
            Collection[Keys[columnIndex]].Value;
        private string GetName(string columnName)
        {
            string name = Collection.Keys.FirstOrDefault(m => m.Equals(columnName, StringComparison.OrdinalIgnoreCase));
            return name ?? columnName;
        }
    }
    public sealed class ParameterDataCollection<TModel>
        where TModel : class
    {
        internal readonly ParameterDataCollection Collection;
        private readonly TModel Data;
        internal ParameterDataCollection(TModel data)
        {
            Collection = ParameterDataCollection.Create();
            Data = data;
        }
        public void AddInput(string columnName, object value)
        => Collection.AddInput(columnName, value);
        public void AddInput(string columnName, Func<TModel, object> getter)
        => Collection.AddInput(columnName, Data, getter);
        public void AddOutput<TCol>(string columnName, int? size = null)
        => Collection.AddOutput<TCol>(columnName, size);
        public void AddInputOutput(string columnName, object value, int? size = null)
        => Collection.AddInputOutput(columnName, value, size);
        public void AddInputOutput(string columnName, Func<TModel, object> getter, int? size = null)
        => Collection.AddInputOutput(columnName, Data, getter, size);


        public void BindInput<TAttribute>()
            where TAttribute : Attribute
        {
            var cols = Accessor<TModel>.GetInstance().FindKeys(vr => vr.TryGetAttribute(out TAttribute attr));
            foreach (var columnName in cols)
            {
                if (Accessor<TModel>.GetInstance().TryGetValue(Data, columnName, out object value))
                {
                    Collection.AddInput(columnName, value);
                }
            }
        }
        public void BindInput()
        {
            foreach (var columnName in Accessor<TModel>.GetInstance().Keys)
            {
                if (Accessor<TModel>.GetInstance().TryGetValue(Data, columnName, out object value))
                {
                    Collection.AddInput(columnName, value);
                }
            }
        }
    }
}
