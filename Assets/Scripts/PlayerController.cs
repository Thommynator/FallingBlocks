using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _movementForceFactor;
    private Rigidbody _body;
    private Vector2 _currentMovementInput;

    void Awake() {
        _body = GetComponent<Rigidbody>();
    }

    void Update() {
        if (transform.position.y < -20) {
            SceneManager.LoadScene("GameScene");
        }
    }

    void FixedUpdate() {
        Move();
    }

    private void Move() {
        var movement = new Vector3(_currentMovementInput.x, 0, _currentMovementInput.y);
        _body.AddForce(_movementForceFactor * movement);

        if (_body.linearVelocity.magnitude > _maxSpeed) {
            _body.linearVelocity = _maxSpeed * _body.linearVelocity.normalized;
        }
    }

    public void MovementInput(InputAction.CallbackContext context) {
        _currentMovementInput = context.ReadValue<Vector2>();
    }
}