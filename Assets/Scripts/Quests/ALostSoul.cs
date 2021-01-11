using Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALostSoul : MonoBehaviour
{
    public QuestManager questManager;

    private void Start()
    {
        PlayerData.ALostSoulCompleted = false;
        PlayerData.ALostSoulPart = 0;
    }
    public void Play()
    {
        PlayerData.ALostSoulStarted = true;
        if (!PlayerData.ALostSoulCompleted)
        {
            if (PlayerData.ALostSoulPart > 0) questManager.CurrentQuest = "A Lost Soul";
            switch (PlayerData.ALostSoulPart)
            {
                case 0: StartCoroutine(Part0()); break;
                //case 1: StartCoroutine(Part1()); break;
                //case 2: StartCoroutine(Part2()); break;
            }
        }
    }

    public string GetProgress()
    {
        return "Started";
    }

    IEnumerator Completed()
    {
        Debug.Log("Completed");
        PlayerData.ALostSoulCompleted = true;
        switch (PlayerData.ALostSoulPart)
        {
            case 0:
                Debug.Log("Part matched");
                //yield return questManager.RemoveQuestMarker(questMarker);
                yield return questManager.Refused("A Lost Soul");
                Debug.Log("Failed");
                break;
            /*case 1:
                yield return questManager.RemoveQuestMarker(swordMarker);
                yield return questManager.Completed(sword, "You stole a sword!");
                Debug.Log("Sword Kept");
                break;
            case 2:
                yield return questManager.RemoveQuestMarker(arthurMarker);
                yield return questManager.Speak("Arthur", "Here. This was my Dads. He'd be happy to know it's in the hands of a capable warrior.");
                yield return questManager.Completed(armour, "You got an old breastplate!");
                Debug.Log("Sword Returned");
                break;*/
            default: Debug.Log("Part not matched" + PlayerData.ALostSoulPart);
                break;
        }
        yield return null;

    }

    IEnumerator Part0()
    {
        yield return questManager.Speak("Sophie", "Help! My friend Soul was supposed to visit days ago but hasn't arrived yet.");
        yield return questManager.Speak("Sophie", "I'm really scared something might have happened to him. Please can you try and find him?");
        questManager.ShowRadial("He's more then likely dead", "I'll do my best, stay strong.");
        while (!questManager.Answered)
        {
            yield return null;
        }
        questManager.HideRadial();
        if (questManager.LastSelection == 0)
        {
            yield return questManager.Speak("Sophie", "How could you say that!");
            StartCoroutine(Completed());
        }
        else
        {
            questManager.CurrentQuest = "A Lost Soul";
            StartCoroutine(questManager.Started());
            PlayerData.ALostSoulPart = 1;
        }
    }
}
