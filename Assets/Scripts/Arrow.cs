using UnityEngine;

public class Arrow : MonoBehaviour
{
 
    [SerializeField] private float speed = 5f;
    private bool isFlipped = false;

    public void SetFlipDirection(bool flipped)
    {
        isFlipped = flipped;
        ApplyFlipToArrow();
    }

    void Update()
    {
        Vector2 direction = isFlipped ? -Vector2.right : Vector2.right;
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.x > 200 || transform.position.x < -200)
            Destroy(gameObject);
    }

    void ApplyFlipToArrow()
    {
        SpriteRenderer arrowRenderer = GetComponent<SpriteRenderer>();
        if (arrowRenderer != null)
        {
            arrowRenderer.flipX = isFlipped;
        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.x = isFlipped ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInChildren<InfoPlayer>().Damage(10);
            Destroy(gameObject);
        }
    }

}
