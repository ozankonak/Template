using System;
using System.Collections.Generic;

namespace ServiceLocator
{
    internal class CommonServiceLocator:IServiceLocator 
    {
        private readonly Dictionary<Type, object> services = new(100);

        public void Register<T>(T service) where T: IService
        {
            services[typeof(T)] = service;
        }

        public T Get<T>() where T: IService
        {
            return (T)services[typeof(T)];
        }

        public bool Has<T>() where T : IService
        {
            return services.ContainsKey(typeof(T));
        }
    }
}