namespace DbFacade.DataLayer.Models
{
    /// <summary>
    ///   <br />
    /// </summary>
    public interface IDataCollection
    {
        /// <summary>Gets the keys.</summary>
        /// <value>The keys.</value>
        string[] Keys { get; }
        /// <summary>Gets the <see cref="System.Object" /> with the specified column index.</summary>
        /// <param name="columnIndex">Index of the column.</param>
        /// <value>The <see cref="System.Object" />.</value>
        /// <returns>
        ///   <br />
        /// </returns>
        object this[int columnIndex] { get; }
        /// <summary>Gets the <see cref="System.Object" /> with the specified column name.</summary>
        /// <param name="columnName">Name of the column.</param>
        /// <value>The <see cref="System.Object" />.</value>
        /// <returns>
        ///   <br />
        /// </returns>
        object this[string columnName] { get; }
    }
}
