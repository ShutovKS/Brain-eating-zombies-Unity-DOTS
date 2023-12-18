using ComponentsAndTags;
using Unity.Entities;
using UnityEngine;

namespace AuthoringAndMono
{
    public class BrainMono : MonoBehaviour
    {
        public float HealthMax;
    }

    public class BrainBaker : Baker<BrainMono>
    {
        public override void Bake(BrainMono authoring)
        {
            var brainEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(brainEntity, new BrainTag());
            AddComponent(brainEntity, new BrainHealth(max: authoring.HealthMax, value: authoring.HealthMax));
            AddBuffer<BrainDamageBufferElement>(brainEntity);
        }
    }
}