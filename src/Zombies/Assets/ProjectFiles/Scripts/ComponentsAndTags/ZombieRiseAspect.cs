using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ComponentsAndTags
{
    public readonly partial struct ZombieRiseAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRO<ZombieRiseRate> _zombieRiseRate;

        public void Rise(float deltaTime)
        {
            _transform.ValueRW.Position += math.up() * _zombieRiseRate.ValueRO.Value * deltaTime;
        }

        public bool IsAboveGround => _transform.ValueRO.Position.y >= 0f;

        public void SetAtGroundLevel()
        {
            var position = _transform.ValueRO.Position;
            position.y = 0f;
            _transform.ValueRW.Position = position;
        }
    }

    public readonly partial struct ZombieWalkAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRW<ZombieTimer> _walkTimer;
        private readonly RefRO<ZombieWalkProperties> _walkProperties;
        private readonly RefRO<ZombieHeading> _heading;

        private float WalkSpeed => _walkProperties.ValueRO.WalkSpeed;
        private float WalkAmplitude => _walkProperties.ValueRO.WalkAmplitude;
        private float WalkFrequency => _walkProperties.ValueRO.WalkFrequency;
        private float Heading => _heading.ValueRO.Value;

        private float WalkTimer
        {
            get => _walkTimer.ValueRO.Value;
            set => _walkTimer.ValueRW.Value = value;
        }

        public void Walk(float deltaTime)
        {
            WalkTimer += deltaTime;
            _transform.ValueRW.Position += _transform.ValueRO.Forward() * WalkSpeed * deltaTime;
    
            var swayAngle = WalkAmplitude * math.sin(WalkFrequency * WalkTimer);
            _transform.ValueRW.Rotation = quaternion.Euler(0, Heading, swayAngle);
        }
    }
}