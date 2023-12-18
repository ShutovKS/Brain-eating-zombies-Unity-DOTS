using Unity.Entities;
using Unity.Mathematics;

namespace ComponentsAndTags
{
    public struct ZombieSpawnPointsBlob
    {
        public BlobArray<float3> Value;
    }
}