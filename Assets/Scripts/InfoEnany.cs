using UnityEngine;

public class InfoEnany : MonoBehaviour
{
    
    public float HP = 100f;
    public float maxHP = 100f;
    public bool isLive=true;

    public void Damage(float damage)
    {
        HP -= damage;
        Debug.Log(HP);
        Debug.Log("=========");
    }

    public void UpHP(float value)
    {
        HP += value;
        Debug.Log(HP);
    }

  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isLive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP<=0)
        {
            isLive = false;

        }
    }
}
