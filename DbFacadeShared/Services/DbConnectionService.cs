using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DbFacade.Services
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbConnectionService
    {
        /// <summary>
        /// The connection configs
        /// </summary>
        private static ConcurrentDictionary<Type, DbConnectionConfigBase> connectionConfigs = new ConcurrentDictionary<Type, DbConnectionConfigBase>();

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
        /// <returns></returns>
        /// <exception cref="DbConnectionConfigNotRegisteredException"></exception>
        internal static TDbConnectionConfig Get<TDbConnectionConfig>()
            where TDbConnectionConfig : DbConnectionConfigBase
        {
            var type = typeof(TDbConnectionConfig);
            if (connectionConfigs.TryGetValue(type, out DbConnectionConfigBase value) && value is TDbConnectionConfig config)
            {
                return config;
            }
            throw new DbConnectionConfigNotRegisteredException(type);
        }
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
        /// <param name="config">The configuration.</param>
        public static void Register<TDbConnectionConfig>(this TDbConnectionConfig config)
            where TDbConnectionConfig : DbConnectionConfigBase
        {
            connectionConfigs.GetOrAdd(typeof(TDbConnectionConfig), config);
        }
        private static ConcurrentDictionary<Type, IDictionary<string, Guid>> CommandIdMaps = new ConcurrentDictionary<Type, IDictionary<string, Guid>>();
        private static IDictionary<string, Guid> GetCommandIdMap<T>()
        {
            if (CommandIdMaps.TryGetValue(typeof(T), out IDictionary<string, Guid> map))
            {
                return map;
            }
            ConcurrentDictionary<string, Guid> dic = new ConcurrentDictionary<string, Guid>();
            IEnumerable<FieldInfo> commands = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(fi => fi.FieldType.GetInterfaces().Contains(typeof(IDbCommandConfig)));

            foreach (FieldInfo fi in commands)
            {
                dic.TryAdd(fi.Name, fi.GetValue(null) is IDbCommandConfig cmd ? cmd.CommandId : default);
            }
            CommandIdMaps.TryAdd(typeof(T), dic);
            return dic;
        }

        /// <summary>
        /// Enables the mock mode.
        /// </summary>
        /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
        /// <param name="mockDataResponseDictionary">The mock data response dictionary.</param>
        public static void EnableMockMode<TDbConnectionConfig>(IDictionary<Guid, MockResponseData> mockDataResponseDictionary)
            where TDbConnectionConfig : DbConnectionConfigBase
        {
            if (connectionConfigs.TryGetValue(typeof(TDbConnectionConfig), out DbConnectionConfigBase config))
            {
                config.EnableMockMode(mockDataResponseDictionary);
            }
        }
        /// <summary>
        /// Disables the mock mode.
        /// </summary>
        /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
        public static void DisableMockMode<TDbConnectionConfig>()
            where TDbConnectionConfig : DbConnectionConfigBase
        {
            if (connectionConfigs.TryGetValue(typeof(TDbConnectionConfig), out DbConnectionConfigBase config))
            {
                config.DisableMockMode();
            }
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
        /// <returns></returns>
        /// <exception cref="DbConnectionConfigNotRegisteredException"></exception>
        internal static async Task<TDbConnectionConfig> GetAsync<TDbConnectionConfig>()
            where TDbConnectionConfig : DbConnectionConfigBase
        {
            var type = typeof(TDbConnectionConfig);
            if (connectionConfigs.TryGetValue(type, out DbConnectionConfigBase value) && value is TDbConnectionConfig config)
            {
                await Task.CompletedTask;
                return config;
            }
            await Task.CompletedTask;
            throw new DbConnectionConfigNotRegisteredException(type);
        }
        /// <summary>
        /// Registers the asynchronous.
        /// </summary>
        /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
        /// <param name="config">The configuration.</param>
        public static async Task RegisterAsync<TDbConnectionConfig>(this TDbConnectionConfig config)
            where TDbConnectionConfig : DbConnectionConfigBase
        {
            connectionConfigs.GetOrAdd(typeof(TDbConnectionConfig), config);
            await Task.CompletedTask;
        }
    }
}
