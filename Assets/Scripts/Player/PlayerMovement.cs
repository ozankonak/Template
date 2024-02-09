using UnityEngine;
using VContainer;

namespace Player
{
    public class PlayerMovement
    {
        [Inject] private PlayerAnimation PlayerAnimation;
        
        private float forwardSpeed = 5f;
        private float backwardSpeed = 3f;
        
        private float jumpHeight = 2.0f;
        private float jumpSpeed = -2.5f;

        private float currentSpeed;
        
        private Vector3 movement;
        private Vector3 playerVelocity;

        private bool groundedPlayer;
        
        private const float gravityValue = -9.81f;
        private readonly string JumpButtonName = "Jump";
        
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
            
            PlayerAnimation.GroundAnimation(playerCharacterController.isGrounded);
            PlayerAnimation.MoveAnimation(movement);
        }

        public void Jump()
        {
            groundedPlayer = playerCharacterController.isGrounded;
            
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
            
            if (Input.GetButton(JumpButtonName) && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * jumpSpeed * gravityValue);
                PlayerAnimation.JumpAnimation();
            }
            
            playerVelocity.y += gravityValue * Time.deltaTime;
            playerCharacterController.Move(playerVelocity * Time.deltaTime);
        }
    }
}
