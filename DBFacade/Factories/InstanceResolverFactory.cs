using DBFacade.Services;
using System.Threading.Tasks;

namespace DBFacade.Factories
{
    internal class InstanceResolverFactory
    {
        private static IInstanceResolver<IInstanceResolver> Resolvers = new InstanceResolver<IInstanceResolver>();
        public static IInstanceResolver<T> Get<T>()
        {
            return Resolvers.Get<InstanceResolver<T>>();
        }
        public static async Task<IInstanceResolver<T>> GetAsync<T>()
        {
            return await Resolvers.GetAsync<InstanceResolver<T>>();
        }
    }
}
