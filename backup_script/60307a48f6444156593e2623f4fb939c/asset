using UnityEngine;
using UnityEngine.UI;

public class EndureBar : MonoBehaviour
{
    public Image endureBar;
    public InfoPlayer player;

    [System.Obsolete]
    void Start()
    {
        endureBar = GetComponent<Image>();
        player = FindObjectOfType<InfoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        endureBar.fillAmount = player.ST / player.maxSt;
    }
}
