using UnityEngine;

public class DestroyDeepDown : MonoBehaviour
{
    private float hightThreshold = -50;

    void Update()
    {
        if (transform.position.y < hightThreshold)
        {
            Destroy(gameObject);
        }
    }
}
