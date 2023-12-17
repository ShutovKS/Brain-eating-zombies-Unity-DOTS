using ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;

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

            new ZombieWalkJob
            {
                DeltaTime = deltaTime,
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct ZombieWalkJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(ZombieWalkAspect zombieWalkAspect)
        {
            zombieWalkAspect.Walk(DeltaTime);
        }
    }
}