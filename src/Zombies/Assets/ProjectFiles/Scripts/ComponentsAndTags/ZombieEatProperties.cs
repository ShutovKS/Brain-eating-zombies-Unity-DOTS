using Unity.Entities;

namespace ComponentsAndTags
{
    public struct ZombieEatProperties : IComponentData, IEnableableComponent
    {
        public float EatDamagePerSecond;
        public float EatAmplitude;
        public float EatFrequency;
    }
}