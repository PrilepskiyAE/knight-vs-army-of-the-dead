using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private readonly RaycastHit2D[] raycastHit2D = new RaycastHit2D[1];
    public ContactFilter2D contactFilter;
    private Rigidbody2D rb;

    // Флаги столкновений (чтобы использовать в DrawRaycastVisualization)
    private bool collisionCount;
    private bool collisionCount2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D не найден на объекте!");
        }
    }

    private void FixedUpdate()
    {
        // Выполняем рейкасты и сохраняем результаты во флаги
        collisionCount = rb.Cast(transform.right, contactFilter, raycastHit2D, 2) > 0;
        collisionCount2 = rb.Cast(-transform.right, contactFilter, raycastHit2D, 2) > 0;

        if (collisionCount || collisionCount2)
        {
            Debug.Log("Animation Event Triggered Action!");
        }

        // Визуализация (только в редакторе для отладки)
#if UNITY_EDITOR
        DrawRaycastVisualization();
#endif
    }

    public void OnAnimationEvent()
    {
        Debug.Log("Animation Event Triggered PLAYER!");
    }

    // Вспомогательный метод для отрисовки лучей
    private void DrawRaycastVisualization()
    {
        Vector2 origin = rb.position;

        // Луч вправо
        Vector2 endPointRight = origin + (Vector2)(transform.right * 2f);
        Debug.DrawLine(origin, endPointRight,
            collisionCount ? Color.red : Color.green,
            0f, false);

        // Луч влево
        Vector2 endPointLeft = origin + (Vector2)(-transform.right * 2f);
        Debug.DrawLine(origin, endPointLeft,
            collisionCount2 ? Color.red : Color.green,
            0f, false);

        // Точки попаданий
        foreach (var hit in raycastHit2D)
        {
            if (hit.collider != null)
            {
                DrawWireSphere(hit.point, 0.1f, Color.yellow, 0f);
            }
        }
    }

    // Метод для отрисовки сферы через линии
    private void DrawWireSphere(Vector2 center, float radius, Color color, float duration)
    {
        const int segments = 12;
        float angleStep = 360f / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle1 = i * angleStep * Mathf.Deg2Rad;
            float angle2 = (i + 1) * angleStep * Mathf.Deg2Rad;

            Vector2 point1 = center + new Vector2(Mathf.Cos(angle1), Mathf.Sin(angle1)) * radius;
            Vector2 point2 = center + new Vector2(Mathf.Cos(angle2), Mathf.Sin(angle2)) * radius;

            Debug.DrawLine(point1, point2, color, duration);
        }
    }
}
