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
    private float speed = 1;
    private Transform targetPoint;
    private int currentPoint;
    public bool cyclr = true;
    private bool forward;
    private Animator animator;
    private SpriteRenderer sprite;
    private InfoEnany infoEnany;
    private float _threshold = 1.1f;
    private bool isLive = true;
    private bool detected = true;
   
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        currentState = EnemyState.GoState;
        forward = false;
        currentPoint = 0;
        targetPoint = points[currentPoint];
        infoEnany = GetComponent<InfoEnany>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (infoEnany.isLive)
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
        if (isLive)
        {
            isLive = false;
            animator.SetBool("attack", false);
            animator.SetTrigger("Dead");
        }

    }
    void Shase()
    {
        animator.SetBool("attack", false);
        
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
            speed * Time.deltaTime
        );
        newX = Mathf.Clamp(newX, minX, maxX);
        bool reachedLeft = Mathf.Approximately(newX, minX);
        bool reachedRight = Mathf.Approximately(newX, maxX);

        if (reachedLeft || reachedRight) 
        {
            animator.SetBool("Idle",true);
        }else
        {
            animator.SetBool("Idle",false);
            transform.position = new Vector2(newX, currentPos.y);
        }
        
    }

    void Go()
    {
        animator.SetBool("attack", false);
        animator.SetBool("Idle", false);

        if (Vector3.Distance(transform.position, targetPoint.position) < _threshold)
        {

            if (forward) currentPoint++; else currentPoint--;
            if (currentPoint >= points.Length && cyclr)
            {
                currentPoint = 0;

            }
            else if (currentPoint >= points.Length && !cyclr)
            {
                currentPoint = points.Length - 2;
                forward = false;

            }
            else if (currentPoint < 0)
            {
                forward = true;
                currentPoint = 1;

            }
            sprite.flipX = currentPoint == 0;
            targetPoint = points[currentPoint];
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && infoEnany.isLive)
        {
            currentState = EnemyState.ShaseState;
        }
    }
    public void IsShase(bool action)
    {
       
        if (action)
        {
            if (detected)
            {
                if (!enabledShase)
                {
                    StartCoroutine(StartAttack(1f));
                   currentState = EnemyState.StopState;
                }
                else
                {
                currentState = EnemyState.ShaseState;
                detected = false;
                }
            }
        }
        else
        {
            currentState = EnemyState.GoState;
            detected = true;
        }
    }

    public void IsAttack(bool action)
    {
        if(!infoEnany.isLive)return;
        if (action)
        {
            StartCoroutine(StartAttack(1f));
            currentState = EnemyState.StopState;
        }
        else
        {   
            animator.SetBool("Idle", false);
            currentState = EnemyState.ShaseState;
        }
    }

    private IEnumerator StartAttack(float delayTime)
    {
         animator.SetBool("Idle", true);
        yield return new WaitForSeconds(delayTime);
         animator.SetBool("Idle", false);
         animator.SetBool("attack", true);
    }
}
