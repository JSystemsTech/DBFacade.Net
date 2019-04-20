using DBFacade.DataLayer.Manifest;
using DBFacade.Exceptions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web.Mvc;

namespace DBFacade.DataLayer.Models
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DbMethod">The type of the b method.</typeparam>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    /// <seealso cref="System.Collections.Generic.List{TDbDataModel}" />
    /// <seealso cref="Models.IDbResponse{TDbDataModel}" />
    internal class DbResponse<DbMethod, TDbDataModel> : List<TDbDataModel>, IDbResponse<TDbDataModel>
        where TDbDataModel : DbDataModel
        where DbMethod : IDbMethod
    {
        /// <summary>
        /// Adds the fetched data.
        /// </summary>
        /// <param name="dbReader">The database reader.</param>
        private void AddFetchedData(DbDataReader dbReader)
        {
            while (dbReader.Read())
            {
                Add(DbDataModel.ToDbDataModel<TDbDataModel, DbMethod>(dbReader));
            }
        }
        /// <summary>
        /// Gets or sets the return value.
        /// </summary>
        /// <value>
        /// The return value.
        /// </value>
        private object ReturnVal { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbResponse{DbMethod, TDbDataModel}"/> class.
        /// </summary>
        /// <param name="dbReader">The database reader.</param>
        /// <param name="returnValue">The return value.</param>
        public DbResponse(DbDataReader dbReader, object returnValue)
        {
            AddFetchedData(dbReader);
            if (returnValue != null)
            {
                ReturnVal = returnValue;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbResponse{DbMethod, TDbDataModel}"/> class.
        /// </summary>
        /// <param name="returnValue">The return value.</param>
        public DbResponse(object returnValue)
        {
            if (returnValue != null)
            {
                ReturnVal = returnValue;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbResponse{DbMethod, TDbDataModel}"/> class.
        /// </summary>
        /// <param name="e">The e.</param>
        public DbResponse(FacadeException e)
        {
            Error = e;
        }
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        /// <summary>
        /// Converts to jsonresult.
        /// </summary>
        /// <returns></returns>
        public JsonResult ToJsonResult()
        {
            return new JsonResult { Data = this, MaxJsonLength = int.MaxValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        private FacadeException Error { get; set; }
        /// <summary>
        /// Returns the value.
        /// </summary>
        /// <returns></returns>
        public object ReturnValue()
        {
            return ReturnVal;
        }
        /// <summary>
        /// Resultses this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TDbDataModel> Results()
        {
            return this;
        }
        public TDbDataModel Result()
        {
            if (Count == 0)
            {
                return default(TDbDataModel);
            }
            return this.First();
        }

        /// <summary>
        /// Determines whether this instance has error.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.
        /// </returns>
        public bool HasError()
        {
            return Error != null;
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <returns></returns>
        public FacadeException GetException()
        {
            return Error;
        }


    }
}
