using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _fallDelaySeconds;

    private Rigidbody _body;
    private float _heat;
    [SerializeField] private MMF_Player _touchedFeedback;
    [SerializeField] private MMF_Player _fallingFeedback;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
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
        LevelGenerator.Instance.ResetCubeAt(this.transform.position);
    }


}
