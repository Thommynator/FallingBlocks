using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    private Rigidbody _body;
    private Vector3 _target;
    private Explosion _explosion;

    void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _explosion = GetComponentInChildren<Explosion>();
    }

    void Update()
    {
        AdjustOrientation();
    }

    public void FireTo(Vector3 target, float shootingAngleInDeg)
    {
        _target = target;
        Vector3 shootingVelocity = GetShootingVelocityVector(target - transform.position, shootingAngleInDeg);
        if (float.IsNaN(shootingVelocity.x) || float.IsNaN(shootingVelocity.y) || float.IsNaN(shootingVelocity.z))
        {
            return;
        }
        _body.AddForce(shootingVelocity, ForceMode.VelocityChange);
    }

    /// Computes v0 (scalar) that is needed to reach the distance with the given shooting angle
    private float GetShootingVelocityScalar(float distance, float shootingAngleInDeg)
    {
        float h0 = transform.position.y;
        float shootingAngleInRad = shootingAngleInDeg * Mathf.Deg2Rad;
        float denominator = 2 * (h0 + (distance * Mathf.Tan(shootingAngleInRad))) * Mathf.Cos(shootingAngleInRad) * Mathf.Cos(shootingAngleInRad);

        if (Mathf.Approximately(distance, 0.0f) || Mathf.Approximately(denominator, 0.0f))
        {
            return 0.0f;
        }

        return Mathf.Sqrt(distance * distance * -Physics.gravity.y / denominator);
    }

    private Vector3 GetShootingVelocityVector(Vector3 distanceVector, float shootingAngleInDeg)
    {
        float angleToWorldX = Vector3.SignedAngle(distanceVector, Vector3.right, Vector3.up);
        float v0 = GetShootingVelocityScalar(distanceVector.magnitude, shootingAngleInDeg);
        float shootingAngleInRad = shootingAngleInDeg * Mathf.Deg2Rad;
        float vx = v0 * Mathf.Cos(shootingAngleInRad) * Mathf.Cos(angleToWorldX * Mathf.Deg2Rad);
        float vy = v0 * Mathf.Sin(shootingAngleInRad);
        float vz = v0 * Mathf.Cos(shootingAngleInRad) * Mathf.Sin(angleToWorldX * Mathf.Deg2Rad);
        return new Vector3(vx, vy, vz);
    }

    /**
    * While flying up, the bomb aims in the shooting direction. 
    * When falling down, the bomb aims in the target direction.
    */
    private void AdjustOrientation()
    {
        if (_body.velocity.y > 0)
        {
            transform.LookAt(transform.position + _body.velocity, Vector3.up);
        }
        else
        {
            transform.LookAt(_target + Vector3.down, Vector3.up);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("GAME OVER");
            _explosion.Explode();
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Cube"))
        {
            _explosion.Explode();
            Destroy(this.gameObject);
        }
    }

}
