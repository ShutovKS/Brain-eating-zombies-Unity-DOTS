using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ComponentsAndTags
{
    public readonly partial struct BrainAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRW<BrainHealth> _brainHealth;
        private readonly DynamicBuffer<BrainDamageBufferElement> _brainDamageBuffer;

        public void DamageBrain()
        {
            foreach (var brainDamageBufferElement in _brainDamageBuffer)
            {
                _brainHealth.ValueRW.Value -= brainDamageBufferElement.Value;
            }

            _brainDamageBuffer.Clear();
            
            var ltw = _transform.ValueRO;
            ltw.Scale = _brainHealth.ValueRO.Value / _brainHealth.ValueRO.Max;
            _transform.ValueRW = ltw;
        }
    }
}