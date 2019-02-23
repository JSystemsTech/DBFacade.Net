using DomainFacade.DataLayer.Manifest;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DomainFacade.DataLayer.Models
{
        
    internal class DbResponse<DbMethod, TDbDataModel> : List<TDbDataModel>, IDbResponse<TDbDataModel>
        where TDbDataModel : DbDataModel
        where DbMethod : IDbMethod
    {
        private void AddFetchedData(DbDataReader dbReader)
        {            
            while (dbReader.Read())
            {
                Add(DbDataModel.ToDbDataModel<TDbDataModel, DbMethod>(dbReader));
            }
        }
        private object ReturnVal { get; set; }
        public DbResponse(DbDataReader dbReader, object returnValue)
        {
            AddFetchedData(dbReader);
            if(returnValue != null)
            {
                ReturnVal = returnValue;
            }
        }
        public DbResponse(object returnValue)
        {
            if (returnValue != null)
            {
                ReturnVal = returnValue;
            }
        }
        public object ReturnValue()
        {
            return ReturnVal;
        }
        public IEnumerable<TDbDataModel> Data()
        {
            return this;
        }
        public TDbDataModel Model()
        {
            if(Count == 0)
            {
                return default(TDbDataModel);
            }
            return this.First();
        }
    }
}
