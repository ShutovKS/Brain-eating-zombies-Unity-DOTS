using Unity.Collections;
using Unity.Entities;

namespace ComponentsAndTags
{
    public struct ZombieSpawnPoints : IComponentData
    {
        public BlobAssetReference<ZombieSpawnPointsBlob> Value;
    }
}