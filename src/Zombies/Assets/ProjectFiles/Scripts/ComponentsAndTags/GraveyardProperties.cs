using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ComponentsAndTags
{
    public struct GraveyardProperties : IComponentData
    {
        public float2 FieldDemensions;
        public int NumberTombstonesToSpawn;
        public Entity TombstonePrefab;
    }
    
    public struct GraveyardRandom : IComponentData
    {
        public Unity.Mathematics.Random Value;
    }

    public readonly partial struct GraveyardAspect : IAspect
    {
        public readonly Entity Entity;
        
        private readonly RefRW<LocalTransform> _transformAspect;
        
        private readonly RefRO<GraveyardProperties> _graveyardProperties;
        private readonly RefRW<GraveyardRandom> _graveyardRandom;
        
        public int NumberTombstonesToSpawn => _graveyardProperties.ValueRO.NumberTombstonesToSpawn;
        public Entity TombstonePrefab => _graveyardProperties.ValueRO.TombstonePrefab;
    }
}