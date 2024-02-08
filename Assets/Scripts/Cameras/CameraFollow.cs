using Providers;
using UnityEngine;
using VContainer;

namespace Cameras
{
    public class CameraFollow
    {
        [Inject] private Transform player;
        [Inject] private Camera mainCamera;
        [Inject] private readonly UpdateProvider updateProvider;
        
        private Vector3 Offset;

        private float SmoothTime = 0.1f;

        private Vector3 velocity = Vector3.zero;
        
        public void Init()
        {
            Offset = mainCamera.transform.position - player.position;
            updateProvider.CheckInputUpdate += ManualUpdate;
        }

        private void ManualUpdate()
        {
            if (player == null) return;

            Vector3 targetPosition = player.position + Offset;
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, targetPosition, ref velocity, SmoothTime);
        }
        
    }
}
