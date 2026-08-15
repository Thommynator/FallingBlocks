using UnityEngine;

public class Explosion : MonoBehaviour {
    [SerializeField] private float _explosionRadius;
    [SerializeField] private GameObject _explosionFx;

    public void Explode() {
        Instantiate(_explosionFx, transform.position, Quaternion.identity);

        Collider[] hitObjects = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (var hitObject in hitObjects) {
            if (hitObject.CompareTag("Player") || hitObject.CompareTag("Enemy")) {
                Debug.Log($"Apply explosion force to {hitObject.gameObject.name}");
                hitObject.transform.GetComponent<Rigidbody>().AddExplosionForce(50, transform.position, 3 * _explosionRadius, 1, ForceMode.Impulse);
            }

            hitObject.TryGetComponent(out Cube cube);
            cube?.TriggerFall();
        }
    }
}