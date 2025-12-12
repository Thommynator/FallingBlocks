using System.Collections;
using UnityEngine;

namespace Enemies {
    public class EnemySpawner : MonoBehaviour
    {

        [SerializeField] private BaseEnemy _enemyPrefab;
        [SerializeField] private float _waitTimeInSeconds;
        [SerializeField] private float _chance;

        private WaitForSeconds _spawnWaitForSeconds;

        private void Awake()
        {
            _spawnWaitForSeconds = new WaitForSeconds(_waitTimeInSeconds);
        }

        void Start()
        {
            StartCoroutine(SpawnLoop());
        }

        private void SpawnRandomEnemy() {
            if (!(Random.Range(0.0f, 1.0f) <= _chance)) return;
            Vector3 spawnPosition = LevelGenerator.Instance.GetRandomCubePosition();
            Instantiate(_enemyPrefab, spawnPosition + 2 * Vector3.up, Quaternion.identity);
        }

        IEnumerator SpawnLoop()
        {
            while (true)
            {
                yield return _spawnWaitForSeconds;
                SpawnRandomEnemy();
            }
        }


    }
}
