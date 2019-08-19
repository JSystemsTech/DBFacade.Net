using System;
using System.Data;

namespace DBFacade.DataLayer.Models.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DbColumn" />
    public abstract class DbBufferedColumn : DbColumn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbBufferedColumn"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fieldOffset">The field offset.</param>
        /// <param name="bufferoffset">The bufferoffset.</param>
        /// <param name="length">The length.</param>
        public DbBufferedColumn(string name, long fieldOffset, int bufferoffset, int length) : base(name)
        {
            InitBufferColumn(fieldOffset, bufferoffset, length);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbBufferedColumn"/> class.
        /// </summary>
        /// <param name="TDbManifestMethodType">Type of the d b method.</param>
        /// <param name="name">The name.</param>
        /// <param name="fieldOffset">The field offset.</param>
        /// <param name="bufferoffset">The bufferoffset.</param>
        /// <param name="length">The length.</param>
        public DbBufferedColumn(Type TDbManifestMethodType, string name, long fieldOffset, int bufferoffset, int length) : base(TDbManifestMethodType, name)
        {
            InitBufferColumn(fieldOffset, bufferoffset, length);
        }

        /// <summary>
        /// Initializes the buffer column.
        /// </summary>
        /// <param name="fieldOffset">The field offset.</param>
        /// <param name="bufferoffset">The bufferoffset.</param>
        /// <param name="length">The length.</param>
        private void InitBufferColumn(long fieldOffset, int bufferoffset, int length)
        {
            FieldOffset = fieldOffset;
            BufferOffset = bufferoffset;
            Length = length;
        }
        /// <summary>
        /// The field offset
        /// </summary>
        private long FieldOffset;
        /// <summary>
        /// The buffer offset
        /// </summary>
        private int BufferOffset;
        /// <summary>
        /// The length
        /// </summary>
        private int Length;

        /// <summary>
        /// Gets the column value.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        protected override object GetColumnValue(IDataRecord data)
        {
            if (HasColumn(data) && !data.IsDBNull(GetOrdinal(data)))
            {
                return GetBufferedData(data, GetOrdinal(data));
            }
            return null;
        }
        /// <summary>
        /// Gets the buffered data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="columnOrdinal">The column ordinal.</param>
        /// <returns></returns>
        protected abstract long GetBufferedData(IDataRecord data, int columnOrdinal);
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DbBufferedColumn" />
        public abstract class BytesBase : DbBufferedColumn
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="BytesBase"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="fieldOffset">The field offset.</param>
            /// <param name="buffer">The buffer.</param>
            /// <param name="bufferoffset">The bufferoffset.</param>
            /// <param name="length">The length.</param>
            public BytesBase(string name, long fieldOffset, byte[] buffer, int bufferoffset, int length) : base(name, fieldOffset, bufferoffset, length) { Buffer = buffer; }

            /// <summary>
            /// Initializes a new instance of the <see cref="BytesBase"/> class.
            /// </summary>
            /// <param name="TDbManifestMethodType">Type of the d b method.</param>
            /// <param name="name">The name.</param>
            /// <param name="fieldOffset">The field offset.</param>
            /// <param name="buffer">The buffer.</param>
            /// <param name="bufferoffset">The bufferoffset.</param>
            /// <param name="length">The length.</param>
            public BytesBase(Type TDbManifestMethodType, string name, long fieldOffset, byte[] buffer, int bufferoffset, int length) : base(TDbManifestMethodType, name, fieldOffset, bufferoffset, length) { Buffer = buffer; }

            /// <summary>
            /// The buffer
            /// </summary>
            private byte[] Buffer;
            /// <summary>
            /// Gets the buffered data.
            /// </summary>
            /// <param name="data">The data.</param>
            /// <param name="columnOrdinal">The column ordinal.</param>
            /// <returns></returns>
            protected override long GetBufferedData(IDataRecord data, int columnOrdinal)
            {
                return data.GetBytes(columnOrdinal, FieldOffset, this.Buffer, BufferOffset, Length);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="BytesBase" />
        public sealed class Bytes : BytesBase
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Bytes"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="fieldOffset">The field offset.</param>
            /// <param name="buffer">The buffer.</param>
            /// <param name="bufferoffset">The bufferoffset.</param>
            /// <param name="length">The length.</param>
            public Bytes(string name, long fieldOffset, byte[] buffer, int bufferoffset, int length) : base(name, fieldOffset, buffer, bufferoffset, length) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="Bytes"/> class.
            /// </summary>
            /// <param name="TDbManifestMethodType">Type of the d b method.</param>
            /// <param name="name">The name.</param>
            /// <param name="fieldOffset">The field offset.</param>
            /// <param name="buffer">The buffer.</param>
            /// <param name="bufferoffset">The bufferoffset.</param>
            /// <param name="length">The length.</param>
            public Bytes(Type TDbManifestMethodType, string name, long fieldOffset, byte[] buffer, int bufferoffset, int length) : base(TDbManifestMethodType, name, fieldOffset, buffer, bufferoffset, length) { }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DbBufferedColumn" />
        public sealed class Chars : DbBufferedColumn
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Chars"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="fieldOffset">The field offset.</param>
            /// <param name="buffer">The buffer.</param>
            /// <param name="bufferoffset">The bufferoffset.</param>
            /// <param name="length">The length.</param>
            public Chars(string name, long fieldOffset, char[] buffer, int bufferoffset, int length) : base(name, fieldOffset, bufferoffset, length) { Buffer = buffer; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Chars"/> class.
            /// </summary>
            /// <param name="TDbManifestMethodType">Type of the d b method.</param>
            /// <param name="name">The name.</param>
            /// <param name="fieldOffset">The field offset.</param>
            /// <param name="buffer">The buffer.</param>
            /// <param name="bufferoffset">The bufferoffset.</param>
            /// <param name="length">The length.</param>
            public Chars(Type TDbManifestMethodType, string name, long fieldOffset, char[] buffer, int bufferoffset, int length) : base(TDbManifestMethodType, name, fieldOffset, bufferoffset, length) { Buffer = buffer; }

            /// <summary>
            /// The buffer
            /// </summary>
            private char[] Buffer;
            /// <summary>
            /// Gets the buffered data.
            /// </summary>
            /// <param name="data">The data.</param>
            /// <param name="columnOrdinal">The column ordinal.</param>
            /// <returns></returns>
            protected override long GetBufferedData(IDataRecord data, int columnOrdinal)
            {
                return data.GetChars(columnOrdinal, FieldOffset, Buffer, BufferOffset, Length);
            }
        }
    }
}
