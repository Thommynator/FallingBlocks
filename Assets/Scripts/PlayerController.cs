using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MoreMountains.Feedbacks;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _movementForceFactor;
    [SerializeField] private float _jumpForceFactor;
    [SerializeField] private float _spaceJumpForceFactor;
    [SerializeField] private MMF_Player _spaceJumpFeedback;
    private Vector2 _currentMovementInput;
    private Rigidbody _body;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y < -20)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _body.AddForce(_movementForceFactor * new Vector3(_currentMovementInput.x, 0, _currentMovementInput.y));

        if (_body.velocity.magnitude > _maxSpeed)
        {
            _body.velocity = _maxSpeed * _body.velocity.normalized;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        _body.AddForce(_jumpForceFactor * Vector3.up, ForceMode.Impulse);
    }

    public void SpaceJump(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }

        if (CollectablesManager.Instance.TryToUseSpaceJump())
        {
            _spaceJumpFeedback.PlayFeedbacks();
            StartCoroutine(SpaceJumpLoop());
        }
    }

    private IEnumerator SpaceJumpLoop()
    {
        for (int i = 0; i < 10; i++)
        {
            _body.AddForce(_spaceJumpForceFactor * Vector3.up, ForceMode.Acceleration);
            yield return new WaitForEndOfFrame();
        }
    }

    public void MovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
    }

}
