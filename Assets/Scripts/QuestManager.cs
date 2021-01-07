using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestManager : MonoBehaviour
{

    //Speech Box Components
    public UIManager uiManager;
    public Image icon;
    public TextMeshProUGUI speakerName;

    //Radial Menu Components
    public Button menuLeft;
    public Text menuLeftText;
    public Button menuRight;
    public Text menuRightText;
    public bool Answered { get; set; }
    public int LastSelection { get; set; }
    //Progress Components
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questProgress;
    public string CurrentQuest { get; set; }
    //Quests
    public GrandmasStew grandmasStew;

    void Start()
    {
        questTitle.gameObject.SetActive(false);
        questProgress.gameObject.SetActive(false);
        menuLeft.gameObject.SetActive(false);
        menuRight.gameObject.SetActive(false);
    }

    private void Update()
    {
        switch (CurrentQuest)
        {
            case "Grandma's Stew": ShowDetails(CurrentQuest, "Red Mushrooms Collected: " + PlayerData.MushroomsCollected + "/15");
                break;
        }
    }

    public void NewQuest(int ID)
    {
        switch (ID)
        {
            case 0: grandmasStew.Play();
                break;
        }
    }
    public void ShowRadial(string leftText, string rightText)
    {
        Answered = false;
        menuLeftText.text = leftText;
        menuRightText.text = rightText;
        menuLeft.gameObject.SetActive(true);
        menuRight.gameObject.SetActive(true);
    }

    public void HideRadial()
    {
        menuLeft.gameObject.SetActive(false);
        menuRight.gameObject.SetActive(false);
    }

    public void OptionSelected(int option)
    {
        switch (option)
        {
            case 0: LastSelection = 0;
                break;

            case 1: LastSelection = 1;
                break;
        }
        Answered = true;
    }

    public void ShowDetails(string title, string progress)
    {
        questTitle.gameObject.SetActive(true);
        questTitle.text = title;
        questProgress.gameObject.SetActive(true);
        questProgress.text = progress;
    }

    public void SetSpeaker(string speaker)
    {
        speakerName.text = speaker;
        /*switch (speaker) { //Put Speaker Icon Here
            case "Grandma": ;
                break;
        }*/
    }

    public void Speak(string speaker, string message)
    {
        SetSpeaker(speaker);
        uiManager.SetTextBox(message);
    }
}
