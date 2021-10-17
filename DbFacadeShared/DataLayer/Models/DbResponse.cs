using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace DbFacade.DataLayer.Models
{
    internal class DbResponseFactory<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        private static bool IsEmptyDataReturn = typeof(TDbDataModel) == typeof(DbDataModel);
        public static IDbResponse<TDbDataModel> Create(Guid commandId)
            => !IsEmptyDataReturn ?
                new DbResponse<TDbDataModel>(commandId) :
                (IDbResponse<TDbDataModel>)new DbResponse(commandId);
        public static IDbResponse<TDbDataModel> Create(Guid commandId, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => !IsEmptyDataReturn ?
                new DbResponse<TDbDataModel>(commandId, returnValue, outputValues) :
                (IDbResponse<TDbDataModel>)new DbResponse(commandId, returnValue, outputValues);
        public static async Task<IDbResponse<TDbDataModel>> CreateAsync(Guid commandId)
            => !IsEmptyDataReturn ?
                await DbResponse<TDbDataModel>.CreateAsync(commandId) :
                (IDbResponse<TDbDataModel>) await DbResponse.CreateAsync(commandId);
        public static async Task<IDbResponse<TDbDataModel>> CreateAsync(Guid commandId, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => !IsEmptyDataReturn ?
                await DbResponse<TDbDataModel>.CreateAsync(commandId, returnValue, outputValues) :
                (IDbResponse<TDbDataModel>)await DbResponse.CreateAsync(commandId, returnValue, outputValues);


    }
    internal class DbResponse : DbResponse<DbDataModel>, IDbResponse
    {
        public DbResponse(Guid commandId) :base(commandId) { }
        public DbResponse(Guid commandId, int returnValue = 0, IDictionary<string, object> outputValues = null) 
            :base(commandId, returnValue, outputValues) { }
        public static new async Task<DbResponse> CreateAsync(Guid commandId)
        {
            DbResponse response = new DbResponse(commandId);
            await response.InitializeAsync(commandId);
            return response;
        }
        public static new async Task<DbResponse> CreateAsync(Guid commandId, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            DbResponse response = new DbResponse(commandId);
            await response.InitializeAsync(commandId, returnValue, outputValues);
            return response;
        }
    }
    internal class DbResponse<TDbDataModel> : List<TDbDataModel>, IDbResponse<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        public bool HasDataBindingErrors { get=> this.Any(item=>item.HasDataBindingErrors);}
        private IDictionary<string, object> OutputValues { get; set; }
        public bool IsNull { get; private set; }
        public Guid CommandId { get; private set; }
        public DbResponse()
        {
            IsNull = true;
        }
        public DbResponse(Guid commandId)
        {
            IsNull = true;
            CommandId = commandId;
        }
        public static async Task<DbResponse<TDbDataModel>> CreateAsync(Guid commandId)
        {
            DbResponse<TDbDataModel> response = new DbResponse<TDbDataModel>();
            await response.InitializeAsync(commandId);
            return response;
        }
        public static async Task<DbResponse<TDbDataModel>> CreateAsync(Guid commandId, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            DbResponse<TDbDataModel> response = new DbResponse<TDbDataModel>();
            await response.InitializeAsync(commandId,returnValue, outputValues);
            return response;
        }
        protected async Task InitializeAsync(Guid commandId)
        {
            IsNull = true;
            CommandId = commandId;
            await Task.CompletedTask;
        }
        protected async Task InitializeAsync(Guid commandId, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            IsNull = false;
            CommandId = commandId;
            ReturnValue = returnValue;
            OutputValues = outputValues;
            await Task.CompletedTask;
        }
        public DbResponse(Guid commandId, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            ReturnValue = returnValue;
            OutputValues = outputValues;
            CommandId = commandId;
        }

        public int ReturnValue { get; private set; }
        public object GetOutputValue(string key)
        {
            object value = null;
            if (OutputValues != null && OutputValues.TryGetValue(key, out object val))
            {
                value = val;
            }
            return value;
        }
        public T GetOutputValue<T>(string key)
        => OutputDbDataModel<T>.ToDbDataModel(CommandId, OutputValues, key).Value;
        public T GetOutputModel<T>() where T: DbDataModel
        => DbDataModel.ToDbDataModel<T>(CommandId, OutputValues);

        public async Task<object> GetOutputValueAsync(string key)
        {
            object value = null;
            if (OutputValues != null && OutputValues.TryGetValue(key, out object val))
            {
                value = val;
            }
            await Task.CompletedTask;
            return value;
        }
        public async Task<T> GetOutputValueAsync<T>(string key)
        {
            var model = await OutputDbDataModel<T>.ToDbDataModelAsync(CommandId, OutputValues, key);
            return model.Value;
        }
        public async Task<T> GetOutputModelAsync<T>() where T : DbDataModel
        => await DbDataModel.ToDbDataModelAsync<T>(CommandId, OutputValues);

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void Serialize(TextWriter textWriter)
        {
            using (var xmlWriter = XmlWriter.Create(textWriter))
            {
                Serialize(xmlWriter);
            }
        }

        public void Serialize(XmlWriter xmlWriter)
        {
            var data = ToList();
            var serializer = new DataContractSerializer(data.GetType());
            serializer.WriteObject(xmlWriter, data);
        }

        int IReadOnlyDbCollection<TDbDataModel>.Count()
        {
            return Count;
        }

        public TDbDataModel First()
        {
            return this[0];
        }

        public List<TDbDataModel> ToList()
        {
            return ToArray().ToList();
        }

        public async Task<string> ToJsonAsync()
        {
            string json = ToJson();
            await Task.CompletedTask;
            return json;
        }

        public async Task SerializeAsync(TextWriter textWriter)
        {
            using (var xmlWriter = XmlWriter.Create(textWriter))
            {
                await SerializeAsync(xmlWriter);
            }            
        }

        public async Task SerializeAsync(XmlWriter xmlWriter)
        {
            var data = await ToListAsync();
            var serializer = new DataContractSerializer(data.GetType());
            serializer.WriteObject(xmlWriter, data);
            await Task.CompletedTask;
        }

        public async Task<List<TDbDataModel>> ToListAsync()
        {
            List<TDbDataModel> list = ToList();
            await Task.CompletedTask;
            return list;
        }
    }
}