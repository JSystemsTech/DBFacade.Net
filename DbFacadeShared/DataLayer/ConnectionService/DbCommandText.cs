using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Validators;
using DbFacade.Services;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.ConnectionService
{
    public interface IDbCommandConfig
    {
        Guid CommandId { get; }
        IDbCommandMethod CreateMethod(Action<IDbCommandConfigParams<DbParamsModel>> parametersInitializer = null);
        Task<IDbCommandMethod> CreateMethodAsync(Func<IDbCommandConfigParams<DbParamsModel>, Task> parametersInitializer = null);

        

        IParameterlessDbCommandMethod<TDbDataModel> CreateParameterlessMethod<TDbDataModel>(Action<IDbCommandConfigParams<DbParamsModel>> parametersInitializer = null)
            where TDbDataModel : DbDataModel;
        Task<IParameterlessDbCommandMethod<TDbDataModel>> CreateParameterlessMethodAsync<TDbDataModel>(Func<IDbCommandConfigParams<DbParamsModel>, Task> parametersInitializer = null)
            where TDbDataModel : DbDataModel;

        IDbCommandMethod<TDbParams> CreateMethod<TDbParams>(
         Action<IDbCommandConfigParams<TDbParams>> parametersInitializer = null,
        Action<IValidator<TDbParams>> validatorInitializer = null)
         where TDbParams : DbParamsModel;

        IDbCommandMethod<TDbParams> CreateMethod<TDbParams>(
         Action<IDbCommandConfigParams<TDbParams>> parametersInitializer,
        Action<IValidator<TDbParams>> validatorInitializer,
        params Action<TDbParams>[] onBeforeActions)
         where TDbParams : DbParamsModel;


        IDbCommandMethod<TDbParams, TDbDataModel> CreateMethod<TDbParams, TDbDataModel>(
         Action<IDbCommandConfigParams<TDbParams>> parametersInitializer = null,
        Action<IValidator<TDbParams>> validatorInitializer = null)
         where TDbParams : DbParamsModel
            where TDbDataModel : DbDataModel;

        IDbCommandMethod<TDbParams, TDbDataModel> CreateMethod<TDbParams, TDbDataModel>(
         Action<IDbCommandConfigParams<TDbParams>> parametersInitializer,
        Action<IValidator<TDbParams>> validatorInitializer,
        params Action<TDbParams>[] onBeforeActions)
         where TDbParams : DbParamsModel
         where TDbDataModel : DbDataModel;



        Task<IDbCommandMethod<TDbParams>> CreateMethodAsync<TDbParams>(
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer = null,
        Func<IValidator<TDbParams>, Task> validatorInitializer = null)
            where TDbParams : DbParamsModel;

        Task<IDbCommandMethod<TDbParams>> CreateMethodAsync<TDbParams>(
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer,
        Func<IValidator<TDbParams>, Task> validatorInitializer,
        params Func<TDbParams, Task>[] onBeforeAsyncActions)
            where TDbParams : DbParamsModel;

        Task<IDbCommandMethod<TDbParams, TDbDataModel>> CreateMethodAsync<TDbParams, TDbDataModel>(
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer = null,
        Func<IValidator<TDbParams>, Task> validatorInitializer = null)
            where TDbParams : DbParamsModel
            where TDbDataModel : DbDataModel;

        Task<IDbCommandMethod<TDbParams, TDbDataModel>> CreateMethodAsync<TDbParams, TDbDataModel>(
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer,
        Func<IValidator<TDbParams>, Task> validatorInitializer,
        params Func<TDbParams, Task>[] onBeforeAsyncActions)
            where TDbParams : DbParamsModel
            where TDbDataModel : DbDataModel;

    }
    internal abstract class DbCommandSettingsBase{
        public Guid CommandId { get; protected set; }
        public string CommandText { get; protected set; }
        public string Label { get; protected set; }
        public CommandType CommandType { get; protected set; }
        public bool IsTransaction { get; protected set; }
        public bool RequiresValidation { get; protected set; }

    }
    internal class DbCommand<TDbConnectionConfig> : DbCommandSettingsBase, IDbCommandConfig
        where TDbConnectionConfig : DbConnectionConfigBase
    {        
        private DbCommand(
            string commandText,
            string label,
            CommandType commandType = CommandType.StoredProcedure,
            bool isTransaction = false,
            bool requiresValidation = false)
        {
            CommandId = Guid.NewGuid();
            CommandText = commandText;
            Label = label;
            CommandType = commandType;
            IsTransaction = isTransaction;
            RequiresValidation = requiresValidation;
        }
        
        public static DbCommand<TDbConnectionConfig> Create(
            string commandText,
            string label,
            CommandType commandType = CommandType.StoredProcedure,
            bool isTransaction = false,
            bool requiresValidation = false) => new DbCommand<TDbConnectionConfig>(commandText, label, commandType, isTransaction, requiresValidation);
        public static async Task<DbCommand<TDbConnectionConfig>> CreateAsync(
            string commandText,
            string label,
            CommandType commandType = CommandType.StoredProcedure,
            bool isTransaction = false,
            bool requiresValidation = false)
        {
            DbCommand<TDbConnectionConfig> command = new DbCommand<TDbConnectionConfig>(commandText, label, commandType, isTransaction, requiresValidation);
            await Task.CompletedTask;
            return command;
        }
        public IDbCommandMethod CreateMethod(Action<IDbCommandConfigParams<DbParamsModel>> parametersInitializer = null)
        => DbCommandMethod.Create<TDbConnectionConfig>(this, parametersInitializer);

        public IParameterlessDbCommandMethod<TDbDataModel> CreateParameterlessMethod<TDbDataModel>(Action<IDbCommandConfigParams<DbParamsModel>> parametersInitializer = null)
            where TDbDataModel : DbDataModel
        => ParameterlessDbCommandMethod<TDbDataModel>.Create<TDbConnectionConfig>(this, parametersInitializer);


        public IDbCommandMethod<TDbParams> CreateMethod<TDbParams>(
         Action<IDbCommandConfigParams<TDbParams>> parametersInitializer = null,
        Action<IValidator<TDbParams>> validatorInitializer = null)
         where TDbParams : DbParamsModel
        => DbCommandMethod<TDbParams>.Create<TDbConnectionConfig>(this, parametersInitializer, validatorInitializer);

        public IDbCommandMethod<TDbParams> CreateMethod<TDbParams>(
         Action<IDbCommandConfigParams<TDbParams>> parametersInitializer,
        Action<IValidator<TDbParams>> validatorInitializer,
        params Action<TDbParams>[] onBeforeActions)
         where TDbParams : DbParamsModel
        => DbCommandMethod<TDbParams>.Create<TDbConnectionConfig>(this, parametersInitializer, validatorInitializer, onBeforeActions);
                

        public IDbCommandMethod<TDbParams, TDbDataModel> CreateMethod<TDbParams, TDbDataModel>(
         Action<IDbCommandConfigParams<TDbParams>> parametersInitializer = null,
        Action<IValidator<TDbParams>> validatorInitializer = null)
         where TDbParams : DbParamsModel
            where TDbDataModel : DbDataModel
        => DbCommandMethod<TDbParams, TDbDataModel>.Create<TDbConnectionConfig>(this, parametersInitializer, validatorInitializer);

        public IDbCommandMethod<TDbParams, TDbDataModel> CreateMethod<TDbParams, TDbDataModel>(
         Action<IDbCommandConfigParams<TDbParams>> parametersInitializer,
        Action<IValidator<TDbParams>> validatorInitializer,
        params Action<TDbParams>[] onBeforeActions)
         where TDbParams : DbParamsModel
         where TDbDataModel : DbDataModel
        => DbCommandMethod<TDbParams, TDbDataModel>.Create<TDbConnectionConfig>(this, parametersInitializer, validatorInitializer, onBeforeActions);


        public async Task<IDbCommandMethod> CreateMethodAsync(Func<IDbCommandConfigParams<DbParamsModel>, Task> parametersInitializer = null)
        => await DbCommandMethod.CreateAsync<TDbConnectionConfig>(this, parametersInitializer);
        
        public async Task<IParameterlessDbCommandMethod<TDbDataModel>> CreateParameterlessMethodAsync<TDbDataModel>(Func<IDbCommandConfigParams<DbParamsModel>, Task> parametersInitializer = null)
            where TDbDataModel : DbDataModel
        => await ParameterlessDbCommandMethod<TDbDataModel>.CreateAsync<TDbConnectionConfig>(this, parametersInitializer);
        public async Task<IDbCommandMethod<TDbParams>> CreateMethodAsync<TDbParams>(
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer = null,
        Func<IValidator<TDbParams>, Task> validatorInitializer = null)
            where TDbParams : DbParamsModel
        => await DbCommandMethod<TDbParams>.CreateAsync<TDbConnectionConfig>(this, parametersInitializer, validatorInitializer);

        public async Task<IDbCommandMethod<TDbParams>> CreateMethodAsync<TDbParams>(
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer,
        Func<IValidator<TDbParams>, Task> validatorInitializer,
        params Func<TDbParams,Task>[] onBeforeAsyncActions)
            where TDbParams : DbParamsModel
        => await DbCommandMethod<TDbParams>.CreateAsync<TDbConnectionConfig>(this, parametersInitializer, validatorInitializer, onBeforeAsyncActions);

        public async Task<IDbCommandMethod<TDbParams, TDbDataModel>> CreateMethodAsync<TDbParams, TDbDataModel>(
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer = null,
        Func<IValidator<TDbParams>, Task> validatorInitializer = null)
            where TDbParams : DbParamsModel
            where TDbDataModel : DbDataModel
        => await DbCommandMethod<TDbParams, TDbDataModel>.CreateAsync<TDbConnectionConfig>(this, parametersInitializer, validatorInitializer);

        public async Task<IDbCommandMethod<TDbParams, TDbDataModel>> CreateMethodAsync<TDbParams, TDbDataModel>(
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer,
        Func<IValidator<TDbParams>, Task> validatorInitializer,
        params Func<TDbParams, Task>[] onBeforeAsyncActions)
            where TDbParams : DbParamsModel
            where TDbDataModel : DbDataModel
        => await DbCommandMethod<TDbParams, TDbDataModel>.CreateAsync<TDbConnectionConfig>(this, parametersInitializer, validatorInitializer, onBeforeAsyncActions);

    }
}