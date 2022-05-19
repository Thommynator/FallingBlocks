using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _fallDelaySeconds;
    private Rigidbody _body;
    [SerializeField] private MMF_Player _touchedFeedback;
    [SerializeField] private MMF_Player _fallingFeedback;
    [SerializeField] private Material _originalMaterial;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        GetComponentInChildren<MeshRenderer>().material = _originalMaterial;
    }

    void Update()
    {
        if (transform.position.y < -50)
        {
            LevelGenerator.Instance.ResetCube(this);

        }
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
