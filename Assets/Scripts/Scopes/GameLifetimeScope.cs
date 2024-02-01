using Containers;
using Game;
using Providers;
using Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private ParticleContainer particleContainer;

        private Camera mainCamera;
        
        protected override void Configure(IContainerBuilder builder)
        {
            mainCamera = Camera.main;
            builder.RegisterComponent(mainCamera);
            builder.RegisterComponent(particleContainer);
            
            builder.Register<ParticleService>(Lifetime.Singleton);
            builder.Register<InputService>(Lifetime.Singleton);
            
            builder.Register<UpdateProvider>(Lifetime.Singleton).As<ITickable, UpdateProvider>();
            
            builder.RegisterEntryPoint<GameStarter>();
        }
    }
}
