using ComponentsAndTags;
using Unity.Entities;
using Unity.Mathematics;

namespace AuthoringAndMono
{
    public class GraveyardBaker : Baker<GraveyardMono>
    {
        public override void Bake(GraveyardMono authoring)
        {
            var graveyardEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(graveyardEntity, new GraveyardProperties
            {
                FieldDimensions = authoring.FieldDimensions,
                NumberTombstonesToSpawn = authoring.NumberTombstonesToSpawn,
                TombstonePrefab = GetEntity(authoring.TombstonePrefab, TransformUsageFlags.Dynamic),
                ZombiePrefab = GetEntity(authoring.ZombiePrefab, TransformUsageFlags.Dynamic),
                ZombieSpawnRate = authoring.ZombieSpawnRate
            });
            AddComponent(graveyardEntity, new GraveyardRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
            AddComponent<ZombieSpawnPoints>(graveyardEntity);
            AddComponent<ZombieSpawnTimer>(graveyardEntity);
        }
    }
}