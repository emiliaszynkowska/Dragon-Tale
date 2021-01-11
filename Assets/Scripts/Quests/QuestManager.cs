using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    public class QuestManager : MonoBehaviour
    {
        //Player
        public PlayerMovement player;
        public Compass compass;

        //Dialog
        public Sprite grandmaIcon;
        public Sprite yvryrIcon;

        //Speech Box Components
        public UIManager uiManager;
        public Image icon;
        public TextMeshProUGUI speakerName;
        public TextMeshProUGUI skipText;
        public float flashSpeed = 0.5f;
        private bool flashing;
        private bool speaking;
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
        public Excalibwhere excalibwhere;
        public ALostSoul aLostSoul;

        //Completion Components
        public Fade fade;
        public RawImage questCompletedImg;
        public Texture questCompletedTxr;
        public Texture questRefusedTxr;
        public Texture questStartedTxr;
        public RawImage rewardIcon;
        public TextMeshProUGUI rewardText;

        //Quest Menu
        public GameObject menu;


        //Hides UI components if I forget
        void Start()
        {
            questTitle.gameObject.SetActive(false);
            questProgress.gameObject.SetActive(false);
            menuLeft.gameObject.SetActive(false);
            menuRight.gameObject.SetActive(false);
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                compass.gameObject.SetActive(menu.activeInHierarchy);
                menu.SetActive(!menu.activeInHierarchy);
                player.SetCanMove(!menu.activeInHierarchy);
            }

            if (!flashing && speaking)
            {
                StartCoroutine(FlashText());
            }

            switch (CurrentQuest)
            {
                case "Grandma's Stew" when PlayerData.GrandmasStewCompleted || menu.activeInHierarchy:
                    HideDetails();
                    break;
                case "Grandma's Stew":
                    ShowDetails(CurrentQuest, grandmasStew.GetProgress());
                    break;
                case "Excalibwhere?" when PlayerData.ExcalibwhereCompleted || menu.activeInHierarchy:
                    HideDetails();
                    break;
                case "Excalibwhere?":
                    ShowDetails(CurrentQuest, excalibwhere.GetProgress());
                    break;
                case "A Lost Soul" when PlayerData.ALostSoulCompleted || menu.activeInHierarchy:
                    HideDetails();
                    break;
                case "A Lost Soul":
                    ShowDetails(CurrentQuest, aLostSoul.GetProgress());
                    break;
            }
        }

        public void NewQuest(string name)
        {
            switch (name)
            {
                case "Grandma's Stew": grandmasStew.Play();
                    break;
                case "Excalibwhere?": excalibwhere.Play();
                    break;
                case "A Lost Soul": aLostSoul.Play();
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

        public void HideDetails()
        {
            questTitle.gameObject.SetActive(false);
            questProgress.gameObject.SetActive(false);
        }

        public void SetSpeaker(string speaker)
        {
            speakerName.text = speaker;
            switch (speaker) { //Put Speaker Icon Here
            case "Grandma": icon.sprite = grandmaIcon;
                break;
            case "Yvryr": icon.sprite = yvryrIcon;
                break;
        }
        }

        public IEnumerator Speak(string speaker, string message)
        {
            player.SetCanMove(false);
            speaking = true;
            SetSpeaker(speaker);
            uiManager.SetTextBox(message);
            yield return StartCoroutine(WaitForKeyDown(KeyCode.V));
            yield return null;
            speaking = false;
            player.SetCanMove(true);
            uiManager.UnSetTextBox();
        }

        IEnumerator FlashText()
        {
            flashing = true;
            yield return new WaitForSeconds(flashSpeed);
            skipText.enabled = !skipText.enabled;
            flashing = false;
        }

        IEnumerator WaitForKeyDown(KeyCode keyCode)
        {
            while (!Input.GetKeyDown(keyCode))
                yield return null;
        }

        public IEnumerator Completed(Texture img, string text)
        {
            questCompletedImg.texture = questCompletedTxr;
            rewardIcon.texture = img;
            rewardText.text = text;
            StartCoroutine(fade.FadeInAndOut(rewardIcon.gameObject, 3));
            StartCoroutine(fade.FadeInAndOut(questCompletedImg.gameObject, 3));
            yield return fade.FadeInAndOut(rewardText.gameObject, 3);
        }

        public IEnumerator Started() 
        {
            questCompletedImg.texture = questStartedTxr;
            rewardText.text = CurrentQuest;
            StartCoroutine(fade.FadeInAndOut(questCompletedImg.gameObject, 3));
            yield return fade.FadeInAndOut(rewardText.gameObject, 3);
        }

        public IEnumerator Refused(string quest)
        {
            questCompletedImg.texture = questRefusedTxr;
            rewardText.text = quest;
            StartCoroutine(fade.FadeInAndOut(questCompletedImg.gameObject, 3));
            yield return fade.FadeInAndOut(rewardText.gameObject, 3);
        }

        public IEnumerator AddQuestMarker(QuestMarker marker)
        {
            yield return compass.AddQuestMarker(marker);
        }

        public IEnumerator RemoveQuestMarker(QuestMarker marker)
        {
            yield return compass.RemoveQuestMarker(marker);
        }

        public IEnumerator UpdateQuestMarker(QuestMarker next, QuestMarker old)
        {
            yield return RemoveQuestMarker(old);
            yield return AddQuestMarker(next);
        }
    }
}
