using DbFacade.DataLayer.ConnectionService;
using DbFacade.Exceptions;
using DbFacade.Extensions;
using System;
using System.Collections.Generic;
using System.Data;

namespace DbFacade.DataLayer.Models
{
    internal class DbResponse : IDbResponse
    {
        public int ReturnValue { get; private set; }
        public object ScalarReturnValue { get; private set; }
        private IDataCollection OutputValues { get; set; }
        public IEnumerable<IDbDataTable> DbDataTables { get; private set; }
        public DataSet DataSet { get; private set; }

        public EndpointErrorInfo ErrorInfo { get; private set; }        
        public bool HasError => ErrorInfo != null;
        internal DbResponse(EndpointErrorInfo info = null)
        {
            ErrorInfo = info;
            OutputValues = DbCommandDataCollection.Empty;
            DbDataTables = Array.Empty<IDbDataTable>();
        }
        internal DbResponse(int returnValue, IDataCollection outputValues, DataSet ds, object scalarReturnValue)
        {
            ReturnValue = returnValue;
            OutputValues = outputValues;
            DataSet = ds;
            DbDataTables = DataSet != null ? ds.ToDbDataTables() : Array.Empty<IDbDataTable>();
            ScalarReturnValue = scalarReturnValue;
        }

        
        public object GetOutputValue(string key)
        => OutputValues.GetValue(key, (object)null);
        public T GetOutputValue<T>(string key)
        => OutputValues.GetValue<T>(key);
        public T GetOutputModel<T>(Action<T, IDataCollection> initialize)
            where T : class
        => OutputValues.ToDbDataModel(initialize);
        public T GetOutputModel<T>()
            where T : class, IDbDataModel
        => OutputValues.ToDbDataModel<T>();

        internal static IDbResponse Create(IDataCollection outputValues, int returnValue = 1, DataSet ds = null, object scalarReturnValue = null)
            => new DbResponse(returnValue, outputValues, ds, scalarReturnValue);
        internal static IDbResponse Create(EndpointErrorInfo info)
            => new DbResponse(info);
    }
}