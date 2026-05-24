using System.Collections;
using MoreMountains.Feedbacks;
using Sisus.Init;
using UnityEngine;

public class Cube : MonoBehaviour<LevelGenerator> {
    [SerializeField] private float fallDelaySeconds;

    [SerializeField] private MMF_Player spawnFeedback;
    [SerializeField] private MMF_Player touchedFeedback;
    [SerializeField] private MMF_Player fallingFeedback;
    [SerializeField] private Material originalMaterial;

    private readonly int _respawnHeight = -5;
    private Rigidbody _body;
    private BoxCollider _boxCollider;
    private WaitForSeconds _fallDelayWaitForSeconds;
    private bool _isBelowRespawnHeight = false;
    private LevelGenerator _levelGenerator;
    private MeshRenderer _meshRenderer;

    void Update() {
        if (!_isBelowRespawnHeight && transform.position.y < _respawnHeight) {
            // Cube can be reset when below the respawn height, but only once
            _isBelowRespawnHeight = true;
            StartCoroutine(_levelGenerator.ResetCube(this));
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            TriggerFall();
        }
    }

    protected override void OnAwake() {
        base.OnAwake();
        _body = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _meshRenderer.material = originalMaterial;
        spawnFeedback.Initialization();
        touchedFeedback.Initialization();
        fallingFeedback.Initialization();
        _fallDelayWaitForSeconds = new WaitForSeconds(fallDelaySeconds);
    }

    protected override void Init(LevelGenerator levelGenerator) {
        _levelGenerator = levelGenerator;
    }

    public void SetPosition(Vector3 newPosition) {
        transform.position = newPosition;
    }

    public void SetParentTo(Transform parent) {
        transform.SetParent(parent);
    }

    public void SpawnActions() {
        _body.isKinematic = true;
        _boxCollider.enabled = true;
        gameObject.SetActive(true);
        spawnFeedback.PlayFeedbacks();
    }

    public void DeactivationActions() {
        gameObject.SetActive(false);
        _isBelowRespawnHeight = false;
        ResetToOriginalColor();
    }


    public void TriggerFall() {
        StartCoroutine(Fall());
    }

    private IEnumerator Fall() {
        touchedFeedback.PlayFeedbacks();
        yield return new WaitForSeconds(Random.Range(0, 0.5f));
        fallingFeedback.PlayFeedbacks();
        yield return _fallDelayWaitForSeconds;
        _boxCollider.enabled = false;
        _body.isKinematic = false;
    }

    private void ResetToOriginalColor() {
        _meshRenderer.material = originalMaterial;
    }
}