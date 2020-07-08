using System;
using System.Data;
using System.Web.UI.WebControls;
using DBFacade.DataLayer.Models;
using DBFacade.Factories;

namespace DBFacade.DataLayer.CommandConfig.Parameters
{
    /// <summary></summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    internal partial class DbCommandParameterConfig<TDbParams> : IInternalDbCommandParameterConfig<TDbParams>
        where TDbParams : IDbParamsModel
    {
        private class DbCommandOutputParameterConfig : DbCommandParameterConfig<TDbParams>
        {
            public DbCommandOutputParameterConfig(DbType dbType) : base(dbType, true)
            {
                IsOutput = true;
            }

        }        
        
        internal IDbCommandParameterConfig<TDbParams> OutputByte()
        => new DbCommandOutputParameterConfig(DbType.Byte);
        

        internal IDbCommandParameterConfig<TDbParams> OutputSByte()
        => new DbCommandOutputParameterConfig(DbType.SByte);
        

        internal IDbCommandParameterConfig<TDbParams> OutputInt16()
        => new DbCommandOutputParameterConfig(DbType.Int16);
        

        internal IDbCommandParameterConfig<TDbParams> OutputInt32()
        => new DbCommandOutputParameterConfig(DbType.Int32);
        

        internal IDbCommandParameterConfig<TDbParams> OutputInt64()
        => new DbCommandOutputParameterConfig(DbType.Int64);
        

        internal IDbCommandParameterConfig<TDbParams> OutputUInt16()
        => new DbCommandOutputParameterConfig(DbType.UInt16);
        

        internal IDbCommandParameterConfig<TDbParams> OutputUInt32()
        => new DbCommandOutputParameterConfig(DbType.UInt32);
        

        internal IDbCommandParameterConfig<TDbParams> OutputUInt64()
        =>new DbCommandOutputParameterConfig(DbType.UInt64);
        

        internal IDbCommandParameterConfig<TDbParams> OutputSingle()
        =>new DbCommandOutputParameterConfig(DbType.Single);
        

        internal IDbCommandParameterConfig<TDbParams> OutputDouble()
        => new DbCommandOutputParameterConfig(DbType.Double);
        

        internal IDbCommandParameterConfig<TDbParams> OutputDecimal()
        => new DbCommandOutputParameterConfig(DbType.Decimal);
        

        internal IDbCommandParameterConfig<TDbParams> OutputBoolean()
        => new DbCommandOutputParameterConfig(DbType.Boolean);
        

        internal IDbCommandParameterConfig<TDbParams> OutputGuid()
        => new DbCommandOutputParameterConfig(DbType.Guid);
        

        internal IDbCommandParameterConfig<TDbParams> OutputTimeSpan()
        =>new DbCommandOutputParameterConfig(DbType.Time);
        

        internal IDbCommandParameterConfig<TDbParams> OutputDateTime()
        =>new DbCommandOutputParameterConfig(DbType.DateTime);
        

        internal IDbCommandParameterConfig<TDbParams> OutputDateTime2()
        =>new DbCommandOutputParameterConfig(DbType.DateTime2);
        

        internal IDbCommandParameterConfig<TDbParams> OutputDateTimeOffset()
        => new DbCommandOutputParameterConfig(DbType.DateTimeOffset);
        

        internal IDbCommandParameterConfig<TDbParams> OutputBinary()
        => new DbCommandOutputParameterConfig(DbType.Binary);
        

        internal IDbCommandParameterConfig<TDbParams> OutputChar()
        => new DbCommandOutputParameterConfig(DbType.StringFixedLength);
        

        internal IDbCommandParameterConfig<TDbParams> OutputString()
        => new DbCommandOutputParameterConfig(DbType.String);
        

        internal IDbCommandParameterConfig<TDbParams> OutputCharArray()
        => new DbCommandOutputParameterConfig(DbType.String);
        

        internal IDbCommandParameterConfig<TDbParams> OutputAnsiStringFixedLength()
        => new DbCommandOutputParameterConfig(DbType.AnsiStringFixedLength);
        

        internal IDbCommandParameterConfig<TDbParams> OutputAnsiString()
        => new DbCommandOutputParameterConfig(DbType.AnsiString);
        

        internal IDbCommandParameterConfig<TDbParams>OutputXml()=> new DbCommandOutputParameterConfig(DbType.Xml);
        
        
    }
}