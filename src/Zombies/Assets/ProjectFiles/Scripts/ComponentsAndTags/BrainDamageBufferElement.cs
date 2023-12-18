using Unity.Entities;

namespace ComponentsAndTags
{
    public struct BrainDamageBufferElement : IBufferElementData
    {
        public float Value;

        public BrainDamageBufferElement(float value)
        {
            Value = value;
        }
    }
}