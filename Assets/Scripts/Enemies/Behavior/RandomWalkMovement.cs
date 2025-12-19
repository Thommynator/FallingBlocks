using ScriptableObjects;
using UnityEngine;

namespace Enemies.Behavior {
    public class RandomWalkMovement : IMovementBehavior {
        private readonly Rigidbody _body;
        private readonly MovementProperties _movementProperties;
        private Vector3 _currentRandomTarget;

        public RandomWalkMovement(MovementProperties movementProperties, Rigidbody selfRigidBody) {
            _movementProperties = movementProperties;
            _currentRandomTarget = GetRandomTarget();
            _body = selfRigidBody;
        }

        public Vector3 MoveTo(Vector3 currentPosition, Vector3 target) {
            if (currentPosition.IsNear(_currentRandomTarget, 5f)) {
                _currentRandomTarget = GetRandomTarget();
            }

            var desiredVelocity = (_currentRandomTarget - currentPosition).normalized * _movementProperties.maxSpeed;
            return Vector3
                .ClampMagnitude(desiredVelocity - _body.linearVelocity, _movementProperties.steeringForce)
                .InXZPlane(currentPosition.y);
        }

        private Vector3 GetRandomTarget() {
            return LevelGenerator.Instance.GetRandomCubePosition();
        }
    }
}