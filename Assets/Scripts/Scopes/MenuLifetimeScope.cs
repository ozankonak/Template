using MainMenu;
using MainMenu.UI;
using Providers;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class MenuLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<MenuUISystem>();
            builder.RegisterComponentInHierarchy<MenuSceneLoader>();
            
            builder.Register<UpdateProvider>(Lifetime.Singleton).As<ITickable, UpdateProvider>();
            
            //Start point of menu
            builder.RegisterEntryPoint<MenuStarter>();
        }
    }
}
