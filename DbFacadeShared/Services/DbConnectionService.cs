﻿using DbFacade.DataLayer.ConnectionService;
using DbFacade.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace DbFacade.Services
{
    public sealed class DbConnectionService
    {
        private static ConcurrentDictionary<Type, DbConnectionConfigBase> connectionConfigs = new ConcurrentDictionary<Type, DbConnectionConfigBase>();
        public async Task InitAsync()
        {
            await Task.CompletedTask;
        }
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
        public static void Register<TDbConnectionConfig>(TDbConnectionConfig config)
            where TDbConnectionConfig : DbConnectionConfigBase
        {
            connectionConfigs.GetOrAdd(typeof(TDbConnectionConfig), config);
        }

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
        public static async Task RegisterAsync<TDbConnectionConfig>(TDbConnectionConfig config)
            where TDbConnectionConfig : DbConnectionConfigBase
        {
            connectionConfigs.GetOrAdd(typeof(TDbConnectionConfig), config);
            await Task.CompletedTask;
        }
    }
}
