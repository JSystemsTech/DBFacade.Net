namespace DomainFacade.DataLayer.ConnectionService
{

    internal abstract class DbCommandText: IDbCommandTextBase
    {
        private string CommandTextValue { get; set; }
        private string LabelValue { get; set; }
        public DbCommandText(string commandText, string label)
        {
            CommandTextValue = commandText;
            LabelValue = label;
        }
        public string CommandText()
        {
            return CommandTextValue;
        }

        public string Label()
        {
            return LabelValue;
        }
    }
    public interface IDbCommandTextBase
    {
        string CommandText();
        string Label();
    }
    public interface IDbCommandText<TConnection> : IDbCommandTextBase where TConnection : IDbConnectionConfig { }
    internal class DbCommandText<TConnection> : DbCommandText, IDbCommandText<TConnection> where TConnection : IDbConnectionConfig
    {
        public DbCommandText(string commandText, string label) : base(commandText, label) { }
    }
}
