using UnityEngine;

namespace ParticleStrategy
{
    public class ParticleStarter : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] ParticleSystems;
        [SerializeField] private float TimeToDisable = 3f;

        [SerializeField] private bool UseRandomSeed;
        public void PlayBehavior(IParticleBehavior behavior,Vector3 pos)
        {
            if(UseRandomSeed)   PlayRandomSeed();
            
            behavior.Play(ParticleSystems,pos,gameObject,TimeToDisable);
        }

        public void PlayBehavior(IParticleBehavior behavior)
        {
            if(UseRandomSeed)   PlayRandomSeed();
            
            behavior.Play(ParticleSystems,gameObject,TimeToDisable);
        }

        private void PlayRandomSeed()
        {
            var particleSeed = Random.Range(0, int.MaxValue);

            if (ParticleSystems.Length <= 0) return;
        
            foreach (var t in ParticleSystems)
            {
                t.Stop();
                t.randomSeed = (uint) particleSeed;
            }
        }
    }
}
