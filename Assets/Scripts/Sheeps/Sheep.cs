using Providers;
using UnityEngine;
using VContainer;

namespace Sheeps
{
    public class Sheep : MonoBehaviour
    {
        [Inject] private UpdateProvider updateProvider;

        private void Start()
        {
            updateProvider.SheepMovement += ManualUpdate;
        }

        private void ManualUpdate()
        {
            transform.Translate(0,0,0.2f);
        }

        public void DestroySheep()
        {
            updateProvider.SheepMovement -= ManualUpdate;
            Destroy(gameObject);
        }
    }
}
