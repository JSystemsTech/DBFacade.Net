using System.Data;
using System.Data.Common;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    internal class MockDbParameter : DbParameter
    {
        private DbType _DbType { get; set; }
        private ParameterDirection _Direction { get; set; }
        private bool _IsNullable { get; set; }
        private string _ParameterName { get; set; }
        private int _Size { get; set; }
        private string _SourceColumn { get; set; }
        private bool _SourceColumnNullMapping { get; set; }
        private object _Value { get; set; }

        public override DbType DbType { get => _DbType; set => _DbType=value; }
        public override ParameterDirection Direction { get => _Direction; set => _Direction = value; }
        public override bool IsNullable { get => _IsNullable; set => _IsNullable = value; }
        public override string ParameterName { get => _ParameterName; set => _ParameterName = value; }
        public string ParameterNameAsKey() => _ParameterName.Replace("@", "");
        public override int Size { get => _Size; set => _Size = value; }
        public override string SourceColumn { get => _SourceColumn; set => _SourceColumn = value; }
        public override bool SourceColumnNullMapping { get => _SourceColumnNullMapping; set => _SourceColumnNullMapping = value; }
        public override object Value { get => _Value; set => _Value = value; }

        public override void ResetDbType() => _DbType = DbType.Int32;
    }
}
