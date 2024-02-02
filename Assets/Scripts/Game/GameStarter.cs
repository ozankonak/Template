using Events;
using Systems;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public class GameStarter : IStartable
    {
        [Inject] private VFXSystem vfxSystem;
        [Inject] private InputSystem inputSystem;
        
        void IStartable.Start()
        {
            // This event was added in case any external service wants to listen.
            new OnGameStarted().Trigger(this);
            
            vfxSystem.Init();
            inputSystem.Init();
        }
    }
}
