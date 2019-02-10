using DomainFacade.DataLayer.CommandConfig.Parameters;
using DomainFacade.DataLayer.ConnectionService;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace DomainFacade.DataLayer.CommandConfig
{
    internal abstract class DbCommandConfigCore<T, C, TDbMethod> : DbCommandConfigBase, IDbCommandConfig
        where T : IDbParamsModel
        where C : DbConnectionCore
        where TDbMethod : DbMethodCallType
    {
        public IDbCommandConfigParams<T> DbParams { get; private set; }
        private DbCommandConfigParams<T> DefaultConfigParams = new DbCommandConfigParams<T>() { };
        public DbCommandText<C> DbCommand { get; private set; }
        public string ReturnValue { get; private set; }
        private CommandType DbCommandType { get; set; }
        public override Type GetDbMethodCallType() { return typeof(TDbMethod); }

        public DbCommandConfigCore(DbCommandText<C> dbCommand, CommandType dbCommandType)
        {
            Init(dbCommand, dbCommandType, DefaultConfigParams, null, ParamsValidatorEmpty);
        }
        public DbCommandConfigCore(DbCommandText<C> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<T> dbParams)
        {
            Init(dbCommand, dbCommandType, dbParams, null, ParamsValidatorEmpty);
        }
        public DbCommandConfigCore(DbCommandText<C> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<T> dbParams, Validator<T> validator)
        {
            Init(dbCommand, dbCommandType, dbParams, null, validator);
        }
        public DbCommandConfigCore(DbCommandText<C> dbCommand)
        {
            Init(dbCommand, CommandType.StoredProcedure, DefaultConfigParams, null, ParamsValidatorEmpty);
        }
        public DbCommandConfigCore(DbCommandText<C> dbCommand, IDbCommandConfigParams<T> dbParams)
        {
            Init(dbCommand, CommandType.StoredProcedure, dbParams, null, ParamsValidatorEmpty);
        }
        public DbCommandConfigCore(DbCommandText<C> dbCommand, IDbCommandConfigParams<T> dbParams, Validator<T> validator)
        {
            Init(dbCommand, CommandType.StoredProcedure, dbParams, null, validator);
        }
        public DbCommandConfigCore(DbCommandText<C> dbCommand, CommandType dbCommandType, string returnValue)
        {
            Init(dbCommand, dbCommandType, DefaultConfigParams, returnValue, ParamsValidatorEmpty);
        }
        public DbCommandConfigCore(DbCommandText<C> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<T> dbParams, string returnValue)
        {
            Init(dbCommand, dbCommandType, dbParams, returnValue, ParamsValidatorEmpty);
        }
        public DbCommandConfigCore(DbCommandText<C> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<T> dbParams, string returnValue, Validator<T> validator)
        {
            Init(dbCommand, dbCommandType, dbParams, returnValue, validator);
        }
        public DbCommandConfigCore(DbCommandText<C> dbCommand, string returnValue)
        {
            Init(dbCommand, CommandType.StoredProcedure, DefaultConfigParams, returnValue, ParamsValidatorEmpty);
        }
        public DbCommandConfigCore(DbCommandText<C> dbCommand, IDbCommandConfigParams<T> dbParams, string returnValue)
        {
            Init(dbCommand, CommandType.StoredProcedure, dbParams, returnValue, ParamsValidatorEmpty);
        }
        public DbCommandConfigCore(DbCommandText<C> dbCommand, IDbCommandConfigParams<T> dbParams, string returnValue, Validator<T> validator)
        {
            Init(dbCommand, CommandType.StoredProcedure, dbParams, returnValue, validator);
        }

        private void Init(DbCommandText<C> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<T> dbParams, string returnValue, Validator<T> validator)
        {
            ReturnValue = returnValue;
            DbCommand = dbCommand;
            DbCommandType = dbCommandType;
            DbParams = dbParams;
            ParamsValidator = validator;
        }
        protected override Con GetDbConnectionCore<Con>()
        {
            return (Con)DbConnectionService.GetDbConnection<C>().GetDbConnection();
        }
        protected override bool HasStoredProcedureCore()
        {
            C Connection = DbConnectionService.GetDbConnection<C>();
            if (Connection.CheckStoredProcAvailability())
            {
                string[] storedProcNames = DbConnectionService.GetAvailableStoredProcedured<C>().Select(sproc => sproc.Name).ToArray();
                return Array.IndexOf(storedProcNames, DbCommand.CommandText) != -1;
            }
            return true;
        }
        protected override Type GetDBConnectionTypeCore()
        {
            return typeof(C);
        }
        protected override Cmd GetDbCommandCore<Con, Cmd, Prm>(IDbParamsModel dbMethodParams, Con dbConnection)
        {
            Cmd dbCommand = (Cmd)dbConnection.CreateCommand();
            dbCommand = AddParams<Cmd, Prm>(dbCommand, (T)dbMethodParams);
            dbCommand.CommandText = DbCommand.CommandText;
            dbCommand.CommandType = DbCommandType;
            return dbCommand;
        }
        public bool HasReturnValue()
        {
            return !string.IsNullOrEmpty(ReturnValue);
        }

        protected override object GetReturnValueCore<Cmd>(Cmd dbCommand)
        {
            if (HasReturnValue())
            {
                return dbCommand.Parameters["@" + ReturnValue].Value;
            }
            else
            {
                throw new Exception("no return value specified");
            }
        }
        private Cmd AddParams<Cmd, Prm>(Cmd dbCommand, T dbMethodParams)
        where Cmd : DbCommand
        where Prm : DbParameter
        {

            foreach (KeyValuePair<string, DbCommandParameterConfig<T>> config in DbParams)
            {
                Prm dbParameter = (Prm)dbCommand.CreateParameter();

                string paramKey = "@" + config.Key;

                dbParameter.DbType = config.Value.DBType;
                dbParameter.Direction = ParameterDirection.Input;
                dbParameter.IsNullable = config.Value.IsNullable();
                dbParameter.ParameterName = paramKey;
                dbParameter.Value = config.Value.GetParam(dbMethodParams);
                dbCommand.Parameters.Add(dbParameter);

            }
            if (HasReturnValue())
            {
                Prm dbParameter = (Prm)dbCommand.CreateParameter();
                dbParameter.Direction = ParameterDirection.ReturnValue;
                dbParameter.ParameterName = "@" + ReturnValue;
                dbCommand.Parameters.Add(dbParameter);
            }
            return dbCommand;
        }

        protected override bool ValidateCore(IDbParamsModel paramsModel)
        {
            if (DbParams != null && DbParams.ParamsCount() > 0)
            {
                return ParamsValidator.Validate((T)paramsModel);
            }
            return true;
        }
        private Validator<T> ParamsValidator { get; set; }
        private static Validator<T> ParamsValidatorEmpty = new Validator<T>();

    }
}
