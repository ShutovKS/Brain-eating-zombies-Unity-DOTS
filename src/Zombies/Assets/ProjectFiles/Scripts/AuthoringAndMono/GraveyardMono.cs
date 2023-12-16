using Unity.Mathematics;
using UnityEngine;

namespace AuthoringAndMono
{
    public class GraveyardMono : MonoBehaviour 
    {
        public float2 FieldDemensions;
        public int NumberTombstonesToSpawn;
        public GameObject TombstonePrefab;
        public uint RandomSeed;
        public GameObject ZombiePrefab;
        public float ZombieSpawnRate;
    }
}