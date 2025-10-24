using DbFacade.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;

namespace DbFacade.DataLayer.Models
{
    internal class DbResponse : IDbResponse
    {
        private IDbDataCollection OutputValues { get; set; }
        public IEnumerable<IDbDataSet> DataSets { get; private set; }
        public DataSet DataSet { get; private set; }

        public Exception Error { get; private set; }        
        public bool HasError => Error != null;
        public string ErrorMessage => HasError ? Error.Message : "";
        public string ErrorDetails => HasError && Error is FacadeException ex ? ex.ErrorDetails : "";
        internal DbResponse(Exception error = null)
        {
            Error = error;
            OutputValues = DbDataCollection.Empty;
        }
        internal DbResponse(int returnValue, IDictionary<string, object> outputValues, DataSet ds)
        {
            ReturnValue = returnValue;
            OutputValues = DbFacade.Utils.Utils.MakeInstance<DbDataCollection>(outputValues);
            DataSet = ds;
            DataSets = DataSet != null ? DbDataSet.CreateDataSets(ds) : Array.Empty<IDbDataSet>();
        }

        public int ReturnValue { get; private set; }
        public object GetOutputValue(string key)
        => OutputValues.GetColumn(key, (object)null);
        public T GetOutputValue<T>(string key)
        => OutputValues.GetColumn<T>(key);
        public T GetOutputModel<T>(Action<T, IDbDataCollection> initialize)
            where T : class
        => OutputValues.ToDbDataModel(initialize);
        public T GetOutputModel<T>()
            where T : class, IDbDataModel
        => OutputValues.ToDbDataModel<T>();

        internal static IDbResponse Create(int returnValue, IDictionary<string, object> outputValues, DataSet ds)
            => new DbResponse(returnValue, outputValues, ds);
        internal static IDbResponse Create(Exception error)
            => new DbResponse(error);
    }
}