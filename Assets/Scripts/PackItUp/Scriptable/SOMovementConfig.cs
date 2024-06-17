using UnityEngine;

namespace PackItUp.Scriptable {
    [CreateAssetMenu(fileName = "MovementConfig", menuName = "ScriptableObjects/Data/MovementConfig", order = 1)]
    public class SOMovementConfig : ScriptableObject {
        public float topSpeed = 10f;
        public float acceleration = 1f;
        public float momentum = 0f;
        public float dashDuration = 0.2f;
        public float dashSpeed = 20f;
        public float dashCooldown = 5f;
    }
}