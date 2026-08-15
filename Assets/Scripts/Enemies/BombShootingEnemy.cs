using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies {
    public class BombShootingEnemy : FollowerEnemy {
        [SerializeField] private float _shootingAngleInDeg;
        [SerializeField] private float _shootingCooldownInSeconds;

        private GameObject _barrel;
        private WaitForSeconds _shootingCooldownWaitForSeconds;

        private void Awake() {
            _shootingCooldownWaitForSeconds = new WaitForSeconds(_shootingCooldownInSeconds);
        }

        public override void Start() {
            base.Start();
            _barrel = transform.Find("Body").Find("Barrel").gameObject;
            StartCoroutine(Fire());
        }

        private void Update() {
            Vector3 targetOnGround = target.transform.position.InXZPlane(transform.position.y);
            transform.LookAt(targetOnGround);
            _barrel.transform.LookAt(target.transform.position);
        }

        private void OnDrawGizmos() {
            Debug.DrawLine(transform.position, GetPredictedTargetPosition(), Color.blue, 2f);
        }

        private IEnumerator Fire() {
            while (true) {
                Bomb bomb = BombPool.Instance.GetBomb();
                bomb.transform.position = transform.position + Vector3.up;
                bomb.FireTo(GetPredictedTargetPosition(), _shootingAngleInDeg);
                yield return _shootingCooldownWaitForSeconds;
            }
        }

        private Vector3 GetPredictedTargetPosition() {
            return target.transform.position + targetRigidbody.linearVelocity * Random.Range(0f, 2f);
        }
    }
}