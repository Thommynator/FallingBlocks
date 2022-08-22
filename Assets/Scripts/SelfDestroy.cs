using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private float lowerHeightThreshold = -50;
    [SerializeField] private float upperHeightThreshold = 200;
    [SerializeField] private bool hasMaxLiveTime = false;
    [SerializeField] private float maxLiveTime = 20;


    void Start()
    {
        if (hasMaxLiveTime)
        {
            Destroy(gameObject, maxLiveTime);
        }

        StartCoroutine(CheckHeight());
    }

    private IEnumerator CheckHeight()
    {
        while (true)
        {
            if (transform.position.y < lowerHeightThreshold || transform.position.y > upperHeightThreshold)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
