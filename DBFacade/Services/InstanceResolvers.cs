using System.Threading.Tasks;

namespace DBFacade.Services
{
    internal class InstanceResolvers
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
