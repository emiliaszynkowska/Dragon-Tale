using Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleJuice : MonoBehaviour
{

    public QuestManager questManager;
    public GameObject jesse;
    public List<Creature> beetles;
    private void Start()
    {
        foreach (Creature beetle in beetles)
        {
            beetle.gameObject.SetActive(false);
        }
        jesse.SetActive(false);
    }
    public void Play()
    {

        PlayerData.BeetleJuiceStarted = true;
        if (!PlayerData.BeetleJuiceCompleted)
        {
            if (PlayerData.BeetleJuicePart > 0) questManager.CurrentQuest = "Beetle Juice";
            switch (PlayerData.BeetleJuicePart)
            {
                case 0: StartCoroutine(Part0()); break;
                case 1: StartCoroutine(Part1()); break;
                //case 2: StartCoroutine(Part2()); break;
            }
        }
    }

    public string GetProgress()
    {
        switch (PlayerData.BeetleJuicePart)
        {
            case 1:
                return "started";
            case 2:
                return "Return to Arthur";
            default: return "";
        }
    }

    IEnumerator Completed()
    {

        PlayerData.BeetleJuiceCompleted = true;
        switch (PlayerData.BeetleJuicePart)
        {
            case 0:
                //yield return questManager.RemoveQuestMarker(questMarker);
                yield return questManager.Refused("Beetle Juice");
                Debug.Log("Failed");
                break;
            case 1:
                //yield return questManager.RemoveQuestMarker(swordMarker);
                //yield return questManager.Completed(sword, "You stole a sword!");
                //inventory.AddItem(sword, "Sword. This sword increases your base attack by 25%");
                //Debug.Log("Sword Kept");
                break;
            case 2:
                //yield return questManager.RemoveQuestMarker(arthurMarker);
                //yield return questManager.Speak("Arthur", "Here. This was my Dads. He'd be happy to know it's in the hands of a capable warrior.");
                //inventory.AddItem(armour, "Breastplate. This breastplate increases you base defence by 25%");
                //yield return questManager.Completed(armour, "You got an old breastplate!");
                //Debug.Log("Sword Returned");
                break;
        }
        yield return null;

    }



    IEnumerator Part0()
    {
        yield return questManager.Speak("Luna", "You're for hire right? I've got a huge problem.");
        yield return questManager.Speak("Luna", "There's an island on the East side of the town. It's beautiful.");
        yield return questManager.Speak("Luna", "I often go there at night and watch the moon to relax.");
        yield return questManager.Speak("Luna", "Recently, a bunch of disgusting beetles have invaded making the area inaccessable and will likely hurt anyone of goes there.");
        yield return questManager.Speak("Luna", "Please remove these creatures and restore the peace.");
        questManager.ShowRadial("Not my problem.", "I'll see what I can do.");
        while (!questManager.Answered)
        {
            yield return null;
        }
        questManager.HideRadial();
        if (questManager.LastSelection == 0)
        {
            yield return questManager.Speak("Luna", "Coward. I'll do it myself.");
            StartCoroutine(Completed());
        }
        else
        {
            questManager.CurrentQuest = "Beetle Juice";
            PlayerData.BeetleJuicePart = 1;
            foreach (Creature beetle in beetles)
            {
                beetle.gameObject.SetActive(true);
            }
            jesse.SetActive(true);
            StartCoroutine(questManager.Started());
        }
    }

    IEnumerator Part1()
    {
        yield return questManager.Speak("Jesse", "Sup");
    }
}
