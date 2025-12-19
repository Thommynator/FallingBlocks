using System.Collections;
using MoreMountains.Feedbacks;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Jumper : MonoBehaviour {
    [SerializeField] private JumpProperties jumpProperties;
    [SerializeField] private MMF_Player spaceJumpFeedback;

    [FormerlySerializedAs("groundLayer")] [SerializeField]
    private LayerMask groundLayerMask;

    private Rigidbody _body;
    private bool _isJumping;
    private float _lastGroundedTime;

    void Start() {
        _body = GetComponent<Rigidbody>();
        _isJumping = false;
    }

    private void OnCollisionEnter(Collision other) {
        if (IsInLayerMask(other.gameObject, groundLayerMask) && Physics.Raycast(transform.position, Vector3.down, 3f, groundLayerMask)) {
            _lastGroundedTime = Time.time;
            _isJumping = false;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_body.position, jumpProperties.blastRadius);
    }

    // Used in Input System Event
    public void Jump(InputAction.CallbackContext context) {
        if (context.phase != InputActionPhase.Performed) {
            return;
        }

        Debug.Log($"Is jumping {_isJumping}");
        if (_isJumping || !CanCoyoteJump()) {
            return;
        }

        // I'm not using the suggested OverlapSphereNonAlloc because then I need to pre-allocate the array size and I have the clear the array after use
        var cubeColliders = Physics.OverlapSphere(_body.position, jumpProperties.blastRadius, groundLayerMask);
        foreach (var cubeCollider in cubeColliders) {
            if (cubeCollider == null) continue;
            cubeCollider.TryGetComponent(out Cube cube);
            cube?.TriggerFall();
        }

        _body.AddForce(jumpProperties.jumpForceFactor * Vector3.up, ForceMode.Impulse);
        Debug.Log("JUMP");
        _isJumping = true;
    }

    // Used in Input System Event
    public void SpaceJump(InputAction.CallbackContext context) {
        if (context.phase != InputActionPhase.Performed) {
            return;
        }

        if (!CollectablesManager.Instance.TryToUseSpaceJump()) return;
        spaceJumpFeedback.PlayFeedbacks();
        StartCoroutine(SpaceJumpLoop());
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask) {
        // Convert the object's layer to a bitfield for comparison
        var objLayerMask = (1 << obj.layer);
        return (layerMask.value & objLayerMask) > 0;
    }

    private bool CanCoyoteJump() {
        return jumpProperties.coyoteJumpEnabled && Time.time - _lastGroundedTime <= jumpProperties.coyoteTimeSeconds;
    }

    private IEnumerator SpaceJumpLoop() {
        for (var i = 0; i < jumpProperties.spaceJumpIterations; i++) {
            _body.AddForce(jumpProperties.spaceJumpForceFactor * Vector3.up, ForceMode.Acceleration);
            yield return new WaitForEndOfFrame();
        }
    }
}