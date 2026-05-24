using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;

namespace Enemies.Spawn {
    public class EnemySpawner : MonoBehaviour {
        [SerializeField] private List<SpawnWave> spawnWaves;
        private int _currentWave;

        private void Start() {
            StartCoroutine(WaveLoop());
        }

        private IEnumerator WaveLoop() {
            while (true) {
                var spawnWave = spawnWaves[_currentWave];
                Debug.Log($"Starting wave {spawnWave.name}");
                var coroutines = spawnWave.spawnProperties.Select(spawnProperty => StartCoroutine(StartSpawnLoop(spawnProperty))).ToList();

                yield return new WaitForSeconds(spawnWave.waveDurationSeconds);

                foreach (var coroutine in coroutines) {
                    StopCoroutine(coroutine);
                }

                _currentWave = (_currentWave + 1) % spawnWaves.Count;
            }
        }

        private void SpawnGameObject(SpawnProperties spawnProperty) {
            Vector3 spawnPosition = LevelGenerator.Instance.GetRandomCubePosition();
            Instantiate(spawnProperty.prefab, spawnPosition + 2 * Vector3.up, Quaternion.identity);
        }


        private IEnumerator StartSpawnLoop(SpawnProperties spawnProperties) {
            while (true) {
                SpawnGameObject(spawnProperties);
                yield return new WaitForSeconds(spawnProperties.spawnIntervalSeconds);
            }
        }
    }
}