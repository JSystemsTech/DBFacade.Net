
using System.Data;

namespace DomainFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DomainFacade.DataLayer.Models.IDbParamsModel" />
    public class DbParamsModel:IDbParamsModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbParamsModel"/> class.
        /// </summary>
        public DbParamsModel() { }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IDbFunctionalTestParamsModel
    {
        /// <summary>
        /// Gets the parameters model.
        /// </summary>
        /// <returns></returns>
        IDbParamsModel GetParamsModel();
        /// <summary>
        /// Gets the test response.
        /// </summary>
        /// <returns></returns>
        IDataReader GetTestResponse();
        /// <summary>
        /// Gets the return value.
        /// </summary>
        /// <returns></returns>
        object GetReturnValue();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="DomainFacade.DataLayer.Models.DbParamsModel" />
    /// <seealso cref="DomainFacade.DataLayer.Models.IDbFunctionalTestParamsModel" />
    internal sealed class MockParamsModel<DbParams> : DbParamsModel,IDbFunctionalTestParamsModel where DbParams: IDbParamsModel
    {
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        private DbParams Model { get; set; }
        /// <summary>
        /// Gets or sets the test response data.
        /// </summary>
        /// <value>
        /// The test response data.
        /// </value>
        private IDataReader TestResponseData { get; set; }
        /// <summary>
        /// Gets or sets the return value.
        /// </summary>
        /// <value>
        /// The return value.
        /// </value>
        private object ReturnValue { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="MockParamsModel{DbParams}" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="testResponseData">The test response data.</param>
        public MockParamsModel(DbParams model, IDataReader testResponseData) { Model = model; TestResponseData = testResponseData;  }
        /// <summary>
        /// Initializes a new instance of the <see cref="MockParamsModel{DbParams}"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="testResponseData">The test response data.</param>
        /// <param name="returnValue">The return value.</param>
        public MockParamsModel(DbParams model, IDataReader testResponseData, object returnValue) { Model = model; TestResponseData = testResponseData; ReturnValue = returnValue; }
        /// <summary>
        /// Gets the parameters model.
        /// </summary>
        /// <returns></returns>
        public IDbParamsModel GetParamsModel()
        {
            return Model;
        }

        /// <summary>
        /// Gets the test response.
        /// </summary>
        /// <returns></returns>
        public IDataReader GetTestResponse()
        {
            return TestResponseData;
        }

        /// <summary>
        /// Gets the return value.
        /// </summary>
        /// <returns></returns>
        public object GetReturnValue()
        {
            return ReturnValue;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="DomainFacade.DataLayer.Models.DbParamsModel" />
    public class SimpleDbParamsModel<T> : DbParamsModel
    {
        public T Param1 { get; private set; }
        public SimpleDbParamsModel(T param1) : base() { Param1 = param1; }
        public SimpleDbParamsModel() : base() { }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <seealso cref="DomainFacade.DataLayer.Models.DbParamsModel" />
    public class SimpleDbParamsModel<T, U> : SimpleDbParamsModel<T>
    {
        public U Param2 { get; private set; }
        public SimpleDbParamsModel(T param1, U param2) : base(param1) { Param2 = param2; }
        public SimpleDbParamsModel() : base() { }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <seealso cref="DomainFacade.DataLayer.Models.DbParamsModel" />
    public class SimpleDbParamsModel<T, U, V> : SimpleDbParamsModel<T, U>
    {
        public V Param3 { get; private set; }
        public SimpleDbParamsModel(T param1, U param2, V param3) : base(param1, param2) { Param3 = param3; }
        public SimpleDbParamsModel() : base() { }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <typeparam name="W"></typeparam>
    /// <seealso cref="DomainFacade.DataLayer.Models.DbParamsModel" />
    public class SimpleDbParamsModel<T, U, V, W> : SimpleDbParamsModel<T, U, V>
    {
        public W Param4 { get; private set; }
        public SimpleDbParamsModel(T param1, U param2, V param3, W param4) : base(param1, param2, param3) { Param4 = param4; }
        public SimpleDbParamsModel() : base() { }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <typeparam name="W"></typeparam>
    /// <typeparam name="X"></typeparam>
    /// <seealso cref="DomainFacade.DataLayer.Models.DbParamsModel" />
    public class SimpleDbParamsModel<T, U, V, W, X> : SimpleDbParamsModel<T, U, V, W>
    {
        public X Param5 { get; private set; }
        public SimpleDbParamsModel(T param1, U param2, V param3, W param4, X param5) : base(param1, param2, param3, param4) { Param5 = param5; }
        public SimpleDbParamsModel() : base() { }
    }
}
