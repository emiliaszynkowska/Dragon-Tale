using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fade : MonoBehaviour
{
    public RawImage img; 
    public PlayerMovement player;
    
    void Start()
    {
        img.color = new Color(0f, 0f, 0f, 1f);
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()//Fade In brings the given object into view
    {
        player.SetCanMove(false);
        img.CrossFadeAlpha(1f, 1f, false);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator FadeOut()//Fade Out hides the given object
    {
        player.SetCanMove(true);
        img.CrossFadeAlpha(0f, 2f, false);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator FadeInAndOut()
    {
        yield return FadeIn();
        yield return new WaitForSeconds(1);
        yield return FadeOut();
    }

    public IEnumerator FadeIn(RawImage img)
    {
        //Fading in doesnt work
        Color fixedColor = img.color; 
        fixedColor.a = 1;
        img.color = fixedColor;
        img.CrossFadeAlpha(0f, 0f, true);

        img.CrossFadeAlpha(1f, 1f, false);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator FadeOut(RawImage img)
    {
        img.CrossFadeAlpha(0f, 2f, false);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator FadeInAndOut(RawImage img, float time)
    {
        yield return FadeIn(img);
        yield return new WaitForSeconds(time);
        yield return FadeOut(img);
    }

    public IEnumerator FadeIn(TextMeshProUGUI text)
    {
        Color fixedColor = text.color;
        fixedColor.a = 1;
        text.color = fixedColor;
        text.CrossFadeAlpha(0f, 0f, true);

        text.CrossFadeAlpha(1f, 1f, false);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator FadeOut(TextMeshProUGUI text)
    {
        text.CrossFadeAlpha(0f, 2f, false);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator FadeInAndOut(TextMeshProUGUI text, float time)
    {
        yield return FadeIn(text);
        yield return new WaitForSeconds(time);
        yield return FadeOut(text);
    }
}