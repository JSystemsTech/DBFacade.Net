namespace DomainFacade.DataLayer.ConnectionService
{
    public abstract class DbCommandTextCore
    {
        public string CommandText { get; private set; }
        public DbCommandTextCore(string commandText)
        {
            CommandText = commandText;
        }
    }
    public sealed class DbCommandText<TConnection> : DbCommandTextCore where TConnection : DbConnectionConfig
    {
        public DbCommandText(string commandText) : base(commandText) { }

    }
}
