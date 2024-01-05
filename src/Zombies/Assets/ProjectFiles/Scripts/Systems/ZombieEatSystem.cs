using ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    [BurstCompile, UpdateAfter(typeof(ZombieWalkSystem))]
    public partial struct ZombieEatSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BrainTag>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var brainEntity = SystemAPI.GetSingletonEntity<BrainTag>();
            var brainScale = SystemAPI.GetComponent<LocalTransform>(brainEntity).Scale;
            var brainRadius = brainScale * 5f + 1f;
            
            new ZombieEatJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
                BrainEntity = brainEntity,
                BrainRadiusSquared = brainRadius * brainRadius
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct ZombieEatJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;
        public Entity BrainEntity;
        public float BrainRadiusSquared;

        [BurstCompile]
        private void Execute(ZombieEatAspect zombieEatAspect, [EntityIndexInQuery] int sortKey)
        {
            if (zombieEatAspect.IsInEatingRange(float3.zero, BrainRadiusSquared))
            {
                zombieEatAspect.Eat(DeltaTime, ECB, sortKey, BrainEntity);
            }
            else
            {
                ECB.SetComponentEnabled<ZombieEatProperties>(sortKey, zombieEatAspect.Entity, false);
                ECB.SetComponentEnabled<ZombieWalkProperties>(sortKey, zombieEatAspect.Entity, true);
            }
        }
    }
}