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

    public void UpdateHealth(float h)
    {
        fill.fillAmount = h / 50;
    }

    public IEnumerator Die()
    {
        yield return uiManager.DeathScreen();
    }

}