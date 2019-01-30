using DomainFacade.Utils;

namespace DomainFacade.DataLayer.DbManifest
{
    public abstract class DbCommandTextCore
    {
        public string CommandText { get; private set; }
        public DbCommandTextCore(string commandText)
        {
            CommandText = commandText;
        }
    }
    public sealed class DbCommandText<T> : DbCommandTextCore where T:DbConnectionCore
    {
        public DbCommandText(string commandText) : base(commandText) { }

    }
}
