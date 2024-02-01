using DG.Tweening;
using UnityEngine;

namespace ParticleStrategy.ParticleBehaviourTypes
{
    public class RegularParticleBehavior : IParticleBehavior
    {
        public virtual void Play(ParticleSystem[] particlesToPlay,Vector3 pos,GameObject gameObject,float time)
        {
            gameObject.transform.position = pos;
            PlayParticles(particlesToPlay);
            DisableAfterTime(time,gameObject);
        }

        public virtual void Play(ParticleSystem[] particlesToPlay, GameObject gameObject, float time)
        {
            PlayParticles(particlesToPlay);
            DisableAfterTime(time,gameObject);
        }

        protected void PlayParticles(ParticleSystem[] particlesToPlay)
        {
            foreach (ParticleSystem pr in particlesToPlay)
            {
                pr.Stop();
                pr.Play();
            }
        }

        public void DisableAfterTime(float time,GameObject gameObject)
        {
            DOVirtual.DelayedCall(time, ()=> Disable(gameObject), false);
        }

        public void Disable(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}