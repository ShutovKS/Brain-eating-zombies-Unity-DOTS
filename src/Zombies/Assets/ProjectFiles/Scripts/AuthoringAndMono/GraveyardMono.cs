using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace AuthoringAndMono
{
    public class GraveyardMono : MonoBehaviour 
    {
        [FormerlySerializedAs("FieldDemensions")] public float2 FieldDimensions;
        public int NumberTombstonesToSpawn;
        public GameObject TombstonePrefab;
        public uint RandomSeed;
        public GameObject ZombiePrefab;
        public float ZombieSpawnRate;
    }
}