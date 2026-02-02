using UnityEngine;

public class AITurn : MonoBehaviour
{
    private readonly RaycastHit2D[] _raycastHit2D = new RaycastHit2D[1];
    private ContactFilter2D _contactFilter;
    private Rigidbody2D _rb;

    private InfoEnany _infoEnany;

    private bool _collisionCount;
    private bool _collisionCount2;

    void Start()
    {
       _infoEnany = GetComponent<InfoEnany>();
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            Debug.LogError("Rigidbody2D не найден на объекте!");
        }

        _contactFilter.SetLayerMask(LayerMask.GetMask("Player"));
        _contactFilter.useLayerMask = true;
        _contactFilter.useTriggers = true;  
    }

    void Update()
    {
         if (_infoEnany.isLive)
        {
            Debug.DrawRay(transform.position, transform.right * 4, Color.red);
            Debug.DrawRay(transform.position, -transform.right * 4, Color.blue);

            _collisionCount = _rb.Cast(transform.right, _contactFilter, _raycastHit2D, 4) > 0;
            _collisionCount2 = _rb.Cast(-transform.right, _contactFilter, _raycastHit2D, 4) > 0;

            if (_collisionCount || _collisionCount2)
            {
                TurnTowardsPlayer();
            }
        }
    }
     void TurnTowardsPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        GetComponent<AINavigation>().IsShase(true);
        Vector3 direction = player.transform.position - transform.position;

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}
