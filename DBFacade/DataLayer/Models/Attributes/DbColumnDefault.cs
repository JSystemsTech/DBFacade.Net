using System;

namespace DBFacade.DataLayer.Models.Attributes
{
    public class DbColumnDefault : DbColumn
    {

        private DbColumnDefault(string name, object defaultValue) : base(name, defaultValue) { }
        private DbColumnDefault(Type dBMethodType, string name, object defaultValue) : base(dBMethodType, name, defaultValue) { }
        
        public sealed class Boolean : DbColumnDefault
        {
            public Boolean(string name, bool defaultValue) : base(name, defaultValue) { }           
            public Boolean(Type dbMethodType, string name, bool defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Byte : DbColumnDefault
        {
            public Byte(string name, byte defaultValue) : base(name, defaultValue) { }
            public Byte(Type dbMethodType, string name, byte defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Char : DbColumnDefault
        {
            public Char(string name, char defaultValue) : base(name, defaultValue) { }
            
            public Char(Type dbMethodType, string name, char defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class DateTime : DbColumnDefault
        {
            
            public DateTime(string name, System.DateTime defaultValue) : base(name, defaultValue) { }
            public DateTime(Type dbMethodType, string name, System.DateTime defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Decimal : DbColumnDefault
        {
            public Decimal(string name, decimal defaultValue) : base(name, defaultValue) { }
            public Decimal(Type dbMethodType, string name, decimal defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Double : DbColumnDefault
        {
            public Double(string name, double defaultValue) : base(name, defaultValue) { }
            public Double(Type dbMethodType, string name, double defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Float : DbColumnDefault
        {
            public Float(string name, float defaultValue) : base(name, defaultValue) { }
            public Float(Type dbMethodType, string name, float defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Guid : DbColumnDefault
        {
            public Guid(string name, Guid defaultValue) : base(name, defaultValue) { }
            public Guid(Type dbMethodType, string name, Guid defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Short : DbColumnDefault
        {
            public Short(string name, short defaultValue) : base(name, defaultValue) { }
            public Short(Type dbMethodType, string name, short defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Int : DbColumnDefault
        {
            public Int(string name, int defaultValue) : base(name, defaultValue) { }
            public Int(Type dbMethodType, string name, int defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Long : DbColumnDefault
        {
            public Long(string name, long defaultValue) : base(name, defaultValue) { }
            public Long(Type dbMethodType, string name, long defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class UShort : DbColumnDefault
        {
            public UShort(string name, ushort defaultValue) : base(name, defaultValue) { }
            public UShort(Type dbMethodType, string name, ushort defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class UInt : DbColumnDefault
        {
            public UInt(string name, uint defaultValue) : base(name, defaultValue) { }
            public UInt(Type dbMethodType, string name, uint defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class ULong : DbColumnDefault
        {
            public ULong(string name, ulong defaultValue) : base(name, defaultValue) { }
            public ULong(Type dbMethodType, string name, ulong defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class String : DbColumnDefault
        {
            public String(string name, string defaultValue) : base(name, defaultValue) { }
            public String(Type dbMethodType, string name, string defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
    }
}
