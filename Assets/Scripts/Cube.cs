using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _fallDelaySeconds;
    private WaitForSeconds _fallDelayWaitForSeconds;
    private Rigidbody _body;
    private BoxCollider _boxCollider;
    private MeshRenderer _meshRenderer;

    [SerializeField] private MMF_Player _spawnFeedback;
    [SerializeField] private MMF_Player _touchedFeedback;
    [SerializeField] private MMF_Player _fallingFeedback;
    [SerializeField] private Material _originalMaterial;

    private int _respawnHeight = -5;
    private bool _isBelowRespawnHeight = false;

    void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _meshRenderer.material = _originalMaterial;
        _spawnFeedback.Initialization();
        _touchedFeedback.Initialization();
        _fallingFeedback.Initialization();
        _fallDelayWaitForSeconds = new WaitForSeconds(_fallDelaySeconds);
    }

    void Update()
    {
        if (!_isBelowRespawnHeight && transform.position.y < _respawnHeight)
        {
            // Cube can be resetted when below the respawn height, but only once
            _isBelowRespawnHeight=true;
            StartCoroutine(LevelGenerator.Instance.ResetCube(this));
        }
    }

    public void SetPosition(Vector3 newPosition)
    {
        this.transform.position = newPosition;
    }

    public void SetParentTo(Transform parent)
    {
        this.transform.SetParent(parent);
    }

    public void SpawnActions()
    {
        _body.velocity = Vector3.zero;
        _body.isKinematic = true;
        _boxCollider.enabled = true;
        this.gameObject.SetActive(true);
        _spawnFeedback.PlayFeedbacks();

    }

    public void DeactivationActions()
    {
        this.gameObject.SetActive(false);
        this._isBelowRespawnHeight = false;
        ResetToOriginalColor();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerFall();
        }
    }


    public void TriggerFall()
    {
        StartCoroutine(Fall());
    }

    private IEnumerator Fall()
    {
        _touchedFeedback.PlayFeedbacks();
        yield return new WaitForSeconds(Random.Range(0, 0.5f));
        _fallingFeedback.PlayFeedbacks();
        yield return _fallDelayWaitForSeconds;
        _boxCollider.enabled = false;
        _body.isKinematic = false;
    }

    public void ResetToOriginalColor()
    {
        _meshRenderer.material = _originalMaterial;
    }



}
