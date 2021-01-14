using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Potion : MonoBehaviour
{
    public GameObject potionObj;
    public RawImage potionIcon;
    public TextMeshProUGUI potionText;
    public Texture lv1Potion;
    public Texture lv2Potion;
    public Texture lv3Potion;

   
    private void Update()
    {
        if (potionObj.activeInHierarchy && Input.GetKeyDown(KeyCode.G) && PlayerData.Health < 100)
        {
            if (PlayerData.HealthPotion25)
            {
                PlayerData.Health += 25;
                PlayerData.HealthPotion25 = false;
                Empty();
            } 
            else if (PlayerData.HealthPotion50)
            {
                PlayerData.Health += 50;
                PlayerData.HealthPotion50 = false;
                Empty();
            }
            else if (PlayerData.HealthPotion75)
            {
                PlayerData.Health += 75;
                PlayerData.HealthPotion75 = false;
                Empty();
            }
            if (PlayerData.Health > 100) PlayerData.Health = 100;
        }
    }
    public void Show()
    {
        if (PlayerData.GrandmasStewCompleted && PlayerData.GrandmasStewPart != 0)
        {
            switch (PlayerData.GrandmasStewPart)
            {
                case 1:
                    potionIcon.texture = lv1Potion;
                    break;
                case 2:
                    potionIcon.texture = lv2Potion;
                    break;
                case 3:
                    potionIcon.texture = lv3Potion;
                    break;
            }
            potionObj.SetActive(true);
        }
    }
    
    public void CheckEmpty()
    {
        if (PlayerData.GrandmasStewCompleted && ((PlayerData.GrandmasStewPart == 1 && !PlayerData.HealthPotion25)
                                              || (PlayerData.GrandmasStewPart == 2 && !PlayerData.HealthPotion50)
                                              || (PlayerData.GrandmasStewPart == 3 && !PlayerData.HealthPotion75))) Empty();
    }

    void Empty()
    {
        potionIcon.color = new Color(1, 1, 1, 0.25f);
        potionText.text = "Refill";
    }

    public void Restock()
    {
        potionIcon.color = new Color(1, 1, 1, 1);
        potionText.text = "Press G";
        switch (PlayerData.GrandmasStewPart)
        {
            case 1:
                PlayerData.HealthPotion25 = true;
                break;
            case 2:
                PlayerData.HealthPotion50 = true;
                break;
            case 3:
                PlayerData.HealthPotion75 = true;
                break;
        }
    }
}
