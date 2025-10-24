namespace DbFacade.DataLayer.Models
{
    /// <summary>
    ///   <br />
    /// </summary>
    public interface IDbDataModel
    {
        /// <summary>Initializes the specified collection.</summary>
        /// <param name="collection">The collection.</param>
        void Init(IDataCollection collection);
    }
}
