using ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Systems
{
    [BurstCompile, UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnTombstoneSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GraveyardProperties>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var graveyardEntity = SystemAPI.GetSingletonEntity<GraveyardProperties>();
            var graveyard = SystemAPI.GetAspect<GraveyardAspect>(graveyardEntity);

            var ecb = new EntityCommandBuffer(Allocator.Temp);

            for (var i = 0; i < graveyard.NumberTombstonesToSpawn; i++)
            {
                var newTombstone = ecb.Instantiate(graveyard.TombstonePrefab);
                var newTombstoneTransform = graveyard.GetRandomTombstoneTransform();
                ecb.SetComponent(newTombstone, newTombstoneTransform);
            }

            ecb.Playback(state.EntityManager);
        }
    }
}