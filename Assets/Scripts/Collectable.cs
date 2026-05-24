using MoreMountains.Feedbacks;
using Sisus.Init;
using UnityEngine;

public class Collectable : MonoBehaviour<CollectablesManager> {
    [SerializeField] private CollectableType type;
    private CollectablesManager _collectablesManager;

    private MMF_Player _collectionFeedback;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            _collectionFeedback.PlayFeedbacks();
            _collectablesManager.Collect(type);
        }
    }

    protected override void Init(CollectablesManager collectablesManager) {
        _collectablesManager = collectablesManager;
    }

    protected override void OnAwake() {
        base.OnAwake();
        _collectionFeedback = GetComponentInChildren<MMF_Player>();
    }
}