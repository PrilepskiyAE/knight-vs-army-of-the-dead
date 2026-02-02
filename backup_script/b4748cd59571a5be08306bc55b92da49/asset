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

    private MusicSystem  _musicSystem;

    private int _isSound = 1;
    private int _isMusic = 1;




    private void Start()
    {

        _musicSystem=  gameObject.GetComponent(typeof(MusicSystem)) as MusicSystem;
        
        gameObject.GetComponent<MusicSystem>();
       
        _isSound = PlayerPrefs.GetInt("isSound", 1);
        _isMusic = PlayerPrefs.GetInt("isMusic", 1);  
        SetListener();
    }

    private void Update()
    {
        InitSettings();
    }

    void OnClickPop() => SceneManager.LoadScene(0);


    void OnClickSoundBt()
    {
        if (_isSound == 1) { 
        
             PlayerPrefs.SetInt("isSound", 0);
             _isSound = 0;
              }
        else {
            PlayerPrefs.SetInt("isSound", 1); 
            _isSound = 1;
            }
    }

    void OnClickMusicBt()
    {
        if (_isMusic == 1)
        {
            _musicSystem.StopMusic();
            PlayerPrefs.SetInt("isMusic", 0);
            _isMusic = 0;
        }
        else
        {
             _musicSystem.PlayMusic();
            PlayerPrefs.SetInt("isMusic", 1);
            _isMusic = 1;
        }

    }
    
    private void InitSettings()
    {
       
        if (_isSound == 1) 
        soundBtText.text = "ЗВУК : ВКЛ";     
        else soundBtText.text = "ЗВУК :  ВЫКЛ"; 

        if (_isMusic == 1) musicBtText.text = "МУЗЫКА : ВКЛ";  
        else musicBtText.text = "МУЗЫКА : ВЫКЛ"; 

    }

    private void SetListener()
    {
        popB.onClick.AddListener(OnClickPop);
        soundBt.onClick.AddListener(OnClickSoundBt);
        musicBt.onClick.AddListener(OnClickMusicBt);
    }
}
