using System;

namespace DbFacade.DataLayer.Models.Attributes
{
    public class DbColumnDefault : DbColumn
    {
        private DbColumnDefault(string name, object defaultValue) : base(name, defaultValue)
        {
        }

        private DbColumnDefault(Type TDbMethodManifestMethodType, string name, object defaultValue) : base(
            TDbMethodManifestMethodType, name, defaultValue, null)
        {
        }

        public sealed class Boolean : DbColumnDefault
        {
            public Boolean(string name, bool defaultValue) : base(name, defaultValue)
            {
            }

            public Boolean(Type TDbMethodManifestMethodType, string name, bool defaultValue) : base(
                TDbMethodManifestMethodType, name, defaultValue)
            {
            }
        }

        public sealed class Byte : DbColumnDefault
        {
            public Byte(string name, byte defaultValue) : base(name, defaultValue)
            {
            }

            public Byte(Type TDbMethodManifestMethodType, string name, byte defaultValue) : base(
                TDbMethodManifestMethodType, name, defaultValue)
            {
            }
        }

        public sealed class Char : DbColumnDefault
        {
            public Char(string name, char defaultValue) : base(name, defaultValue)
            {
            }

            public Char(Type TDbMethodManifestMethodType, string name, char defaultValue) : base(
                TDbMethodManifestMethodType, name, defaultValue)
            {
            }
        }

        public sealed class DateTime : DbColumnDefault
        {
            public DateTime(string name, System.DateTime defaultValue) : base(name, defaultValue)
            {
            }

            public DateTime(Type TDbMethodManifestMethodType, string name, System.DateTime defaultValue) : base(
                TDbMethodManifestMethodType, name, defaultValue)
            {
            }
        }

        public sealed class TimeSpan : DbColumnDefault
        {
            public TimeSpan(string name, System.TimeSpan defaultValue) : base(name, defaultValue)
            {
            }

            public TimeSpan(Type TDbMethodManifestMethodType, string name, System.TimeSpan defaultValue) : base(
                TDbMethodManifestMethodType, name, defaultValue)
            {
            }
        }

        public sealed class Decimal : DbColumnDefault
        {
            public Decimal(string name, decimal defaultValue) : base(name, defaultValue)
            {
            }

            public Decimal(Type TDbMethodManifestMethodType, string name, decimal defaultValue) : base(
                TDbMethodManifestMethodType, name, defaultValue)
            {
            }
        }

        public sealed class Double : DbColumnDefault
        {
            public Double(string name, double defaultValue) : base(name, defaultValue)
            {
            }

            public Double(Type TDbMethodManifestMethodType, string name, double defaultValue) : base(
                TDbMethodManifestMethodType, name, defaultValue)
            {
            }
        }

        public sealed class Float : DbColumnDefault
        {
            public Float(string name, float defaultValue) : base(name, defaultValue)
            {
            }

            public Float(Type TDbMethodManifestMethodType, string name, float defaultValue) : base(
                TDbMethodManifestMethodType, name, defaultValue)
            {
            }
        }

        public sealed class Guid : DbColumnDefault
        {
            public Guid(string name, Guid defaultValue) : base(name, defaultValue)
            {
            }

            public Guid(Type TDbMethodManifestMethodType, string name, Guid defaultValue) : base(
                TDbMethodManifestMethodType, name, defaultValue)
            {
            }
        }

        public sealed class Short : DbColumnDefault
        {
            public Short(string name, short defaultValue) : base(name, defaultValue)
            {
            }

            public Short(Type TDbMethodManifestMethodType, string name, short defaultValue) : base(
                TDbMethodManifestMethodType, name, defaultValue)
            {
            }
        }

        public sealed class Int : DbColumnDefault
        {
            public Int(string name, int defaultValue) : base(name, defaultValue)
            {
            }

            public Int(Type TDbMethodManifestMethodType, string name, int defaultValue) : base(
                TDbMethodManifestMethodType, name, defaultValue)
            {
            }
        }

        public sealed class Long : DbColumnDefault
        {
            public Long(string name, long defaultValue) : base(name, defaultValue)
            {
            }

            public Long(Type TDbMethodManifestMethodType, string name, long defaultValue) : base(
                TDbMethodManifestMethodType, name, defaultValue)
            {
            }
        }

        public sealed class UShort : DbColumnDefault
        {
            public UShort(string name, ushort defaultValue) : base(name, defaultValue)
            {
            }

            public UShort(Type TDbMethodManifestMethodType, string name, ushort defaultValue) : base(
                TDbMethodManifestMethodType, name, defaultValue)
            {
            }
        }

        public sealed class UInt : DbColumnDefault
        {
            public UInt(string name, uint defaultValue) : base(name, defaultValue)
            {
            }

            public UInt(Type TDbMethodManifestMethodType, string name, uint defaultValue) : base(
                TDbMethodManifestMethodType, name, defaultValue)
            {
            }
        }

        public sealed class ULong : DbColumnDefault
        {
            public ULong(string name, ulong defaultValue) : base(name, defaultValue)
            {
            }

            public ULong(Type TDbMethodManifestMethodType, string name, ulong defaultValue) : base(
                TDbMethodManifestMethodType, name, defaultValue)
            {
            }
        }

        public sealed class String : DbColumnDefault
        {
            public String(string name, string defaultValue) : base(name, defaultValue)
            {
            }

            public String(Type TDbMethodManifestMethodType, string name, string defaultValue) : base(
                TDbMethodManifestMethodType, name, defaultValue)
            {
            }
        }
    }
}