using Unity.VisualScripting;
using UnityEngine;
public interface IShase
{
    void  IsShase(bool action);
}
public class EnamyNavigation : MonoBehaviour,IShase
{
    public enum EnemyState { Go,AttackPlayer, Dead, cShase }
    public EnemyState currentState;
    public Transform[] points;
    private float speed=1;
    private Transform targetPoint;
    private int currentPoint;
    public bool cyclr = true;
    private bool forward;
    private Animator animator;
    private SpriteRenderer sprite;

    private InfoEnany infoEnany;

    private float _threshold = 1.1f;

    private bool detected=true;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        sprite=GetComponentInChildren<SpriteRenderer>();
        currentState=EnemyState.Go;
        forward = false;
        currentPoint = 0;    
        targetPoint = points[currentPoint];
        infoEnany= GetComponent<InfoEnany>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case EnemyState.Go: Go(); break;
            case EnemyState.AttackPlayer: AttackPlayer(); break;
            case EnemyState.Dead: Dead(); break;
            case EnemyState.cShase: Shase(); break;
        }
    }
    void Dead(){
         animator.SetBool("dead", true);
    }
void Shase()
{
         animator.SetBool("attak", false);
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
    bool reachedLeft  = Mathf.Approximately(newX, minX);
    bool reachedRight = Mathf.Approximately(newX, maxX);


    if (reachedLeft || reachedRight) animator.SetTrigger("IdleOk");
        
        
        

    transform.position = new Vector2(newX, currentPos.y);
}

    void Go()
    {
        if (!infoEnany.isLive)
        {
            currentState = EnemyState.Dead;
        }
         animator.SetBool("attak", false);
         animator.SetTrigger("IdleNok");

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

    void AttackPlayer() {
        if (!infoEnany.isLive)
        {
            currentState = EnemyState.Dead;
        }
         animator.SetTrigger("IdleNok");
         animator.SetBool("run", false);
         animator.SetBool("attak",true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            currentState=EnemyState.AttackPlayer;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentState = EnemyState.cShase;
        }
    }

    public void IsShase(bool action)
    {
        
        if (action)
        {
            if (detected)
            {
               currentState = EnemyState.cShase; 
               detected=false;
            }
            
        } else {
            currentState = EnemyState.Go;
            detected=true;
            }
    }
}
