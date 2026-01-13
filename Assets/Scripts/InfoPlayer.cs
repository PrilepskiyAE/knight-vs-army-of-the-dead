using UnityEngine;

public class InfoPlayer : MonoBehaviour
{

    public float HP = 100f;
    public float maxHP = 100f;

    public void Damage(float damage)
    {
        HP -= damage;
    }

    public void UpHP(float value) {
    HP += value;
    }

   
}
