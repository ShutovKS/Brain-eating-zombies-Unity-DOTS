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
        public Random Value;
    }

    public readonly partial struct GraveyardAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transformAspect;

        private readonly RefRO<GraveyardProperties> _graveyardProperties;
        private readonly RefRW<GraveyardRandom> _graveyardRandom;

        public int NumberTombstonesToSpawn => _graveyardProperties.ValueRO.NumberTombstonesToSpawn;
        public Entity TombstonePrefab => _graveyardProperties.ValueRO.TombstonePrefab;

        public LocalTransform GetRandomTombstoneTransform()
        {
            return new LocalTransform
            {
                Position = GetRandomPosition(),
                Rotation = GetRandomRotation(),
                Scale = GetRandomScale(0.5f)
            };
        }

        private const float BRAIN_SAFETY_RADIUS_SQ = 100;

        private float3 GetRandomPosition()
        {
            float3 randomPosition;

            do
            {
                randomPosition = _graveyardRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            } while (math.distancesq(_transformAspect.ValueRO.Position, randomPosition) <= BRAIN_SAFETY_RADIUS_SQ);


            return randomPosition;
        }

        private float3 MinCorner => _transformAspect.ValueRO.Position - HalfDimensions;
        private float3 MaxCorner => _transformAspect.ValueRO.Position + HalfDimensions;

        private float3 HalfDimensions => new()
        {
            x = _graveyardProperties.ValueRO.FieldDemensions.x * 0.5f,
            y = 0f,
            z = _graveyardProperties.ValueRO.FieldDemensions.y * 0.5f
        };
        
        private quaternion GetRandomRotation() => quaternion.RotateY(_graveyardRandom.ValueRW.Value.NextFloat(-0.25f, 0.25f));
        private float GetRandomScale(float min) => _graveyardRandom.ValueRW.Value.NextFloat(min, 1f);
    }
}