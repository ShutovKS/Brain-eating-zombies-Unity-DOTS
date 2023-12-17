using ComponentsAndTags;
using Unity.Entities;
using UnityEngine;

namespace AuthoringAndMono
{
    public class ZombieMono : MonoBehaviour
    {
        public float RiseRate;
        public float WalkSpeed;
        public float WalkAmplitude;
        public float WalkFrequency;
    }

    public class ZombieBaker : Baker<ZombieMono>
    {
        public override void Bake(ZombieMono authoring)
        {
            var zombieEntity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(zombieEntity, new ZombieRiseRate { Value = authoring.RiseRate });
            AddComponent(zombieEntity, new ZombieWalkProperties
            {
                WalkSpeed = authoring.WalkSpeed,
                WalkAmplitude = authoring.WalkAmplitude,
                WalkFrequency = authoring.WalkFrequency
            });
            AddComponent<ZombieTimer>(zombieEntity);
            AddComponent<ZombieHeading>(zombieEntity);
            AddComponent<NewZombieTag>(zombieEntity);
        }
    }
}