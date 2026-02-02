using UnityEngine;

public class AIAttack : MonoBehaviour
{
    private Animator animator;
    private InfoPlayer _infoPlayer;
    private IAttack iAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        iAttack = GetComponent<AINavigation>();      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {   
            _infoPlayer = other.GetComponentInChildren<InfoPlayer>();
            iAttack.IsAttack(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _infoPlayer = null;
            iAttack.IsAttack(false);
        }
    }
 
}
