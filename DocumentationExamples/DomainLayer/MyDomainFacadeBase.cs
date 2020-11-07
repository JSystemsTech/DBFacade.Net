using DbFacade.Facade.Core;
using DBFacade.DataLayer.Models;
using DBFacade.Exceptions;
using DBFacade.Facade;
using DocumentationExamples.DataLayer.Connection;
using DocumentationExamples.DataLayer.Methods;
using DocumentationExamples.Models.Data;
using System;

namespace DocumentationExamples.DomainLayer
{
    internal partial class MyDomainFacadeBase : DomainFacade<MyMethods>
    {
        public MyDomainFacadeBase(string connectionString, string connectionProvider)
        {
            /* Initialize instance of your db connection */
            InitConnectionConfig(new MySqlDbConnection(connectionString, connectionProvider));
        }
        /*Define Handlers */
        protected IFetch<MyDataModel> GetDataHandler { get => BuildFetch<MyDataModel, MyMethods.GetData>(); }
        protected IFetch<MyDataModel> GetDataWithOutputParametersHandler { get => BuildFetch<MyDataModel, MyMethods.GetDataWithOutputParameters>(); }
        protected IGenericFetch<MyDataModel, string, int> GetDataWithParametersHandler { get => BuildGenericFetch<MyDataModel, MyMethods.GetDataWithParameters, string, int>(); }
        protected IGenericTransaction<string> MyTransactionHandler { get => BuildGenericTransaction<MyMethods.GetDataWithParameters, string>(); }
    }
}
