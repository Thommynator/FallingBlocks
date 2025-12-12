using UnityEngine;

namespace Enemies {
    public class BaseEnemy : MonoBehaviour
    {
        protected Rigidbody _body;

        public virtual void Start()
        {
            _body = GetComponent<Rigidbody>();
        }

    }
}