﻿using DBFacade.DataLayer.ConnectionService;
using DBFacade.Factories;
using DBFacade.Services;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.ConnectionService
{
    internal class DbConnectionConfigManager
    {
        public static IDbConnectionConfigInternal Resolve<TDbConnectionConfig>()
        where TDbConnectionConfig : IDbConnectionConfig
        => InstanceResolverFactory.Get<IDbConnectionConfig>().Get<TDbConnectionConfig>() as IDbConnectionConfigInternal;

        public static async Task<IDbConnectionConfigInternal> ResolveAsync<TDbConnectionConfig>()
        where TDbConnectionConfig : IDbConnectionConfig
        {
            IInstanceResolver<IDbConnectionConfig> connectionConfigResolver = await InstanceResolverFactory.GetAsync<IDbConnectionConfig>();
            return await connectionConfigResolver.GetAsync<TDbConnectionConfig>() as IDbConnectionConfigInternal;
        }

        public static void Add<TDbConnectionConfig>(TDbConnectionConfig value)
        where TDbConnectionConfig : IDbConnectionConfig
        => InstanceResolverFactory.Get<IDbConnectionConfig>().Add(value);

        public static async Task AddAsync<TDbConnectionConfig>(TDbConnectionConfig value)
        where TDbConnectionConfig : IDbConnectionConfig
        {
            IInstanceResolver<IDbConnectionConfig> connectionConfigResolver = await InstanceResolverFactory.GetAsync<IDbConnectionConfig>();
            await connectionConfigResolver.AddAsync(value);
        }
    }
}