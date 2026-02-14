using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogActivation : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogSystem;
    [SerializeField]
    private Text text;
    [SerializeField]
    private int index = 0;

    private List<string> dialogs = new List<string>() {
        "Проклятая лошадь сбежала",
        "О, Костер на кладбище!!! Выглядит очень странно!",
        "О черт, Это зомби" };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
        dialogSystem.SetActive(true);
        text.text = dialogs[index];
        } 
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
             dialogSystem.SetActive(false);
             this.gameObject.SetActive(false);
        }
       
    }
}
