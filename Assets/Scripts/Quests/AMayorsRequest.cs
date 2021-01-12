using System.Collections;
using UnityEngine;

namespace Quests
{
    public class AMayorsRequest : MonoBehaviour
    {
        public QuestManager questManager;
        public UIManager uiManager;
        public Fade fade;
        public Inventory inventory;

        //Reward Icons
        public Texture reward1;

        //Quest Markers
        public QuestMarker questMarker;

        private void Start()
        {
            StartCoroutine(questManager.AddQuestMarker(questMarker));
            PlayerData.AMayorsRequestCompleted = false;
        }

        public void Play()
        {
            PlayerData.AMayorsRequestStarted = true;
        }
        
        public IEnumerator Part0()
        {
            yield return questManager.Speak("Mayor", "Greetings, traveller. How may I help you?");
            yield return questManager.Speak("Mayor", "Yvryr attacked you!? What a fearsome beast.");
            yield return questManager.Speak("Mayor", "No one from the village dares to approach his lair. And those who do never come back.");
            yield return questManager.Speak("Mayor", "A quest to kill the dragon is very noble. Perhaps if you help out the villagers, they might help you in your quest.");
            questManager.ShowRadial("No, I've got better things to do.", "Yes, I'll try my best!");
            while (!questManager.Answered)
            {
                yield return null;
            }
            questManager.HideRadial();
            if (questManager.LastSelection == 0)
            {
                yield return questManager.Speak("Mayor", "Good luck fighting the dragon alone.");
                StartCoroutine(Completed());
            }
            else
            {
                questManager.CurrentQuest = "A Mayor's Request";
                StartCoroutine(questManager.Started());
                questManager.grandmasStew.gameObject.SetActive(true);
                questManager.excalibwhere.gameObject.SetActive(true);
                questManager.aLostSoul.gameObject.SetActive(true);
            }
        }

        public IEnumerator Part1()
        {
            if (!PlayerData.GrandmasStewCompleted && !PlayerData.ExcalibwhereCompleted && !PlayerData.ALostSoulCompleted)
            {
                yield return questManager.Speak("Mayor", "Please help the villagers.");
            } else
            {
                yield return questManager.Speak("Mayor", "We are all so grateful for your help.");
                yield return questManager.Speak("Mayor", "The time has come for you to fight Yvryr.");
                yield return questManager.Speak("Mayor", "I see how strong you've become, Yvryr won't stand a chance.");
                yield return questManager.Speak("Mayor", "Good luck, traveller.");
            }
            yield return null;
        }
        
        public string GetProgress()
        {
            switch (PlayerData.AMayorsRequestPart)
            {
                case 1:
                    return "Help the villagers";
                case 2:
                    return "Return to the mayor";
                default: return "";
            }
        }

        IEnumerator Completed()
        {
            PlayerData.AMayorsRequestCompleted = true;
            switch (PlayerData.AMayorsRequestPart)
            {
                case 0:
                    yield return questManager.RemoveQuestMarker(questMarker);
                    yield return questManager.Refused("A Mayor's Request");
                    Debug.Log("Didn't help villagers");
                    break;
            }
        }
    }
}
