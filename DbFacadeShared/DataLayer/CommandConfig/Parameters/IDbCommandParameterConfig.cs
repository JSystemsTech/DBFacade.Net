using System;
using System.Data;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.CommandConfig.Parameters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public interface IDbCommandParameterConfig<TDbParams>
    { }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    internal interface IInternalDbCommandParameterConfig<TDbParams> : IDbCommandParameterConfig<TDbParams>
    {
        /// <summary>
        /// Gets the type of the database.
        /// </summary>
        /// <value>
        /// The type of the database.
        /// </value>
        DbType DbType { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is nullable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is nullable; otherwise, <c>false</c>.
        /// </value>
        bool IsNullable { get; }
        /// <summary>
        /// Gets the parameter direction.
        /// </summary>
        /// <value>
        /// The parameter direction.
        /// </value>
        ParameterDirection ParameterDirection { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is output.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is output; otherwise, <c>false</c>.
        /// </value>
        bool IsOutput { get; }
        /// <summary>Gets the type of the output.</summary>
        /// <value>The type of the output.</value>
        Type OutputType { get; }
        /// <summary>
        /// Gets the size of the output.
        /// </summary>
        /// <value>
        /// The size of the output.
        /// </value>
        int? OutputSize { get; }
        /// <summary>
        /// Values the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        object Value(TDbParams model);
        /// <summary>
        /// Values the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<object> ValueAsync(TDbParams model);
    }
}