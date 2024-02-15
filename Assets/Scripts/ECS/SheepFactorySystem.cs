using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using BoxCollider = Unity.Physics.BoxCollider;
using Material = Unity.Physics.Material;

namespace ECS
{
    [BurstCompile]
    public partial struct SheepFactorySystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //Disabled
            return; 
            
            if (!SystemAPI.TryGetSingletonEntity<SheepFactoryComponent>(out Entity factoryEntity)) return;

            RefRW<SheepFactoryComponent> factory = SystemAPI.GetComponentRW<SheepFactoryComponent>(factoryEntity);

            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);

            if (factory.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                Entity newEntity = ecb.Instantiate(factory.ValueRO.prefab);
                
                var collider = BoxCollider.Create(new BoxGeometry
                {
                    Center = float3.zero,
                    Orientation = quaternion.identity,
                    Size = new float3(1, 1, 1),
                    BevelRadius = 0.05f
                }, CollisionFilter.Default, new Material
                {
                    CollisionResponse = CollisionResponsePolicy.Collide,
                    Friction = 0.5f,
                    Restitution = 0.3f,
                });
                
                float3 spawnPosition = new float3(GetRandomFloatValue(state,-4.5f,4.5f,100000f), 0.2f, 488f); 
                
                ecb.AddComponent(newEntity, new LocalTransform() { Position = spawnPosition,Rotation = new quaternion(0,-1,0,0),Scale = 1});
                
                ecb.AddComponent(newEntity,new SheepComponent {moveDirection = new float3(0,0,-1),
                    moveSpeed = 10,
                    boxCollider = collider});

                factory.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + factory.ValueRO.spawnRate;
                
                ecb.Playback(state.EntityManager);
            }
        }

        private float GetRandomFloatValue(SystemState state,float minValue, float maxValue,float intensity)
        {
            uint seed = (uint)(state.WorldUnmanaged.Time.ElapsedTime * intensity);
            Random random = new Random(seed);
            return random.NextFloat(minValue, maxValue);
        }
    }
}
