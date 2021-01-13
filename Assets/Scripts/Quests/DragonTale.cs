using Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTale : MonoBehaviour
{

    public QuestManager questManager;

    public Compass compass;
    public QuestMarker gsMarker;
    public QuestMarker exMarker;
    public QuestMarker lsMarker;
    public QuestMarker bjMarker;
    public QuestMarker mayorMarker;
    public QuestMarker lairMarker;

    public Canvas gsCanvas;
    public Canvas exCanvas;
    public Canvas lsCanvas;
    public Canvas bjCanvas;

    public GameObject logs;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerData.DragonsTalePart = 1;
        if (PlayerData.DragonsTalePart == 1)
        {
            Debug.Log("Set to -1");
            PlayerData.DragonsTalePart = 2;
            PlayerData.GrandmasStewPart = -1;
            PlayerData.ExcalibwherePart = -1;
            PlayerData.ALostSoulPart = -1;
            PlayerData.BeetleJuicePart = -1;
            gsCanvas.gameObject.SetActive(false);
            exCanvas.gameObject.SetActive(false);
            lsCanvas.gameObject.SetActive(false);
            bjCanvas.gameObject.SetActive(false);
            StartCoroutine(compass.AddQuestMarker(mayorMarker));
        }
    }

    public void Play()
    {

        if (!PlayerData.DragonsTaleCompleted)
        {
            if (PlayerData.DragonsTalePart > 0) questManager.CurrentQuest = "Dragon Tale";
            switch (PlayerData.DragonsTalePart)
            {
                case 2: StartCoroutine(Part2()); break;
                case 3: StartCoroutine(Part3()); break;
            }
        }
    }

    public string GetProgress()
    {
        switch (PlayerData.DragonsTalePart)
        {
            case 2:
                return "Talk to the Mayor";
            case 3 when PlayerData.VillageComplete:
                return "Go to Yvryr's Lair";
            case 3:
                return "Go to the Dragon's Lair or help the villagers";
            default: return "";
        }
    }

    IEnumerator Part2()
    {
        yield return questManager.Speak("Mayor", "Greetings, traveller. How may I help you?");
        yield return questManager.Speak("Mayor", "Yvryr attacked you!? What a fearsome beast.");
        yield return questManager.Speak("Mayor", "No one from the village dares to approach his lair. And those who do never come back.");
        yield return questManager.Speak("Mayor", "A quest to kill the dragon is very noble. Perhaps if you help out the villagers, they might help you with your mission.");
        logs.SetActive(false);
        
        PlayerData.GrandmasStewPart = 0;
        PlayerData.ExcalibwherePart = 0;
        PlayerData.ALostSoulPart = 0;
        PlayerData.BeetleJuicePart = 0;

        gsCanvas.gameObject.SetActive(true);
        exCanvas.gameObject.SetActive(true);
        lsCanvas.gameObject.SetActive(true);
        bjCanvas.gameObject.SetActive(true);

        StartCoroutine(compass.AddQuestMarker(gsMarker));
        StartCoroutine(compass.AddQuestMarker(exMarker));
        StartCoroutine(compass.AddQuestMarker(lsMarker));
        StartCoroutine(compass.AddQuestMarker(bjMarker));

        PlayerData.DragonsTalePart = 3;
        StartCoroutine(compass.AddQuestMarker(lairMarker));
        StartCoroutine(compass.RemoveQuestMarker(mayorMarker));
    }

    IEnumerator Part3()
    {
        if (PlayerData.TalkingTo == "Mayor")
        {
            if (!PlayerData.GrandmasStewCompleted)
            {
                yield return questManager.Speak("Mayor", "Your Grandma lives in this village correct? I'm sure she'd appreciate a visit.");
            } else if (!PlayerData.ExcalibwhereCompleted)
            {
                yield return questManager.Speak("Mayor", "Athur's Dad died recently and he's not been the same since. Maybe you could check in with him?");
            } else if (!PlayerData.ALostSoulCompleted)
            {
                yield return questManager.Speak("Mayor", "Sophie's been a bit on edge recently. Please can you see what the problem is.");
            } else if (!PlayerData.BeetleJuiceCompleted)
            {
                yield return questManager.Speak("Mayor", "Luna's been banging on my door for days about some sort of pest problem. Please can you help her");
            } else
            {
                yield return questManager.Speak("Mayor", "You've spoken to everyone in the village. I hope your help is appreciated.");
                yield return questManager.Speak("Mayor", "All that's left to do now is face Yvryr. Good luck.");
                PlayerData.VillageComplete = true;
            }
        }
    }


}
