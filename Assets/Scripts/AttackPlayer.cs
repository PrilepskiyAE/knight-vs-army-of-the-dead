using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private readonly RaycastHit2D[] raycastHit2D = new RaycastHit2D[1];
    private ContactFilter2D contactFilter;
    private Rigidbody2D rb;

    private InfoEnany infoEnany;

    private bool collisionCount;
    private bool collisionCount2;

    private void Start()
    {
        infoEnany = GetComponent<InfoEnany>();
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D не найден на объекте!");
        }

        contactFilter.SetLayerMask(LayerMask.GetMask("Player"));
        contactFilter.useLayerMask = true;
        contactFilter.useTriggers = true;
    }


    void Update()
    {
        if (infoEnany.isLive)
        {
            // Визуализация лучей
            Debug.DrawRay(transform.position, transform.right * 4, Color.red);
            Debug.DrawRay(transform.position, -transform.right * 4, Color.blue);

            collisionCount = rb.Cast(transform.right, contactFilter, raycastHit2D, 4) > 0;
            collisionCount2 = rb.Cast(-transform.right, contactFilter, raycastHit2D, 4) > 0;

            //  target.IsShase(collisionCount || collisionCount2);
            if (collisionCount || collisionCount2)
            {
                TurnTowardsPlayer();
            }
        }

    }

    void TurnTowardsPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        GetComponent<EnamyNavigation>().IsShase(true);
        Vector3 direction = player.transform.position - transform.position;

        // Определяем, нужно ли отразить спрайт по оси X
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Если игрок слева от нас (по оси X), отражаем спрайт
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            // Если игрок справа — возвращаем нормальное состояние
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }

}
