using Events;
using MainMenu.UI;
using VContainer;
using VContainer.Unity;

namespace MainMenu
{
    public class MenuStarter : IStartable
    {
        [Inject] private MenuUISystem menuUISystem;
        [Inject] private ObjectWarmer warmer;
        
        void IStartable.Start()
        {
            // This event was added in case any external service wants to listen.
            new OnMenuStarted().Trigger(this);
            
            warmer.StartToWarm();
            menuUISystem.Init();
        }
    }
}
