using DBFacade.DataLayer.Manifest;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Threading.Tasks;

namespace DBFacade.DataLayer.Models
{
    internal class DbResponse<TDbManifestMethod, TDbDataModel> : List<TDbDataModel>, IDbResponse<TDbDataModel>
        where TDbDataModel : DbDataModel
        where TDbManifestMethod : IDbManifestMethod
    {        
        public object ReturnValue { get; private set; }
        public bool IsNull { get; private set; }
        public DbResponse() { IsNull = true; }
        public DbResponse(object returnValue = null)
        {
            ReturnValue = returnValue;
        }
        public string ToJson()=> JsonConvert.SerializeObject(this);
        public void Serialize(TextWriter textWriter)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(textWriter))
            {
                Serialize(xmlWriter);
            }
        }
        public void Serialize(XmlWriter xmlWriter)
        {
            List<TDbDataModel> data = ToList();
            DataContractSerializer serializer = new DataContractSerializer(data.GetType());
            serializer.WriteObject(xmlWriter, data);
        }
        
        int IReadOnlyDbCollection<TDbDataModel>.Count() => Count;
        public TDbDataModel First() => this[0];
        public List<TDbDataModel> ToList()=>ToArray().ToList();

        public async Task<string> ToJsonAsync() => await Task.Run(() => ToJson());

        public async Task SerializeAsync(TextWriter textWriter) => await Task.Run(() => Serialize(textWriter));

        public async Task SerializeAsync(XmlWriter xmlWriter) => await Task.Run(() => Serialize(xmlWriter));
        
        public async Task<List<TDbDataModel>> ToListAsync() => await Task.Run(() => ToList());
    }
}
