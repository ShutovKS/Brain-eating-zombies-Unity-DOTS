using Unity.Mathematics;

namespace Unit
{
    public static class MathHelpers
    {
        public static float GetHeading(float3 objectPosition, float3 targetPosition)
        {
            var x = targetPosition.x - objectPosition.x;
            var y = targetPosition.z - objectPosition.z;
            return math.atan2(y, x) + math.PI;
        }
    }
}