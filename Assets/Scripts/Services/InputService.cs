using Providers;
using UnityEngine;
using VContainer;

namespace Services
{
    public class InputService
    {
        [Inject] private Camera mainCamera;
        [Inject] private readonly UpdateProvider updateProvider;

        public void Init()
        {
            updateProvider.CheckInputUpdate += ManualUpdate;
        }

        private void ManualUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse pos :" + GetMouseWorldPos());
            }
        }
        
        private Vector3 GetMouseWorldPos()
        {
            Vector2 mousePoint = Input.mousePosition;
            return mainCamera == null ? Vector3.zero : mainCamera.ScreenToWorldPoint(mousePoint);
        }
    }
}
