using Events;
using VContainer.Unity;

namespace Game
{
    public class GameStarter : IStartable
    {
        void IStartable.Start()
        {
            // This event was added in case any external service wants to listen.
            new OnGameStarted().Trigger(this);
        }
    }
}
