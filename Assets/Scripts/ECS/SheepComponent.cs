using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace ECS
{
    public struct SheepComponent : IComponentData
    {
        public float3 moveDirection;
        public float moveSpeed;
        public BlobAssetReference<Collider> boxCollider;
    }
}
