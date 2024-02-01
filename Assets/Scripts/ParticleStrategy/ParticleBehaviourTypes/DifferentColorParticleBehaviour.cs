using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace ParticleStrategy.ParticleBehaviourTypes
{
    public class DifferentColorParticleBehaviour : RegularParticleBehavior
    {
        private ParticleSystem.TextureSheetAnimationModule ts;
        private Color color;

        public void SetElementColor(Color color)
        {
            this.color = color;
        }
        
        public override void Play(ParticleSystem[] particlesToPlay, Vector3 pos, GameObject gameObject, float time)
        {
            gameObject.transform.position = pos;
            
            SetColorAndPlay(particlesToPlay,gameObject,time);
        }

        public override void Play(ParticleSystem[] particlesToPlay, GameObject gameObject, float time)
        {
            SetColorAndPlay(particlesToPlay,gameObject,time);
        }

        private void SetColorAndPlay(ParticleSystem[] particlesToPlay,GameObject gameObject,float time)
        {
            if(color == Color.red)  SetParticlesTextureAnimation(particlesToPlay,4/6f);
            else if (color == Color.yellow) SetParticlesTextureAnimation(particlesToPlay,5/6f);
            else if (color == Color.magenta) SetParticlesTextureAnimation(particlesToPlay,3/6f);
            else if (color == Color.blue) SetParticlesTextureAnimation(particlesToPlay,0f);
            else if (color == Color.cyan) SetParticlesTextureAnimation(particlesToPlay,1/6f);
            else if (color == Color.green) SetParticlesTextureAnimation(particlesToPlay,2/6f);
            
            PlayParticles(particlesToPlay);
            DisableAfterTime(time,gameObject);
        }
        
        private void SetParticlesTextureAnimation(ParticleSystem[] particles,float spriteIndex)
        {
            ts = particles[0].textureSheetAnimation;

            var constantStartFrame = new ParticleSystem.MinMaxCurve(0);
            ts.startFrame = constantStartFrame;
        
            var constantSpriteIndex = new ParticleSystem.MinMaxCurve(spriteIndex);
            ts.frameOverTime = constantSpriteIndex;
        }
    }
}
