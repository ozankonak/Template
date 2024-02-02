namespace ServiceLocator
{
    public interface IServiceLocator
    {
        void Register<T>(T service) where T : IService;
        T Get<T>() where T : IService;
        bool Has<T>() where T : IService;
    }
}
