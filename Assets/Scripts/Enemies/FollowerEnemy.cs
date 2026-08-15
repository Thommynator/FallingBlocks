using Enemies.Behavior;
using MoreMountains.Feedbacks;
using ScriptableObjects;
using UnityEngine;

namespace Enemies {
    public class FollowerEnemy : BaseEnemy {
        [SerializeField] protected MovementProperties movementProperties;
        [SerializeField] protected MMF_Player collisionFx;
        protected IMovementBehavior movementBehavior;
        protected GameObject target;
        protected Rigidbody targetRigidbody;


        public override void Start() {
            base.Start();
            target = GameObject.FindGameObjectWithTag("Player");
            targetRigidbody = target.GetComponent<Rigidbody>();
            movementBehavior = new FollowMovement(movementProperties, _body);
        }

        void FixedUpdate() {
            MoveToTarget();
        }

        private void OnCollisionEnter(Collision other) {
            if (!other.gameObject.CompareTag("Player")) return;
            var intensity = MMFeedbacksHelpers.Remap(other.relativeVelocity.magnitude, 0f, 10f, 0f, 1f);
            collisionFx?.PlayFeedbacks(transform.position, intensity);
        }

        private void MoveToTarget() {
            _body.AddForce(movementBehavior.MoveTo(transform.position, target.transform.position));
        }
    }
}