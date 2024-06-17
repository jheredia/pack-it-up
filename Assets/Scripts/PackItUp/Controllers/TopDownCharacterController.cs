using System;
using PackItUp.InputSystem;
using PackItUp.Scriptable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PackItUp.Controllers {

    [RequireComponent(typeof(Rigidbody2D))]
    public class TopDownCharacterController : MonoBehaviour {
        
        private const float TimeToReachTopSpeed = 0.1f;
        [SerializeField] private SOPlayerConfig playerConfig;
        [SerializeField] private SOMovementConfig movementConfig;
        private Vector2 _moveDirection;
        private Rigidbody2D _rb;
        private CharacterControllerBindings _playerControls;
        private InputAction _moveAction;
        private InputAction _dashAction;
        private bool _isDashing;
        private Vector2 _dashDirection;
        private float _timeDashing;
        private float _timeSinceLastDash;

        private bool _isActive = true;
        
        public float NormalizedTimeUntilDash => Math.Clamp(_timeSinceLastDash / movementConfig.dashCooldown, 0, 1);

        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
            _playerControls = new CharacterControllerBindings();
            _moveAction = playerConfig.GetMoveAction(_playerControls);
            _moveAction.performed += ctx => _moveDirection = ctx.ReadValue<Vector2>();
            _moveAction.canceled += _ => _moveDirection = Vector2.zero;
            _dashAction = playerConfig.GetDashAction(_playerControls);
            _dashAction.performed += _ => Dash();
        }

        private void OnEnable() {
            _moveAction.Enable();
            _dashAction.Enable();
        }

        private void OnDisable() {
            _moveAction.Disable();
            _dashAction.Disable();
        }

        private void FixedUpdate() {
            if (!_isActive) return;
            _timeSinceLastDash += Time.fixedDeltaTime;
            
            if (_isDashing) {
                PerformDash();
            } else if (_moveDirection.magnitude > 0) {
                Accelerate();
            } else {
                Decelerate();
            }
        }
        
        private void Decelerate() {
            // Decelerate based on momentum
            var decelerationTime = (1 - movementConfig.momentum) * Time.fixedDeltaTime; // At 0 momentum, factor is 1 (instant stop). At 1 momentum, factor is 0 (no deceleration).
            _rb.velocity = Vector2.Lerp(_rb.velocity, Vector2.zero, decelerationTime);
            // Vector2.SmoothDamp(_rb.velocity, Vector2.zero, ref _rb.velocity, TimeToReachTopSpeed);
        }
        
        
        private void Accelerate() {
            // Determine the target velocity based on input
            var targetVelocity = _moveDirection.normalized * movementConfig.topSpeed;
            // Calculate the acceleration rate based on the time to reach top speed
            var acceleration = movementConfig.acceleration / TimeToReachTopSpeed;
            _rb.velocity = Vector2.MoveTowards(_rb.velocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }

        public void ForceStop() {
            _rb.velocity = Vector2.zero;
        }
        
        public void ToggleActive() {
            _isActive = !_isActive;
        }
        
        public void SetActive(bool value) {
            _isActive = value;
        }

        private void Dash() {
            if (_isDashing) return;
            if (_timeSinceLastDash < movementConfig.dashCooldown) return;
            _isDashing = true;
            _timeSinceLastDash = 0;
            _timeDashing = 0;
            _dashDirection = _rb.velocity.magnitude == 0 ? Vector2.up : _rb.velocity.normalized;
            PerformDash();
        }
        
        private void PerformDash() {
            if (_timeDashing >= movementConfig.dashDuration) {
                _isDashing = false;
                return;
            }
            _rb.velocity = _dashDirection * movementConfig.dashSpeed;
            _timeDashing += Time.fixedDeltaTime;
        }
    }
}