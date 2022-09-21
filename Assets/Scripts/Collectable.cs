using UnityEngine;
using MoreMountains.Feedbacks;

public class Collectable : MonoBehaviour
{

    [SerializeField] private CollectableType _type;

    private MMF_Player _collectionFeedback;

    private void Awake()
    {
        _collectionFeedback = GetComponentInChildren<MMF_Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           _collectionFeedback.PlayFeedbacks();
            CollectablesManager.Instance.Collect(_type);
        }
    }
}
