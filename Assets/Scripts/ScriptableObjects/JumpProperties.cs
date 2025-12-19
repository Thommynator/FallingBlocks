using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "JumpProperties", menuName = "ScriptableObjects/Jump", order = 1)]
    public class JumpProperties : ScriptableObject {
        public bool coyoteJumpEnabled;
        public float coyoteTimeSeconds;
        public float jumpForceFactor;
        public float blastRadius;
        public float spaceJumpForceFactor;
        public float spaceJumpIterations;
    }
}