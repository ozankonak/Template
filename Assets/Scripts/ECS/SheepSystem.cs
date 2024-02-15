using Sheeps;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECS
{
    [BurstCompile]
    public partial struct SheepSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //Disabled
            return;
            
            EntityManager entityManager = state.EntityManager;

            NativeArray<Entity> entities = entityManager.GetAllEntities();

            foreach (Entity entity in entities)
            {
                if (entityManager.HasComponent<SheepComponent>(entity))
                {
                    SheepComponent sheep = entityManager.GetComponentData<SheepComponent>(entity);
                    LocalTransform localTransform = entityManager.GetComponentData<LocalTransform>(entity);
                    
                    float3 moveDirection = sheep.moveDirection * SystemAPI.Time.DeltaTime * sheep.moveSpeed;

                    localTransform.Position += moveDirection;
                    entityManager.SetComponentData(entity,localTransform);
                    
                    entityManager.SetComponentData(entity,sheep);

                }
            }
        }
    }
}
