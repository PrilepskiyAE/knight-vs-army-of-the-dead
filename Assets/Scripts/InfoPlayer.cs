using Unity.VisualScripting;
using UnityEngine;

public class InfoPlayer : MonoBehaviour
{

    public float HP = 100f;
    public float maxHP = 100f;

    public float ST = 100f;
    public float maxSt = 100f;

    private float timer=0;

    public float recoveryDelay = 1f;     

    public void Damage(float damage)
    {
        HP -= damage;
        ST -= 5; 
    }

    public void UpHP(float value) {
    HP += value;
    }

    void Update()
    {

        if (ST < maxSt)
        {
            timer += Time.deltaTime;
            if (timer >= recoveryDelay)
            {
                ST++;
                timer -= recoveryDelay;  // Обнуляем таймер (с учётом «перелива»)
            }
        }
        else
        {
            timer = 0;
        }

    }

   
}
