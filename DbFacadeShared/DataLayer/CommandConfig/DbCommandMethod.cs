using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Validators;
using DbFacade.Exceptions;
using DbFacade.Extensions;
using System;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.CommandConfig
{
    internal class DbCommandMethod : IDbCommandMethod
    {
        internal DbConnectionConfigBase DbConnection { get; private set; }
        internal EndpointSettings DbCommandSettings { get; private set; }

        private readonly MockResponseDataResponseBuilder MockResponseDataResponseBuilder;
        internal DbCommandMethod(DbConnectionConfigBase dbConnection, EndpointSettings dbCommand)
        {
            DbConnection = dbConnection;
            DbCommandSettings = dbCommand;
            MockResponseDataResponseBuilder = new MockResponseDataResponseBuilder();
        }
        internal MockResponseData MockData { get; private set; }
        public void SetMockData(Action<IMockResponseDataResponseBuilder> builderHandler, bool clear = false)
        {
            if (clear)
            {
                ClearMockData();
            }
            MockData = MockResponseDataResponseBuilder.Build(builderHandler);
        }
        public void ClearMockData()
        {
            if (MockData != null)
            {
                MockData = null;
            }
            MockResponseDataResponseBuilder.Clear();
        }

        internal void Validate(object paramsModel)
        {
            if(DbCommandSettings.Validator != null)
            {
                var validationResult = DbCommandSettings.Validator.Validate(paramsModel);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult, paramsModel,
                        $"{paramsModel.TypeName()} values failed to pass validation for command '{DbCommandSettings.Label}'");
                }
            }
        }

        public IDbResponse Execute(object parameters = null)
        => DbConnection.ExecuteDbAction(this, parameters);

        public async Task<IDbResponse> ExecuteAsync(object parameters = null)
        {
            var result = Execute(parameters);
            await Task.CompletedTask;
            return result;
        }
    }
}