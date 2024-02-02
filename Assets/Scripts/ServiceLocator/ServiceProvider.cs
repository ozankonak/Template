namespace ServiceLocator
{
    public static class ServiceProvider
    {
        private static readonly IServiceLocator Current = new CommonServiceLocator();
        
        public static void Register<T>(T service) where T : IService
        {
            Current.Register(service);
        }
        public static T Get<T>() where T : IService
        {
            return Current.Get<T>();
        }
        public static bool Has<T>() where T:IService
        {
            return Current.Has<T>();
        }
    }
}
