using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private BaseEnemy _enemyPrefab;
    [SerializeField] private float _waitTimeInSeconds;
    [SerializeField] private float _chance;


    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private void SpawnRandomEnemy()
    {
        if (Random.Range(0.0f, 1.0f) <= _chance)
        {
            Vector3 spawnPosition = LevelGenerator.Instance.GetRandomCubePosition();
            GameObject.Instantiate(_enemyPrefab, spawnPosition + 2 * Vector3.up, Quaternion.identity);
        }
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(_waitTimeInSeconds);
            SpawnRandomEnemy();
        }
    }


}
