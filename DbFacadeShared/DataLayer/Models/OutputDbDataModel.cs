using DbFacade.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models
{
    internal sealed class OutputDbDataModel<T> : DbDataModel
    {
        public T Value { get; private set; }
        private string Name { get; set; }
        internal static OutputDbDataModel<T> ToDbDataModel(Guid commandId, IDictionary<string, object> data, string name)
        {
            var model = GenericInstance.GetInstance<OutputDbDataModel<T>>();
            model.Data = data;
            model.CommandId = commandId;
            model.Name = name;
            model.Init();
            return model;
        }
        internal static async Task<OutputDbDataModel<T>> ToDbDataModelAsync(Guid commandId, IDictionary<string, object> data, string name)
        {
            var model = await GenericInstance.GetInstanceAsync<OutputDbDataModel<T>>();
            model.Data = data;
            model.CommandId = commandId;
            model.Name = name;
            await model.InitAsync();
            return model;
        }
        protected sealed override void Init() {
            Value = GetColumn<T>(Name);
        }
        protected sealed override async Task InitAsync() {
            Value = await GetColumnAsync<T>(Name);
        }

    }
}
