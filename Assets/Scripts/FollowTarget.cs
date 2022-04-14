using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    [SerializeField] private GameObject _target;
    [SerializeField] private float height;

    void Update()
    {
        var targetPosition = _target.transform.position;
        this.transform.position = new Vector3(targetPosition.x, height, targetPosition.z);
    }
}
