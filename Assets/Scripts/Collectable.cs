using UnityEngine;
using MoreMountains.Feedbacks;

public class Collectable : MonoBehaviour
{

    [SerializeField] private CollectableType _type;

    void Start()
    {
        // destroys itself after 30 seconds, be quick to catch it while it lives ;) 
        Destroy(this.gameObject, 30);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInChildren<MMF_Player>().PlayFeedbacks();
            CollectablesManager.Instance.Collect(_type);
        }
    }
}
