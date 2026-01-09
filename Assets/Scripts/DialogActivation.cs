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

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("7");
        dialogSystem.SetActive(true);
        text.text = dialogs[index];     
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("72");
        dialogSystem.SetActive(false);
    }
}
