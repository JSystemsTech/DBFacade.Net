using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DomainFacade.DataLayer.Models.Attributes
{
    public abstract class DbEnumerableColumn : DbColumn
    {
        public DbEnumerableColumn(string name) : base(name) { }
        public DbEnumerableColumn(Type dBMethodType, string name) : base(dBMethodType, name) { }
        public DbEnumerableColumn(string name, char delimeter) : base(name, delimeter) { }
        public DbEnumerableColumn(Type dBMethodType, string name, char delimeter) : base(dBMethodType, name, delimeter) { }
        public DbEnumerableColumn(string name, object defaultValue) : base(name, defaultValue) { }
        public DbEnumerableColumn(Type dBMethodType, string name, object defaultValue) : base(dBMethodType, name, defaultValue) { }
        public DbEnumerableColumn(string name, object defaultValue, char delimeter) : base(name, defaultValue, delimeter) { }
        public DbEnumerableColumn(Type dBMethodType, string name, object defaultValue, char delimeter) : base(dBMethodType, name, defaultValue, delimeter) { }


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
            public Array(Type dBMethodType, string name) : base(dBMethodType, name) { }
            public Array(string name, char delimeter) : base(name, delimeter) { }
            public Array(Type dBMethodType, string name, char delimeter) : base(dBMethodType, name, delimeter) { }
            public Array(string name, object defaultValue) : base(name, defaultValue) { }
            public Array(Type dBMethodType, string name, object defaultValue) : base(dBMethodType, name, defaultValue) { }
            public Array(string name, object defaultValue, char delimeter) : base(name, defaultValue, delimeter) { }
            public Array(Type dBMethodType, string name, object defaultValue, char delimeter) : base(dBMethodType, name, defaultValue, delimeter) { }



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
            public List(Type dBMethodType, string name) : base(dBMethodType, name) { }
            public List(string name, char delimeter) : base(name, delimeter) { }
            public List(Type dBMethodType, string name, char delimeter) : base(dBMethodType, name, delimeter) { }
            public List(string name, object defaultValue) : base(name, defaultValue) { }
            public List(Type dBMethodType, string name, object defaultValue) : base(dBMethodType, name, defaultValue) { }
            public List(string name, object defaultValue, char delimeter) : base(name, defaultValue, delimeter) { }
            public List(Type dBMethodType, string name, object defaultValue, char delimeter) : base(dBMethodType, name, defaultValue, delimeter) { }



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
