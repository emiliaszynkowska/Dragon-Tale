using System.Collections;
using System.Collections.Generic;
using Quests;
using UnityEngine;
using TMPro;

public class DialogPrompt : MonoBehaviour
{
    public QuestManager questManager;
    public TextMeshProUGUI promptText;
    public string questName;
    public Canvas canvas;

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
            case "ALostSoul":
                active = !PlayerData.ALostSoulCompleted;
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
        if (Input.GetKeyDown(KeyCode.X) && active)
        {
            canvas.GetComponent<Canvas>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            questManager.NewQuest(questName);
            promptText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (active) promptText.gameObject.SetActive(false);
    }


}
