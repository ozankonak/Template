using Containers;
using Game;
using Providers;
using Systems;
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
            
            builder.Register<VFXSystem>(Lifetime.Singleton);
            builder.Register<InputSystem>(Lifetime.Singleton);
            
            builder.Register<UpdateProvider>(Lifetime.Singleton).As<ITickable, UpdateProvider>();
            
            //Start point of game
            builder.RegisterEntryPoint<GameStarter>();
        }
    }
}
