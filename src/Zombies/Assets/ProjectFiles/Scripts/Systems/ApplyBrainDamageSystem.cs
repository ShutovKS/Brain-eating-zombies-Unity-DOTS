using ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    [BurstCompile, UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true),
     UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial struct ApplyBrainDamageSystem : ISystem
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
            foreach (var brainAspect in SystemAPI.Query<BrainAspect>())
            {
                brainAspect.DamageBrain();
            }
        }
    }
}