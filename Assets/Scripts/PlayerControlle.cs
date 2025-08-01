using UnityEngine;

public class PlayerControlle : MonoBehaviour
{

    public float _horizontal;
    private float _speed = 6;
    public Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * Time.deltaTime * _speed * _horizontal);
       
        anim.SetBool("Attack", Input.GetKey(KeyCode.Space));

        anim.SetBool("Walk", _horizontal !=0);

        if (_horizontal>0)
        {
            GetComponent<SpriteRenderer>().flipX = false;

        }

        if(_horizontal < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;

        }

       

    }
}
