using UnityEngine;
using UnityEngine.UI;
public class EnamyBar : MonoBehaviour
{
    [SerializeField]
    private Transform healthBar;

    private InfoEnany infoEn => GetComponent<InfoEnany>();
    // Масштаб при 100% HP (100 HP)
    [SerializeField, Tooltip("Масштаб по X при полном здоровье (100 HP)")]
    private float fullHealthScale = 0.03f;



    void Update()
    {   if (infoEn == null || healthBar == null)return;
   
        if (infoEn.isLive)
        {
            float normalizedHP = (float)infoEn.HP / infoEn.maxHP;
            float newScaleX = normalizedHP * fullHealthScale;
            Vector2 currentScale = healthBar.localScale;
            currentScale.x = newScaleX;
            healthBar.localScale = currentScale;
        }
        else
        {
            Vector2 currentScale = healthBar.localScale;
            currentScale.x = 0f;
            healthBar.localScale = currentScale;
        }

    }
}

