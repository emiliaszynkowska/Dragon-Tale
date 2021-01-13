using Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleJuice : MonoBehaviour
{

    public QuestManager questManager;
    public GameObject jesse;
    public List<Creature> beetles;
    public GameObject player;
    public Inventory inventory;
    public Texture speedPotion;
    public Texture damagePotion;
    public QuestMarker questMarker;
    public QuestMarker beetleMarker;

    private bool hunting;
    private void Start()
    {
        //StartCoroutine(questManager.AddQuestMarker(questMarker));
        foreach (Creature beetle in beetles)
        {
            beetle.gameObject.SetActive(false);
        }
        jesse.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerData.BeetleJuicePart == 1 && other.CompareTag("Player"))
        {
            StartCoroutine(Part1());
            GetComponent<BoxCollider>().enabled = false;
        } else if (PlayerData.BeetleJuicePart == 2 && other.CompareTag("Player"))
        {
            hunting = true;
            GetComponent<SphereCollider>().enabled = false;
        }
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
                case 2: StartCoroutine(Part2()); break;
            }
        }
    }

    public string GetProgress()
    {
        switch (PlayerData.BeetleJuicePart)
        {
            case 1:
                return "Go to the Island";
            case 2:
                return "Metalons Killed: " + PlayerData.BeetlesKilled + "/3";
            default: return "";
        }
    }

    IEnumerator Completed()
    {

        PlayerData.BeetleJuiceCompleted = true;
        switch (PlayerData.BeetleJuicePart)
        {
            case 0:
                yield return questManager.RemoveQuestMarker(questMarker);
                yield return questManager.Refused("Beetle Juice");
                Debug.Log("Failed");
                break;
            case 1:
                yield return questManager.Speak("Jesse", "Thank you for showing mercy! Here have this elixir, it should make you move faster for a time");
                yield return questManager.RemoveQuestMarker(beetleMarker);
                yield return questManager.Completed(speedPotion, "You got a strange substance off a strange man.");
                inventory.AddItem(speedPotion, "Speed Potion. Makes you move faster, side effects may vary.");
                Debug.Log("Beetles Spared");
                break;
            case 2:
                yield return questManager.RemoveQuestMarker(beetleMarker);
                inventory.AddItem(damagePotion, "Metalon Blood. Toxic to others, the blood of your enemies boosts power.");
                yield return questManager.Completed(damagePotion, "Take their blood and sprinkle it on your sword.");
                Debug.Log("Beetles Killed");
                break;
        }
        questManager.CurrentQuest = "Dragon Tale";
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
            GetComponent<BoxCollider>().enabled = true;
            yield return questManager.UpdateQuestMarker(beetleMarker, questMarker);
            StartCoroutine(questManager.Started());
        }
    }

    IEnumerator Part1()
    {
        GetComponent<BoxCollider>().enabled = false;
        jesse.transform.LookAt(player.transform);
        yield return questManager.Speak("Jesse", "Excuse me, please wait!");
        yield return questManager.Speak("Jesse", "I know Luna has asked you to kill these Metalons but I beg you, please spare them.");
        yield return questManager.Speak("Jesse", "If left alone they are completely harmless. I promise I'll relocated them. Just give me time.");
        questManager.ShowRadial("They are a danger and must be dealth with.", "As you wish. They're kinda cute anyway.");
        while (!questManager.Answered)
        {
            yield return null;
        }
        questManager.HideRadial();
        if (questManager.LastSelection == 0)
        {
            PlayerData.BeetleJuicePart = 2;
            GetComponent<SphereCollider>().enabled = true;
            StartCoroutine(Part2());
        }
        else
        {
            yield return questManager.Speak("Jesse", "Thank you! Thank you so much!");
            StartCoroutine(Completed());
        }
    }

    IEnumerator Part2()
    {
        while (!hunting)
        {
            yield return null;
        }
        foreach(Creature beetle in beetles)
        {
            beetle.Hunt(player);
        }
        while (PlayerData.BeetlesKilled < 3)
        {
            yield return null;
        }
        StartCoroutine(Completed());
        Debug.Log("Done");
    }
}
