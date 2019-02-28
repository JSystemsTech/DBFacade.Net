using DomainFacade.DataLayer.CommandConfig.Parameters;
using DomainFacade.DataLayer.ConnectionService;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace DomainFacade.DataLayer.CommandConfig
{
    internal abstract class DbCommandConfigCore<TDbParams, TConnection> : DbCommandConfigBase, IDbCommandConfig
        where TDbParams : IDbParamsModel
        where TConnection : DbConnectionConfig
    {
        public IDbCommandConfigParams<TDbParams> DbParams { get; private set; }
        private DbCommandConfigParams<TDbParams> DefaultConfigParams = new DbCommandConfigParams<TDbParams>() { };
        public DbCommandText<TConnection> DbCommand { get; private set; }
        public string ReturnValue { get; private set; }
        private CommandType DbCommandType { get; set; }

        public DbCommandConfigCore(DbCommandText<TConnection> dbCommand, CommandType dbCommandType)
        {
            Init(dbCommand, dbCommandType, DefaultConfigParams, null, ParamsValidatorEmpty);
        }
        public DbCommandConfigCore(DbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams)
        {
            Init(dbCommand, dbCommandType, dbParams, null, ParamsValidatorEmpty);
        }
        public DbCommandConfigCore(DbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator)
        {
            Init(dbCommand, dbCommandType, dbParams, null, validator);
        }
        public DbCommandConfigCore(DbCommandText<TConnection> dbCommand)
        {
            Init(dbCommand, CommandType.StoredProcedure, DefaultConfigParams, null, ParamsValidatorEmpty);
        }
        public DbCommandConfigCore(DbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams)
        {
            Init(dbCommand, CommandType.StoredProcedure, dbParams, null, ParamsValidatorEmpty);
        }
        public DbCommandConfigCore(DbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator)
        {
            Init(dbCommand, CommandType.StoredProcedure, dbParams, null, validator);
        }
        public DbCommandConfigCore(DbCommandText<TConnection> dbCommand, CommandType dbCommandType, string returnValue)
        {
            Init(dbCommand, dbCommandType, DefaultConfigParams, returnValue, ParamsValidatorEmpty);
        }
        public DbCommandConfigCore(DbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams, string returnValue)
        {
            Init(dbCommand, dbCommandType, dbParams, returnValue, ParamsValidatorEmpty);
        }
        public DbCommandConfigCore(DbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams, string returnValue, Validator<TDbParams> validator)
        {
            Init(dbCommand, dbCommandType, dbParams, returnValue, validator);
        }
        public DbCommandConfigCore(DbCommandText<TConnection> dbCommand, string returnValue)
        {
            Init(dbCommand, CommandType.StoredProcedure, DefaultConfigParams, returnValue, ParamsValidatorEmpty);
        }
        public DbCommandConfigCore(DbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, string returnValue)
        {
            Init(dbCommand, CommandType.StoredProcedure, dbParams, returnValue, ParamsValidatorEmpty);
        }
        public DbCommandConfigCore(DbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, string returnValue, Validator<TDbParams> validator)
        {
            Init(dbCommand, CommandType.StoredProcedure, dbParams, returnValue, validator);
        }

        private void Init(DbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams, string returnValue, Validator<TDbParams> validator)
        {
            ReturnValue = returnValue;
            DbCommand = dbCommand;
            DbCommandType = dbCommandType;
            DbParams = dbParams;
            ParamsValidator = validator;
        }
        protected override Con GetDbConnectionCore<Con>()
        {
            return (Con)DbConnectionService.GetDbConnection<TConnection>().GetDbConnection();
        }
        protected override bool HasStoredProcedureCore()
        {
            if (GetDBConnectionConfigCore().CheckStoredProcAvailability())
            {
                string[] storedProcNames = DbConnectionService.GetAvailableStoredProcedured<TConnection>().Select(sproc => sproc.Name).ToArray();
                return Array.IndexOf(storedProcNames, DbCommand.CommandText) != -1;
            }
            return true;
        }
        protected override DbConnectionConfigCore GetDBConnectionConfigCore()
        {
            return DbConnectionService.GetDbConnection<TConnection>();
        }
        protected override Cmd GetDbCommandCore<Con, Cmd, Prm>(IDbParamsModel dbMethodParams, Con dbConnection)
        {
            Cmd dbCommand = (Cmd)dbConnection.CreateCommand();
            dbCommand = AddParams<Cmd, Prm>(dbCommand, (TDbParams)dbMethodParams);
            dbCommand.CommandText = DbCommand.CommandText;
            dbCommand.CommandType = DbCommandType;
            return dbCommand;
        }
        private bool HasReturnValue()
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
                return null;
            }
        }
        protected override void SetReturnValueCore<Cmd>(Cmd dbCommand, object value)
        {
            if (HasReturnValue())
            {
                dbCommand.Parameters["@" + ReturnValue].Value = value;
            }
        }
        private Cmd AddParams<Cmd, Prm>(Cmd dbCommand, TDbParams dbMethodParams)
        where Cmd : DbCommand
        where Prm : DbParameter
        {

            foreach (KeyValuePair<string, DbCommandParameterConfig<TDbParams>> config in DbParams)
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
                return ParamsValidator.Validate((TDbParams)paramsModel);
            }
            return true;
        }

        private Validator<TDbParams> ParamsValidator { get; set; }
        private static Validator<TDbParams> ParamsValidatorEmpty = new Validator<TDbParams>();

    }
}
