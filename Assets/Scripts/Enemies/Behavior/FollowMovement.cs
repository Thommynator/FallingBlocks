using UnityEngine;

namespace Enemies.Behavior {
    public class FollowMovement : IMovementBehavior {
        private readonly float _maxSpeed;
        private readonly float _steeringForce;
        private readonly Rigidbody _body;

        public FollowMovement(float maxSpeed, float steeringForce, Rigidbody selfRigidBody) {
            _maxSpeed = maxSpeed;
            _steeringForce = steeringForce;
            _body = selfRigidBody;
        }

        public Vector3 MoveTo(Vector3 currentPosition, Vector3 target) {
            var desiredVelocity = (target - currentPosition).normalized * _maxSpeed;
            return Vector3
                .ClampMagnitude(desiredVelocity - _body.linearVelocity, _steeringForce)
                .InXZPlane(currentPosition.y);
        }
    }
}