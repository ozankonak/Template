using Events;
using Observer;
using UnityEngine;

namespace Systems
{
    public class ExternalSystem : MonoBehaviour
    {
        public void OnEnable()
        {
            EventManager.Subscribe<OnGameStarted>(HandleGameStarted, this);
        }

        private void OnDisable()
        {
            EventManager.Unsubscribe<OnGameStarted>(HandleGameStarted);
        }

        private void HandleGameStarted(OnGameStarted eventData)
        {
            Debug.Log(eventData.sender.GetType() + " disabled " + GetType());
            gameObject.SetActive(false);
        }
        
    } 
}
