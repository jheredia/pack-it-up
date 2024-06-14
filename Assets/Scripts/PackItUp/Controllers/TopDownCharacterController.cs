using PackItUp.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PackItUp.Controllers {

    [RequireComponent(typeof(Rigidbody2D))]
    public class TopDownCharacterController : MonoBehaviour {
        [SerializeField] private SOPlayerConfig config;
        private Vector2 _moveDirection;
        private Rigidbody2D _rb;
        private CharacterControllerBindings _playerControls;
        private InputAction _moveAction;


        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
            _playerControls = new CharacterControllerBindings();
            _moveAction = config.GetMoveAction(_playerControls);
            _moveAction.performed += ctx => _moveDirection = ctx.ReadValue<Vector2>();
            _moveAction.canceled += _ => _moveDirection = Vector2.zero;
        }

        private void OnEnable() {
            _moveAction.Enable();
        }

        private void OnDisable() {
            _moveAction.Disable();
        }

        private void FixedUpdate() {
            Move();
        }

        private void Move() {
            _rb.velocity = new Vector2(_moveDirection.x * config.moveSpeed, _moveDirection.y * config.moveSpeed);
        }
    }
}