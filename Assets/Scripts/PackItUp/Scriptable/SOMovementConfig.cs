using UnityEngine;

namespace PackItUp.Scriptable {
    [CreateAssetMenu(fileName = "MovementConfig", menuName = "ScriptableObjects/Data/MovementConfig", order = 1)]
    public class SOMovementConfig : ScriptableObject {
        public float topSpeed = 10;
        public float acceleration = 1f;
        public float momentum = 0f;
    }
}