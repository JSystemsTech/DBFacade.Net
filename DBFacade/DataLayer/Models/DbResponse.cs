using DBFacade.DataLayer.Manifest;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;

namespace DBFacade.DataLayer.Models
{
    internal class DbResponse<TDbManifestMethod, TDbDataModel> : List<TDbDataModel>, IDbResponse<TDbDataModel>
        where TDbDataModel : DbDataModel
        where TDbManifestMethod : IDbManifestMethod
    {        
        private object ReturnVal { get; set; }
        private bool ObjectIsNull { get; set; }
        public DbResponse() { ObjectIsNull = true; }
        public DbResponse(object returnValue = null)
        {            
            ReturnVal = returnValue;
        }
        public string ToJson()=> JsonConvert.SerializeObject(this);
        
        public JsonResult ToJsonResult() => new JsonResult { Data = this, MaxJsonLength = int.MaxValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        
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
        public static IDbResponse<TDbDataModel> Deserialize(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DbResponse<TDbManifestMethod, TDbDataModel>));
            return (DbResponse<TDbManifestMethod, TDbDataModel>)serializer.Deserialize(stream);
        }

        public object ReturnValue() => ReturnVal;
        int IReadOnlyDbCollection<TDbDataModel>.Count() => Count;
        public TDbDataModel First() => this[0];
        public List<TDbDataModel> ToList()=>ToArray().ToList();     

        public bool IsNull() => ObjectIsNull;
    }
}
