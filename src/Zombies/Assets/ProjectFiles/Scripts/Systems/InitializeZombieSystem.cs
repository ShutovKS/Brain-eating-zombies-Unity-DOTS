using ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Systems
{
    [BurstCompile, UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct InitializeZombieSystem : ISystem
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
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            foreach (var zombieWalkAspect in SystemAPI.Query<ZombieWalkAspect>().WithAll<NewZombieTag>())
            {
                ecb.RemoveComponent<NewZombieTag>(zombieWalkAspect.Entity);
                ecb.SetComponentEnabled<ZombieWalkProperties>(zombieWalkAspect.Entity, false);
                ecb.SetComponentEnabled<ZombieEatProperties>(zombieWalkAspect.Entity, false);
            }
            
            ecb.Playback(state.EntityManager);
        }
    }
}