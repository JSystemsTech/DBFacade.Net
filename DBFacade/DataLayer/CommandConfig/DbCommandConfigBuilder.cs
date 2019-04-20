using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;

namespace DBFacade.DataLayer.CommandConfig
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DbCommandConfigBuilder
    {
        /// <summary>
        /// Gets the fetch configuration.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <param name="dbCommandText">The database command text.</param>
        /// <returns></returns>
        public static IDbCommandConfig GetFetchConfig<Con>(IDbCommandText<Con> dbCommandText)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<IDbParamsModel, Con>(dbCommandText);
        }
        /// <summary>
        /// Gets the fetch configuration with return.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <param name="dbCommandText">The database command text.</param>
        /// <param name="returnParam">The return parameter.</param>
        /// <returns></returns>
        public static IDbCommandConfig GetFetchConfigWithReturn<Con>(IDbCommandText<Con> dbCommandText, string returnParam)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<IDbParamsModel, Con>(dbCommandText, returnParam);
        }
        /// <summary>
        /// Gets the transaction configuration.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <param name="dbCommandText">The database command text.</param>
        /// <returns></returns>
        public static IDbCommandConfig GetTransactionConfig<Con>(IDbCommandText<Con> dbCommandText)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<IDbParamsModel, Con>(dbCommandText).AsTransaction();
        }
        /// <summary>
        /// Gets the transaction configuration with return.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <param name="dbCommandText">The database command text.</param>
        /// <param name="returnParam">The return parameter.</param>
        /// <returns></returns>
        public static IDbCommandConfig GetTransactionConfigWithReturn<Con>(IDbCommandText<Con> dbCommandText, string returnParam)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<IDbParamsModel, Con>(dbCommandText, returnParam).AsTransaction();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public class DbCommandConfigBuilder<TDbParams>
        where TDbParams : IDbParamsModel
    {

        /// <summary>
        /// Gets the fetch configuration.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <param name="dbCommandText">The database command text.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <returns></returns>
        public static IDbCommandConfig GetFetchConfig<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams)
        where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams);
        }
        /// <summary>
        /// Gets the fetch configuration.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <param name="dbCommandText">The database command text.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="validator">The validator.</param>
        /// <returns></returns>
        public static IDbCommandConfig GetFetchConfig<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, validator);
        }
        /// <summary>
        /// Gets the fetch configuration with return.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <param name="dbCommandText">The database command text.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="returnParam">The return parameter.</param>
        /// <returns></returns>
        public static IDbCommandConfig GetFetchConfigWithReturn<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, string returnParam)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, returnParam);
        }
        /// <summary>
        /// Gets the fetch configuration with return.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <param name="dbCommandText">The database command text.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="returnParam">The return parameter.</param>
        /// <param name="validator">The validator.</param>
        /// <returns></returns>
        public static IDbCommandConfig GetFetchConfigWithReturn<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, string returnParam, Validator<TDbParams> validator)
        where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, returnParam, validator);
        }

        /// <summary>
        /// Gets the transaction configuration.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <param name="dbCommandText">The database command text.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <returns></returns>
        public static IDbCommandConfig GetTransactionConfig<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams).AsTransaction();
        }
        /// <summary>
        /// Gets the transaction configuration.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <param name="dbCommandText">The database command text.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="validator">The validator.</param>
        /// <returns></returns>
        public static IDbCommandConfig GetTransactionConfig<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, validator).AsTransaction();
        }

        /// <summary>
        /// Gets the transaction configuration with return.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <param name="dbCommandText">The database command text.</param>
        /// <param name="returnParam">The return parameter.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <returns></returns>
        public static IDbCommandConfig GetTransactionConfigWithReturn<Con>(IDbCommandText<Con> dbCommandText, string returnParam, IDbCommandConfigParams<TDbParams> dbParams)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, returnParam).AsTransaction();
        }
        /// <summary>
        /// Gets the transaction configuration with return.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <param name="dbCommandText">The database command text.</param>
        /// <param name="returnParam">The return parameter.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="validator">The validator.</param>
        /// <returns></returns>
        public static IDbCommandConfig GetTransactionConfigWithReturn<Con>(IDbCommandText<Con> dbCommandText, string returnParam, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, returnParam, validator).AsTransaction();
        }
    }
}
