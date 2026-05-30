using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies.Behavior;
using UnityEngine;

namespace Enemies {
    public class RepairBotEnemy : FollowerEnemy {
        [SerializeField] private int repairRange;
        [SerializeField] private float repairIntervalSeconds;
        [SerializeField] private float followDistanceThreshold;
        [SerializeField] private LayerMask groundLayerMask;

        private ExplodeNearPlayer _explodeNearPlayer;
        private IMovementBehavior _followMovement;
        private IMovementBehavior _randomWalkMovement;

        private WaitForSeconds _repairIntervalWaitForSeconds;

        private void Awake() {
            _repairIntervalWaitForSeconds = new WaitForSeconds(repairIntervalSeconds);
        }

        public override void Start() {
            base.Start();
            _followMovement = movementBehavior;
            _randomWalkMovement = new RandomWalkMovement(movementProperties, _body);
            TryGetComponent(out _explodeNearPlayer);
            StartCoroutine(RepairSurroundingCubes());
        }

        public void Update() {
            movementBehavior = transform.position.IsNear(target.transform.position, followDistanceThreshold) ? _followMovement : _randomWalkMovement;
        }

        private IEnumerator RepairSurroundingCubes() {
            while (!_explodeNearPlayer.IsExploded()) {
                var positions = GetSurroundingPositionsInSquare(transform.position, repairRange);

                // only enable cubes if the repair bot is above them, don't spawn cubes from below
                foreach (var position in positions.Where(cubePosition => transform.position.y >= cubePosition.y)) {
                    LevelGenerator.Instance.EnableCube(position.To2dInt());
                }

                yield return _repairIntervalWaitForSeconds;
            }
        }

        /**
         * Gets all 2D positions around the current position inside the radius in a square.
        */
        private List<Vector3> GetSurroundingPositionsInSquare(Vector3 position, int radius) {
            List<Vector3> positions = new List<Vector3>();
            for (var x = -radius; x <= radius; x++) {
                for (var z = -radius; z <= radius; z++) {
                    positions.Add(new Vector3(position.x + x, 0, position.z + z));
                }
            }

            return positions;
        }
    }
}