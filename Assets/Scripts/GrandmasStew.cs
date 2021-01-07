using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmasStew : MonoBehaviour
{
    public QuestManager questManager;
    public UIManager uiManager;
    public bool Completed { get; set; }

    public void Play()
    {
        if (!Completed)
        {
            if (PlayerData.GrandmasStewPart > 0) questManager.CurrentQuest = "Grandma's Stew";
            switch (PlayerData.GrandmasStewPart)
            {
                case 0: StartCoroutine(Part0()); break;
                case 1: StartCoroutine(Part1()); break;
            }
        }
        
    }
    public IEnumerator Part0()
    {
        questManager.Speak("Grandma", "Hello, my dear! You look famished, has no one been feeding you? I'm going to make you my special stew.");
        yield return new WaitForSeconds(3.5f);
        questManager.Speak("Grandma", "I think I've run out of red mushrooms. Be a sweetheart and get some from the forest for me, please.");
        yield return new WaitForSeconds(3.5f);
        uiManager.UnSetTextBox();
        questManager.ShowRadial("No thank you, Grandma. I'm very busy.", "That'd be great, thank you. Be right back.");
        while (!questManager.Answered)
        {
            yield return null;
        }
        questManager.HideRadial();
        if (questManager.LastSelection == 0)
        {
            questManager.Speak("Grandma", "Oh, ok honey. Stay safe.");
            yield return new WaitForSeconds(3.5f);
            uiManager.UnSetTextBox();
            Completed = true;
        }
        else
        {
            questManager.ShowDetails("Grandma's Stew", "Red Mushrooms Collected: " + PlayerData.MushroomsCollected + "/15");
            questManager.CurrentQuest = "Grandma's Stew";
            PlayerData.GrandmasStewPart = 1;
        }
    }

    public IEnumerator Part1()
    {
        if (PlayerData.MushroomsCollected < 15)
        {
            uiManager.SetTextBox("Still need a few more mushrooms.");
            yield return new WaitForSeconds(3.5f);
            uiManager.UnSetTextBox();
        }
        yield return null;
    }
}
