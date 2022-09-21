using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeNearPlayer : MonoBehaviour
{

    private GameObject _player;

    [SerializeField] private Explosion _explosion;

    [SerializeField] private float _distance;

    private float _distanceSquared;

    private bool _isExploded;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _isExploded = false;
        _distanceSquared = _distance * _distance;
    }

    void Update()
    {
        if (!_isExploded && IsCloseToPlayer())
        {
            _isExploded = true;
            _explosion.Explode();
            Destroy(this.gameObject);
        }
    }

    private bool IsCloseToPlayer()
    {
        return Vector3.SqrMagnitude(this.transform.position - _player.transform.position) < _distanceSquared;
    }

    public bool IsExploded()
    {
        return _isExploded;
    }

    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distance);
    }

}
