using ScriptableObjects;
using UnityEngine;

namespace Enemies.Behavior {
    public class FollowMovement : IMovementBehavior {
        private readonly MovementProperties _movementProperties;
        private readonly Rigidbody _body;

        public FollowMovement(MovementProperties movementProperties, Rigidbody selfRigidBody) {
            _movementProperties = movementProperties;
            _body = selfRigidBody;
        }

        public Vector3 MoveTo(Vector3 currentPosition, Vector3 target) {
            var desiredVelocity = (target - currentPosition).normalized * _movementProperties.maxSpeed;
            return Vector3
                .ClampMagnitude(desiredVelocity - _body.linearVelocity, _movementProperties.steeringForce)
                .InXZPlane(currentPosition.y);
        }
    }
}