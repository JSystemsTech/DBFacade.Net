using System;
using System.Data;

namespace DomainFacade.DataLayer.Models.Attributes
{
    public abstract class DbBufferedColumn : DbColumn
    {
        public DbBufferedColumn(string name, long fieldOffset, int bufferoffset, int length) : base(name)
        {
            InitBufferColumn(fieldOffset, bufferoffset, length);
        }
        public DbBufferedColumn(Type dBMethodType, string name, long fieldOffset, int bufferoffset, int length) : base(dBMethodType, name)
        {
            InitBufferColumn(fieldOffset, bufferoffset, length);
        }

        private void InitBufferColumn(long fieldOffset, int bufferoffset, int length)
        {
            FieldOffset = fieldOffset;
            BufferOffset = bufferoffset;
            Length = length;
        }
        private long FieldOffset;
        private int BufferOffset;
        private int Length;

        protected override object GetColumnValue(IDataRecord data)
        {
            if (HasColumn(data) && !data.IsDBNull(GetOrdinal(data)))
            {
                return GetBufferedData(data, GetOrdinal(data));
            }
            return null;
        }
        protected abstract long GetBufferedData(IDataRecord data, int columnOrdinal);
        public abstract class BytesBase : DbBufferedColumn
        {
            public BytesBase(string name, long fieldOffset, byte[] buffer, int bufferoffset, int length) : base(name, fieldOffset, bufferoffset, length) { Buffer = buffer; }

            public BytesBase(Type dBMethodType, string name, long fieldOffset, byte[] buffer, int bufferoffset, int length) : base(dBMethodType, name, fieldOffset, bufferoffset, length) { Buffer = buffer; }

            private byte[] Buffer;
            protected override long GetBufferedData(IDataRecord data, int columnOrdinal)
            {
                return data.GetBytes(columnOrdinal, FieldOffset, this.Buffer, BufferOffset, Length);
            }
        }
        public sealed class Bytes : BytesBase
        {
            public Bytes(string name, long fieldOffset, byte[] buffer, int bufferoffset, int length) : base(name, fieldOffset, buffer, bufferoffset, length) { }

            public Bytes(Type dBMethodType, string name, long fieldOffset, byte[] buffer, int bufferoffset, int length) : base(dBMethodType, name, fieldOffset, buffer, bufferoffset, length) { }

        }
        public sealed class Chars : DbBufferedColumn
        {
            public Chars(string name, long fieldOffset, char[] buffer, int bufferoffset, int length) : base(name, fieldOffset, bufferoffset, length) { Buffer = buffer; }

            public Chars(Type dBMethodType, string name, long fieldOffset, char[] buffer, int bufferoffset, int length) : base(dBMethodType, name, fieldOffset, bufferoffset, length) { Buffer = buffer; }

            private char[] Buffer;
            protected override long GetBufferedData(IDataRecord data, int columnOrdinal)
            {
                return data.GetChars(columnOrdinal, FieldOffset, Buffer, BufferOffset, Length);
            }
        }
    }
}
