namespace DBFacade.DataLayer.ConnectionService
{
    internal abstract class DbCommandText : IDbCommandText
    {
        private string LabelValue { get; set; }
        public DbCommandText(string label)
        {
            LabelValue = label;
        }
        public string Label() => LabelValue;
    }
    public interface IDbCommandText
    {
        string Label();
    }
    public interface IDbCommandText<TConnection> : IDbCommandText where TConnection : IDbConnectionConfig {
        string CommandText();
    }
    
    internal class DbCommandText<TConnection> : DbCommandText, IDbCommandText<TConnection> where TConnection : IDbConnectionConfig
    {
        private string CommandTextValue { get; set; }
        public DbCommandText(string commandText, string label) : base(label) { CommandTextValue = commandText; }

        public string CommandText() => CommandTextValue;
    }
}
