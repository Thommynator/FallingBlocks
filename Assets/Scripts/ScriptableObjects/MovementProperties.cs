using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "MovementProperties", menuName = "ScriptableObjects/Movement", order = 1)]
    public class MovementProperties : ScriptableObject {
        public float maxSpeed;
        public float steeringForce;
    }
}