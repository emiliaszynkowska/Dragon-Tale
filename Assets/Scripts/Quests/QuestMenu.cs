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
    public PlayerMovement player;

    public GrandmasStew grandmasStew;
    public Button grandmasStewBtn;
    public TextMeshProUGUI grandmasStewTxt;

    public Excalibwhere excalibwhere;
    public Button excalibwhereBtn;
    public TextMeshProUGUI excalibwhereTxt;

    public ALostSoul aLostSoul;
    public Button aLostSoulBtn;
    public TextMeshProUGUI aLostSoulTxt;

    private void Start()
    {
        grandmasStewBtn.onClick.AddListener(() => Current("Grandma's Stew"));
        excalibwhereBtn.onClick.AddListener(() => Current("Excalibwhere?"));
        aLostSoulBtn.onClick.AddListener(() => Current("A Lost Soul"));
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
            excalibwhereTxt.text = "Excalibwhere? - Complete";
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

        if (PlayerData.ALostSoulCompleted && PlayerData.ALostSoulPart == 0)
        {
            //Refused
            aLostSoulBtn.image.color = new Color(1f, 0.5f, 0.5f);
            aLostSoulBtn.interactable = false;
            aLostSoulTxt.text = "A Lost Soul - Refused";
        }
        else if (PlayerData.ALostSoulCompleted)
        {
            //Complete
            aLostSoulBtn.image.color = new Color(0.5f, 1f, 0.5f);
            aLostSoulBtn.interactable = false;
            aLostSoulTxt.text = "A Lost Soul - Complete";
        }
        else if (!PlayerData.ALostSoulStarted)
        {
            //Undiscovered
            aLostSoulBtn.image.color = new Color(0.5f, 0.5f, 0.5f);
            aLostSoulBtn.interactable = false;
            aLostSoulTxt.text = "Undiscovered";
        }
        else
        {
            //Details
            aLostSoulBtn.interactable = true;
            aLostSoulBtn.image.color = new Color(1f, 1f, 1f);
            aLostSoulTxt.text = "A Lost Soul - " + aLostSoul.GetProgress();

        }
    }

    private void Current(string quest)
    {
        questManager.CurrentQuest = quest; 
        menu.SetActive(false);
        questManager.FreezePlayer(false);
    }
}
