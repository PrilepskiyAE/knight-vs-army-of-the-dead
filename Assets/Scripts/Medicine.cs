using UnityEngine;

public class Medicine : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<InfoPlayer>().UpHP(10);
            Destroy(gameObject);
        }

    }
}
