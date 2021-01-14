using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTest : MonoBehaviour
{
    public Image fill;
    public PlayerUI ui;
    private void OnTriggerEnter(Collider other)
    {
        float h = PlayerData.Health;
        PlayerData.Health -= 20;
        Debug.Log(h + "/" + PlayerData.Health + "/" + fill.fillAmount);

    }
}
