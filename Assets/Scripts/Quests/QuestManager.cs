using Home;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    public class QuestManager : MonoBehaviour
    {
        //Player
        public Transform player;
        public Transform cam;
        public Compass compass;

        //Dialog
        public Sprite grandmaIcon;
        public Sprite yvryrIcon;

        //Speech Box Components
        public UIManager uiManager;
        public SoundManager soundManager;
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
        public AMayorsRequest aMayorsRequest;
        public GrandmasStew grandmasStew;
        public Excalibwhere excalibwhere;
        public ALostSoul aLostSoul;
        public BeetleJuice beetleJuice;

        //Completion Components
        public Fade fade;
        public RawImage questCompletedImg;
        public Texture questCompletedTxr;
        public Texture questRefusedTxr;
        public Texture questStartedTxr;
        public RawImage rewardIcon;
        public TextMeshProUGUI rewardText;

        //Quest Menu
        public GameObject questMenu;
        public GameObject inventoryMenu;

        //People
        public Transform mayor;
        public Transform grandma;
        public Transform arthur;
        public Transform sophie;
        public Transform soul;
        public Transform luna;
        public Transform jesse;


        //Hides UI components if I forget
        void Start()
        {
            questTitle.gameObject.SetActive(false);
            questProgress.gameObject.SetActive(false);
            menuLeft.gameObject.SetActive(false);
            menuRight.gameObject.SetActive(false);
            PlayerData.FreeCam = false;
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                inventoryMenu.SetActive(false);
                compass.gameObject.SetActive(questMenu.activeInHierarchy);
                questMenu.SetActive(!questMenu.activeInHierarchy);
                FreezePlayer(questMenu.activeInHierarchy);
            } else if (Input.GetKeyDown(KeyCode.Z))
            {
                questMenu.SetActive(false);
                compass.gameObject.SetActive(inventoryMenu.activeInHierarchy);
                inventoryMenu.SetActive(!inventoryMenu.activeInHierarchy);
                FreezePlayer(inventoryMenu.activeInHierarchy);
            }

            if (!flashing && speaking)
            {
                StartCoroutine(FlashText());
            }

            switch (CurrentQuest)
            {
                case "A Mayor's Request" when PlayerData.AMayorsRequestCompleted || questMenu.activeInHierarchy || inventoryMenu.activeInHierarchy:
                    HideDetails();
                    break;
                case "A Mayor's Request":
                    ShowDetails(CurrentQuest, aMayorsRequest.GetProgress());
                    break;
                case "Grandma's Stew" when PlayerData.GrandmasStewCompleted || questMenu.activeInHierarchy || inventoryMenu.activeInHierarchy:
                    HideDetails();
                    break;
                case "Grandma's Stew":
                    ShowDetails(CurrentQuest, grandmasStew.GetProgress());
                    break;
                case "Excalibwhere?" when PlayerData.ExcalibwhereCompleted || questMenu.activeInHierarchy || inventoryMenu.activeInHierarchy:
                    HideDetails();
                    break;
                case "Excalibwhere?":
                    ShowDetails(CurrentQuest, excalibwhere.GetProgress());
                    break;
                case "A Lost Soul" when PlayerData.ALostSoulCompleted || questMenu.activeInHierarchy || inventoryMenu.activeInHierarchy:
                    HideDetails();
                    break;
                case "A Lost Soul":
                    ShowDetails(CurrentQuest, aLostSoul.GetProgress());
                    break;
                case "Beetle Juice" when PlayerData.BeetleJuiceCompleted || questMenu.activeInHierarchy || inventoryMenu.activeInHierarchy:
                    HideDetails();
                    break;
                case "Beetle Juice":
                    ShowDetails(CurrentQuest, beetleJuice.GetProgress());
                    break;
            }
        }

        public void NewQuest(string name)
        {
            switch (name)
            {
                case "A Mayor's Request": aMayorsRequest.Play();
                    break;
                case "Grandma's Stew": grandmasStew.Play();
                    break;
                case "Excalibwhere?": excalibwhere.Play();
                    break;
                case "A Lost Soul": aLostSoul.Play();
                    break;
                case "Beetle Juice": beetleJuice.Play();
                    break;
            }
        }
        public void ShowRadial(string leftText, string rightText)
        {
            FreezePlayer(true);
            Answered = false;
            menuLeftText.text = leftText;
            menuRightText.text = rightText;
            menuLeft.gameObject.SetActive(true);
            menuRight.gameObject.SetActive(true);
        }

        public void HideRadial()
        {
            FreezePlayer(false);
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
            switch (speaker)
            { //Put Speaker Icon Here
                case "Mayor":
                    cam.LookAt(mayor);
                    //icon.sprite = mayorIcon;
                    break;
                case "Grandma":
                    cam.LookAt(grandma);
                    icon.sprite = grandmaIcon;
                    break;
                case "Arthur":
                    cam.LookAt(arthur);
                    //icon.sprite = arthurIcon;
                    break;
                case "Sophie":
                    cam.LookAt(sophie);
                    //icon.sprite = sophieIcon;
                    break;
                case "Soul" when !PlayerData.FreeCam:
                    cam.LookAt(soul);
                    //icon.sprite = soulIcon;
                    break;
                case "Luna":
                    cam.LookAt(luna);
                    break;
                case "Jesse":
                    cam.LookAt(jesse);
                    break;
                case "Yvryr":
                    //cam.LookAt(yvryr);
                    icon.sprite = yvryrIcon;
                    break;
        }
        }

        public IEnumerator Speak(string speaker, string message)
        {
            FreezePlayer(true);
            speaking = true;
            SetSpeaker(speaker);
            uiManager.SetTextBox(message);
            yield return StartCoroutine(WaitForKeyDown(KeyCode.V));
            yield return null;
            speaking = false;
            FreezePlayer(false);
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
            rewardText.text = text;
            if (img != null)
            {
                rewardIcon.texture = img;
                StartCoroutine(fade.FadeInAndOut(rewardIcon.gameObject, 3));
                rewardText.alignment = TextAlignmentOptions.Left;
                rewardText.rectTransform.anchoredPosition = new Vector3(-20, 50, 0);
            } else
            {
                rewardText.alignment = TextAlignmentOptions.Center;
                rewardText.rectTransform.anchoredPosition = new Vector3(-200, 50, 0);
            }
            
            StartCoroutine(fade.FadeInAndOut(questCompletedImg.gameObject, 3));
            yield return fade.FadeInAndOut(rewardText.gameObject, 3);
        }

        public IEnumerator Started() 
        {
            questCompletedImg.texture = questStartedTxr;
            rewardText.text = CurrentQuest;
            rewardText.alignment = TextAlignmentOptions.Center;
            rewardText.rectTransform.anchoredPosition = new Vector3(-200, 50, 0);
            StartCoroutine(fade.FadeInAndOut(questCompletedImg.gameObject, 3));
            yield return fade.FadeInAndOut(rewardText.gameObject, 3);
        }

        public IEnumerator Refused(string quest)
        {
            questCompletedImg.texture = questRefusedTxr;
            rewardText.text = quest;
            rewardText.alignment = TextAlignmentOptions.Center;
            rewardText.rectTransform.anchoredPosition = new Vector3(-200, 50, 0);
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

        public void FreezePlayer(bool freeze)
        {
            player.GetComponent<PlayerRotation>().enabled = !freeze;
            player.GetComponent<PlayerMovement>().SetCanMove(!freeze);
        }
    }
}
