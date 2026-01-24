using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    [SerializeField]
    private AudioClip musicClip;
    private AudioSource _audioSource;

    void Start()
    {
        // Получаем или добавляем AudioSource
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        _audioSource.clip = musicClip;
        _audioSource.loop = true;
        _audioSource.playOnAwake = false;
        int isMusic = PlayerPrefs.GetInt("isMusic", 1);  
        if (isMusic == 1)
        {
            PlayMusic();
        }
        else
        {
            StopMusic();
        }
    
    }

    public  void PlayMusic()
    {
            _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    public void ResumeMusic()
    {
        if (!_audioSource.isPlaying)
            _audioSource.Play();
    }

}
