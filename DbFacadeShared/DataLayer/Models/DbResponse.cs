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
        public static IDbResponse<TDbDataModel> Create()
            => !IsEmptyDataReturn ?
                new DbResponse<TDbDataModel>() :
                (IDbResponse<TDbDataModel>)new DbResponse();
        public static IDbResponse<TDbDataModel> Create(int returnValue = 0, IDictionary<string, object> outputValues = null)
            => !IsEmptyDataReturn ?
                new DbResponse<TDbDataModel>(returnValue, outputValues) :
                (IDbResponse<TDbDataModel>)new DbResponse(returnValue, outputValues);
        public static async Task<IDbResponse<TDbDataModel>> CreateAsync()
            => !IsEmptyDataReturn ?
                await DbResponse<TDbDataModel>.CreateAsync() :
                (IDbResponse<TDbDataModel>) await DbResponse.CreateAsync();
        public static async Task<IDbResponse<TDbDataModel>> CreateAsync(int returnValue = 0, IDictionary<string, object> outputValues = null)
            => !IsEmptyDataReturn ?
                await DbResponse<TDbDataModel>.CreateAsync(returnValue, outputValues) :
                (IDbResponse<TDbDataModel>)await DbResponse.CreateAsync(returnValue, outputValues);


    }
    internal class DbResponse : DbResponse<DbDataModel>, IDbResponse
    {
        public DbResponse():base() { }
        public DbResponse(int returnValue = 0, IDictionary<string, object> outputValues = null) 
            :base(returnValue, outputValues) { }
        public static new async Task<DbResponse> CreateAsync()
        {
            DbResponse response = new DbResponse();
            await response.InitializeAsync();
            return response;
        }
        public static new async Task<DbResponse> CreateAsync(int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            DbResponse response = new DbResponse();
            await response.InitializeAsync(returnValue, outputValues);
            return response;
        }
    }
    internal class DbResponse<TDbDataModel> : List<TDbDataModel>, IDbResponse<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        public bool HasDataBindingErrors { get=> this.Any(item=>item.HasDataBindingErrors);}
        public DbResponse()
        {
            IsNull = true;
        }
        public static async Task<DbResponse<TDbDataModel>> CreateAsync()
        {
            DbResponse<TDbDataModel> response = new DbResponse<TDbDataModel>();
            await response.InitializeAsync();
            return response;
        }
        public static async Task<DbResponse<TDbDataModel>> CreateAsync(int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            DbResponse<TDbDataModel> response = new DbResponse<TDbDataModel>();
            await response.InitializeAsync(returnValue, outputValues);
            return response;
        }
        protected async Task InitializeAsync()
        {
            IsNull = true;
            await Task.CompletedTask;
        }
        protected async Task InitializeAsync(int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            IsNull = false;
            ReturnValue = returnValue;
            OutputValues = outputValues;
            await Task.CompletedTask;
        }
        public DbResponse(int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            ReturnValue = returnValue;
            OutputValues = outputValues;
        }

        public int ReturnValue { get; private set; }
        public object GetOutputValue(string key)
        {
            object value;
            if(OutputValues != null && OutputValues.TryGetValue(key, out value))
            {
                return value;
            }
            return null;
        }
        public T GetOutputValue<T>(string key)
        => GetOutputValue(key) is T value ? value : default(T);
        public IDictionary<string, object> OutputValues { get; private set; }
        public bool IsNull { get; private set; }

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