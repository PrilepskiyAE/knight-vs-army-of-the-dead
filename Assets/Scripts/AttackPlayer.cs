using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private readonly RaycastHit2D[] raycastHit2D = new RaycastHit2D[1];
    public ContactFilter2D contactFilter;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
         var collisionCount = rb.Cast(transform.right, contactFilter, raycastHit2D, 2) > 0;
        var collisionCount2 = rb.Cast(-transform.right, contactFilter, raycastHit2D, 2) > 0;
        if (collisionCount || collisionCount2)
        {


        }
    
    }

    public void OnAnimationEvent()
    {
        Debug.Log("Animation Event Triggered PLAYER!");
    }
       
}
