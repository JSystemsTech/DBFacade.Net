using DbFacade.DataLayer.ConnectionService;
using DbFacade.Utils;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal sealed class OutputDbDataModel<T> : DbDataModel
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value { get; private set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        private string Name { get; set; }
        /// <summary>
        /// Converts to dbdatamodel.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="data">The data.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        internal static OutputDbDataModel<T> ToDbDataModel(IDbCommandSettings dbCommandSettings, IDictionary<string, object> data, string name)
        {
            var model = GenericInstance.GetInstance<OutputDbDataModel<T>>();
            model.Data = data;
            model.DbCommandSettings = dbCommandSettings;
            model.Name = name;
            model.Init();
            return model;
        }
        /// <summary>
        /// Converts to dbdatamodelasync.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="data">The data.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        internal static async Task<OutputDbDataModel<T>> ToDbDataModelAsync(IDbCommandSettings dbCommandSettings, IDictionary<string, object> data, string name)
        {
            var model = await GenericInstance.GetInstanceAsync<OutputDbDataModel<T>>();
            model.Data = data;
            model.DbCommandSettings = dbCommandSettings;
            model.Name = name;
            await model.InitAsync();
            return model;
        }
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected sealed override void Init()
        {
            Value = GetColumn<T>(Name);
        }
        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        protected sealed override async Task InitAsync()
        {
            Value = await GetColumnAsync<T>(Name);
        }

    }
}
