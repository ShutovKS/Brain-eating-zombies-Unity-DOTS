using Unity.Entities;
using Unity.Mathematics;

namespace ComponentsAndTags
{
    public struct GraveyardRandom : IComponentData
    {
        public Random Value;
    }
}