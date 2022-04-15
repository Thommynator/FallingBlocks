using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    protected Rigidbody _body;

    public virtual void Start()
    {
        _body = GetComponent<Rigidbody>();
        gameObject.AddComponent<SelfDestroy>();
    }

}