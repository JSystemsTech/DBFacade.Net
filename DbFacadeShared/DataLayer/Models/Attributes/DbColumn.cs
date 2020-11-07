﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DbFacade.Extensions;

namespace DbFacade.DataLayer.Models.Attributes
{

    internal interface IDbColumn
    {
        object GetValue(IDataRecord data, Type propType);
        Task<object> GetValueAsync(IDataRecord data, Type propType);
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Constructor, AllowMultiple = true)]
    public class DbColumn : Attribute, IDbColumn
    {
        internal const char DefaultDelimiter = ',';
        internal const int DefaultBufferSize = 100;
        private readonly object _defaultValue;

        private IEnumerable<string> Columns { get; set; }

        internal DbColumn() { }

        public DbColumn(string name, char delimiter = DefaultDelimiter)
            : this(name, null, DefaultBufferSize, delimiter) { }


        public DbColumn(string name, int bufferSize)
            : this(name, null, bufferSize) { }

        internal DbColumn(string name, object defaultValue)
            : this(name, defaultValue, DefaultBufferSize, DefaultDelimiter) { }
        internal DbColumn(string name, object defaultValue, char delimiter = DefaultDelimiter)
            : this(name, defaultValue, DefaultBufferSize, delimiter) { }

        internal DbColumn(string name, object defaultValue, int bufferSize = DefaultBufferSize,
            char delimiter = DefaultDelimiter)
        {
            Delimiter = delimiter;
            Columns = name.Split(DefaultDelimiter);
            _defaultValue = defaultValue;
            BufferSize = bufferSize;
        }

        private char Delimiter { get; }
        private int BufferSize { get; }
    

        protected object TryGetValue(IDataRecord data, Type propType)
        {
            string columnName = Columns.Where(c => data.GetOrdinal(c) >= 0).FirstOrDefault();
            try
            {
                return string.IsNullOrWhiteSpace(columnName) ? _defaultValue: data.GetColumn(columnName, propType, BufferSize, Delimiter, _defaultValue);
            }
            catch (Exception e)
            {
                return DbDataModelBindingError.Create(e, propType, data.GetFieldType(data.GetOrdinal(columnName)), columnName);
            }
        }
        protected async Task<object> TryGetValueAsync (IDataRecord data, Type propType)
        {
            string columnName = Columns.Where(c => data.GetOrdinal(c) >= 0).FirstOrDefault();
            try
            {
                if (string.IsNullOrWhiteSpace(columnName))
                {
                    await Task.CompletedTask;
                    return _defaultValue;
                }
                return await data.GetColumnAsync(columnName, propType, BufferSize, Delimiter, _defaultValue);                
            }
            catch (Exception e)
            {
                 return await DbDataModelBindingError.CreateAsync(e, propType, data.GetFieldType(data.GetOrdinal(columnName)), columnName);
            }
        }

        public virtual object GetValue(IDataRecord data, Type propType)
        => TryGetValue(data, propType);
        public virtual async Task<object> GetValueAsync(IDataRecord data, Type propType)
        => await TryGetValueAsync(data, propType);
    }
}