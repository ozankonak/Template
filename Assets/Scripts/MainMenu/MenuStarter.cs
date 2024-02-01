using Events;
using MainMenu.UI;
using VContainer;
using VContainer.Unity;

namespace MainMenu
{
    public class MenuStarter : IStartable
    {
        [Inject] private MenuUISystem menuUISystem;
        
        void IStartable.Start()
        {
            // This event was added in case any external service wants to listen.
            new OnMenuStarted().Trigger(this);
            
            menuUISystem.Init();
        }
    }
}
