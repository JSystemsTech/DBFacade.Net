using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.ConnectionService.MockDb;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Extensions;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace DbFacade.DataLayer.CommandConfig
{
    internal class DbCommandMethod : IDbCommandMethod
    {
        internal readonly EndpointSettings EndpointSettings;
        internal bool UseMockConnection { get; private set; }
        public void EnableMockMode(Action<IMockResponse> setMockData)
        {
            MockResponse.Clear();
            setMockData(MockResponse);
            UseMockConnection = true;
        }
        public void DisableMockMode() {
            MockResponse.Clear();
            UseMockConnection = false;
        }
        internal DbCommandMethod(EndpointSettings endpointSettings)
        {
            EndpointSettings = endpointSettings;
            MockResponse = new MockResponse(endpointSettings);
        }
        
        internal MockResponse MockResponse { get; private set; }
        

        internal void Validate<T>(T paramsModel)
        {
            var validationResult = EndpointSettings.Validate(paramsModel);
            if (!validationResult.isValid)
            {
                throw new ValidationException(validationResult.errors, paramsModel,
                    $"{paramsModel.TypeName()} values failed to pass validation for command '{EndpointSettings.Name}'");
            }
        }
        internal IDbConnection GetConnection()
        {
            try
            {
                return UseMockConnection ? MockDbConnection.Create(this) :
                EndpointSettings.DbConnectionProvider.GetDbConnection(EndpointSettings.ConnectionStringId);
            }
            catch (Exception ex)
            {
                ErrorHelper.ThrowUnableToCreateConnectionError(ex);
                return null;
            }
        }
        private IDbResponse MakeDbResponse(IDbCommand dbCommand, DataSet ds)
        => DbResponse.Create(new DbCommandDataCollection(dbCommand, this), dbCommand.GetReturnValue(), ds);
        internal IDbResponse MakeDbResponse(IDbCommand dbCommand, XmlReader reader = null)
        {
            DataSet ds = new DataSet($"{EndpointSettings.Name} Result DataSet");
            if (reader != null)
            {
                ds.ReadXml(reader, EndpointSettings.XmlReadMode);
            }
            else
            {
                IDbDataAdapter da = UseMockConnection ? new MockDataAdapter(MockResponse) : EndpointSettings.DbConnectionProvider.GetDbDataAdapter();
                da.SelectCommand = dbCommand;
                da.Fill(ds);
            }
            return MakeDbResponse(dbCommand, ds);
        }
        internal IDbResponse MakeDbResponse(object scalarReturnValue)
        {
            DataCollection dc = new DataCollection();
            return DbResponse.Create(dc,0,null, scalarReturnValue);
        }
        internal IDbResponse MakeDbResponse(int returnValue)
        => DbResponse.Create(DbCommandDataCollection.Empty, returnValue);

        public IDbResponse Execute<T>(T parameters)
        => DbConnectionManager.ExecuteDbAction(this, parameters);
        public IDbResponse Execute()
        => DbConnectionManager.ExecuteDbAction<object>(this,null);

        public async Task<IDbResponse> ExecuteAsync()
        => await ExecuteAsync<object>(CancellationToken.None, null);
        public async Task<IDbResponse> ExecuteAsync<T>(T parameters)
        => await ExecuteAsync(CancellationToken.None, parameters);
        public async Task<IDbResponse> ExecuteAsync<T>(CancellationToken cancellationToken, T parameters)
        => await DbConnectionManager.ExecuteDbActionAsync(this, parameters, cancellationToken);
        public async Task<IDbResponse> ExecuteAsync(CancellationToken cancellationToken)
        => await ExecuteAsync<object>(cancellationToken, null);
    }
}