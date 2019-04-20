namespace DomainFacade.DataLayer.ConnectionService
{

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DomainFacade.DataLayer.ConnectionService.IDbCommandTextBase" />
    internal abstract class DbCommandText: IDbCommandTextBase
    {
        /// <summary>
        /// Gets or sets the command text value.
        /// </summary>
        /// <value>
        /// The command text value.
        /// </value>
        private string CommandTextValue { get; set; }
        /// <summary>
        /// Gets or sets the label value.
        /// </summary>
        /// <value>
        /// The label value.
        /// </value>
        private string LabelValue { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandText"/> class.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        public DbCommandText(string commandText, string label)
        {
            CommandTextValue = commandText;
            LabelValue = label;
        }
        /// <summary>
        /// Commands the text.
        /// </summary>
        /// <returns></returns>
        public string CommandText()
        {
            return CommandTextValue;
        }

        /// <summary>
        /// Labels this instance.
        /// </summary>
        /// <returns></returns>
        public string Label()
        {
            return LabelValue;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IDbCommandTextBase
    {
        /// <summary>
        /// Commands the text.
        /// </summary>
        /// <returns></returns>
        string CommandText();
        /// <summary>
        /// Labels this instance.
        /// </summary>
        /// <returns></returns>
        string Label();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TConnection">The type of the connection.</typeparam>
    /// <seealso cref="DomainFacade.DataLayer.ConnectionService.IDbCommandTextBase" />
    public interface IDbCommandText<TConnection> : IDbCommandTextBase where TConnection : IDbConnectionConfig { }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TConnection">The type of the connection.</typeparam>
    /// <seealso cref="DomainFacade.DataLayer.ConnectionService.IDbCommandTextBase" />
    internal class DbCommandText<TConnection> : DbCommandText, IDbCommandText<TConnection> where TConnection : IDbConnectionConfig
    {
        public DbCommandText(string commandText, string label) : base(commandText, label) { }
    }
}
