using Containers;
using MainMenu;
using MainMenu.UI;
using Managers;
using Providers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class MenuLifetimeScope : LifetimeScope
    {
        [SerializeField] private ParticleContainer particleContainer;
        [SerializeField] private GameObjectContainer gameObjectContainer;
        [SerializeField] private MenuAudioContainer menuAudioContainer;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(particleContainer);
            builder.RegisterComponent(gameObjectContainer);
            builder.RegisterComponent(menuAudioContainer);
            
            builder.RegisterComponentInHierarchy<MenuUISystem>();
            builder.RegisterComponentInHierarchy<MenuSceneLoader>();
            builder.RegisterComponentInHierarchy<AudioManager>();

            builder.Register<ObjectWarmer>(Lifetime.Singleton);
            
            builder.Register<UpdateProvider>(Lifetime.Singleton).As<ITickable, UpdateProvider>();
            
            //Start point of menu
            builder.RegisterEntryPoint<MenuStarter>();
        }
    }
}
