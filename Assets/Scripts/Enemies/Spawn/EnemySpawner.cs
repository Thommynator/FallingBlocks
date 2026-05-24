using System.Collections;
using UnityEngine;

namespace Enemies.Spawn {
    public class EnemySpawner : MonoBehaviour
    {

        [SerializeField] private BaseEnemy enemyPrefab;
        [SerializeField] private float waitTimeInSeconds;
        [SerializeField] private float chance;
        private WaitForSeconds _spawnWaitForSeconds;

        private void Awake()
        {
            _spawnWaitForSeconds = new WaitForSeconds(waitTimeInSeconds);
        }

        void Start()
        {
            StartCoroutine(spawnLoop());
        }

        private void spawnRandomEnemy() {
            if (!(Random.Range(0.0f, 1.0f) <= chance)) return;
            Vector3 spawnPosition = LevelGenerator.Instance.GetRandomCubePosition();
            Instantiate(enemyPrefab, spawnPosition + 2 * Vector3.up, Quaternion.identity);
        }

        private void spawnWave() {
            
        }

        private IEnumerator spawnLoop()
        {
            while (true)
            {
                yield return _spawnWaitForSeconds;
                spawnRandomEnemy();
            }
        }


    }
}
