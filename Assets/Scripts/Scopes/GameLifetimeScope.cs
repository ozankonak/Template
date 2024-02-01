using Containers;
using Game;
using Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private ParticleContainer particleContainer;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(particleContainer);
            
            builder.Register<ParticleService>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<GameStarter>();
        }
    }
}
