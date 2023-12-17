using Unity.Entities;
using UnityEngine;

namespace AuthoringAndMono
{
    public class BrainMono : MonoBehaviour
    {
    }

    public class BrainBaker : Baker<BrainMono>
    {
        public override void Bake(BrainMono authoring)
        {
            var brainEntity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(brainEntity, new ComponentsAndTags.BrainTag());
        }
    }
}