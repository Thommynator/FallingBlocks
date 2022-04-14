using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeNearPlayer : MonoBehaviour
{

    private GameObject _player;

    [SerializeField] private Explosion _explosion;

    [SerializeField] private float _distance;

    private bool _isExploded;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _isExploded = false;
    }

    void Update()
    {
        if (!_isExploded && Vector3.Distance(this.transform.position, _player.transform.position) < _distance)
        {
            _isExploded = true;
            _explosion.Explode();
            Destroy(this.gameObject);
        }
    }

    public bool IsExploded()
    {
        return _isExploded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distance);
    }

}
