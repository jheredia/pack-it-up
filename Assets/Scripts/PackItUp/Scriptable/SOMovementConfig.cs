using UnityEngine;

namespace PackItUp.Scriptable {
    [CreateAssetMenu(fileName = "MovementConfig", menuName = "ScriptableObjects/Data/MovementConfig", order = 1)]
    public class SOMovementConfig : ScriptableObject {
        public float topSpeed = 15f;
        public float acceleration = 200f;
        public float turnRate = 0.95f;
        public float drag = 0.8f;
        public float drift = 0.8f;
        public float dashDuration = 0.1f;
        public float dashMultiplier = 2.5f;
        public float dashCooldown = 5f;
    }
}