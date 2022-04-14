using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private LayerMask _cubeLayer;
    private MMFeedbacks _feedback;

    void Start()
    {
        _feedback = GetComponent<MMFeedbacks>();
    }

    public void Explode()
    {
        _feedback.PlayFeedbacks();
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (var hitObject in hitObjects)
        {
            if (hitObject.CompareTag("Player"))
            {
                Debug.Log(Vector3.Distance(hitObject.transform.position, this.transform.position));
                hitObject.transform.parent.GetComponent<Rigidbody>().AddExplosionForce(50, this.transform.position, 3 * _explosionRadius, 1, ForceMode.Impulse);
            }

            hitObject.TryGetComponent<Cube>(out Cube cube);
            if (cube != null)
            {
                cube.TriggerFall();
            }
        }
    }


}
