using UnityEngine;

namespace PackItUp.Controllers {
    [CreateAssetMenu(fileName = "CharacterMoveConfig", menuName = "ScriptableObjects/CharacterMoveConfig", order = 0)]
    public class CharacterMoveConfig : ScriptableObject {
        public float moveSpeed = 1f;    
    }
}