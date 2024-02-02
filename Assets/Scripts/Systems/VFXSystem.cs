using Containers;
using ObjectPool;
using ParticleStrategy;
using UnityEngine;
using VContainer;

namespace Systems
{
    public class VFXSystem : ParticleBehaviours
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
