using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using DBFacade.DataLayer.Manifest;
using Newtonsoft.Json;

namespace DBFacade.DataLayer.Models
{
    internal class DbResponse<TDbMethodManifestMethod, TDbDataModel> : List<TDbDataModel>, IDbResponse<TDbDataModel>
        where TDbDataModel : DbDataModel
        where TDbMethodManifestMethod : IDbManifestMethod
    {
        public bool HasDataBindingErrors { get=> this.Any(item=>item.HasDataBindingErrors);}
        public DbResponse()
        {
            IsNull = true;
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
            return await Task.Run(() => ToJson());
        }

        public async Task SerializeAsync(TextWriter textWriter)
        {
            await Task.Run(() => Serialize(textWriter));
        }

        public async Task SerializeAsync(XmlWriter xmlWriter)
        {
            await Task.Run(() => Serialize(xmlWriter));
        }

        public async Task<List<TDbDataModel>> ToListAsync()
        {
            return await Task.Run(() => ToList());
        }
    }
}