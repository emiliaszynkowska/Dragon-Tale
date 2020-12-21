using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public AudioSource audioSource;
    public AudioClip music;
    public AudioClip homeMusic;
    public AudioClip villageMusic;
    //public AudioClip fightMusic;
    //public AudioClip bossMusic;
    //public AudioClip click;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = music;
    }
    
    public void PlayMusic()
    {
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
    
    // public void PlayFight()
    // {
    //     audioSource.PlayOneShot(fightMusic);
    // }

}
