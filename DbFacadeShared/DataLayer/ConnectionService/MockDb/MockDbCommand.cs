using DbFacade.DataLayer.Models;
using System.Data;
using System.Data.Common;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    internal class MockDbCommand : DbCommand
    {
        private string _CommandText { get; set; }
        private int _CommandTimeout { get; set; }
        private CommandType _CommandType { get; set; }
        private bool _DesignTimeVisible { get; set; }
        private UpdateRowSource _UpdatedRowSource { get; set; }
        private DbConnection _DbConnection { get; set; }
        private DbParameterCollection _DbParameterCollection { get; set; }
        private DbTransaction _DbTransaction { get; set; }
        private IInternalDbParamsModel ParamsModel { get; set; }

        public override string CommandText { get => _CommandText; set => _CommandText = value; }
        public override int CommandTimeout { get => _CommandTimeout; set => _CommandTimeout = value; }
        public override CommandType CommandType { get => _CommandType; set => _CommandType = value; }
        public override bool DesignTimeVisible { get => _DesignTimeVisible; set => _DesignTimeVisible = value; }
        public override UpdateRowSource UpdatedRowSource { get => _UpdatedRowSource; set => _UpdatedRowSource = value; }
        protected override DbConnection DbConnection { get => _DbConnection; set => _DbConnection = value; }

        protected override DbParameterCollection DbParameterCollection => _DbParameterCollection;

        protected override DbTransaction DbTransaction { get => _DbTransaction; set => _DbTransaction = value; }

        public MockDbCommand(MockDbConnection dbConnection, IInternalDbParamsModel paramsModel)
        {
            _DbConnection = dbConnection;
            ParamsModel = paramsModel;
            _DbParameterCollection = new MockDbParameterCollection();
        }
         
        public override void Cancel() { }

        public override int ExecuteNonQuery() { SetResponse(); return 0; }

        public override object ExecuteScalar() { SetResponse(); return null; }

        public override void Prepare() { }

        protected override DbParameter CreateDbParameter()=> new MockDbParameter();
        private void SetResponse()
        {
            if(_DbParameterCollection is MockDbParameterCollection mockDbParameterCollection)
            {
                if(mockDbParameterCollection.GetReturnValueParam() is MockDbParameter param && param != null)
                {
                    param.Value = ParamsModel.ReturnValue;
                }
                if(ParamsModel.OutputValues != null)
                {                    
                    foreach (MockDbParameter outputParam in mockDbParameterCollection.GetOutputParams())
                    {
                        object value;
                        if (ParamsModel.OutputValues.TryGetValue(outputParam.ParameterNameAsKey(), out value))
                        {
                            outputParam.Value = value;
                        }
                    }
                }
            }
        }
        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            SetResponse();
            return ParamsModel.ResponseData;
        }
    }
}
