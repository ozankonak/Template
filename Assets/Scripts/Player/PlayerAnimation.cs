using UnityEngine;

namespace Player
{
    public class PlayerAnimation
    {
        private readonly Animator playerAnimation;
        
        private readonly string XInputKey = "xInput";
        private readonly string ZInputKey = "zInput";

        private float velocityZ;
        private float velocityX;

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
    }
}
