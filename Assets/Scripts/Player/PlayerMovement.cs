using UnityEngine;
using VContainer;

namespace Player
{
    public class PlayerMovement
    {
        [Inject] private PlayerAnimation PlayerAnimation;
        
        private float forwardSpeed = 5f;
        private float backwardSpeed = 3f;

        private float currentSpeed;
        
        
        private Vector3 movement;
        
        private readonly CharacterController playerCharacterController;

        public PlayerMovement(CharacterController playerCharacterController)
        {
            this.playerCharacterController = playerCharacterController;
        }
        public void Move(float xInput, float zInput)
        {
            movement = new Vector3(xInput, 0f, zInput);

            currentSpeed = zInput < 0 ? backwardSpeed : forwardSpeed;
            
            if (movement.magnitude > 0)
            {
                movement.Normalize();
                movement *= currentSpeed * Time.deltaTime;

                playerCharacterController.Move(movement);
            }
            
            PlayerAnimation.MoveAnimation(movement);
        }
    }
}
