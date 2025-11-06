using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models.Parameters;
using DbFacade.Exceptions;
using DbFacade.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models
{
    internal enum ExecutionMethod
    {
        NonQuery,
        Query,
        Xml,
        Scalar
    }
    /// <summary>
    ///   <br />
    /// </summary>
    public class EndpointSettings
    {
        /// <summary>Gets or sets the connection string identifier.</summary>
        /// <value>The connection string identifier.</value>
        public string ConnectionStringId { get; set; }
        /// <summary>The name</summary>
        public readonly string Name;
        private readonly List<Func<object, string>> CommandTextBuilders;
        internal string BuildCommandText<T>(T model) => string.Join(Environment.NewLine, CommandTextBuilders.Select(b => b(model)));
        internal CommandType CommandType { get; set; }
        internal bool IsTransaction { get; set; }
        internal IsolationLevel IsolationLevel { get; set; }
        internal bool UseReturnValue { get; set; }
        internal ExecutionMethod ExecutionMethod { get; set; }
        internal XmlReadMode XmlReadMode { get; set; }
        
        private readonly Dictionary<Type, Action<IDbCommand, object>> ParameterResolvers;
        internal Action<IDbCommand, object> OnBeforeExecuteHandler { get; set; }
        internal Func<IDbCommand, object, CancellationToken, Task> OnBeforeExecuteHandlerAsync { get; set; }

        private readonly Dictionary<Type, Func<object, (bool isValid, string[] errors)>> TypeValidators;
        
        internal readonly Schema Schema;
        internal readonly IDbConnectionProvider DbConnectionProvider;
        internal Action<EndpointErrorInfo> ErrorHandler { get; set; }
        internal EndpointSettings(Schema schema, string name) : this(schema.DbConnectionProvider, name)
        {
            Schema = schema;
        }
        internal EndpointSettings(IDbConnectionProvider dbConnectionProvider, string name)
        {
            DbConnectionProvider = dbConnectionProvider;
            Name = name;
            CommandTextBuilders = new List<Func<object, string>>();
            ParameterResolvers = new Dictionary<Type, Action<IDbCommand, object>>();
            TypeValidators = new Dictionary<Type, Func<object, (bool isValid, string[] errors)>>();
        }
        internal (Type providerType, string connectionStringId) GetConnectionGroupKey()
            => (DbConnectionProvider.GetType(), ConnectionStringId);
        internal async Task OnBeforeExecuteAsync<T>(IDbCommand command, T model, CancellationToken cancellationToken)
        {
            if (OnBeforeExecuteHandlerAsync != null)
            {
                await OnBeforeExecuteHandlerAsync(command, model, cancellationToken);
            }
            else
            {
                OnBeforeExecute(command, model);
                await Task.CompletedTask;
            }
        }
        internal void OnBeforeExecute<T>(IDbCommand command, T model)
        {
            if (OnBeforeExecuteHandler != null)
            {
                OnBeforeExecuteHandler(command, model);
            }
        }
        internal void OnError(EndpointErrorInfo endpointErrorInfo)
        {
            if (ErrorHandler != null)
            {
                ErrorHandler(endpointErrorInfo);
            }
        }
        internal void AddCommandText(params Func<object, string>[] queryBuilders)
        {
            CommandTextBuilders.Clear();
            foreach (var builder in queryBuilders)
            {
                CommandTextBuilders.Add(builder);
            }
        }
        internal void AddCommandText(params string[] queries)
        {
            CommandTextBuilders.Clear();
            foreach (var query in queries)
            {
                CommandTextBuilders.Add(m => query);
            }
        }
        internal (bool isValid, string[] errors) Validate<T>(T model)
        {
            bool hasTypeValidation = TypeValidators.Keys.Count > 0;
            Type key = model == null ? typeof(object) : Accessor<T>.GetInstance().Type;
            if (TypeValidators.ContainsKey(key) && TypeValidators[key] is Func<object, (bool isValid, string[] errors)> validate)
            {
                return validate(model);
            }
            else if (hasTypeValidation)
            {
                return (false, new string[] { $"Unable to validate parameters: expected model of type(s) {TypeValidators.Keys.ToArray().TypeNames()} but got {model.TypeName()}" });
            }
            return (true, Array.Empty<string>());
        }
        internal void AddValidation<T>(Action<Validator<T>> validatorInitializer)
        {
            Type key = typeof(T);
            var validator = new Validator<T>();
            validatorInitializer(validator);
            Func<object, (bool isValid, string[] errors)> validate = data => {
                bool isValid = validator.Validate((T)data, out string[] errors); //Type check done in Validate method
                return (isValid, errors);
            };
            TypeValidators[key] = validate;
        }

        internal void AddParams<T>(IDbCommand command, T model)
        {
            Accessor<T> accessor = Accessor<T>.GetInstance();
            
            if (ParameterResolvers.ContainsKey(accessor.Type) && ParameterResolvers[accessor.Type] is Action<IDbCommand, object> resolver)
            {
                resolver(command, model);
            }
            else if(AddParamsGeneral != null)
            {
                AddParamsGeneral(command, model);
            }
            else if (accessor.IsNullable && model == null)
            {
                return;
            }
            else
            {
                ErrorHelper.ThrowInvalidParametersError(accessor.Type, ParameterResolvers.Keys.ToArray());
            }
        }
        private Action<IDbCommand, object> AddParamsGeneral { get; set; }
        internal void AddParameterResolver<T>(Action<ParameterDataCollection<T>> parametersBuilder)
        {
            Type key = typeof(T);
            Action<IDbCommand, object> resolver = (cmd, data) => {
                ParameterDataCollection<T> collection = new ParameterDataCollection<T>((T)data); //Type check done in AddParams method
                parametersBuilder(collection);
                cmd.AddParameters(collection.Collection, this);
            };
            ParameterResolvers[key] = resolver;
        }
        internal void AddParameterResolver(Action<ParameterDataCollection> parametersBuilder)
        {
            Type key = typeof(object);
            AddParamsGeneral = (cmd, data) => {
                ParameterDataCollection collection = ParameterDataCollection.Create(parametersBuilder);
                cmd.AddParameters(collection, this);
            };
        }

    }
}
