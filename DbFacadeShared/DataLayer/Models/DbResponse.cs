using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace DbFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    internal class DbResponseFactory<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        /// <summary>
        /// The is empty data return
        /// </summary>
        private static bool IsEmptyDataReturn = typeof(TDbDataModel) == typeof(DbDataModel);
        /// <summary>
        /// Creates the specified command identifier.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <returns></returns>
        public static IDbResponse<TDbDataModel> Create(Guid commandId)
            => !IsEmptyDataReturn ?
                new DbResponse<TDbDataModel>(commandId,null) :
                (IDbResponse<TDbDataModel>)new DbResponse(commandId, null);
        /// <summary>
        /// Creates the specified command identifier.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        public static IDbResponse<TDbDataModel> Create(Guid commandId, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => !IsEmptyDataReturn ?
                new DbResponse<TDbDataModel>(commandId, returnValue, outputValues) :
                (IDbResponse<TDbDataModel>)new DbResponse(commandId, returnValue, outputValues);
        /// <summary>
        /// Creates the error response.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public static IDbResponse<TDbDataModel> CreateErrorResponse(Guid commandId, Exception error)
            => !IsEmptyDataReturn ?
                new DbResponse<TDbDataModel>(commandId, error) :
                (IDbResponse<TDbDataModel>)new DbResponse(commandId, error);
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <returns></returns>
        public static async Task<IDbResponse<TDbDataModel>> CreateAsync(Guid commandId)
            => !IsEmptyDataReturn ?
                await DbResponse<TDbDataModel>.CreateAsync(commandId) :
                (IDbResponse<TDbDataModel>) await DbResponse.CreateAsync(commandId);
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        public static async Task<IDbResponse<TDbDataModel>> CreateAsync(Guid commandId, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => !IsEmptyDataReturn ?
                await DbResponse<TDbDataModel>.CreateAsync(commandId, returnValue, outputValues) :
                (IDbResponse<TDbDataModel>)await DbResponse.CreateAsync(commandId, returnValue, outputValues);
        /// <summary>
        /// Creates the error response asynchronous.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public static async Task<IDbResponse<TDbDataModel>> CreateErrorResponseAsync(Guid commandId, Exception error)
            => !IsEmptyDataReturn ?
                await DbResponse<TDbDataModel>.CreateErrorResponseAsync(commandId, error) :
                (IDbResponse<TDbDataModel>)await DbResponse.CreateErrorResponseAsync(commandId, error);
    }
    /// <summary>
    /// 
    /// </summary>
    internal class DbResponse : DbResponse<DbDataModel>, IDbResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbResponse"/> class.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="error">The error.</param>
        public DbResponse(Guid commandId, Exception error = null) :base(commandId, error) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbResponse"/> class.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        public DbResponse(Guid commandId, int returnValue = 0, IDictionary<string, object> outputValues = null) 
            :base(commandId, returnValue, outputValues) { }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <returns></returns>
        public static new async Task<DbResponse> CreateAsync(Guid commandId)
        {
            DbResponse response = new DbResponse(commandId, null);
            await response.InitializeAsync(commandId, null);
            return response;
        }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        public static new async Task<DbResponse> CreateAsync(Guid commandId, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            DbResponse response = new DbResponse(commandId, null);
            await response.InitializeAsync(commandId, returnValue, outputValues);
            return response;
        }
        /// <summary>
        /// Creates the error response asynchronous.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public static new async Task<DbResponse> CreateErrorResponseAsync(Guid commandId, Exception error)
        {
            DbResponse response = new DbResponse(commandId, error);
            await response.InitializeAsync(commandId, error);
            return response;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    internal class DbResponse<TDbDataModel> : List<TDbDataModel>, IDbResponse<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        /// <summary>
        /// Gets a value indicating whether this instance has data binding errors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has data binding errors; otherwise, <c>false</c>.
        /// </value>
        public bool HasDataBindingErrors { get=> this.Any(item=>item.HasDataBindingErrors);}
        /// <summary>
        /// Gets or sets the output values.
        /// </summary>
        /// <value>
        /// The output values.
        /// </value>
        private IDictionary<string, object> OutputValues { get; set; }
        /// <summary>
        /// Gets a value indicating whether this instance is null.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is null; otherwise, <c>false</c>.
        /// </value>
        public bool IsNull { get; private set; }
        /// <summary>
        /// Gets the command identifier.
        /// </summary>
        /// <value>
        /// The command identifier.
        /// </value>
        public Guid CommandId { get; private set; }
        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public Exception Error { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance has error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.
        /// </value>
        public bool HasError => Error != null;
        /// <summary>
        /// Initializes a new instance of the <see cref="DbResponse{TDbDataModel}"/> class.
        /// </summary>
        public DbResponse()
        {
            IsNull = true;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbResponse{TDbDataModel}"/> class.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="error">The error.</param>
        public DbResponse(Guid commandId, Exception error = null)
        {
            IsNull = true;
            CommandId = commandId;
            Error = error;
        }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <returns></returns>
        public static async Task<DbResponse<TDbDataModel>> CreateAsync(Guid commandId)
        {
            DbResponse<TDbDataModel> response = new DbResponse<TDbDataModel>();
            await response.InitializeAsync(commandId, null);
            return response;
        }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        public static async Task<DbResponse<TDbDataModel>> CreateAsync(Guid commandId, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            DbResponse<TDbDataModel> response = new DbResponse<TDbDataModel>();
            await response.InitializeAsync(commandId,returnValue, outputValues);
            return response;
        }
        /// <summary>
        /// Creates the error response asynchronous.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public static async Task<DbResponse<TDbDataModel>> CreateErrorResponseAsync(Guid commandId, Exception error)
        {
            DbResponse<TDbDataModel> response = new DbResponse<TDbDataModel>();
            await response.InitializeAsync(commandId, error);            
            return response;
        }
        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="error">The error.</param>
        protected async Task InitializeAsync(Guid commandId, Exception error = null)
        {
            IsNull = true;
            CommandId = commandId;
            Error = error;
            await Task.CompletedTask;
        }
        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        protected async Task InitializeAsync(Guid commandId, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            IsNull = false;
            CommandId = commandId;
            ReturnValue = returnValue;
            OutputValues = outputValues;
            await Task.CompletedTask;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbResponse{TDbDataModel}"/> class.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        public DbResponse(Guid commandId, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            ReturnValue = returnValue;
            OutputValues = outputValues;
            CommandId = commandId;
        }

        /// <summary>
        /// Gets the return value.
        /// </summary>
        /// <value>
        /// The return value.
        /// </value>
        public int ReturnValue { get; internal set; }
        /// <summary>
        /// Gets the output value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object GetOutputValue(string key)
        {
            object value = null;
            if (OutputValues != null && OutputValues.TryGetValue(key, out object val))
            {
                value = val;
            }
            return value;
        }
        /// <summary>
        /// Gets the output value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T GetOutputValue<T>(string key)
        => OutputDbDataModel<T>.ToDbDataModel(CommandId, OutputValues, key).Value;
        /// <summary>
        /// Gets the output model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetOutputModel<T>() where T: DbDataModel
        => DbDataModel.ToDbDataModel<T>(CommandId, OutputValues);

        /// <summary>
        /// Gets the output value asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<object> GetOutputValueAsync(string key)
        {
            object value = null;
            if (OutputValues != null && OutputValues.TryGetValue(key, out object val))
            {
                value = val;
            }
            await Task.CompletedTask;
            return value;
        }
        /// <summary>
        /// Gets the output value asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<T> GetOutputValueAsync<T>(string key)
        {
            var model = await OutputDbDataModel<T>.ToDbDataModelAsync(CommandId, OutputValues, key);
            return model.Value;
        }
        /// <summary>
        /// Gets the output model asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> GetOutputModelAsync<T>() where T : DbDataModel
        => await DbDataModel.ToDbDataModelAsync<T>(CommandId, OutputValues);

        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Serializes the specified text writer.
        /// </summary>
        /// <param name="textWriter">The text writer.</param>
        public void Serialize(TextWriter textWriter)
        {
            using (var xmlWriter = XmlWriter.Create(textWriter))
            {
                Serialize(xmlWriter);
            }
        }

        /// <summary>
        /// Serializes the specified XML writer.
        /// </summary>
        /// <param name="xmlWriter">The XML writer.</param>
        public void Serialize(XmlWriter xmlWriter)
        {
            var data = ToList();
            var serializer = new DataContractSerializer(data.GetType());
            serializer.WriteObject(xmlWriter, data);
        }

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        int IReadOnlyDbCollection<TDbDataModel>.Count()
        {
            return Count;
        }

        /// <summary>
        /// Firsts this instance.
        /// </summary>
        /// <returns></returns>
        public TDbDataModel First()
        {
            return this[0];
        }

        /// <summary>
        /// Converts to list.
        /// </summary>
        /// <returns></returns>
        public List<TDbDataModel> ToList()
        {
            return ToArray().ToList();
        }

        /// <summary>
        /// Converts to jsonasync.
        /// </summary>
        /// <returns></returns>
        public async Task<string> ToJsonAsync()
        {
            string json = ToJson();
            await Task.CompletedTask;
            return json;
        }

        /// <summary>
        /// Serializes the asynchronous.
        /// </summary>
        /// <param name="textWriter">The text writer.</param>
        public async Task SerializeAsync(TextWriter textWriter)
        {
            using (var xmlWriter = XmlWriter.Create(textWriter))
            {
                await SerializeAsync(xmlWriter);
            }            
        }

        /// <summary>
        /// Serializes the asynchronous.
        /// </summary>
        /// <param name="xmlWriter">The XML writer.</param>
        public async Task SerializeAsync(XmlWriter xmlWriter)
        {
            var data = await ToListAsync();
            var serializer = new DataContractSerializer(data.GetType());
            serializer.WriteObject(xmlWriter, data);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Converts to listasync.
        /// </summary>
        /// <returns></returns>
        public async Task<List<TDbDataModel>> ToListAsync()
        {
            List<TDbDataModel> list = ToList();
            await Task.CompletedTask;
            return list;
        }
    }
}