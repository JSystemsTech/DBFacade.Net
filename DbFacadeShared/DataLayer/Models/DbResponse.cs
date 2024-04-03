using DbFacade.DataLayer.ConnectionService;
using DbFacade.Exceptions;
using DbFacadeShared.DataLayer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
        private static readonly bool IsEmptyDataReturn = typeof(TDbDataModel) == typeof(DbDataModel);
        /// <summary>
        /// Creates the specified command identifier.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <returns></returns>
        public static IDbResponse<TDbDataModel> Create(IDbCommandSettings dbCommandSettings)
            => !IsEmptyDataReturn ?
                new DbResponse<TDbDataModel>(dbCommandSettings, null) :
                (IDbResponse<TDbDataModel>)new DbResponse(dbCommandSettings, null);
        /// <summary>
        /// Creates the specified command identifier.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        public static IDbResponse<TDbDataModel> Create(IDbCommandSettings dbCommandSettings, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => !IsEmptyDataReturn ?
                new DbResponse<TDbDataModel>(dbCommandSettings, returnValue, outputValues) :
                (IDbResponse<TDbDataModel>)new DbResponse(dbCommandSettings, returnValue, outputValues);
        /// <summary>
        /// Creates the error response.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public static IDbResponse<TDbDataModel> CreateErrorResponse(IDbCommandSettings dbCommandSettings, Exception error)
            => !IsEmptyDataReturn ?
                new DbResponse<TDbDataModel>(dbCommandSettings, error) :
                (IDbResponse<TDbDataModel>)new DbResponse(dbCommandSettings, error);
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <returns></returns>
        public static async Task<IDbResponse<TDbDataModel>> CreateAsync(IDbCommandSettings dbCommandSettings)
            => !IsEmptyDataReturn ?
                await DbResponse<TDbDataModel>.CreateAsync(dbCommandSettings) :
                (IDbResponse<TDbDataModel>)await DbResponse.CreateAsync(dbCommandSettings);
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        public static async Task<IDbResponse<TDbDataModel>> CreateAsync(IDbCommandSettings dbCommandSettings, int returnValue = 0, IDictionary<string, object> outputValues = null)
            => !IsEmptyDataReturn ?
                await DbResponse<TDbDataModel>.CreateAsync(dbCommandSettings, returnValue, outputValues) :
                (IDbResponse<TDbDataModel>)await DbResponse.CreateAsync(dbCommandSettings, returnValue, outputValues);
        /// <summary>
        /// Creates the error response asynchronous.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public static async Task<IDbResponse<TDbDataModel>> CreateErrorResponseAsync(IDbCommandSettings dbCommandSettings, Exception error)
            => !IsEmptyDataReturn ?
                await DbResponse<TDbDataModel>.CreateErrorResponseAsync(dbCommandSettings, error) :
                (IDbResponse<TDbDataModel>)await DbResponse.CreateErrorResponseAsync(dbCommandSettings, error);

    }

    /// <summary>
    /// 
    /// </summary>
    internal class DbResponse : DbResponse<DbDataModel>, IDbResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbResponse" /> class.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="error">The error.</param>
        public DbResponse(IDbCommandSettings dbCommandSettings, Exception error = null) : base(dbCommandSettings, error) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbResponse" /> class.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        public DbResponse(IDbCommandSettings dbCommandSettings, int returnValue = 0, IDictionary<string, object> outputValues = null)
            : base(dbCommandSettings, returnValue, outputValues) { }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <returns></returns>
        public static new async Task<DbResponse> CreateAsync(IDbCommandSettings dbCommandSettings)
        {
            DbResponse response = new DbResponse(dbCommandSettings, null);
            await response.InitializeAsync(dbCommandSettings, null);
            return response;
        }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        public static new async Task<DbResponse> CreateAsync(IDbCommandSettings dbCommandSettings, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            DbResponse response = new DbResponse(dbCommandSettings, null);
            await response.InitializeAsync(dbCommandSettings, returnValue, outputValues);
            return response;
        }
        /// <summary>
        /// Creates the error response asynchronous.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public static new async Task<DbResponse> CreateErrorResponseAsync(IDbCommandSettings dbCommandSettings, Exception error)
        {
            DbResponse response = new DbResponse(dbCommandSettings, error);
            await response.InitializeAsync(dbCommandSettings, error);
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
        public bool HasDataBindingErrors { get => this.Any(item => item.HasDataBindingErrors); }
        /// <summary>
        /// Gets or sets the output values.
        /// </summary>
        /// <value>
        /// The output values.
        /// </value>
        private IDictionary<string, object> OutputValues { get; set; }

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
        public Guid CommandId => DbCommandSettings.CommandId;
        /// <summary>
        /// Gets the database command settings.
        /// </summary>
        /// <value>
        /// The database command settings.
        /// </value>
        public IDbCommandSettings DbCommandSettings { get; private set; }
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

        /// <summary>Gets the error message.</summary>
        /// <value>The error message.</value>
        public string ErrorMessage => HasError ? Error.Message : "";
        /// <summary>Gets the error details.</summary>
        /// <value>The error details.</value>
        public string ErrorDetails => HasError && Error is FacadeException ex ? ex.ErrorDetails : "";
        /// <summary>
        /// Initializes a new instance of the <see cref="DbResponse{TDbDataModel}" /> class.
        /// </summary>
        public DbResponse()
        {
            IsNull = true;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbResponse{TDbDataModel}" /> class.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="error">The error.</param>
        public DbResponse(IDbCommandSettings dbCommandSettings, Exception error = null)
        {
            IsNull = true;
            DbCommandSettings = dbCommandSettings;
            Error = error;
        }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <returns></returns>
        public static async Task<DbResponse<TDbDataModel>> CreateAsync(IDbCommandSettings dbCommandSettings)
        {
            DbResponse<TDbDataModel> response = new DbResponse<TDbDataModel>();
            await response.InitializeAsync(dbCommandSettings, null);
            return response;
        }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        public static async Task<DbResponse<TDbDataModel>> CreateAsync(IDbCommandSettings dbCommandSettings, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            DbResponse<TDbDataModel> response = new DbResponse<TDbDataModel>();
            await response.InitializeAsync(dbCommandSettings, returnValue, outputValues);
            return response;
        }
        /// <summary>
        /// Creates the error response asynchronous.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public static async Task<DbResponse<TDbDataModel>> CreateErrorResponseAsync(IDbCommandSettings dbCommandSettings, Exception error)
        {
            DbResponse<TDbDataModel> response = new DbResponse<TDbDataModel>();
            await response.InitializeAsync(dbCommandSettings, error);
            return response;
        }
        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="error">The error.</param>
        protected async Task InitializeAsync(IDbCommandSettings dbCommandSettings, Exception error = null)
        {
            IsNull = true;
            DbCommandSettings = dbCommandSettings;
            Error = error;
            await Task.CompletedTask;
        }
        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        protected async Task InitializeAsync(IDbCommandSettings dbCommandSettings, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            IsNull = false;
            DbCommandSettings = dbCommandSettings;
            ReturnValue = returnValue;
            OutputValues = outputValues;
            await Task.CompletedTask;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbResponse{TDbDataModel}" /> class.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        public DbResponse(IDbCommandSettings dbCommandSettings, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            ReturnValue = returnValue;
            OutputValues = outputValues;
            DbCommandSettings = dbCommandSettings;
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
        => OutputValues != null && OutputValues.TryGetValue(key, out object value) ? value : (object)null;
        /// <summary>
        /// Gets the output value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T GetOutputValue<T>(string key)
        => OutputDbDataModel<T>.ToDbDataModel(DbCommandSettings, OutputValues, key).Value;
        /// <summary>
        /// Gets the output model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetOutputModel<T>() where T : DbDataModel
        => DbDataModel.ToDbDataModel<T>(DbCommandSettings, OutputValues);

        /// <summary>
        /// Gets the output value asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<object> GetOutputValueAsync(string key)
        {
            await Task.CompletedTask;
            return GetOutputValue(key);
        }
        /// <summary>
        /// Gets the output value asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<T> GetOutputValueAsync<T>(string key)
        {
            var model = await OutputDbDataModel<T>.ToDbDataModelAsync(DbCommandSettings, OutputValues, key);
            return model.Value;
        }
        /// <summary>
        /// Gets the output model asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> GetOutputModelAsync<T>() where T : DbDataModel
        => await DbDataModel.ToDbDataModelAsync<T>(DbCommandSettings, OutputValues);

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
        public IEnumerable<IDbDataSet> DataSets { get; private set; }
        public DataSet DataSet { get; private set; }

        internal void InitDataSets(IEnumerable<IDbDataSet> dataSets, DataSet ds, bool rawDataOnly)
        {
            DataSets = dataSets;
            DataSet = ds;
            //ignore setting data when data type is the abstract base class
            if (DataSets.Count() > 0 && typeof(TDbDataModel) != typeof(DbDataModel) && !rawDataOnly)
            {
                AddRange(DataSets.First().ToDbDataModelList<TDbDataModel>());
            }
        }
    }
}