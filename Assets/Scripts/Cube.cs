using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

public class Cube : MonoBehaviour {
    [SerializeField] private float fallDelaySeconds;
    [SerializeField] private float fallDurationSeconds;

    [SerializeField] private MMF_Player spawnFeedback;
    [SerializeField] private MMF_Player touchedFeedback;
    [SerializeField] private MMF_Player fallingFeedback;
    [SerializeField] private Material originalMaterial;

    private Rigidbody _body;
    private BoxCollider _boxCollider;
    private WaitForSeconds _fallDelayWaitForSeconds;
    private WaitForSeconds _fallDurationWaitForSeconds;

    private MeshRenderer _meshRenderer;

    protected void Awake() {
        _body = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _meshRenderer.material = originalMaterial;
        spawnFeedback.Initialization();
        touchedFeedback.Initialization();
        fallingFeedback.Initialization();
        _fallDelayWaitForSeconds = new WaitForSeconds(fallDelaySeconds);
        _fallDurationWaitForSeconds = new WaitForSeconds(fallDurationSeconds);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            TriggerFall();
        }
    }

    public void SetPosition(Vector3 newPosition) {
        transform.position = newPosition;
    }

    public void SetParentTo(Transform parent) {
        transform.SetParent(parent);
    }

    public void SpawnActions() {
        // stop fall process if cube is spawned while falling
        StopCoroutine(Fall());

        _body.isKinematic = true;
        _boxCollider.enabled = true;
        gameObject.SetActive(true);
        spawnFeedback.PlayFeedbacks();
    }

    public void DeactivationActions() {
        ResetToOriginalColor();
        gameObject.SetActive(false);
    }


    public void TriggerFall() {
        StartCoroutine(Fall());
    }

    private IEnumerator Fall() {
        touchedFeedback.PlayFeedbacks();
        // short time between touching and falling trigger
        yield return new WaitForSeconds(0.25f);
        fallingFeedback.PlayFeedbacks();
        yield return _fallDelayWaitForSeconds;
        _boxCollider.enabled = false;
        _body.isKinematic = false;
        yield return _fallDurationWaitForSeconds;
        DeactivationActions();
    }

    private void ResetToOriginalColor() {
        _meshRenderer.material = originalMaterial;
    }
}