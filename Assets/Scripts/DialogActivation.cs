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
        "Проклятая лошадь сбежала. Путь до деревни не близкий, придется срезать через кладбище",
        "Надо посмотреть что там за огни впереди", 
        "Вурдалак, бей его" };

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
