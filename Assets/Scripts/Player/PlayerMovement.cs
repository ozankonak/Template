using UnityEngine;
using VContainer;

namespace Player
{
    public class PlayerMovement
    {
        [Inject] private PlayerAnimation PlayerAnimation;
        
        private float speed = 5f;
        private Vector3 movement;
        
        private readonly CharacterController playerCharacterController;

        public PlayerMovement(CharacterController playerCharacterController)
        {
            this.playerCharacterController = playerCharacterController;
        }
        public void Move(float xInput, float zInput)
        {
            movement = new Vector3(xInput, 0f, zInput);

            if (movement.magnitude > 0)
            {
                movement.Normalize();
                movement *= speed * Time.deltaTime;

                playerCharacterController.Move(movement);
            }
            
            PlayerAnimation.MoveAnimation(movement);
        }
    }
}
