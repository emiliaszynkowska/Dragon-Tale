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
    public AudioClip win;
    public AudioClip die;
    public AudioClip meow;
    public AudioClip roar;
    public AudioClip fire;
    public AudioClip lava;
    public AudioClip click;
    public AudioClip attack;
    public AudioClip clawAttack;
    public AudioClip questStarted;

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
    public void PlayClick()
    {
        audioSource.PlayOneShot(click);
    }

    // Sound Effects
    public void PlayMeow()
    {
        audioSource.PlayOneShot(meow);
    }

    public void PlayRoar()
    {
        audioSource.PlayOneShot(roar);
    }

    public void PlayAttack()
    {
        audioSource.PlayOneShot(attack);
    }

    public void PlayClawAttack()
    {
        audioSource.PlayOneShot(clawAttack);
    }

    public void PlayFire()
    {
        audioSource.clip = fire;
        audioSource.Play();
    }

    public void PlayLava()
    {
        audioSource.clip = lava;
        audioSource.Play();
    }

    public void PlayQuestStarted()
    {
        audioSource.PlayOneShot(questStarted);
    }

    public void PlayWin()
    {
        audioSource.PlayOneShot(win);
    }

    public void PlayDie()
    {
        audioSource.PlayOneShot(die);
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

    public void PlayBoss()
    {
        audioSource.clip = bossMusic;
        audioSource.Play();
    }

    public IEnumerator FadeOut()
    {
        while (audioSource.volume > 0.1)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0, 5 * Time.deltaTime);
            yield return null;
        }
        audioSource.volume = 0;
    }

    public IEnumerator FadeIn()
    {
        while (audioSource.volume < 0.9)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 1, 5 * Time.deltaTime);
            yield return null;
        }
        audioSource.volume = 1;
    }

}