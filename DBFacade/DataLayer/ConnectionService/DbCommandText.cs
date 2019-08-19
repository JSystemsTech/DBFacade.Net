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
    public interface IDbCommandText<TDbConnectionConfig> : IDbCommandText where TDbConnectionConfig : IDbConnectionConfig {
        string CommandText();
    }
    
    internal class DbCommandText<TDbConnectionConfig> : DbCommandText, IDbCommandText<TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig
    {
        private string CommandTextValue { get; set; }
        public DbCommandText(string commandText, string label) : base(label) { CommandTextValue = commandText; }

        public string CommandText() => CommandTextValue;
    }
}
