using System.Data;
using System.Data.Common;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    /// <summary>
    /// 
    /// </summary>
    internal class MockDbParameter : DbParameter
    {
        /// <summary>
        /// Gets or sets the type of the database.
        /// </summary>
        /// <value>
        /// The type of the database.
        /// </value>
        private DbType _DbType { get; set; }
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        private ParameterDirection _Direction { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is nullable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is nullable; otherwise, <c>false</c>.
        /// </value>
        private bool _IsNullable { get; set; }
        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        /// <value>
        /// The name of the parameter.
        /// </value>
        private string _ParameterName { get; set; }
        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        private int _Size { get; set; }
        /// <summary>
        /// Gets or sets the source column.
        /// </summary>
        /// <value>
        /// The source column.
        /// </value>
        private string _SourceColumn { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [source column null mapping].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [source column null mapping]; otherwise, <c>false</c>.
        /// </value>
        private bool _SourceColumnNullMapping { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        private object _Value { get; set; }

        /// <summary>
        /// Gets or sets the type of the database.
        /// </summary>
        /// <value>
        /// The type of the database.
        /// </value>
        public override DbType DbType { get => _DbType; set => _DbType=value; }
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public override ParameterDirection Direction { get => _Direction; set => _Direction = value; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is nullable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is nullable; otherwise, <c>false</c>.
        /// </value>
        public override bool IsNullable { get => _IsNullable; set => _IsNullable = value; }
        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        /// <value>
        /// The name of the parameter.
        /// </value>
        public override string ParameterName { get => _ParameterName; set => _ParameterName = value; }
        /// <summary>
        /// Parameters the name as key.
        /// </summary>
        /// <returns></returns>
        public string ParameterNameAsKey() => _ParameterName.Replace("@", "");
        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public override int Size { get => _Size; set => _Size = value; }
        /// <summary>
        /// Gets or sets the source column.
        /// </summary>
        /// <value>
        /// The source column.
        /// </value>
        public override string SourceColumn { get => _SourceColumn; set => _SourceColumn = value; }
        /// <summary>
        /// Gets or sets a value indicating whether [source column null mapping].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [source column null mapping]; otherwise, <c>false</c>.
        /// </value>
        public override bool SourceColumnNullMapping { get => _SourceColumnNullMapping; set => _SourceColumnNullMapping = value; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public override object Value { get => _Value; set => _Value = value; }

        /// <summary>
        /// Resets the type of the database.
        /// </summary>
        public override void ResetDbType() => _DbType = DbType.Int32;
    }
}
