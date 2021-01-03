using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public RawImage img;
    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOut());
    }

    public void Out(RawImage img)
    {
        StartCoroutine(FadeIn(img));
    }

    IEnumerator FadeIn(RawImage img)
    {
        player.SetCanMove(false);
        img.CrossFadeAlpha(1f, 1f, false);
        yield return new WaitForSeconds(1);
    }

    IEnumerator FadeOut()
    {
        player.SetCanMove(true);
        img.CrossFadeAlpha(0f, 2f, false);
        yield return new WaitForSeconds(1);
    }
}
