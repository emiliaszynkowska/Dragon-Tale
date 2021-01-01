using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public AudioSource audioSource;
    public AudioClip music;
    public AudioClip homeMusic;
    public AudioClip villageMusic;
    public AudioClip fightMusic;
    public AudioClip bossMusic;
    public AudioClip meow;
    public AudioClip roar;
    public AudioClip fire;

    public void Start()
    {
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

    // UI
    // TODO
        
    // Sound Effects
    public void PlayMeow()
    {
        audioSource.PlayOneShot(meow);
    }

    public void PlayRoar()
    {
        audioSource.PlayOneShot(roar);
    }

    public void PlayFire()
    {
        audioSource.clip = fire;
        audioSource.Play();
    }
    
    // Music 
    public void PlayHome()
    {
        audioSource.clip = homeMusic;
        audioSource.Play();
    }
    
    public void PlayVillage()
    {
        audioSource.clip = villageMusic;
        audioSource.Play();
    }
    
    public void PlayFight()
    {
        audioSource.PlayOneShot(fightMusic);
    }
    
    public IEnumerator FadeOut()
    {
        while(audioSource.volume > 0.1)
        {
            audioSource.volume = Mathf.Lerp( audioSource.volume, 0, 5 * Time.deltaTime);
            yield return null;
        }
        audioSource.volume = 0;
    }
 
    public IEnumerator FadeIn() 
    {
        while(audioSource.volume < 0.9)
        {
            audioSource.volume = Mathf.Lerp( audioSource.volume, 1, 5 * Time.deltaTime);
            yield return null;
        }
        audioSource.volume = 1;
    }

}
