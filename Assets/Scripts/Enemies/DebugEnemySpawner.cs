using System.Collections.Generic;
using Sisus.Init;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Enemies {
    public class DebugEnemySpawner : MonoBehaviour<LevelGenerator> {
        [SerializeField] private List<BaseEnemy> enemies;
        private LevelGenerator _levelGenerator;

        protected override void Init(LevelGenerator levelGenerator) {
            _levelGenerator = levelGenerator;
        }

        public void SpawnEnemy1(InputAction.CallbackContext context) {
            if (context.phase != InputActionPhase.Performed) {
                return;
            }

            SpawnEnemy(enemies[0]);
        }

        public void SpawnEnemy2(InputAction.CallbackContext context) {
            if (context.phase != InputActionPhase.Performed) {
                return;
            }

            SpawnEnemy(enemies[1]);
        }

        public void SpawnEnemy3(InputAction.CallbackContext context) {
            if (context.phase != InputActionPhase.Performed) {
                return;
            }

            SpawnEnemy(enemies[2]);
        }


        private void SpawnEnemy(BaseEnemy enemy) {
            Vector3 spawnPosition = _levelGenerator.GetRandomCubePosition();
            Instantiate(enemy, spawnPosition + 2 * Vector3.up, Quaternion.identity);
        }
    }
}