using Player;
using Providers;
using UnityEngine;
using VContainer;

namespace Systems
{
    public class InputSystem
    {
        [Inject] private readonly UpdateProvider updateProvider;
        [Inject] private PlayerMovement playerMovement;
        
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        private float xInput;
        private float zInput;

        public void Init()
        {
            updateProvider.CheckInputUpdate += ManualUpdate;
        }

        private void ManualUpdate()
        {
            xInput = Input.GetAxis(Horizontal);
            zInput = Input.GetAxis(Vertical);
            
            playerMovement.Move(xInput,zInput);
        }
    }
}
