using System.Collections;
using System.Collections.Generic;
using Quests;
using UnityEngine;

public class Excalibwhere : MonoBehaviour
{

    //Basic game scripts
    public QuestManager questManager;
    public UIManager uiManager;
    public Inventory inventory;

    //Reward Icons
    public Texture sword;
    public Texture armour;

    //This quest specific
    private bool checkRunning = false;

    //Compass
    public QuestMarker questMarker;
    public QuestMarker arthurMarker;
    public QuestMarker swordMarker;

    private void Start()
    {
        PlayerData.ExcalibwherePart = 0;
        PlayerData.ExcalibwhereCompleted = false;
        StartCoroutine(questManager.AddQuestMarker(questMarker));
    }

    //Called when the player 'talks' to the villager who gives the task
    public void Play()
    {

        PlayerData.ExcalibwhereStarted = true;
        if (!PlayerData.ExcalibwhereCompleted)
        {
            if (PlayerData.ExcalibwherePart > 0) questManager.CurrentQuest = "Excalibwhere?";
            switch (PlayerData.ExcalibwherePart)
            {
                case 0: StartCoroutine(Part0()); break;
                case 1: StartCoroutine(Part1()); break;
                case 2: StartCoroutine(Part2()); break;
            }
        }
    }

    //Seperate parts control which stage of the quest the player is in. This is primarily to pick the correct reward at the end but also aids structure.
    //Speak calls prompt dialog
    //Show radial brings up the players options and waits for a response.
    public IEnumerator Part0()
    {
        yield return questManager.Speak("Arthur", "Hello, adventurer! I wonder if you can help me.");
        yield return questManager.Speak("Arthur", "Last week, whilst I was practicing with my sword on the beach, a big wave came in and knocked it out my hands.");
        yield return questManager.Speak("Arthur", "That sword means a lot to me but I fear it's been claimed by the sea.");
        yield return questManager.Speak("Arthur", "If you happen to stumble upon it when out on your travels please bring it back to me.");
        questManager.ShowRadial("I'm sorry, no. Accept that it's gone.", "Of course, I'll keep an eye out.");
        while (!questManager.Answered)
        {
            yield return null;
        }
        questManager.HideRadial();
        if (questManager.LastSelection == 0)
        {
            yield return questManager.Speak("Arthur", "Never! I know it will find it's way back to me!");
            StartCoroutine(Completed());
        }
        else
        {
            questManager.CurrentQuest = "Excalibwhere?";
            StartCoroutine(questManager.Started());
            PlayerData.ExcalibwherePart = 1;
            StartCoroutine(CheckSword());
        }
    }

    private IEnumerator CheckSword()
    {
        yield return questManager.UpdateQuestMarker(swordMarker, questMarker);
        checkRunning = true;
        while (checkRunning)
        {
            if (PlayerData.ExcalibwherePart == 1 && PlayerData.SwordCollected)
            {
                checkRunning = false;
                yield return StartCoroutine(questManager.Speak("Player", "I wonder if this is Arthur's sword? I should take it back to him... but it does look very powerful..."));
                questManager.ShowRadial("Keep the sword", "Take it back");
                while (!questManager.Answered)
                {
                    yield return null;
                }
                questManager.HideRadial();
                if (questManager.LastSelection == 0)
                {
                    StartCoroutine(Completed());
                }
                else
                {
                    PlayerData.ExcalibwherePart = 2;
                    yield return questManager.UpdateQuestMarker(arthurMarker, swordMarker);
                }
            }
            yield return null;
        }
    }
    public IEnumerator Part1()
    {
        if (!PlayerData.SwordCollected)
        {
            yield return questManager.Speak("Arthur", "Any luck finding finding my sword?");
        }
    }

    public IEnumerator Part2()
    {
        if (PlayerData.SwordCollected)
        {
            yield return questManager.Speak("Arthur", "You found my sword! I don't believe it! Thank you so much!");
            StartCoroutine(Completed());
        }
    }

    //String to be displayed in the HUD
    public string GetProgress()
    {
        switch (PlayerData.ExcalibwherePart)
        {
            case 1:
                return "Look for the lost sword";
            case 2:
                return "Return to Arthur";
            default: return "";
        }
    }

    //Dicates which action to take when the quest is completed/ended
    //If rewarded with an item makes a Completed call in QM
    IEnumerator Completed()
    {

        PlayerData.ExcalibwhereCompleted = true;
        switch (PlayerData.ExcalibwherePart)
        {
            case 0:
                yield return questManager.RemoveQuestMarker(questMarker);
                yield return questManager.Refused("Excalibwhere?");
                Debug.Log("Failed");
                break;
            case 1:
                yield return questManager.RemoveQuestMarker(swordMarker);
                yield return questManager.Completed(sword, "You stole a sword!");
                inventory.AddItem(sword, "Sword. This sword increases your base attack by 25%");
                Debug.Log("Sword Kept");
                break;
            case 2:
                yield return questManager.RemoveQuestMarker(arthurMarker);
                yield return questManager.Speak("Arthur", "Here. This was my Dads. He'd be happy to know it's in the hands of a capable warrior.");
                inventory.AddItem(armour, "Breastplate. This breastplate increases you base defence by 25%");
                yield return questManager.Completed(armour, "You got an old breastplate!");
                Debug.Log("Sword Returned");
                break;
        }
        yield return null;

    }
}
