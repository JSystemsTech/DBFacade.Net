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
        /// <summary>Gets the keys.</summary>
        /// <value>The keys.</value>
        public string[] Keys { get; private set; }
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
                return Collection.TryGetValue(name, out ParameterInfo pi) ? pi.Value : null;
            }
        }
        private void Add(string columnName, object value, Type type, ParameterDirection direction, int? size = null)
        {
            string name = GetName(columnName);
            Collection[name] = new ParameterInfo(name, value, type, direction, size);
            Keys = Collection.Keys.ToArray();
        }
        /// <summary>Adds the input.</summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="value">The value.</param>
        public void AddInput<TCol>(string columnName, TCol value)
        => Add(columnName, value, typeof(TCol), ParameterDirection.Input);

        internal void AddInput(string columnName, object value, Type type)
        => Add(columnName, value, type, ParameterDirection.Input);

        /// <summary>Adds the input.</summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TCol">The type of the col.</typeparam>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="data">The data.</param>
        /// <param name="getter">The getter.</param>
        public void AddInput<TModel,TCol>(string columnName, TModel data, Func<TModel, TCol> getter)
        {
            TCol value = getter(data);
            Add(columnName, value, typeof(TCol), ParameterDirection.Input);
        }

        /// <summary>Adds the output.</summary>
        /// <typeparam name="TCol">The type of the col.</typeparam>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="size">The size.</param>
        public void AddOutput<TCol>(string columnName, int? size = null)
        => Add(columnName, default(TCol), typeof(TCol), ParameterDirection.Output, size);

        /// <summary>Adds the input output.</summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="value">The value.</param>
        /// <param name="size">The size.</param>
        public void AddInputOutput<TCol>(string columnName, TCol value, int? size = null)
        => Add(columnName, value, typeof(TCol), ParameterDirection.InputOutput, size);
        
        /// <summary>Adds the input output.</summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TCol">The type of the col.</typeparam>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="data">The data.</param>
        /// <param name="getter">The getter.</param>
        /// <param name="size">The size.</param>
        public void AddInputOutput<TModel,TCol>(string columnName, TModel data, Func<TModel, TCol> getter, int? size = null)
        {
            TCol value = getter(data);
            Add(columnName, value, typeof(TCol), ParameterDirection.InputOutput, size);
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
    /// <summary>
    ///   <br />
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public sealed class ParameterDataCollection<TModel>
    {
        internal readonly ParameterDataCollection Collection;
        private readonly TModel Data;
        internal ParameterDataCollection(TModel data)
        {
            Collection = ParameterDataCollection.Create();
            Data = data;
        }
        /// <summary>Adds the input.</summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="value">The value.</param>
        public void AddInput<TCol>(string columnName, TCol value)
        => Collection.AddInput(columnName, value);
        
        /// <summary>Adds the input.</summary>
        /// <typeparam name="TCol">The type of the col.</typeparam>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="getter">The getter.</param>
        public void AddInput<TCol>(string columnName, Func<TModel, TCol> getter)
        => Collection.AddInput(columnName,Data, getter);


        /// <summary>Adds the output.</summary>
        /// <typeparam name="TCol">The type of the col.</typeparam>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="size">The size.</param>
        public void AddOutput<TCol>(string columnName, int? size = null)
        => Collection.AddOutput<TCol>(columnName, size);
        /// <summary>Adds the input output.</summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="value">The value.</param>
        /// <param name="size">The size.</param>
        public void AddInputOutput<TCol>(string columnName, TCol value, int? size = null)
        => Collection.AddInputOutput<TCol>(columnName, value, size);
        /// <summary>Adds the input output.</summary>
        /// <typeparam name="TCol">The type of the col.</typeparam>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="getter">The getter.</param>
        /// <param name="size">The size.</param>
        public void AddInputOutput<TCol>(string columnName, Func<TModel, TCol> getter, int? size = null)
        => Collection.AddInputOutput(columnName, Data, getter, size);

        /// <summary>Binds the input.</summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        public void BindInput<TAttribute>()
            where TAttribute : Attribute
        {
            var cols = Accessor<TModel>.GetInstance().FindKeys(vr => vr.TryGetAttribute(out TAttribute attr));
            foreach (var columnName in cols)
            {
                if (Accessor<TModel>.GetInstance().TryGetValue(Data, columnName, out object value, out Type type))
                {
                    Collection.AddInput(columnName, value, type);
                }
            }
        }
        /// <summary>Binds the input.</summary>
        public void BindInput()
        {
            foreach (var columnName in Accessor<TModel>.GetInstance().Keys)
            {
                if (Accessor<TModel>.GetInstance().TryGetValue(Data, columnName, out object value, out Type type))
                {
                    Collection.AddInput(columnName, value, type);
                }
            }
        }
    }

   
}
