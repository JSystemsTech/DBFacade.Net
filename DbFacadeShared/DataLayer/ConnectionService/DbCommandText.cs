namespace DbFacade.DataLayer.ConnectionService
{
    public interface IDbCommandText
    {
        string Label { get; }
    }

    public interface IDbCommandText<TDbConnectionConfig> : IDbCommandText
        where TDbConnectionConfig : IDbConnectionConfig
    {
        string CommandText { get; }
    }

    internal abstract class DbCommandText : IDbCommandText
    {
        protected DbCommandText(string label)
        {
            Label = label;
        }

        public string Label { get; }
    }

    internal class DbCommandText<TDbConnectionConfig> : DbCommandText, IDbCommandText<TDbConnectionConfig>
        where TDbConnectionConfig : IDbConnectionConfig
    {
        public DbCommandText(string commandText, string label) : base(label)
        {
            CommandText = commandText;
        }

        public string CommandText { get; }
    }
}