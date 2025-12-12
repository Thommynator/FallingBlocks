using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private LayerMask _cubeLayer;
    private MMFeedbacks _explosionFeedback;

    void Awake()
    {
        _explosionFeedback = GetComponent<MMFeedbacks>();
    }

    public void Explode()
    {
        _explosionFeedback.PlayFeedbacks();
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (var hitObject in hitObjects)
        {
            if (hitObject.CompareTag("Player"))
            {
                hitObject.transform.parent.GetComponent<Rigidbody>().AddExplosionForce(50, transform.position, 3 * _explosionRadius, 1, ForceMode.Impulse);
            }

            hitObject.TryGetComponent(out Cube cube);
            cube?.TriggerFall();
        }
    }
}
