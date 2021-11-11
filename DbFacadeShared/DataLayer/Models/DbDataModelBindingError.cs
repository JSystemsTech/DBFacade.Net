using System;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface  IDbDataModelBindingError
    {
        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        string Error { get;}
        /// <summary>
        /// Gets the exception message.
        /// </summary>
        /// <value>
        /// The exception message.
        /// </value>
        string ExceptionMessage { get; }
        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        /// <value>
        /// The type of the property.
        /// </value>
        string PropertyType { get; }
        /// <summary>
        /// Gets the type of the column.
        /// </summary>
        /// <value>
        /// The type of the column.
        /// </value>
        string ColumnType { get; }
        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        /// <value>
        /// The name of the column.
        /// </value>
        string ColumnName { get; }
    }
    /// <summary>
    /// 
    /// </summary>
    internal class DbDataModelBindingError: IDbDataModelBindingError
    {
        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; private set; }
        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        internal Exception Exception { get; private set; }
        /// <summary>
        /// Gets the exception message.
        /// </summary>
        /// <value>
        /// The exception message.
        /// </value>
        public string ExceptionMessage => Exception.Message;
        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        /// <value>
        /// The type of the property.
        /// </value>
        public string PropertyType { get; private set; }
        /// <summary>
        /// Gets the type of the column.
        /// </summary>
        /// <value>
        /// The type of the column.
        /// </value>
        public string ColumnType { get; private set; }
        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        /// <value>
        /// The name of the column.
        /// </value>
        public string ColumnName { get; private set; }

        /// <summary>
        /// Creates the specified e.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyType">Type of the property.</param>
        /// <param name="columnType">Type of the column.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public static DbDataModelBindingError Create(Exception e, Type propertyType, Type columnType, string columnName)
        => new DbDataModelBindingError { 
            Exception = e, 
            PropertyType = propertyType.Name, 
            ColumnType = columnType.Name, 
            ColumnName = columnName,
            Error = $"Error converting Column {columnName}: Expected type {propertyType.Name} Actual {columnType.Name}"
        };
        /// <summary>
        /// Creates the specified e.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="modelType">Type of the model.</param>
        /// <returns></returns>
        public static DbDataModelBindingError Create(Exception e, Type modelType)
        => new DbDataModelBindingError
        {
            Exception = e,
            Error = $"Failed to create {modelType.Name} data model"
        };
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="modelType">Type of the model.</param>
        /// <returns></returns>
        public static async Task<DbDataModelBindingError> CreateAsync(Exception e, Type modelType)
        {
            DbDataModelBindingError dataBindingError = new DbDataModelBindingError();
            dataBindingError.Exception = e;
            dataBindingError.Error = $"Failed to create {modelType.Name} data model";
            await Task.CompletedTask;
            return dataBindingError;
        }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="propertyType">Type of the property.</param>
        /// <param name="columnType">Type of the column.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public static async Task<DbDataModelBindingError> CreateAsync(Exception e, Type propertyType, Type columnType, string columnName)
        {
            DbDataModelBindingError dataBindingError = new DbDataModelBindingError();
            dataBindingError.Exception = e;
            dataBindingError.PropertyType = propertyType.Name;
            dataBindingError.ColumnType = columnType.Name;
            dataBindingError.ColumnName = columnName;
            dataBindingError.Error = $"Error converting Column {columnName}: Expected type {propertyType.Name} Actual {columnType.Name}";
            await Task.CompletedTask;
            return dataBindingError;
        }

    }
}
