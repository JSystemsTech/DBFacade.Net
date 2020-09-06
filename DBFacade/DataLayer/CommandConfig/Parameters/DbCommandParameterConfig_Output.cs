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
            public DbCommandOutputParameterConfig(DbType dbType, int size) : base(dbType, true)
            {
                IsOutput = true;
                OutputSize = size;
            }

        }        
        internal IDbCommandParameterConfig<TDbParams> OutputByte(int size)
        => new DbCommandOutputParameterConfig(DbType.Byte, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputSByte(int size)
        => new DbCommandOutputParameterConfig(DbType.SByte, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputInt16(int size)
        => new DbCommandOutputParameterConfig(DbType.Int16, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputInt32(int size)
        => new DbCommandOutputParameterConfig(DbType.Int32, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputInt64(int size)
        => new DbCommandOutputParameterConfig(DbType.Int64, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputUInt16(int size)
        => new DbCommandOutputParameterConfig(DbType.UInt16, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputUInt32(int size)
        => new DbCommandOutputParameterConfig(DbType.UInt32, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputUInt64(int size)
        =>new DbCommandOutputParameterConfig(DbType.UInt64, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputSingle(int size)
        =>new DbCommandOutputParameterConfig(DbType.Single, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputDouble(int size)
        => new DbCommandOutputParameterConfig(DbType.Double, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputDecimal(int size)
        => new DbCommandOutputParameterConfig(DbType.Decimal, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputBoolean(int size)
        => new DbCommandOutputParameterConfig(DbType.Boolean, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputGuid(int size)
        => new DbCommandOutputParameterConfig(DbType.Guid, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputTimeSpan(int size)
        =>new DbCommandOutputParameterConfig(DbType.Time, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputDateTime(int size)
        =>new DbCommandOutputParameterConfig(DbType.DateTime, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputDateTime2(int size)
        =>new DbCommandOutputParameterConfig(DbType.DateTime2, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputDateTimeOffset(int size)
        => new DbCommandOutputParameterConfig(DbType.DateTimeOffset, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputBinary(int size)
        => new DbCommandOutputParameterConfig(DbType.Binary, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputChar(int size)
        => new DbCommandOutputParameterConfig(DbType.StringFixedLength, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputString(int size)
        => new DbCommandOutputParameterConfig(DbType.String, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputCharArray(int size)
        => new DbCommandOutputParameterConfig(DbType.String, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputAnsiStringFixedLength(int size)
        => new DbCommandOutputParameterConfig(DbType.AnsiStringFixedLength, size);
        

        internal IDbCommandParameterConfig<TDbParams> OutputAnsiString(int size)
        => new DbCommandOutputParameterConfig(DbType.AnsiString, size);
        

        internal IDbCommandParameterConfig<TDbParams>OutputXml(int size)
            => new DbCommandOutputParameterConfig(DbType.Xml, size);
        
        
    }
}