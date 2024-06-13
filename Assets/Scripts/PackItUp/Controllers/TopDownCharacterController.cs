using System;
using PackItUp.InputSystem;
using UnityEngine;

namespace PackItUp.Controllers {

    [RequireComponent(typeof(Rigidbody2D))]
    public class TopDownCharacterController : MonoBehaviour {
        [SerializeField] private CharacterMoveConfig config;
        private Vector2 _moveDirection;
        private Rigidbody2D _rb;
        private CharacterMoveConfig _moveConfig;
        private CharacterControllerBindings _playerControls;


        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
            _playerControls = new CharacterControllerBindings();

            _playerControls.P1Controls.Move.performed += ctx => _moveDirection = ctx.ReadValue<Vector2>();
            _playerControls.P1Controls.Move.canceled += ctx => _moveDirection = Vector2.zero;
        }

        private void OnEnable() {
            _playerControls.P1Controls.Enable();
        }

        private void OnDisable() {
            _playerControls.P1Controls.Disable();
        }

        private void FixedUpdate() {
            Move();
        }

        void Move() {
            _rb.velocity = new Vector2(_moveDirection.x * config.moveSpeed, _moveDirection.y * config.moveSpeed);
        }


    }
}