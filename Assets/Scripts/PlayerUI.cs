using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public UIManager uiManager;
    public TextMeshProUGUI health;
    public Image fill;
    public Fade fade;

    public void Update()
    {
        fill.fillAmount = PlayerData.Health / 100;
        if (PlayerData.Health < 25)
        {
            fill.color = new Color(0.8f, 0.2f, 0.2f);
        }
        else if (PlayerData.Health < 50)
        {
            fill.color = new Color(0.8f, 0.6f, 0.2f);
        } else
        {
            fill.color = new Color(0.2f, 0.8f, 0.2f);
        }
        if (PlayerData.Health <= 0)
            StartCoroutine(Die());
    }

    public IEnumerator Die()
    {
        yield return uiManager.DeathScreen();
    }

}