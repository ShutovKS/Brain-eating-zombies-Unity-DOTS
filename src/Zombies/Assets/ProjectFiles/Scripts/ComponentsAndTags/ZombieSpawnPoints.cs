using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace ComponentsAndTags
{
    public struct ZombieSpawnPoints : IComponentData
    {
        public BlobAssetReference<ZombieSpawnPointsBlob> Value;
    }

    public struct ZombieSpawnPointsBlob
    {
        public BlobArray<float3> Value;
    }

    public struct ZombieWalkProperties : IComponentData, IEnableableComponent
    {
        public float WalkSpeed;
        public float WalkAmplitude;
        public float WalkFrequency;
    }

    public struct ZombieTimer : IComponentData
    {
        public float Value;
    }

    public struct ZombieHeading : IComponentData
    {
        public float Value;
    }

    public struct NewZombieTag : IComponentData { }
}