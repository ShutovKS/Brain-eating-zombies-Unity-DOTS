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
            
            AddComponent(new GraveyardProperties
            {
                FieldDemensions = authoring.FieldDemensions,
                NumberTombstonesToSpawn = authoring.NumberTombstonesToSpawn,
                TombstonePrefab = GetEntity(authoring.TombstonePrefab),
                ZombiePrefab = GetEntity(authoring.ZombiePrefab),
                ZombieSpawnRate = authoring.ZombieSpawnRate,
            });
            AddComponent(new GraveyardRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
            AddComponent<ZombieSpawnPoints>(graveyardEntity);
            AddComponent<ZombieSpawnTimer>(graveyardEntity);
        }
    }
}