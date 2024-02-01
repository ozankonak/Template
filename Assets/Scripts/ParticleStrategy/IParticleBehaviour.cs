using UnityEngine;

namespace ParticleStrategy
{
    public interface IParticleBehavior {
        void Play(ParticleSystem[] particlesToPlay,Vector3 pos,GameObject gameObject,float time);
        void Play(ParticleSystem[] particlesToPlay,GameObject gameObject,float time);
        void Disable(GameObject gameObject);
        void DisableAfterTime(float time,GameObject gameObject);
    }
}