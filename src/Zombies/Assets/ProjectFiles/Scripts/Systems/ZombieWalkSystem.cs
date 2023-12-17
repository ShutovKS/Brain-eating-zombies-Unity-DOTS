using ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    [BurstCompile, UpdateAfter(typeof(SpawnZombieSystem))]
    public partial struct ZombieWalkSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
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
            var brainRadius = brainScale * 30f + 0.5f;

            new ZombieWalkJob
            {
                DeltaTime = deltaTime,
                BrakingDistanceSquared = brainRadius,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct ZombieWalkJob : IJobEntity
    {
        public float DeltaTime;
        public float BrakingDistanceSquared;
        public EntityCommandBuffer.ParallelWriter ECB;

        [BurstCompile]
        private void Execute(ZombieWalkAspect zombieWalkAspect, [EntityIndexInQuery] int sortKey)
        {
            zombieWalkAspect.Walk(DeltaTime);
            if (zombieWalkAspect.IsInStoppingRange(float3.zero, BrakingDistanceSquared))
            {
                ECB.SetComponentEnabled<ZombieWalkProperties>(sortKey, zombieWalkAspect.Entity, false);
                ECB.SetComponentEnabled<ZombieEatProperties>(sortKey, zombieWalkAspect.Entity, true);
            }
        }
    }
}