using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Parameters;
using DbFacade.Exceptions;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace DbFacade.Extensions
{
    public static class EndpointSettingsExtensions
    {
        /// <summary>Ases the stored procedure.</summary>
        /// <param name="settings">The settings.</param>
        /// <param name="commandText">The command text.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings AsStoredProcedure(this EndpointSettings settings, string commandText)
        {
            settings.CommandType = CommandType.StoredProcedure;
            settings.ExecutionMethod = ExecutionMethod.Query;
            settings.UseReturnValue = true;
            settings.AddCommandText(m => settings.Schema != null ? settings.Schema.BuildCommandText(commandText) : commandText);            
            return settings;
        }

        /// <summary>Ases the query.</summary>
        /// <param name="settings">The settings.</param>
        /// <param name="queries">The queries.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings AsQuery(this EndpointSettings settings, params string[] queries)
        {
            settings.ExecutionMethod = ExecutionMethod.Query;
            settings.CommandType = CommandType.Text;
            settings.UseReturnValue = false;
            settings.AddCommandText(queries);
            return settings;
        }

        /// <summary>Ases the query.</summary>
        /// <param name="settings">The settings.</param>
        /// <param name="queryBuilders">The query builders.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings AsQuery(this EndpointSettings settings, params Func<object,string>[] queryBuilders)
        {
            settings.ExecutionMethod = ExecutionMethod.Query;
            settings.CommandType = CommandType.Text;
            settings.AddCommandText(queryBuilders);
            settings.UseReturnValue = false;
            return settings;
        }

        /// <summary>Ases the non query.</summary>
        /// <param name="settings">The settings.</param>
        /// <param name="queries">The queries.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings AsNonQuery(this EndpointSettings settings, params string[] queries)
        {
            settings.ExecutionMethod = ExecutionMethod.NonQuery;
            settings.CommandType = CommandType.Text;
            settings.UseReturnValue = false;
            settings.AddCommandText(queries);
            return settings;
        }

        /// <summary>Ases the non query.</summary>
        /// <param name="settings">The settings.</param>
        /// <param name="queryBuilders">The query builders.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings AsNonQuery(this EndpointSettings settings, params Func<object, string>[] queryBuilders)
        {
            settings.ExecutionMethod = ExecutionMethod.NonQuery;
            settings.CommandType = CommandType.Text;
            settings.AddCommandText(queryBuilders);
            settings.UseReturnValue = false;
            return settings;
        }

        /// <summary>Ases the table direct.</summary>
        /// <param name="settings">The settings.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings AsTableDirect(this EndpointSettings settings, string tableName)
        {
            settings.ExecutionMethod = ExecutionMethod.Query;
            settings.CommandType = CommandType.TableDirect;            
            settings.AddCommandText(tableName);
            settings.UseReturnValue = false;
            return settings;
        }

        /// <summary>Ases the scalar.</summary>
        /// <param name="settings">The settings.</param>
        /// <param name="text">The text.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings AsScalar(this EndpointSettings settings, string text)
        {
            settings.ExecutionMethod = ExecutionMethod.Scalar;
            settings.CommandType = CommandType.Text;
            settings.AddCommandText(text);
            settings.UseReturnValue = false;
            return settings;
        }


        /// <summary>Ases the scalar.</summary>
        /// <param name="settings">The settings.</param>
        /// <param name="queryBuilder">The query builder.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings AsScalar(this EndpointSettings settings, Func<object, string> queryBuilder)
        {
            settings.ExecutionMethod = ExecutionMethod.Scalar;
            settings.CommandType = CommandType.Text;
            settings.AddCommandText(queryBuilder);
            settings.UseReturnValue = false;
            return settings;
        }

        /// <summary>Ases the XML.</summary>
        /// <param name="settings">The settings.</param>
        /// <param name="query">The query.</param>
        /// <param name="xmlReadMode">The XML read mode.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings AsXml(this EndpointSettings settings, string query, XmlReadMode xmlReadMode = XmlReadMode.Fragment)
        {
            settings.ExecutionMethod = ExecutionMethod.Xml;
            settings.XmlReadMode = xmlReadMode;
            settings.CommandType = CommandType.Text;
            settings.AddCommandText(query);
            settings.UseReturnValue = false;
            return settings;
        }

        /// <summary>Ases the XML.</summary>
        /// <param name="settings">The settings.</param>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="xmlReadMode">The XML read mode.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings AsXml(this EndpointSettings settings, Func<object, string> queryBuilder, XmlReadMode xmlReadMode = XmlReadMode.Fragment)
        {
            settings.ExecutionMethod = ExecutionMethod.Xml;
            settings.XmlReadMode = xmlReadMode;
            settings.CommandType = CommandType.Text;
            settings.AddCommandText(queryBuilder);
            settings.UseReturnValue = false;
            return settings;
        }


        /// <summary>Ases the transaction.</summary>
        /// <param name="settings">The settings.</param>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings AsTransaction(this EndpointSettings settings, IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            settings.IsTransaction = true;
            settings.IsolationLevel = isolationLevel;
            return settings;
        }

        /// <summary>Withes the parameters.</summary>
        /// <param name="settings">The settings.</param>
        /// <param name="parametersBuilder">The parameters builder.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings WithParameters(this EndpointSettings settings, Action<ParameterDataCollection> parametersBuilder)
        {
            settings.AddParameterResolver(parametersBuilder);
            return settings;
        }


        /// <summary>Withes the parameters.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settings">The settings.</param>
        /// <param name="parametersBuilder">The parameters builder.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings WithParameters<T>(this EndpointSettings settings, Action<ParameterDataCollection<T>> parametersBuilder)
        {
            settings.AddParameterResolver(parametersBuilder);
            return settings;
        }

        /// <summary>Withes the validation.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settings">The settings.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings WithValidation<T>(this EndpointSettings settings, Action<Validator<T>> validatorInitializer)
        where T : class
        {
            settings.AddValidation(validatorInitializer);
            return settings;
        }

        /// <summary>Binds the error handler.</summary>
        /// <param name="settings">The settings.</param>
        /// <param name="errorHandler">The error handler.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings BindErrorHandler(this EndpointSettings settings, Action<EndpointErrorInfo> onError)
        {
            if(onError != null)
            {
                settings.ErrorHandler = onError;
            }
            return settings;
        }

        /// <summary>Binds the on before execute.</summary>
        /// <param name="settings">The settings.</param>
        /// <param name="onBeforeExecute">The on before execute.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings BindOnBeforeExecute(this EndpointSettings settings, Action<IDbCommand, object> onBeforeExecute)
        {
            settings.OnBeforeExecuteHandler = onBeforeExecute;
            return settings;
        }

        /// <summary>Binds the on before execute asynchronous.</summary>
        /// <param name="settings">The settings.</param>
        /// <param name="onBeforeExecuteAsync">The on before execute asynchronous.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static EndpointSettings BindOnBeforeExecuteAsync(this EndpointSettings settings, Func<IDbCommand, object, CancellationToken, Task> onBeforeExecuteAsync)
        {
            settings.OnBeforeExecuteHandlerAsync = onBeforeExecuteAsync;
            return settings;
        }
    }
}
