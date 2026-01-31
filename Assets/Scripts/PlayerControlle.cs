
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum PlayerState { Go, RunAttack, AttackEnemy, Dead, Protection }

public class PlayerControlle : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField, Range(1f, 10f)] private float _walkSpeed = 6f;
    [SerializeField, Range(3f, 15f)] private float _runSpeed = 9f;
    [SerializeField, Range(0f, 1f)] private float _acceleration = 0.4f;

    [Space(10)]
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Button leftBt;
    [SerializeField]
    private Button rightBt;
    [SerializeField]
    private Button attackBt;
    [SerializeField]
    private Button protectedBt;
    [SerializeField]
    private Button jumpBt;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float forwardForce = 1f;

    [SerializeField] private Rigidbody2D rb;
   [SerializeField] private LayerMask groundLayer;

    private InfoPlayer _infoPlayer;
    private InfoEnany _infoEnamy;
    private PlayerState _currentState;
    private bool _deadAnimationPlayed;
    private bool _isRunning;
    private float _horizontalInput;
    private float _currentSpeed;
    private bool _isMoving;

    private bool _sTAction = false;

     private bool _isGrounded = false;


    private void Awake()
    {
        InitializeComponents();
        ValidateReferences();
    }
    private void Start()
    {
        leftBt.onClick.AddListener(OnClickLeft);
        rightBt.onClick.AddListener(OnClickRight);
        attackBt.onClick.AddListener(OnClickAttack);
        jumpBt.onClick.AddListener(Jump);
    }

    private void Update()
    {
        _isGrounded=IsGrounded();
       
        if (_infoPlayer.HP <= 0)
        {
            _currentState = PlayerState.Dead;
        }
        HandleInput();
        StateMachine();
    }


    private void StateMachine()
    {
        switch (_currentState)
        {
            case PlayerState.Protection:
                ProtectionState();
                break;
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

    private void ProtectionState()
    {

        if (_sTAction && _infoPlayer.ST > 0)
        {
            _animator.SetBool("Protected", true);
            _infoPlayer.setSTAction(true);
        }
        else
        {
            _animator.SetBool("Protected", false);
            _infoPlayer.setSTAction(false);
            _currentState = PlayerState.Go;
        }

    }
    private void GoState()
    {
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



    private void HandleInput()
    {
        //if(holdDuration == 0f) 

        _isRunning = Input.GetKey(KeyCode.LeftShift);
        _sTAction = Input.GetKey(KeyCode.F);


        if (Input.GetMouseButton(0))
        {
            // Проверяем, что палец/мышь всё ещё на кнопке
            if (EventSystem.current.currentSelectedGameObject == leftBt.gameObject) _horizontalInput = -1;
            if (EventSystem.current.currentSelectedGameObject == rightBt.gameObject) _horizontalInput = 1;
            _sTAction = EventSystem.current.currentSelectedGameObject == protectedBt.gameObject;
        }
        else
        {
            _horizontalInput = Input.GetAxis("Horizontal");
        }
    }

    private void HandleMovement()
    {
        float targetSpeed = _isRunning ? _runSpeed : _walkSpeed;
        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, _acceleration * Time.deltaTime);
        if (!_sTAction)
        {
            Vector2 movement = Vector2.right * _horizontalInput * _currentSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
        else
        {
            _currentState = PlayerState.Protection;
        }

        if (Input.GetKeyDown(KeyCode.Q)) Jump();


    }

    private void Jump()
    {
        if (_isGrounded && _infoPlayer.ST>0)
        {
            float direction = _spriteRenderer.flipX ? -1f : 1f;
        Vector2 force = new Vector2(forwardForce * direction, jumpForce);
        rb.AddForce(force, ForceMode2D.Impulse);
        _infoPlayer.DamageST(20);
        }
      
    }
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
            OnClickAttack();
        }
    }

    private void HandleFacingDirection()
    {
        if (Mathf.Abs(_horizontalInput) > 0.1f)
        {
            _spriteRenderer.flipX = _horizontalInput < 0f;
        }
    }


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
    {
        if (_infoEnamy != null && _infoEnamy.gameObject.activeInHierarchy)
        {
            // Проверяем, что противник существует и активен
            bool isDamageApplied = _infoEnamy != null;

            if (isDamageApplied)
            {

                _infoEnamy.Damage(30);
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

    void OnClickLeft()
    {
        _horizontalInput = 0;
    }

    void OnClickRight()
    {
        _horizontalInput = 0;
    }

    void OnClickAttack()
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
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);

        return hit.collider != null;
    }

}