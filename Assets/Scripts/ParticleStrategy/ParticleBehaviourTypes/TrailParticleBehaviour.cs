using DG.Tweening;
using UnityEngine;

namespace ParticleStrategy.ParticleBehaviourTypes
{
    public class TrailParticleBehaviour : RegularParticleBehavior
    {
        private Vector3 target;
        private float duration;

        public void SetTargetAndDuration(Vector3 value,float dur)
        {
            target = value;
            duration = dur;
        }
        
        public override void Play(ParticleSystem[] particlesToPlay, Vector3 pos, GameObject gameObject, float time)
        {
            gameObject.transform.position = pos;
            
            foreach (ParticleSystem pr in particlesToPlay)
            {
                pr.Stop();
                pr.Play();
            }
            
            gameObject.transform.DOMove(target,duration).onComplete += () =>
            { 
                gameObject.transform.position = target;
                Disable(gameObject); 
            };
        }
    }
}