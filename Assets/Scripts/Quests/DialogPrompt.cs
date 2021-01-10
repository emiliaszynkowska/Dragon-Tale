using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogPrompt : MonoBehaviour
{
    public QuestManager questManager;
    public TextMeshProUGUI promptText;
    public string questName;

    private bool active = true;

    private void OnTriggerEnter(Collider other)
    {
        switch (questName)
        {
            case "Grandma's Stew":
                active = !PlayerData.GrandmasStewCompleted;
                break;
            case "Excalibwhere?":
                active = !PlayerData.ExcalibwhereCompleted;
                break;
            default:
                break;
        }
        if (active)
        {
            promptText.text = "Press X to talk";
            promptText.gameObject.SetActive(true);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.X) && active)
        {
            GetComponent<Canvas>().enabled = false;
            questManager.NewQuest(questName);
            promptText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (active) promptText.gameObject.SetActive(false);
    }


}
