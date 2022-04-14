using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShootingEnemy : FollowerEnemy
{

    [SerializeField] private float _shootingAngleInDeg;
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private float _shootingCooldownInSeconds;


    public override void Start()
    {
        base.Start();
        StartCoroutine(Fire());
    }

    void Update()
    {
        transform.LookAt(_target.transform, Vector3.up);
    }

    IEnumerator Fire()
    {
        while (true)
        {
            Bomb bomb = GameObject.Instantiate<Bomb>(_bombPrefab, transform.position, Quaternion.identity);
            bomb.FireTo(_target.transform.position, _shootingAngleInDeg);
            yield return new WaitForSeconds(_shootingCooldownInSeconds);
        }
    }

}
