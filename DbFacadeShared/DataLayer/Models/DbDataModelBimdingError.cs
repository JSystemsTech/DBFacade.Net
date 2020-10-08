using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DbFacadeShared.DataLayer.Models
{
    public  interface  IDbDataModelBindingError
    {
        string Error { get;}
        Exception Exception { get; }
        Type PropertyType { get; }
        Type ColumnType { get; }
        string ColumnName { get; }
    }
    internal class DbDataModelBindingError: IDbDataModelBindingError
    {
        public string Error { get; private set; }
        public Exception Exception { get; private set; }
        public Type PropertyType { get; private set; }
        public Type ColumnType { get; private set; }
        public string ColumnName { get; private set; }

        public static DbDataModelBindingError Create(Exception e, Type propertyType, Type columnType, string columnName)
        => new DbDataModelBindingError { 
            Exception = e, 
            PropertyType = propertyType, 
            ColumnType = columnType, 
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
            dataBindingError.PropertyType = propertyType;
            dataBindingError.ColumnType = columnType;
            dataBindingError.ColumnName = columnName;
            dataBindingError.Error = $"Error converting Column {columnName}: Expected type {propertyType.Name} Actual {columnType.Name}";
            await Task.CompletedTask;
            return dataBindingError;
        }

    }
}
