using ParticleStrategy.ParticleBehaviourTypes;

namespace ParticleStrategy
{
    public class ParticleBehaviours
    {
        protected RegularParticleBehavior RegularParticleBehavior = new();
        protected TrailParticleBehaviour TrailParticleBehaviour = new();
        protected DifferentColorParticleBehaviour DifferentColorParticleBehaviour = new();
    }
}