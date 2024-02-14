using System.Collections.Generic;
using Containers;
using ObjectPool;
using ParticleStrategy;
using UnityEngine;
using VContainer;

namespace MainMenu
{
    public class ObjectWarmer 
    {
        [Inject] private readonly ParticleContainer vfxContainer;
        [Inject] private readonly GameObjectContainer gameObjectContainer;

        private static bool hasInit;

        public void StartToWarm()
        {
            if (hasInit) return;
            hasInit = true;
            
            GetParticleObjects(vfxContainer.GetAllParticles());
            GetAllGameObjects(gameObjectContainer.GetAllGameObjects());

            //Shader.WarmupAllShaders();
        }
        
        private void GetParticleObjects(List<ParticleStarter> pooledObjects)
        {
            foreach (ParticleStarter poolItem in pooledObjects)
            {
                PoolObject.Warm(poolItem.gameObject);
            }
        }

        private void GetAllGameObjects(List<GameObject> pooledObjects)
        {
            foreach (GameObject poolItem in pooledObjects)
            {
                PoolObject.Warm(poolItem);
            }
        }
    }
}
