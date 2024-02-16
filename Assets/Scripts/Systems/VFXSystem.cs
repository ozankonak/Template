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
        
        public void CreateSheepCollectParticle(Vector3 pos)
        {
            ParticleStarter particleToPlay = particleContainer.SheepCollectParticle.Spawn();
            particleToPlay.PlayBehavior(RegularParticleBehavior,pos);
        }
    }
}
