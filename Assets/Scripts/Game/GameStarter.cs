using UnityEngine;
using VContainer.Unity;

namespace Game
{
    public class GameStarter : IStartable
    {
        void IStartable.Start()
        {
            Debug.Log("Game Started");
        }
    }
}
