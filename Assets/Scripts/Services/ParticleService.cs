using Containers;
using ObjectPool;
using ParticleStrategy;
using UnityEngine;
using VContainer;

namespace Services
{
    public class ParticleService : ParticleBehaviours
    {
        [Inject] private ParticleContainer particleContainer;
        
        public void Init()
        {
            CreateGameStartParticle(Vector3.zero);
        }

        private void CreateGameStartParticle(Vector3 pos)
        {
            ParticleStarter particleToPlay = particleContainer.GameStartParticle.Spawn();
            particleToPlay.PlayBehavior(RegularParticleBehavior,pos);
        }
    }
}
