using PackItUp.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PackItUp.Scriptable {
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/Data/PlayerConfig", order = 2)]
    public class SOPlayerConfig : ScriptableObject {
        public PlayerValue player;
        
        public InputAction GetMoveAction(CharacterControllerBindings playerControls) {
            return player == PlayerValue.Player1 ? playerControls.P1Controls.Move : playerControls.P2Controls.Move;
        }
    }

    public enum PlayerValue {
        Player1 = 1,
        Player2 = 2
    }
}