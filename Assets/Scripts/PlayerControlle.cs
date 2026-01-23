
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState { Go, RunAttack, AttackEnemy, Dead }

public class PlayerControlle : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField, Range(1f, 10f)] private float _walkSpeed = 3f;
    [SerializeField, Range(3f, 15f)] private float _runSpeed = 6f;
    [SerializeField, Range(0f, 1f)] private float _acceleration = 0.4f;

    [Space(10)]
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private InfoPlayer _infoPlayer;
    private InfoEnany _infoEnamy;
    private PlayerState _currentState;
    private bool _deadAnimationPlayed;
    private bool _isRunning;
    private float _horizontalInput;
    private float _currentSpeed;
    private bool _isMoving;

    #region Unity Events

    private void Awake()
    {
        InitializeComponents();
        ValidateReferences();
    }

    private void Update()
    {
        if (_infoPlayer.HP <= 0)
        {
            _currentState = PlayerState.Dead;
        }

        StateMachine();
    }

    #endregion

    #region State Machine

    private void StateMachine()
    {
        switch (_currentState)
        {
            case PlayerState.Go:
                GoState();
                break;
            case PlayerState.AttackEnemy:
                AttackState();
                break;
            case PlayerState.RunAttack:
                RunAttackState();
                break;
            case PlayerState.Dead:
                DeadState();
                break;
        }
    }

    private void GoState()
    {
        HandleInput();
        HandleMovement();
        HandleAnimation();
        HandleFacingDirection();
    }

    private void AttackState()
    {
        if (_infoEnamy != null)
        {
            Debug.Log("Damage dealt to: " + _infoEnamy.gameObject.name);
        }

        _animator.SetBool("SAttack1", true);
        _currentState = PlayerState.Go;
    }

    private void RunAttackState()
    {
        _animator.SetBool("Attack", true);
        _currentState = PlayerState.Go;
    }

    private void DeadState()
    {
        if (!_deadAnimationPlayed)
        {
            _animator.SetTrigger("Dead");
            _deadAnimationPlayed = true;
        }
    }

    #endregion

    #region Input & Movement

    private void HandleInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _isRunning = Input.GetKey(KeyCode.LeftShift);
    }

    private void HandleMovement()
    {
        float targetSpeed = _isRunning ? _runSpeed : _walkSpeed;
        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, _acceleration * Time.deltaTime);

        Vector2 movement = Vector2.right * _horizontalInput * _currentSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    #endregion

    #region Animation

    private void HandleAnimation()
    {
        _isMoving = Mathf.Abs(_horizontalInput) > 0.1f;

        _animator.SetBool("Walk", _isMoving);
        _animator.SetBool("Run", _isMoving && _isRunning);

        // Reset attack states
        _animator.SetBool("SAttack1", false);
        _animator.SetBool("SAttack2", false);
        _animator.SetBool("Attack", false);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isRunning)
            {
                _currentState = PlayerState.RunAttack;
            }
            else
            {
                _currentState = PlayerState.AttackEnemy;
            }
        }
    }

    #endregion

    #region Facing Direction

    private void HandleFacingDirection()
    {
        if (Mathf.Abs(_horizontalInput) > 0.1f)
        {
            _spriteRenderer.flipX = _horizontalInput < 0f;
        }
    }

    #endregion

    #region Collision Events

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enamy"))
        {
            _infoEnamy = other.GetComponentInChildren<InfoEnany>();
            Debug.Log("Enamy detected: " + _infoEnamy.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enamy"))
        {
            _infoEnamy = null;
            Debug.Log("Enamy lost: " + other.name);
        }
    }
    public void AttemptAttackTask()
    #endregion

    #region Attack Logic


    {
        if (_infoEnamy != null && _infoEnamy.gameObject.activeInHierarchy)
        {
            // Проверяем, что противник существует и активен
            bool isDamageApplied = _infoEnamy != null;

            if (isDamageApplied)
            {

                _infoEnamy.Damage(100);
            }
            else
            {
                Debug.LogWarning("Failed to deal damage to: " + _infoEnamy.gameObject.name);
            }
        }
        else
        {
            Debug.LogWarning("No valid enemy to attack!");
        }
    }

    #endregion

    #region Initialization

    private void InitializeComponents()
    {
        if (_animator == null) _animator = GetComponent<Animator>();
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
        _infoPlayer = GetComponent<InfoPlayer>();
    }

    private void ValidateReferences()
    {
        if (_animator == null) Debug.LogError("Animator не найден на объекте!");
        if (_spriteRenderer == null) Debug.LogError("SpriteRenderer не найден на объекте!");
        if (_infoPlayer == null) Debug.LogError("InfoPlayer не найден на объекте!");
    }

    #endregion
}