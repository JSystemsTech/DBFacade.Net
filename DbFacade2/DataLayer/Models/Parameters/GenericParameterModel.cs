namespace DbFacade.DataLayer.Models.Parameters
{
    /// <summary>
    ///   <br />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericParameterModel<T>
    {
        /// <summary>Gets the data.</summary>
        /// <value>The data.</value>
        public T Data { get; private set; }
        internal GenericParameterModel(T data)
        {
            Data = data;
        }
    }
}
