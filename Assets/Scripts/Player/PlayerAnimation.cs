using UnityEngine;

namespace Player
{
    public class PlayerAnimation
    {
        private readonly Animator playerAnimation;

        private float velocityZ;
        private float velocityX;
        
        private readonly int XInputKey = Animator.StringToHash("xInput");
        private readonly int ZInputKey = Animator.StringToHash("zInput");
        private readonly int Jump = Animator.StringToHash("Jump");
        private readonly int Grounded = Animator.StringToHash("Grounded");

        public PlayerAnimation(Animator playerAnimation)
        {
            this.playerAnimation = playerAnimation;
        }

        public void MoveAnimation(Vector3 movement)
        {
            velocityZ = Vector3.Dot(movement.normalized, playerAnimation.transform.forward);
            velocityX = Vector3.Dot(movement.normalized, playerAnimation.transform.right);

            if (velocityZ < 0) velocityX *= -1;

            playerAnimation.SetFloat(XInputKey, velocityX, 0.1f, Time.deltaTime);
            playerAnimation.SetFloat(ZInputKey, velocityZ, 0.1f, Time.deltaTime);
        }

        public void JumpAnimation()
        {
            playerAnimation.SetTrigger(Jump);
        }

        public void GroundAnimation(bool isGrounded)
        {
            playerAnimation.SetBool(Grounded,isGrounded);
        }
    }
}
