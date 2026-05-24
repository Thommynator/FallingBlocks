using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using Sisus.Init;
using UnityEngine;

namespace Enemies.Spawn {
    public class EnemySpawner : MonoBehaviour<PlayerController, LevelGenerator> {
        [SerializeField] private List<SpawnWave> spawnWaves;
        private int _currentWave;
        private LevelGenerator _levelGenerator;
        private PlayerController _player;

        private void Start() {
            StartCoroutine(WaveLoop());
        }

        protected override void Init(PlayerController player, LevelGenerator levelGenerator) {
            _player = player;
            _levelGenerator = levelGenerator;
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
            Instantiate(spawnProperty.prefab, GetSpawnPosition() + 2 * Vector3.up, Quaternion.identity);
        }

        private Vector3 GetSpawnPosition() {
            Vector3 spawnPosition;
            do {
                spawnPosition = _levelGenerator.GetRandomCubePosition();
            } while (spawnPosition.IsNear(_player.transform.position, 3));

            return spawnPosition;
        }


        private IEnumerator StartSpawnLoop(SpawnProperties spawnProperties) {
            while (true) {
                SpawnGameObject(spawnProperties);
                yield return new WaitForSeconds(spawnProperties.spawnIntervalSeconds);
            }
        }
    }
}