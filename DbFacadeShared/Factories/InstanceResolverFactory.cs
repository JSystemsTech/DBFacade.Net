using System.Threading.Tasks;
using DbFacade.Services;
using DbFacade.Utils;

namespace DbFacade.Factories
{
    internal class InstanceResolverFactory
    {
        private static readonly IInstanceResolver<IInstanceResolver> Resolvers =
            new InstanceResolver<IInstanceResolver>();

        public static IInstanceResolver<T> Get<T>()
            where T : IAsyncInit
        {
            return Resolvers.Get<InstanceResolver<T>>();
        }

        public static async Task<IInstanceResolver<T>> GetAsync<T>()
            where T:IAsyncInit
        {
            return await Resolvers.GetAsync<InstanceResolver<T>>();
        }
    }
}