using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShootingEnemy : FollowerEnemy
{

    [SerializeField] private float _shootingAngleInDeg;
    [SerializeField] private float _shootingCooldownInSeconds;

    private GameObject _barrel;


    public override void Start()
    {
        base.Start();
        _barrel = transform.Find("Body").Find("Barrel").gameObject;
        StartCoroutine(Fire());
    }

    void Update()
    {

        // transform.LookAt(_target.transform, Vector3.up);
        Vector3 targetOnGround = _target.transform.position.InXZPlane(transform.position.y);
        transform.LookAt(targetOnGround);
        _barrel.transform.LookAt(_target.transform.position);
        Debug.DrawLine(transform.position, _target.transform.position);
        Debug.DrawLine(targetOnGround, _target.transform.position);
        Debug.DrawLine(targetOnGround, transform.position);
    }

    IEnumerator Fire()
    {
        while (true)
        {
            Bomb bomb = BombPool.Instance.GetBomb();
            bomb.transform.position = transform.position + Vector3.up;
            bomb.FireTo(_target.transform.position, _shootingAngleInDeg);
            yield return new WaitForSeconds(_shootingCooldownInSeconds);
        }
    }

}
