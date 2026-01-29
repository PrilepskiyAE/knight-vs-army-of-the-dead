using Unity.VisualScripting;
using UnityEngine;

public class InfoPlayer : MonoBehaviour
{
    private float timer = 0;
    private float damageST=15;
    public float HP = 100f;
    public float maxHP = 100f;

    public float ST = 100f;
    public float maxSt = 100f;

    public bool stAction = false;

    public float recoveryDelay = 1f;

    public void Damage(float damage)
    {
        if(!stAction) HP -= damage;
        if (ST > 0) ST -= damageST;
    }

    public void DamageST(float damage)
    {
        if (ST > 0) ST -= damage;
    }

    public void UpHP(float value)
    {
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
                timer -= recoveryDelay;  
                Debug.Log("Stana"+ST);
            }
        }
        else
        {
            timer = 0;
        }

        if(ST<-1)ST=0;

    }

    public void setSTAction(bool action)
    {
        stAction = action;
    }
}
