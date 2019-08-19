namespace DBFacade.Services
{
    internal class InstanceResolvers
    {
        private static IInstanceResolver<IInstanceResolver> Resolvers = new InstanceResolver<IInstanceResolver>();
        public static IInstanceResolver<T> Get<T>()
        {
            return Resolvers.Get<InstanceResolver<T>>();
        }
    }
}
