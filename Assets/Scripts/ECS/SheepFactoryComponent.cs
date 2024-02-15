using Unity.Entities;

namespace ECS
{
    public struct SheepFactoryComponent : IComponentData
    {
        public Entity prefab;
        public float nextSpawnTime;
        public float spawnRate;
    }
}
