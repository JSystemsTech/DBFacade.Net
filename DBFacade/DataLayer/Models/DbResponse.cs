using DBFacade.DataLayer.Manifest;
using DBFacade.Exceptions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Common;
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
        private void AddFetchedData(DbDataReader dbReader)
        {
            while (dbReader.Read())
            {
                Add(DbDataModel.ToDbDataModel<TDbDataModel, TDbManifestMethod>(dbReader));
            }
        }
        private object ReturnVal { get; set; }
        public DbResponse(DbDataReader dbReader, object returnValue = null)
        {
            AddFetchedData(dbReader);
            ReturnVal = returnValue;
        }
        public DbResponse(object returnValue = null)
        {
            ReturnVal = returnValue;
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        public JsonResult ToJsonResult()
        {
            return new JsonResult { Data = this, MaxJsonLength = int.MaxValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
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
        private FacadeException Error { get; set; }
       
        public object ReturnValue()
        {
            return ReturnVal;
        }

        /// <summary>
        /// Determines whether this instance has error.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.
        /// </returns>
        public bool HasError()
        {
            return Error != null;
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <returns></returns>
        public FacadeException GetException()
        {
            return Error;
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
    }
}
