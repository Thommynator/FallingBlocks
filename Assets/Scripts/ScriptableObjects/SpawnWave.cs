using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "SpawnWave", menuName = "ScriptableObjects/SpawnWave", order = 1)]
    public class SpawnWave : ScriptableObject {
        public List<SpawnProperties> spawnProperties;
        public float waveDurationSeconds;
    }
}