using System.Collections;
using UnityEngine;

namespace Quests
{
    public class GrandmasStew : MonoBehaviour
    {
        public QuestManager questManager;
        public UIManager uiManager;
        public Fade fade;

        //Reward Icons
        public Texture reward1;
        public Texture reward2;
        public Texture reward3;

        private void Start()
        {
            PlayerData.GrandmasStewCompleted = false;
        }

        public void Play()
        {
            PlayerData.GrandmasStewStarted = true;
            if (!PlayerData.GrandmasStewCompleted)
            {
                if (PlayerData.GrandmasStewPart > 0) questManager.CurrentQuest = "Grandma's Stew";
                switch (PlayerData.GrandmasStewPart)
                {
                    case 0: StartCoroutine(Part0()); break;
                    case 1: StartCoroutine(Part1()); break;
                    case 2: StartCoroutine(Part2()); break;
                    case 3: StartCoroutine(Part3()); break;
                }
            }
        
        }
        public IEnumerator Part0()
        {
            yield return questManager.Speak("Grandma", "Hello, my dear! You look famished, has no one been feeding you? I'm going to make you my special stew.");
            yield return questManager.Speak("Grandma", "I think I've run out of red mushrooms. Be a sweetheart and get some from the forest for me, please.");
            questManager.ShowRadial("No thank you, Grandma. I'm very busy.", "That'd be great, thank you. Be right back.");
            while (!questManager.Answered)
            {
                yield return null;
            }
            questManager.HideRadial();
            if (questManager.LastSelection == 0)
            {
                yield return questManager.Speak("Grandma", "Oh, ok honey. Stay safe.");
                StartCoroutine(Completed());
            }
            else
            {
                questManager.CurrentQuest = "Grandma's Stew";
                StartCoroutine(questManager.Started());
                PlayerData.GrandmasStewPart = 1;
            }
        }

        public IEnumerator Part1()
        {
            if (PlayerData.MushroomsCollected < 15)
            {
                yield return questManager.Speak("Grandma", "Still need a few more mushrooms.");
            } else
            {
                yield return questManager.Speak("Grandma", "Have you got them all? Great.");
                yield return questManager.Speak("Grandma", "Whilst you were gone I realised we're also out of carrots. Do you mind running to the farm and picking some up, please?");
                questManager.ShowRadial("I don't think so.", "Of course, won't be long.");
                while (!questManager.Answered)
                {
                    yield return null;
                }
                questManager.HideRadial();
                if (questManager.LastSelection == 0)
                {
                    yield return questManager.Speak("Grandma", "Plain mushroom soup? Coming right up.");
                    StartCoroutine(Completed());
                } else
                {
                    PlayerData.GrandmasStewPart = 2;
                }
            }
            yield return null;
        }

        public IEnumerator Part2()
        {
            if (PlayerData.CarrotsCollected < 5)
            {
                yield return questManager.Speak("Grandma", "I think we'll need some more carrots.");
            } else
            {
                yield return questManager.Speak("Grandma", "Brillant. Thanks honey.");
                yield return questManager.Speak("Grandma", "Oh I've just had a fantistic idea. Should we add a couple apples?");
                yield return questManager.Speak("Grandma", "There's some apple trees just north of town. 3 should be plenty.");
                questManager.ShowRadial("I'm starving! Let's eat already.", "That sounds amazing!");
                while (!questManager.Answered)
                {
                    yield return null;
                }
                questManager.HideRadial();
                if (questManager.LastSelection == 0)
                {
                    yield return questManager.Speak("Grandma", "Oh of course! Silly me, I'll get cooking right away!");
                    StartCoroutine(Completed());
                }
                else
                {
                    PlayerData.GrandmasStewPart = 3;
                }
            }
        }

        public IEnumerator Part3()
        {
            if (PlayerData.ApplesCollected < 3)
            {
                yield return questManager.Speak("Grandma", "May need a some more apples please");
            } else
            {
                yield return questManager.Speak("Grandma", "Great, that's everything we need!");
                StartCoroutine(Completed());
            }
        }

        public string GetProgress()
        {
            switch (PlayerData.GrandmasStewPart)
            {
                case 1 when PlayerData.MushroomsCollected < 15:
                    return "Red Mushrooms Collected: " + PlayerData.MushroomsCollected + "/15";
                case 1:
                    return "Return to Grandma";
                case 2 when PlayerData.CarrotsCollected < 5:
                    return "Carrots Collected: " + PlayerData.CarrotsCollected + "/5";
                case 2:
                    return "Return to Grandma";
                case 3 when PlayerData.ApplesCollected < 3:
                    return "Apples Collected: " + PlayerData.ApplesCollected + "/3";
                case 3:
                    return "Return to Grandma";
                default: return "";
            }
        }

        IEnumerator Completed()
        {
        
            PlayerData.GrandmasStewCompleted = true;
            switch (PlayerData.GrandmasStewPart)
            {
                case 0:
                    yield return questManager.Refused("Grandma's Stew");
                    Debug.Log("No Potion");
                    break;
                case 1:
                    yield return fade.BlackInAndOut();
                    yield return questManager.Speak("Grandma", "Thank you for getting the mushrooms. I hope you like it!");
                    yield return questManager.Completed(reward1, "You got... bottled stew?");
                    Debug.Log("Lv 1 Potion");
                    break;
                case 2:
                    yield return fade.BlackInAndOut();
                    yield return questManager.Speak("Grandma", "Thank you for getting the mushrooms and carrots. I hope you like it!");
                    yield return questManager.Completed(reward2, "You got... bottled stew?");
                    Debug.Log("Lv 2 Potion");
                    break;
                case 3:
                    yield return fade.BlackInAndOut();
                    yield return questManager.Speak("Grandma", "Thank you for getting everything! I hope you like it!");
                    yield return questManager.Completed(reward3, "You got... bottled stew?");
                    Debug.Log("Lv 3 Potion");
                    break;
            }
        }
    }
}
