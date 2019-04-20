using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DBFacade.DataLayer.Models.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DbColumn" />
    public abstract class DbEnumerableColumn : DbColumn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbEnumerableColumn"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public DbEnumerableColumn(string name) : base(name) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbEnumerableColumn"/> class.
        /// </summary>
        /// <param name="dBMethodType">Type of the d b method.</param>
        /// <param name="name">The name.</param>
        public DbEnumerableColumn(Type dBMethodType, string name) : base(dBMethodType, name) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbEnumerableColumn"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="delimeter">The delimeter.</param>
        public DbEnumerableColumn(string name, char delimeter) : base(name, delimeter) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbEnumerableColumn"/> class.
        /// </summary>
        /// <param name="dBMethodType">Type of the d b method.</param>
        /// <param name="name">The name.</param>
        /// <param name="delimeter">The delimeter.</param>
        public DbEnumerableColumn(Type dBMethodType, string name, char delimeter) : base(dBMethodType, name, delimeter) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbEnumerableColumn"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        public DbEnumerableColumn(string name, object defaultValue) : base(name, defaultValue) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbEnumerableColumn"/> class.
        /// </summary>
        /// <param name="dBMethodType">Type of the d b method.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        public DbEnumerableColumn(Type dBMethodType, string name, object defaultValue) : base(dBMethodType, name, defaultValue) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbEnumerableColumn"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="delimeter">The delimeter.</param>
        public DbEnumerableColumn(string name, object defaultValue, char delimeter) : base(name, defaultValue, delimeter) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbEnumerableColumn"/> class.
        /// </summary>
        /// <param name="dBMethodType">Type of the d b method.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="delimeter">The delimeter.</param>
        public DbEnumerableColumn(Type dBMethodType, string name, object defaultValue, char delimeter) : base(dBMethodType, name, defaultValue, delimeter) { }


        /// <summary>
        /// Gets the column value.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="propType">Type of the property.</param>
        /// <returns></returns>
        protected override object GetColumnValue(IDataRecord data, Type propType)
        {
            string[] value = GetValue<string[]>(data);
            if (value != null)
            {
                return GetEnumerable(value, propType);
            }
            return null;
        }
        /// <summary>
        /// Gets the enumerable.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="propType">Type of the property.</param>
        /// <returns></returns>
        protected abstract IEnumerable GetEnumerable(string[] values, Type propType);

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DbEnumerableColumn" />
        public sealed class Array : DbEnumerableColumn
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Array"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            public Array(string name) : base(name) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="Array"/> class.
            /// </summary>
            /// <param name="dBMethodType">Type of the d b method.</param>
            /// <param name="name">The name.</param>
            public Array(Type dBMethodType, string name) : base(dBMethodType, name) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="Array"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="delimeter">The delimeter.</param>
            public Array(string name, char delimeter) : base(name, delimeter) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="Array"/> class.
            /// </summary>
            /// <param name="dBMethodType">Type of the d b method.</param>
            /// <param name="name">The name.</param>
            /// <param name="delimeter">The delimeter.</param>
            public Array(Type dBMethodType, string name, char delimeter) : base(dBMethodType, name, delimeter) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="Array"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="defaultValue">The default value.</param>
            public Array(string name, object defaultValue) : base(name, defaultValue) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="Array"/> class.
            /// </summary>
            /// <param name="dBMethodType">Type of the d b method.</param>
            /// <param name="name">The name.</param>
            /// <param name="defaultValue">The default value.</param>
            public Array(Type dBMethodType, string name, object defaultValue) : base(dBMethodType, name, defaultValue) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="Array"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <param name="delimeter">The delimeter.</param>
            public Array(string name, object defaultValue, char delimeter) : base(name, defaultValue, delimeter) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="Array"/> class.
            /// </summary>
            /// <param name="dBMethodType">Type of the d b method.</param>
            /// <param name="name">The name.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <param name="delimeter">The delimeter.</param>
            public Array(Type dBMethodType, string name, object defaultValue, char delimeter) : base(dBMethodType, name, defaultValue, delimeter) { }



            /// <summary>
            /// Gets the enumerable.
            /// </summary>
            /// <param name="values">The values.</param>
            /// <param name="propType">Type of the property.</param>
            /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DbEnumerableColumn" />
        public sealed class List : DbEnumerableColumn
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="List"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            public List(string name) : base(name) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="List"/> class.
            /// </summary>
            /// <param name="dBMethodType">Type of the d b method.</param>
            /// <param name="name">The name.</param>
            public List(Type dBMethodType, string name) : base(dBMethodType, name) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="List"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="delimeter">The delimeter.</param>
            public List(string name, char delimeter) : base(name, delimeter) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="List"/> class.
            /// </summary>
            /// <param name="dBMethodType">Type of the d b method.</param>
            /// <param name="name">The name.</param>
            /// <param name="delimeter">The delimeter.</param>
            public List(Type dBMethodType, string name, char delimeter) : base(dBMethodType, name, delimeter) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="List"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="defaultValue">The default value.</param>
            public List(string name, object defaultValue) : base(name, defaultValue) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="List"/> class.
            /// </summary>
            /// <param name="dBMethodType">Type of the d b method.</param>
            /// <param name="name">The name.</param>
            /// <param name="defaultValue">The default value.</param>
            public List(Type dBMethodType, string name, object defaultValue) : base(dBMethodType, name, defaultValue) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="List"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <param name="delimeter">The delimeter.</param>
            public List(string name, object defaultValue, char delimeter) : base(name, defaultValue, delimeter) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="List"/> class.
            /// </summary>
            /// <param name="dBMethodType">Type of the d b method.</param>
            /// <param name="name">The name.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <param name="delimeter">The delimeter.</param>
            public List(Type dBMethodType, string name, object defaultValue, char delimeter) : base(dBMethodType, name, defaultValue, delimeter) { }



            /// <summary>
            /// Gets the enumerable.
            /// </summary>
            /// <param name="values">The values.</param>
            /// <param name="propType">Type of the property.</param>
            /// <returns></returns>
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
