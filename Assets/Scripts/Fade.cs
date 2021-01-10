using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fade : MonoBehaviour
{
    public RawImage imgRaw;
    public Image img;
    public PlayerMovement player;
    
    void Start()
    {
        StartCoroutine(BlackOut());
        //img.color = new Color(0f, 0f, 0f, 1f);
        //StartCoroutine(FadeOut());
    }

    public IEnumerator BlackIn()
    {
        player.SetCanMove(false);
        if (imgRaw != null)
        {
            yield return FadeIn(imgRaw.gameObject);
        }
        else
        {
            yield return FadeIn(img.gameObject);
        }
        yield return null;
    }

    public IEnumerator BlackOut()
    {
        player.SetCanMove(true);
        if (imgRaw != null)
        {
            imgRaw.color = new Color(0f, 0f, 0f, 1f);
            yield return FadeOut(imgRaw.gameObject);
        }
        else
        {
            img.color = new Color(0f, 0f, 0f, 1f);
            yield return FadeOut(img.gameObject);
        }
        yield return null;
    }

    public IEnumerator BlackInAndOut()
    {
        yield return BlackIn();
        yield return new WaitForSeconds(1);
        yield return BlackOut();
    }

    /*public IEnumerator FadeIn()//Fade In brings the given object into view
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
    }*/

    public IEnumerator FadeIn(GameObject obj)
    {
        if (obj.GetComponent<TextMeshProUGUI>() != null)
        {
            Color fixedColor = obj.GetComponent<TextMeshProUGUI>().color; //These four lines are required to make FadeIn work... for some reason
            fixedColor.a = 1;
            obj.GetComponent<TextMeshProUGUI>().color = fixedColor;
            obj.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0f, 0f, true);
            obj.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1f, 1f, false);
            yield return new WaitForSeconds(1);
        } else if (obj.GetComponent<RawImage>() != null)
        {
            Color fixedColor = obj.GetComponent<RawImage>().color;
            fixedColor.a = 1;
            obj.GetComponent<RawImage>().color = fixedColor;
            obj.GetComponent<RawImage>().CrossFadeAlpha(0f, 0f, true);
            obj.GetComponent<RawImage>().CrossFadeAlpha(1f, 1f, false);
            yield return new WaitForSeconds(1);
        } else if (obj.GetComponent<Image>() != null)
        {
            Color fixedColor = obj.GetComponent<Image>().color;
            fixedColor.a = 1;
            obj.GetComponent<Image>().color = fixedColor;
            obj.GetComponent<Image>().CrossFadeAlpha(0f, 0f, true);
            obj.GetComponent<Image>().CrossFadeAlpha(1f, 1f, false);
            yield return new WaitForSeconds(1);
        }
    }

    public IEnumerator FadeOut(GameObject obj)
    {
        if (obj.GetComponent<TextMeshProUGUI>() != null)
        {
            obj.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0f, 2f, false);
            yield return new WaitForSeconds(1);
        } else if(obj.GetComponent<RawImage>() != null)
        {
            obj.GetComponent<RawImage>().CrossFadeAlpha(0f, 2f, false);
            yield return new WaitForSeconds(1);
        } else if (obj.GetComponent<Image>() != null)
        {
            obj.GetComponent<Image>().CrossFadeAlpha(0f, 2f, false);
            yield return new WaitForSeconds(1);
        }
    }

    public IEnumerator FadeInAndOut(GameObject obj, float time)
    {
        yield return FadeIn(obj);
        yield return new WaitForSeconds(time);
        yield return FadeOut(obj);
    }
}