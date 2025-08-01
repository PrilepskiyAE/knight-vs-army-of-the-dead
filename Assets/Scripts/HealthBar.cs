using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public InfoPlayer player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = GetComponent<Image>();
        player = FindObjectOfType<InfoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = player.HP / player.maxHP;
    }
}
