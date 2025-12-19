using UnityEngine;

public class LookInMovementDirection : MonoBehaviour {
    private Rigidbody _body;

    private void Start() {
        _body = GetComponent<Rigidbody>();
    }

    void Update() {
        Vector3 direction = (transform.position + _body.linearVelocity).InXZPlane(transform.position.y);
        transform.LookAt(direction, Vector3.up);
    }
}