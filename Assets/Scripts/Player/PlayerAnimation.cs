using UnityEngine;

namespace Player
{
    public class PlayerAnimation
    {
        private readonly Animator playerAnimation;
        
        private readonly string XInputKey = "xInput";
        private readonly string ZInputKey = "zInput";

        public PlayerAnimation(Animator playerAnimation)
        {
            this.playerAnimation = playerAnimation;
        }

        public void MoveAnimation(Vector3 movement)
        {
            float velocityZ = Vector3.Dot(movement.normalized, playerAnimation.transform.forward);
            float velocityX = Vector3.Dot(movement.normalized, playerAnimation.transform.right);

            playerAnimation.SetFloat(XInputKey, velocityX, 0.1f, Time.deltaTime);
            playerAnimation.SetFloat(ZInputKey, velocityZ, 0.1f, Time.deltaTime);
        }
    }
}
