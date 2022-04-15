using UnityEngine;
using MoreMountains.Feedbacks;

public class Collectable : MonoBehaviour
{

    [SerializeField] private CollectableType _type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInChildren<MMF_Player>().PlayFeedbacks();
            CollectablesManager.Instance.Collect(_type);
        }
    }
}
