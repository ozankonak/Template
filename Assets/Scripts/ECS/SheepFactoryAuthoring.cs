using Unity.Entities;
using UnityEngine;

namespace ECS
{
    public class SheepFactoryAuthoring : MonoBehaviour
    {
        public GameObject prefab;
        public float spawnRate;
    }

    class SheepFactoryBaker : Baker<SheepFactoryAuthoring>
    {
        public override void Bake(SheepFactoryAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new SheepFactoryComponent
            {
                prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                nextSpawnTime = 0.0f,
                spawnRate = authoring.spawnRate
            });
        }
    }
}
