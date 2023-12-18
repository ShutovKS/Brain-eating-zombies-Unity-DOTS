using Unity.Entities;

namespace ComponentsAndTags
{
    public struct ZombieWalkProperties : IComponentData, IEnableableComponent
    {
        public float WalkSpeed;
        public float WalkAmplitude;
        public float WalkFrequency;
    }
}