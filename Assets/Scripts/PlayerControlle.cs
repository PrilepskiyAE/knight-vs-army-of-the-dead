
using UnityEngine;

public enum PlayerState { Go,RunAttack, AttackEnemy, Dead }

public class PlayerControlle : MonoBehaviour 
{
    [Header("Movement Settings")]
    [SerializeField] private float _walkSpeed = 3f;
    
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private float _runSpeed = 6f;
    
    [Range(0f, 1f)]
    [SerializeField] private float _acceleration = 0.4f;
    private bool _deadAnimationPlayed;
    private bool _isRunning;
    private float _horizontalInput;
    private float _currentSpeed;
    private InfoPlayer _infoPlayer;
    private PlayerState _currentState;
    private bool _isMoving;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _infoPlayer = GetComponent<InfoPlayer>();
        
        _currentState = PlayerState.Go;
    }

    private void Update()
    {   
        switch (_currentState)
        {
            case PlayerState.Go: Go(); break;
            case PlayerState.AttackEnemy: Attack(); break;
            case PlayerState.Dead: Dead(); break;
            case PlayerState.RunAttack: RunAttack();break;

        }
    }

    private void Go()
    {
        if (_infoPlayer.HP > 0)
        {
            HandleInput();
            HandleMovement();
            HandleAnimation();
            HandleFacingDirection();
        }
        else
        {
            _currentState = PlayerState.Dead;
        }
    }

    private void Attack()
    {
    _animator.SetBool("SAttack1", true);     
    _currentState = PlayerState.Go;
    }

    private void RunAttack()
    {
     Debug.Log("EVENT");  
    _animator.SetBool("Attack", true);     
    _currentState = PlayerState.Go;
    }

    private void HandleInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _isRunning = Input.GetKey(KeyCode.LeftShift);
    }

    private void HandleMovement()
    {
        float targetSpeed = _isRunning ? _runSpeed : _walkSpeed;
        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, _acceleration * Time.deltaTime); // Добавлен Time.deltaTime для плавности

        Vector2 movement = Vector2.right * _horizontalInput * _currentSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void HandleAnimation()
    {
        float movementThreshold = 0.1f;
        _isMoving = Mathf.Abs(_horizontalInput) > movementThreshold;
        _animator.SetBool("SAttack1", false);
    
        _animator.SetBool("Walk", _isMoving);
        _animator.SetBool("Run", _isMoving && _isRunning);
 
        
        if (Input.GetKeyDown(KeyCode.Space) && _isRunning)
        {
            _currentState = PlayerState.RunAttack;
        }else
        {
            _animator.SetBool("Attack", false);  
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_isRunning)
        {
            _currentState = PlayerState.AttackEnemy;
        }
        
    }

    private void Dead()
    {
        if (!_deadAnimationPlayed)
    {
        _animator.SetBool("Dead", !_deadAnimationPlayed);
        _deadAnimationPlayed = true;
    }
      
    } 

    private void HandleFacingDirection()
    {
        if (Mathf.Abs(_horizontalInput) > 0.1f)
        {
            _spriteRenderer.flipX = _horizontalInput < 0f;
        }
    }
}