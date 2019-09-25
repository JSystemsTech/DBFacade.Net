using System;

namespace DBFacade.DataLayer.Models.Attributes
{
    public class DbColumnDefault : DbColumn
    {

        private DbColumnDefault(string name, object defaultValue) : base(name, defaultValue, DefaultDelimeter) { }
        private DbColumnDefault(Type TDbManifestMethodType, string name, object defaultValue) : base(TDbManifestMethodType, name, defaultValue,null, DefaultDelimeter) { }
        
        public sealed class Boolean : DbColumnDefault
        {
            public Boolean(string name, bool defaultValue) : base(name, defaultValue) { }           
            public Boolean(Type TDbManifestMethodType, string name, bool defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        }
        public sealed class Byte : DbColumnDefault
        {
            public Byte(string name, byte defaultValue) : base(name, defaultValue) { }
            public Byte(Type TDbManifestMethodType, string name, byte defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        }
        public sealed class Char : DbColumnDefault
        {
            public Char(string name, char defaultValue) : base(name, defaultValue) { }
            
            public Char(Type TDbManifestMethodType, string name, char defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        }
        public sealed class DateTime : DbColumnDefault
        {
            
            public DateTime(string name, System.DateTime defaultValue) : base(name, defaultValue) { }
            public DateTime(Type TDbManifestMethodType, string name, System.DateTime defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        }
        public sealed class TimeSpan : DbColumnDefault
        {

            public TimeSpan(string name, System.TimeSpan defaultValue) : base(name, defaultValue) { }
            public TimeSpan(Type TDbManifestMethodType, string name, System.TimeSpan defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        }
        public sealed class Decimal : DbColumnDefault
        {
            public Decimal(string name, decimal defaultValue) : base(name, defaultValue) { }
            public Decimal(Type TDbManifestMethodType, string name, decimal defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        }
        public sealed class Double : DbColumnDefault
        {
            public Double(string name, double defaultValue) : base(name, defaultValue) { }
            public Double(Type TDbManifestMethodType, string name, double defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        }
        public sealed class Float : DbColumnDefault
        {
            public Float(string name, float defaultValue) : base(name, defaultValue) { }
            public Float(Type TDbManifestMethodType, string name, float defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        }
        public sealed class Guid : DbColumnDefault
        {
            public Guid(string name, Guid defaultValue) : base(name, defaultValue) { }
            public Guid(Type TDbManifestMethodType, string name, Guid defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        }
        public sealed class Short : DbColumnDefault
        {
            public Short(string name, short defaultValue) : base(name, defaultValue) { }
            public Short(Type TDbManifestMethodType, string name, short defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        }
        public sealed class Int : DbColumnDefault
        {
            public Int(string name, int defaultValue) : base(name, defaultValue) { }
            public Int(Type TDbManifestMethodType, string name, int defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        }
        public sealed class Long : DbColumnDefault
        {
            public Long(string name, long defaultValue) : base(name, defaultValue) { }
            public Long(Type TDbManifestMethodType, string name, long defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        }
        public sealed class UShort : DbColumnDefault
        {
            public UShort(string name, ushort defaultValue) : base(name, defaultValue) { }
            public UShort(Type TDbManifestMethodType, string name, ushort defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        }
        public sealed class UInt : DbColumnDefault
        {
            public UInt(string name, uint defaultValue) : base(name, defaultValue) { }
            public UInt(Type TDbManifestMethodType, string name, uint defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        }
        public sealed class ULong : DbColumnDefault
        {
            public ULong(string name, ulong defaultValue) : base(name, defaultValue) { }
            public ULong(Type TDbManifestMethodType, string name, ulong defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        }
        public sealed class String : DbColumnDefault
        {
            public String(string name, string defaultValue) : base(name, defaultValue) { }
            public String(Type TDbManifestMethodType, string name, string defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        }
    }
}
