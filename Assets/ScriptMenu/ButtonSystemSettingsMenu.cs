using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class ButtonSystemSettingsMenu : MonoBehaviour
{
    [SerializeField]
    private Button popB;
    [SerializeField]
    private Button soundBt;
    [SerializeField]
    private Button musicBt;


    [SerializeField]
    private Text musicBtText;

    [SerializeField]
    private Text soundBtText;

    private MusicSystem  musicSystem;

    private int isSound = 1;
    private int isMusic = 1;




    private void Start()
    {

        musicSystem=  gameObject.GetComponent(typeof(MusicSystem)) as MusicSystem;
        
        gameObject.GetComponent<MusicSystem>();
       
        Debug.Log(musicSystem);
        isSound = PlayerPrefs.GetInt("isSound", 1);
        isMusic = PlayerPrefs.GetInt("isMusic", 1);  
        SetListener();
    }

    private void Update()
    {
        InitSettings();
    }

    void OnClickPop() => SceneManager.LoadScene(0);


    void OnClickSoundBt()
    {
        if (isSound == 1) { 
        
             PlayerPrefs.SetInt("isSound", 0);
             isSound = 0;
              }
        else {
            PlayerPrefs.SetInt("isSound", 1); 
            isSound = 1;
            }
    }

    void OnClickMusicBt()
    {
        if (isMusic == 1)
        {
            musicSystem.StopMusic();
            PlayerPrefs.SetInt("isMusic", 0);
            isMusic = 0;
        }
        else
        {
             musicSystem.PlayMusic();
            PlayerPrefs.SetInt("isMusic", 1);
            isMusic = 1;
        }

    }
    
    private void InitSettings()
    {
       
        if (isSound == 1) 
        soundBtText.text = "ЗВУК : ВКЛ";     
        else soundBtText.text = "ЗВУК :  ВЫКЛ"; 

        if (isMusic == 1) musicBtText.text = "МУЗЫКА : ВКЛ";  
        else musicBtText.text = "МУЗЫКА : ВЫКЛ"; 

    }

    private void SetListener()
    {
        popB.onClick.AddListener(OnClickPop);
        soundBt.onClick.AddListener(OnClickSoundBt);
        musicBt.onClick.AddListener(OnClickMusicBt);
    }
}
