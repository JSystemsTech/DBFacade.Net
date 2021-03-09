using DbFacade.DataLayer.ConnectionService;
using System.Data;
using System.Threading.Tasks;

namespace DbFacade.Factories
{
    public class DbCommandConfigFactory<TDbConnectionConfig>
        where TDbConnectionConfig: DbConnectionConfigBase
    {

        public static IDbCommandConfig CreateFetchCommand(string commandText, string label, bool requiresValidation = false)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, CommandType.StoredProcedure, false, requiresValidation);
        public static IDbCommandConfig CreateTransactionCommand(string commandText, string label, bool requiresValidation = true)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, CommandType.StoredProcedure, true, requiresValidation);
        public static IDbCommandConfig CreateFetchCommand(string commandText, string label, CommandType commandType, bool requiresValidation = false)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, commandType, false, requiresValidation);
        protected static IDbCommandConfig CreateTransactionCommand(string commandText, string label, CommandType commandType, bool requiresValidation = true)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, commandType, true, requiresValidation);


        public static async Task<IDbCommandConfig> CreateFetchCommandAsync(string commandText, string label, bool requiresValidation = false)
        => await DbCommand<TDbConnectionConfig>.CreateAsync(commandText, label, CommandType.StoredProcedure, false, requiresValidation);
        public static async Task<IDbCommandConfig> CreateTransactionCommandAsync(string commandText, string label, bool requiresValidation = true)
        => await DbCommand<TDbConnectionConfig>.CreateAsync(commandText, label, CommandType.StoredProcedure, true, requiresValidation);
        public static async Task<IDbCommandConfig> CreateFetchCommandAsync(string commandText, string label, CommandType commandType, bool requiresValidation = false)
        => await DbCommand<TDbConnectionConfig>.CreateAsync(commandText, label, commandType, false, requiresValidation);
        public static async Task<IDbCommandConfig> CreateTransactionCommandAsync(string commandText, string label, CommandType commandType, bool requiresValidation = true)
        => await DbCommand<TDbConnectionConfig>.CreateAsync(commandText, label, commandType, true, requiresValidation);
    }
}
