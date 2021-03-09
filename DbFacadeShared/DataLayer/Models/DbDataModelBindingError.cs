using System;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models
{
    public  interface  IDbDataModelBindingError
    {
        string Error { get;}
        string ExceptionMessage { get; }
        string PropertyType { get; }
        string ColumnType { get; }
        string ColumnName { get; }
    }
    internal class DbDataModelBindingError: IDbDataModelBindingError
    {
        public string Error { get; private set; }
        internal Exception Exception { get; private set; }
        public string ExceptionMessage => Exception.Message;
        public string PropertyType { get; private set; }
        public string ColumnType { get; private set; }
        public string ColumnName { get; private set; }

        public static DbDataModelBindingError Create(Exception e, Type propertyType, Type columnType, string columnName)
        => new DbDataModelBindingError { 
            Exception = e, 
            PropertyType = propertyType.Name, 
            ColumnType = columnType.Name, 
            ColumnName = columnName,
            Error = $"Error converting Column {columnName}: Expected type {propertyType.Name} Actual {columnType.Name}"
        };
        public static DbDataModelBindingError Create(Exception e, Type modelType)
        => new DbDataModelBindingError
        {
            Exception = e,
            Error = $"Failed to create {modelType.Name} data model"
        };
        public static async Task<DbDataModelBindingError> CreateAsync(Exception e, Type modelType)
        {
            DbDataModelBindingError dataBindingError = new DbDataModelBindingError();
            dataBindingError.Exception = e;
            dataBindingError.Error = $"Failed to create {modelType.Name} data model";
            await Task.CompletedTask;
            return dataBindingError;
        }
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
