using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemy : BaseEnemy
{

    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _steeringForce;
    protected GameObject _target;

    public override void Start()
    {
        base.Start();
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        LookAtTarget();
    }

    void FixedUpdate()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        Vector3 desiredVelocity = _maxSpeed * (_target.transform.position - transform.position).normalized;
        Vector3 steering = Vector3
            .ClampMagnitude(desiredVelocity - _body.velocity, _steeringForce)
            .InXZPlane(transform.position.y);
        _body.AddForce(steering);
    }

    private void LookAtTarget()
    {
        Vector3 direction = (transform.position + _body.velocity).InXZPlane(transform.position.y);
        Debug.DrawLine(transform.position, direction, Color.white);
        transform.LookAt(direction, Vector3.up);
    }



}
