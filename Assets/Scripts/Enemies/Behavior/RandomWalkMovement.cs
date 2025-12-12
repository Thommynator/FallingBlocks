using UnityEngine;

namespace Enemies.Behavior {
    public class RandomWalkMovement : IMovementBehavior {
        private readonly Rigidbody _body;
        private readonly float _maxSpeed;
        private readonly float _steeringForce;
        private Vector3 _currentRandomTarget;

        public RandomWalkMovement(float maxSpeed, float steeringForce, Rigidbody selfRigidBody) {
            _maxSpeed = maxSpeed;
            _steeringForce = steeringForce;
            _currentRandomTarget = GetRandomTarget();
            _body = selfRigidBody;
        }

        public Vector3 MoveTo(Vector3 currentPosition, Vector3 target) {
            if (currentPosition.IsNear(_currentRandomTarget, 5f)) {
                _currentRandomTarget = GetRandomTarget();
            }

            var desiredVelocity = (_currentRandomTarget - currentPosition).normalized * _maxSpeed;
            return Vector3
                .ClampMagnitude(desiredVelocity - _body.linearVelocity, _steeringForce)
                .InXZPlane(currentPosition.y);
        }

        private Vector3 GetRandomTarget() {
            return LevelGenerator.Instance.GetRandomCubePosition();
        }
    }
}