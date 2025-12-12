using UnityEngine;

namespace Enemies.Behavior {
    public interface IMovementBehavior {
        Vector3 MoveTo(Vector3 currentPosition, Vector3 target);
    }
}