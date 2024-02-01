using System.Collections.Generic;
using Containers;
using ObjectPool;
using ParticleStrategy;
using VContainer;

namespace MainMenu
{
    public class ObjectWarmer 
    {
        [Inject] private readonly ParticleContainer vfxContainer;

        private static bool hasInit;

        public void StartToWarm()
        {
            if (hasInit) return;
            hasInit = true;
            
            GetParticleObjects(vfxContainer.GetAllParticles());
            //Shader.WarmupAllShaders();
        }
        
        private void GetParticleObjects(List<ParticleStarter> pooledObjects)
        {
            foreach (ParticleStarter poolItem in pooledObjects)
            {
                PoolObject.Warm(poolItem.gameObject);
            }
        }

    }
}
