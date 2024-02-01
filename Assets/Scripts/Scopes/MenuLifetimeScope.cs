using Containers;
using MainMenu;
using MainMenu.UI;
using Providers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class MenuLifetimeScope : LifetimeScope
    {
        [SerializeField] private ParticleContainer particleContainer;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(particleContainer);
            
            builder.RegisterComponentInHierarchy<MenuUISystem>();
            builder.RegisterComponentInHierarchy<MenuSceneLoader>();

            builder.Register<ObjectWarmer>(Lifetime.Singleton);
            builder.Register<UpdateProvider>(Lifetime.Singleton).As<ITickable, UpdateProvider>();
            
            //Start point of menu
            builder.RegisterEntryPoint<MenuStarter>();
        }
    }
}
