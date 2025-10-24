using DbFacade.DataLayer.Models;
using DbFacade.Extensions;
using System;
using System.Collections.Generic;

namespace Docs2._0.docfx_project.CodeExamples
{
    #region CreateDataLayer_DefineDataModels
    public class MyDataModel : IDbDataModel
    {
        public Guid Identifier { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }
        public DateTime? UpdateDate { get; private set; }

        public void Init(IDataCollection collection)
        {
            Identifier = collection.GetValue<Guid>("Identifier");
            FirstName = collection.GetValue<string>("FirstName");
            LastName = collection.GetValue<string>("LastName");
            Age = collection.GetValue<int>("Age");
            UpdateDate = collection.GetValue<DateTime?>("UpdateDate");
        }
    }
    #endregion

    #region CreateDataLayer_DefineDataModels_LookupItem
    public class LookupItem
    {
        public Guid Identifier { get; internal set; }
        public string Name { get; internal set; }
        public string Value { get; internal set; }
    }
    #endregion

    #region CreateDataLayer_DefineDataModels_EndpointMethods
    public class EndpointMethods
    {
        private static Endpoints Endpoints = new Endpoints();
        public IDbResponse TestFetchData(MyParametersModel parameters, out IEnumerable<MyDataModel> data)
        => Endpoints.MyEndpoint.ExecuteAndFetchFirst(parameters, out data);

        public IDbResponse GetLookupListA(out IEnumerable<LookupItem> data)
        => Endpoints.LookupListA.ExecuteAndFetchFirst((model, collection) => {
            model.Identifier = collection.GetValue<Guid>("Identifier");
            model.Name = collection.GetValue<string>("LookupListA__Name");
            model.Value = collection.GetValue<string>("LookupListA__Value");
        }, out data);

        public IDbResponse GetLookupListB(out IEnumerable<LookupItem> data)
        => Endpoints.LookupListB.ExecuteAndFetchFirst((model, collection) => {
            model.Identifier = collection.GetValue<Guid>("Identifier");
            model.Name = collection.GetValue<string>("LookupListB__Name");
            model.Value = collection.GetValue<string>("LookupListB__Value");
        }, out data);
    }
    #endregion
}