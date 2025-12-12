using Enemies.Behavior;
using UnityEngine;

namespace Enemies {
    public class FollowerEnemy : BaseEnemy {
        [SerializeField] protected float _maxSpeed;
        [SerializeField] protected float _steeringForce;
        protected IMovementBehavior _movementBehavior;
        protected GameObject _target;

        public override void Start() {
            base.Start();
            _target = GameObject.FindGameObjectWithTag("Player");
            _movementBehavior = new FollowMovement(_maxSpeed, _steeringForce, _body);
        }

        public void Update() {
            LookInDrivingDirection();
        }

        void FixedUpdate() {
            MoveToTarget();
        }

        private void MoveToTarget() {
            _body.AddForce(_movementBehavior.MoveTo(transform.position, _target.transform.position));
        }

        private void LookInDrivingDirection() {
            Vector3 direction = (transform.position + _body.linearVelocity).InXZPlane(transform.position.y);
            transform.LookAt(direction, Vector3.up);
        }
    }
}