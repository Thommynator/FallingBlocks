using Enemies.Behavior;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies {
    public class FollowerEnemy : BaseEnemy {
        [SerializeField] protected MovementProperties movementProperties;
        protected IMovementBehavior movementBehavior;
        protected GameObject target;

        public override void Start() {
            base.Start();
            target = GameObject.FindGameObjectWithTag("Player");
            movementBehavior = new FollowMovement(movementProperties, _body);
        }

        void FixedUpdate() {
            MoveToTarget();
        }

        private void MoveToTarget() {
            _body.AddForce(movementBehavior.MoveTo(transform.position, target.transform.position));
        }
    }
}