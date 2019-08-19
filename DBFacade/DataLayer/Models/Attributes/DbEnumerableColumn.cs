using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DBFacade.DataLayer.Models.Attributes
{
    public abstract class DbEnumerableColumn : DbColumn
    {
        public DbEnumerableColumn(string name) : base(name) { }
        public DbEnumerableColumn(Type TDbManifestMethodType, string name) : base(TDbManifestMethodType, name) { }
        public DbEnumerableColumn(string name, char delimeter) : base(name, delimeter) { }
        public DbEnumerableColumn(Type TDbManifestMethodType, string name, char delimeter) : base(TDbManifestMethodType, name, delimeter) { }
        
        
        protected override object GetColumnValue(IDataRecord data, Type propType)
        {
            string[] value = GetValue<string[]>(data);
            if (value != null)
            {
                return GetEnumerable(value, propType);
            }
            return null;
        }
        protected abstract IEnumerable GetEnumerable(string[] values, Type propType);
        
        public sealed class Array : DbEnumerableColumn
        {
            
            public Array(string name) : base(name) { }
            public Array(Type TDbManifestMethodType, string name) : base(TDbManifestMethodType, name) { }
            
            public Array(string name, char delimeter) : base(name, delimeter) { }
            
            
            protected override IEnumerable GetEnumerable(string[] values, Type propType)
            {
                if (propType == typeof(string[]))
                {
                    return values;
                }
                else if (propType == typeof(short[]))
                {
                    return System.Array.ConvertAll(values, short.Parse);
                }
                else if (propType == typeof(int[]))
                {
                    return System.Array.ConvertAll(values, int.Parse);
                }
                else if (propType == typeof(long[]))
                {
                    return System.Array.ConvertAll(values, long.Parse);
                }
                else if (propType == typeof(double[]))
                {
                    return System.Array.ConvertAll(values, double.Parse);
                }
                else if (propType == typeof(float[]))
                {
                    return System.Array.ConvertAll(values, float.Parse);
                }
                else if (propType == typeof(decimal[]))
                {
                    return System.Array.ConvertAll(values, decimal.Parse);
                }
                else
                {
                    return null;
                }
            }
        }
        public sealed class List : DbEnumerableColumn
        {
            
            public List(string name) : base(name) { }
            
            public List(Type TDbManifestMethodType, string name) : base(TDbManifestMethodType, name) { }
            
            public List(string name, char delimeter) : base(name, delimeter) { }
            
            public List(Type TDbManifestMethodType, string name, char delimeter) : base(TDbManifestMethodType, name, delimeter) { }
            
            
            protected override IEnumerable GetEnumerable(string[] values, Type propType)
            {
                if (propType == typeof(List<string>))
                {
                    return values.ToList();
                }
                else if (propType == typeof(List<short>))
                {
                    return System.Array.ConvertAll(values, short.Parse).ToList();
                }
                else if (propType == typeof(List<int>))
                {
                    return System.Array.ConvertAll(values, int.Parse).ToList();
                }
                else if (propType == typeof(List<long>))
                {
                    return System.Array.ConvertAll(values, long.Parse).ToList();
                }
                else if (propType == typeof(List<double>))
                {
                    return System.Array.ConvertAll(values, double.Parse).ToList();
                }
                else if (propType == typeof(List<float>))
                {
                    return System.Array.ConvertAll(values, float.Parse).ToList();
                }
                else if (propType == typeof(List<decimal>))
                {
                    return System.Array.ConvertAll(values, decimal.Parse).ToList();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
