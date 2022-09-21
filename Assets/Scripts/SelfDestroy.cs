using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private float _lowerHeightThreshold;
    [SerializeField] private float _upperHeightThreshold;
    [SerializeField] private bool _hasMaxLiveTime;
    [SerializeField] private float _maxLiveTime;

    private WaitForSeconds _waitFor200Ms;


    private void Awake()
    {
        _waitFor200Ms = new WaitForSeconds(0.2f);
    }

    void Start()
    {
        if (_hasMaxLiveTime)
        {
            Destroy(gameObject, _maxLiveTime);
        }

        StartCoroutine(CheckHeight());
    }

    private IEnumerator CheckHeight()
    {
        while (true)
        {
            if (transform.position.y < _lowerHeightThreshold || transform.position.y > _upperHeightThreshold)
            {
                Destroy(gameObject);
            }
            yield return _waitFor200Ms;
        }
    }
}
