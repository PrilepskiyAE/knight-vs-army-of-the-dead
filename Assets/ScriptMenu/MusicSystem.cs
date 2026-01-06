using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    public AudioClip musicClip;
    private AudioSource audioSource;

    void Start()
    {
        // Получаем или добавляем AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = musicClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
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
            audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void ResumeMusic()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

}
