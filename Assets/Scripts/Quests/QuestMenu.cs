using System.Collections;
using System.Collections.Generic;
using Quests;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestMenu : MonoBehaviour
{
    public QuestManager questManager;
    public GameObject menu;

    public GrandmasStew grandmasStew;
    public Button grandmasStewBtn;
    public TextMeshProUGUI grandmasStewTxt;

    public Excalibwhere excalibwhere;
    public Button excalibwhereBtn;
    public TextMeshProUGUI excalibwhereTxt;

    private void Start()
    {
        grandmasStewBtn.onClick.AddListener(() => Current("Grandma's Stew"));
        excalibwhereBtn.onClick.AddListener(() => Current("Excalibwhere?"));
    }
    // Update is called once per frame
    public void Update()
    {
        if(PlayerData.GrandmasStewCompleted && PlayerData.GrandmasStewPart == 0)
        {
            //Refused
            grandmasStewBtn.image.color = new Color(1f, 0.5f, 0.5f);
            grandmasStewBtn.interactable = false;
            grandmasStewTxt.text = "Grandma's Stew - Refused";
        } else if (PlayerData.GrandmasStewCompleted)
        {
            //Complete
            grandmasStewBtn.image.color = new Color(0.5f, 1f, 0.5f);
            grandmasStewBtn.interactable = false;
            grandmasStewTxt.text = "Grandma's Stew - Complete";
        } else if (!PlayerData.GrandmasStewStarted)
        {
            //Undiscovered
            grandmasStewBtn.image.color = new Color(0.5f, 0.5f, 0.5f);
            grandmasStewBtn.interactable = false;
            grandmasStewTxt.text = "Undiscovered";
        } else
        {
            //Details
            grandmasStewBtn.interactable = true;
            grandmasStewBtn.image.color = new Color(1f, 1f, 1f);
            grandmasStewTxt.text = "Grandma's Stew - " + grandmasStew.GetProgress();

        }

        if (PlayerData.ExcalibwhereCompleted && PlayerData.ExcalibwherePart == 0)
        {
            //Refused
            excalibwhereBtn.image.color = new Color(1f, 0.5f, 0.5f);
            excalibwhereBtn.interactable = false;
            excalibwhereTxt.text = "Excalibwhere? - Refused";
        }
        else if (PlayerData.ExcalibwhereCompleted)
        {
            //Complete
            excalibwhereBtn.image.color = new Color(0.5f, 1f, 0.5f);
            excalibwhereBtn.interactable = false;
            excalibwhereTxt.text = "Exclibwhere? - Complete";
        }
        else if (!PlayerData.ExcalibwhereStarted)
        {
            //Undiscovered
            excalibwhereBtn.image.color = new Color(0.5f, 0.5f, 0.5f);
            excalibwhereBtn.interactable = false;
            excalibwhereTxt.text = "Undiscovered";
        }
        else
        {
            //Details
            excalibwhereBtn.interactable = true;
            excalibwhereBtn.image.color = new Color(1f, 1f, 1f);
            excalibwhereTxt.text = "Excalibwhere? - " + excalibwhere.GetProgress();

        }
    }

    private void Current(string quest)
    {
        questManager.CurrentQuest = quest; 
        menu.SetActive(false);
    }
}
