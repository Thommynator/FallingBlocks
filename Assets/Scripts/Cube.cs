using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _fallDelaySeconds;
    private Rigidbody _body;

    [SerializeField] private MMF_Player _spawnFeedback;
    [SerializeField] private MMF_Player _touchedFeedback;
    [SerializeField] private MMF_Player _fallingFeedback;
    [SerializeField] private Material _originalMaterial;

    void Awake()
    {
        _body = GetComponent<Rigidbody>();
        GetComponentInChildren<MeshRenderer>().material = _originalMaterial;
        _spawnFeedback.Initialization();
        _touchedFeedback.Initialization();
        _fallingFeedback.Initialization();
    }

    void Update()
    {
        if (transform.position.y < -10)
        {
            LevelGenerator.Instance.ResetCube(this);

        }
    }

    public void SpawnActions()
    {
        _body.velocity = Vector3.zero;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.SetActive(true);
        _spawnFeedback.PlayFeedbacks();

    }

    public void DeactivationActions()
    {
        this.gameObject.SetActive(false);
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
        yield return new WaitForSeconds(_fallDelaySeconds);
        GetComponent<BoxCollider>().enabled = false;
        _body.isKinematic = false;
    }

    public void ResetToOriginalColor()
    {
        GetComponentInChildren<MeshRenderer>().material = _originalMaterial;
    }



}
