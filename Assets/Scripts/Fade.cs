using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public RawImage img;
    public PlayerMovement player;
    
    void Start()
    {
        StartCoroutine(FadeOut());
    }

    public void Out()
    {
        StartCoroutine(FadeIn());
    }
    
    public void In()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        player.SetCanMove(false);
        img.CrossFadeAlpha(1f, 1f, false);
        yield return new WaitForSeconds(1);
    }

    IEnumerator FadeOut()
    {
        player.SetCanMove(true);
        img.CrossFadeAlpha(0f, 1f, false);
        yield return new WaitForSeconds(1);
    }
}