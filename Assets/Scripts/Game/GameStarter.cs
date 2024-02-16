using Cameras;
using Events;
using Sheeps;
using Systems;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public class GameStarter : IStartable
    {
        [Inject] private InputSystem inputSystem;
        [Inject] private CameraFollow cameraFollow;
        [Inject] private ISheepFactory sheepFactory;
        
        void IStartable.Start()
        {
            // This event was added in case any external service wants to listen.
            new OnGameStarted().Trigger(this);
            
            inputSystem.Init();
            cameraFollow.Init();
            sheepFactory.Init();
        }
    }
}
