using UnityEngine;

public class PlayerControlle : MonoBehaviour
{
[Header("Movement Settings")]
    [SerializeField] private float _walkSpeed = 3f;
    
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _runSpeed = 6f;
    
    [Range(0f, 1f)]
    [SerializeField] private float _acceleration = 0.4f;
    private bool _isRunning;

    private float _horizontalInput;
    private float _currentSpeed;
    

    private void Awake()
    {
    
        if (_animator == null)
            _animator = GetComponent<Animator>();

        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        HandleAnimation();
        HandleFacingDirection();
    }

    private void HandleInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _isRunning = Input.GetKey(KeyCode.LeftShift);
    }

    private void HandleMovement()
    {
    
        float targetSpeed = _isRunning ? _runSpeed : _walkSpeed;
        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, _acceleration);

        Vector2 movement = Vector2.right * _horizontalInput * _currentSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void HandleAnimation()
    {
        float movementThreshold = 0.1f;
        bool isMoving = Mathf.Abs(_horizontalInput) > movementThreshold;
        _animator.SetBool("Walk", isMoving);
        _animator.SetBool("Run", isMoving && _isRunning);
        _animator.SetBool("Attack", isMoving && _isRunning && Input.GetKeyUp(KeyCode.Space));
        if(!_isRunning)
        {
            _animator.SetBool("SAttack1", Input.GetKeyDown(KeyCode.Space)); 
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

