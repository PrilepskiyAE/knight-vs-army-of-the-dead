using Unity.Mathematics;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{
    [SerializeField]
    private InfoPlayer infoPlayer = null;
    [SerializeField]
    private float damage = 3;

    void AttackAction()
    {
        if (infoPlayer != null)
        {
            infoPlayer.Damage(damage);
        }
    }



}
