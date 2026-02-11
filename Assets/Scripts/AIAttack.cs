using UnityEngine;

public class AIAttack : MonoBehaviour
{
    private Animator _animator;
    private InfoPlayer _infoPlayer;
    private IAttack _iAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _iAttack = GetComponent<AINavigation>();      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {   
            _infoPlayer = other.GetComponentInChildren<InfoPlayer>();
            _iAttack.IsAttack(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _infoPlayer = null;
            _iAttack.IsAttack(false);
        }
    }
 
}
