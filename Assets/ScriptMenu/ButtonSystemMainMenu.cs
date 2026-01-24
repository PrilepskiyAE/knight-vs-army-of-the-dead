using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonSystemMainMenu : MonoBehaviour
{
    [SerializeField]
    private Button startBt;
    [SerializeField]
    private Button settingsBt;
    [SerializeField]
    private Button exitBt;

    private void Start()
    {
        startBt.onClick.AddListener(OnClickStart);
        settingsBt.onClick.AddListener(OnClickSettings);
        exitBt.onClick.AddListener(OnClickExit);

    }


    public void OnClickStart() => SceneManager.LoadScene(3);


    public void OnClickSettings() => SceneManager.LoadScene(2);


    public void OnClickExit() => Application.Quit();
}
