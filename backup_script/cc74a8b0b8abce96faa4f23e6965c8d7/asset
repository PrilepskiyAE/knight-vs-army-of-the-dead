using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonSystemSelectLevel : MonoBehaviour
{
    [SerializeField]
    private Button l1;
    void Start()
    {
        l1.onClick.AddListener(navigateL1);
    }
    void navigateL1() => SceneManager.LoadScene(1);
}
