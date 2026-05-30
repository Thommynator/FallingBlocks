using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "SpawnProperties", menuName = "ScriptableObjects/SpawnProperties", order = 1)]
    public class SpawnProperties : ScriptableObject {
        public GameObject prefab;
        public float spawnIntervalSeconds;
        public int batchSize = 1;
    }
}