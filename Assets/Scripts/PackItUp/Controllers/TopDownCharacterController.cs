using System;
using PackItUp.InputSystem;
using PackItUp.Scriptable;
using PackItUp.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace PackItUp.Controllers
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class TopDownCharacterController : MonoBehaviour
    {

        [SerializeField] private SOPlayerConfig playerConfig;
        [SerializeField] private SOMovementConfig movementConfig;
        [SerializeField] private DashReadyBar dashReadyBar;
        private Vector2 _moveDirection;
        private Vector2 _ghostForward = Vector2.up;
        private Vector2 _moveForce;
        private Rigidbody2D _rb;
        private CharacterControllerBindings _playerControls;
        private InputAction _moveAction;
        private InputAction _dashAction;
        private InputAction _exitAction;
        private bool _isDashing;
        private float _timeDashing;
        private bool _isActive = true;

        private Animator _animator;
        [SerializeField] private bool showDebug;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerControls = new CharacterControllerBindings();
            _moveAction = playerConfig.GetMoveAction(_playerControls);
            _moveAction.performed += ctx => _moveDirection = ctx.ReadValue<Vector2>();
            _moveAction.canceled += _ => _moveDirection = Vector2.zero;
            _dashAction = playerConfig.GetDashAction(_playerControls);
            _dashAction.performed += _ => Dash();
            _exitAction = playerConfig.GetExitAction(_playerControls);
            _exitAction.performed += _ => ExitGame();
            dashReadyBar.SetCoolDown(movementConfig.dashCooldown);
            _animator = gameObject.GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _moveAction.Enable();
            _dashAction.Enable();
            _exitAction.Enable();
        }

        private void OnDisable()
        {
            _moveAction.Disable();
            _dashAction.Disable();
            _exitAction.Disable();
        }

        private void FixedUpdate()
        {
            if (!_isActive) return;

            if (_isDashing)
            {
                PerformDash();
            }
            else if (_moveDirection.magnitude > 0)
            {
                ApplyAccelerationMovement();
            }
            else
            {
                Decelerate();
            }

            // Clamp the velocity and force to the top speed and acceleration (when not dashing)
            if (!_isDashing)
            {
                _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, movementConfig.topSpeed);
                _rb.totalForce = Vector2.ClampMagnitude(_rb.totalForce, movementConfig.acceleration);
                _moveForce = Vector2.ClampMagnitude(_moveForce, movementConfig.acceleration);
            }

            GetCardinalDirectionOfMovement();

            if (!showDebug) return;
            Debug.DrawRay(transform.position, _rb.velocity.normalized * 5, Color.red);
            Debug.DrawRay(transform.position, _ghostForward.normalized * 5, Color.green);
            Debug.DrawRay(transform.position, _rb.totalForce.normalized * 5, Color.blue);
            Debug.DrawRay(transform.position, _moveDirection.normalized * 5, Color.magenta);
        }

        private void Decelerate()
        {
            _moveForce = Vector2.Lerp(_moveForce, Vector2.zero, (1 - movementConfig.drag));
            _rb.velocity = Vector2.Lerp(_rb.velocity, Vector2.zero, (1 - movementConfig.drag));

            if (!(Math.Abs(_rb.velocity.magnitude) < 1.5f)) return;
            _rb.velocity = Vector2.zero;
            _moveForce = Vector2.zero;
        }

        private void ApplyAccelerationMovement()
        {
            _moveForce += Vector2.Lerp(_ghostForward, _moveDirection, movementConfig.turnRate).normalized * (movementConfig.acceleration * _moveDirection.magnitude);
            _rb.AddForce(_moveForce, ForceMode2D.Force);
            _ghostForward = Vector2.Lerp(_ghostForward.normalized, _rb.velocity.normalized, movementConfig.drift).normalized;
        }

        public void ForceStop()
        {
            _rb.velocity = Vector2.zero;
            _moveForce = Vector2.zero;
        }

        private void Dash()
        {
            if (_isDashing || !dashReadyBar.IsReady()) return;
            _isDashing = true;
            _timeDashing = 0;
            dashReadyBar.Reset();
            PerformDash();
        }

        private void PerformDash()
        {
            if (_timeDashing >= movementConfig.dashDuration)
            {
                _isDashing = false;
                return;
            }
            _rb.velocity = _ghostForward * (movementConfig.dashMultiplier * movementConfig.topSpeed);
            _timeDashing += Time.fixedDeltaTime;
        }

        public void ToggleActive()
        {
            _isActive = !_isActive;
        }

        public void SetActive(bool value)
        {
            _isActive = value;
        }

        public void GetCardinalDirectionOfMovement()
        {
            ResetAnimatorTriggers();
            if (_moveDirection.magnitude <= 0)
                _animator.SetTrigger("Idle");
            if (_moveDirection.x < 0)
            {
                _rb.gameObject.transform.eulerAngles = new Vector3(
                    _rb.gameObject.transform.eulerAngles.x,
                    -180,
                    _rb.gameObject.transform.eulerAngles.z
                );
                _animator.SetTrigger("RunningLeft");
            }

            if (_moveDirection.x > 0)
            {
                _rb.gameObject.transform.eulerAngles = new Vector3(
                    _rb.gameObject.transform.eulerAngles.x,
                    0,
                    _rb.gameObject.transform.eulerAngles.z
                );
                _animator.SetTrigger("RunningRight");
            }

            if (_moveDirection.y < 0) _animator.SetTrigger("RunningDown");
            if (_moveDirection.y > 0) _animator.SetTrigger("RunningUp");
        }

        private void ResetAnimatorTriggers()
        {
            _animator.ResetTrigger("RunningUp");
            _animator.ResetTrigger("RunningLeft");
            _animator.ResetTrigger("RunningRight");
            _animator.ResetTrigger("RunningDown");
            _animator.ResetTrigger("Idle");
        }

        private void ExitGame()
        {
            GameManager.Instance.LoadMenu(this, EventArgs.Empty);
        }
    }
}