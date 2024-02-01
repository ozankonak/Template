using Events;
using Services;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public class GameStarter : IStartable
    {
        [Inject] private ParticleService particleService;
        
        void IStartable.Start()
        {
            // This event was added in case any external service wants to listen.
            new OnGameStarted().Trigger(this);
            
            particleService.Init();
        }
    }
}
