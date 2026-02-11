using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
public interface IShase
{
    void IsShase(bool action);
}

public interface IAttack
{
    void IsAttack(bool action);
}

public class AINavigation : MonoBehaviour,IAttack, IShase
{
    public enum EnemyState { GoState,  ShaseState, StopState }
    public EnemyState currentState;
    public Transform[] points;

    public bool enabledShase = true;
    private float _speed = 1;
    private Transform _targetPoint;
    private int _currentPoint;
    public bool cyclr = true;
    private bool _forward;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private InfoEnany _infoEnany;
    private float _threshold = 1.1f;
    private bool _isLive = true;
    private bool _detected = true;
   
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        currentState = EnemyState.GoState;
        _forward = false;
        _currentPoint = 0;
        _targetPoint = points[_currentPoint];
        _infoEnany = GetComponent<InfoEnany>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_infoEnany.isLive)
        {
            switch (currentState)
            {
                case EnemyState.GoState: Go(); break;
                case EnemyState.ShaseState: Shase(); break;
                case EnemyState.StopState: break;
            }
        }
        else
        {
            Dead();
        }

    }
    void Dead()
    {
        if (_isLive)
        {
            _isLive = false;
            _animator.SetBool("attack", false);
            _animator.SetTrigger("Dead");
        }

    }
    void Shase()
    {
        _animator.SetBool("attack", false);
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("Игрок не найден!");
            return;
        }

        if (points == null || points.Length < 2)
        {
            Debug.LogError("Массив points не содержит достаточно точек!");
            return;
        }

        Vector2 currentPos = transform.position;
        float targetX = player.transform.position.x;

        float minX = Mathf.Min(points[0].position.x, points[1].position.x);
        float maxX = Mathf.Max(points[0].position.x, points[1].position.x);




        targetX = Mathf.Clamp(targetX, minX, maxX);

        float newX = Mathf.MoveTowards(
            currentPos.x,
            targetX,
            _speed * Time.deltaTime
        );
        newX = Mathf.Clamp(newX, minX, maxX);
        bool reachedLeft = Mathf.Approximately(newX, minX);
        bool reachedRight = Mathf.Approximately(newX, maxX);

        if (reachedLeft || reachedRight) 
        {
            _animator.SetBool("Idle",true);
        }else
        {
            _animator.SetBool("Idle",false);
            transform.position = new Vector2(newX, currentPos.y);
        }
        
    }

    void Go()
    {
        _animator.SetBool("attack", false);
        _animator.SetBool("Idle", false);

        if (Vector3.Distance(transform.position, _targetPoint.position) < _threshold)
        {

            if (_forward) _currentPoint++; else _currentPoint--;
            if (_currentPoint >= points.Length && cyclr)
            {
                _currentPoint = 0;

            }
            else if (_currentPoint >= points.Length && !cyclr)
            {
                _currentPoint = points.Length - 2;
                _forward = false;

            }
            else if (_currentPoint < 0)
            {
                _forward = true;
                _currentPoint = 1;

            }
            _sprite.flipX = _currentPoint == 0;
            _targetPoint = points[_currentPoint];
        }
        transform.position = Vector3.MoveTowards(transform.position, _targetPoint.position, _speed * Time.deltaTime);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _infoEnany.isLive)
        {
            currentState = EnemyState.ShaseState;
        }
    }
    public void IsShase(bool action)
    {
       
        if (action)
        {
            if (_detected)
            {
                if (!enabledShase)
                {
                    _animator.SetBool("attack", true);
                   currentState = EnemyState.StopState;
                }
                else
                {
                currentState = EnemyState.ShaseState;
                _detected = false;
                }
            }
        }
        else
        {
            currentState = EnemyState.GoState;
            _detected = true;
        }
    }

    public void IsAttack(bool action)
    {
        if(!_infoEnany.isLive)return;
        if (action)
        {
            _animator.SetBool("attack", true);
            currentState = EnemyState.StopState;
        }
        else
        {   
            _animator.SetBool("Idle", false);
            currentState = EnemyState.ShaseState;
        }
    }

}
