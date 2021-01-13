using Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTale : MonoBehaviour
{

    public QuestManager questManager;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerData.DragonsTalePart == 1)
        {
            Debug.Log("Set to -1");
            PlayerData.DragonsTalePart = 2;
            PlayerData.GrandmasStewPart = -1;
            PlayerData.ExcalibwherePart = -1;
            PlayerData.ALostSoulPart = -1;
            PlayerData.BeetleJuicePart = -1;
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
            }
        }
    }

    public string GetProgress()
    {
        switch (PlayerData.DragonsTalePart)
        {
            case 2:
                return "Talk to the Mayor";
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
        PlayerData.DragonsTalePart = 3;
    }


}
