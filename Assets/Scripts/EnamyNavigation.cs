using Unity.VisualScripting;
using UnityEngine;

public class EnamyNavigation : MonoBehaviour
{
    public enum EnemyState { Go,AttackPlayer, Dead }
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
        }
    }
    void Dead(){
         animator.SetBool("dead", true);
    }

    void Go()
    {
        
        if (!infoEnany.isLive)
        {
            currentState = EnemyState.Dead;
        }
         animator.SetBool("attak", false);

    if (Vector3.Distance(transform.position, targetPoint.position) < _threshold)
        {
            
            if (forward) currentPoint++; else currentPoint--;
            Debug.Log(currentPoint);
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
            currentState = EnemyState.Go;
        }
    }


    public void OnAnimationEvent()
    {
        Debug.Log("Animation Event Triggered!");
    }

}
