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

        // public bool canMove { get; private set; } = true;
        // public bool canControlMovement { get; private set; } = true;
        private bool isActive = true;

        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
            _playerControls = new CharacterControllerBindings();
            _moveAction = playerConfig.GetMoveAction(_playerControls);
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
            if (!isActive) return;
            if (_moveDirection.magnitude == 0) {
                Decelerate();
            } else {
                Accelarate();
            }
        }
        
        private void Decelerate() {
            // Decelerate based on momentum
            var decelerationFactor = 1 - (movementConfig.momentum); // At 0 momentum, factor is 1 (instant stop). At 1 momentum, factor is 0 (no deceleration).
            _rb.velocity = Vector2.Lerp(_rb.velocity, Vector2.zero, decelerationFactor);
            // Vector2.SmoothDamp(_rb.velocity, Vector2.zero, ref _rb.velocity, TimeToReachTopSpeed);
        }
        
        
        private void Accelarate() {
            // Vector2 targetVelocity = new Vector2(_moveDirection.x * config.moveSpeed, _moveDirection.y * config.moveSpeed);
            // _rb.velocity = Vector2.SmoothDamp(_rb.velocity, targetVelocity, ref targetVelocity, TimeToReachTopSpeed);
            
            // Determine the target velocity based on input
            var targetVelocity = _moveDirection.normalized * movementConfig.topSpeed;
            // Calculate the acceleration rate based on the time to reach top speed
            var acceleration = movementConfig.acceleration / TimeToReachTopSpeed;
            _rb.velocity = Vector2.MoveTowards(_rb.velocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }

        public void ForceStop() {
            _rb.velocity = Vector2.zero;
        }
        
        public void TogleMovement() {
            isActive = !isActive;
        }
        
        public void SetActive(bool active) {
            isActive = active;
        }

        // private void Move() {
        //     _rb.velocity = new Vector2(_moveDirection.x * movementConfig.moveSpeed, _moveDirection.y * movementConfig.moveSpeed);
        // }
    }
}