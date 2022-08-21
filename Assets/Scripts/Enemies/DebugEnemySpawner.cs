using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugEnemySpawner : MonoBehaviour
{

    [SerializeField] private List<BaseEnemy> enemies;

    public void SpawnEnemy1(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        SpawnEnemy(enemies[0]);
    }

    public void SpawnEnemy2(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        SpawnEnemy(enemies[1]);
    }

    public void SpawnEnemy3(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        SpawnEnemy(enemies[2]);
    }


    private void SpawnEnemy(BaseEnemy enemy)
    {
        Vector3 spawnPosition = LevelGenerator.Instance.GetRandomCubePosition();
        GameObject.Instantiate(enemy, spawnPosition + 2 * Vector3.up, Quaternion.identity);

    }
}
