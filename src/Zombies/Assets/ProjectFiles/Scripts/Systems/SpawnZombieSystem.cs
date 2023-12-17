using ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    [BurstCompile]
    public partial struct SpawnZombieSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ZombieSpawnTimer>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

            new SpawnZombieJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Schedule();
        }
    }

    [BurstCompile]
    public partial struct SpawnZombieJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;

        [BurstCompile]
        private void Execute(GraveyardAspect graveyardAspect)
        {
            graveyardAspect.ZombieSpawnTimer -= DeltaTime;
            
            if(!graveyardAspect.TimeToSpawnZombie) return;
            if(!graveyardAspect.ZombieSpawnPointInitialized()) return;

            graveyardAspect.ZombieSpawnTimer = graveyardAspect.ZombieSpawnRate;
            var newZombie = ECB.Instantiate(graveyardAspect.ZombiePrefab);

            var newZombieTransform = graveyardAspect.GetZombieSpawnPoint();
            ECB.SetComponent(newZombie, newZombieTransform);
        }
    }
}