namespace DBFacade.DataLayer.ConnectionService
{
    
    public interface IDbCommandText
    {
        string Label { get; }
    }
    public interface IDbCommandText<TDbConnectionConfig> : IDbCommandText where TDbConnectionConfig : IDbConnectionConfig {
        string CommandText { get; }
    }
    internal abstract class DbCommandText : IDbCommandText
    {
        public string Label { get; private set; }
        public DbCommandText(string label)
        {
            Label = label;
        }
    }
    internal class DbCommandText<TDbConnectionConfig> : DbCommandText, IDbCommandText<TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig
    {
        public string CommandText { get; private set; }
        public DbCommandText(string commandText, string label) : base(label) { CommandText = commandText; }
    }
}
