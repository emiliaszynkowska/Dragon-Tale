using System;
using System.Collections;
using Home;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro; 

namespace Lair
{
    public class LairManager : MonoBehaviour
    {
        public UIManager uiManager;
        public SoundManager soundManager;
        public PlayerMovement playerMovement;
        public PlayerRotation playerRotation;
        public BossMovement bossMovement;
        public SoulMovement soulMovement;
        public GameObject marker;
        public Fade fade;
        public Compass compass;
        public QuestMarker questMaker;
        public GameObject questDetails;
        public GameObject controls;
        public GameObject textBox;

        private void Start()
        {
            StartCoroutine(compass.AddQuestMarker(questMaker));
        }

        /*
        private void Start()
        {
            // Debug
            // PlayerData.Reputation = -1;
            // PlayerData.GrandmasStewCompleted = true;
            // PlayerData.ALostSoulCompleted = true;
            // PlayerData.ExcalibwhereCompleted = true;
            // PlayerData.BeetleJuicePart = 2;
        }*/

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Menu();
        }
        
        public void Menu()
        {
            uiManager.Menu();
        }
        
        public IEnumerator BossBattle()
        {
            soundManager.PlayClick();
            Destroy(marker);
            yield return fade.BlackIn();
            uiManager.uiBar.SetActive(false);
            questDetails.SetActive(false);
            playerMovement.canMove = false;
            playerRotation.enabled = false;
            playerMovement.SetCameraPosition(new Vector3(0, 60, -40));
            playerMovement.SetCameraRotation(Quaternion.Euler(45, 0, 0));
            if (PlayerData.Reputation >= 0)
                StartCoroutine(VillagerDialog());
            else
                StartCoroutine(BossDialog());
        }

        public IEnumerator VillagerDialog()
        {

            controls.SetActive(false);
            yield return fade.BlackOut();
            soundManager.StopMusic();
            soundManager.PlayVillage();
            // Soul
            if (PlayerData.ALostSoulCompleted)
            {
                soulMovement.gameObject.SetActive(true);
                uiManager.SetIconBox(uiManager.soulIcon, "Soul");
                uiManager.SetTextBox("Wait!");
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
                soundManager.PlayClick();
                yield return new WaitForSeconds(0.1f);
                uiManager.SetTextBox("You saved me in the forest. I think it's time I returned the favour.");
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
                soundManager.PlayClick();
                yield return new WaitForSeconds(0.1f);
                uiManager.SetTextBox("Let me fight with you!");
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
                soundManager.PlayClick();
                yield return new WaitForSeconds(0.1f);
            }
            // Grandma
            if (PlayerData.GrandmasStewCompleted)
            {
                uiManager.SetIconBox(uiManager.grandmaIcon, "Grandma");
                uiManager.SetTextBox("Before you battle, remember to use my mushroom stew. It will restore your health.");
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
                soundManager.PlayClick();
                yield return new WaitForSeconds(0.1f);
            }
            if (PlayerData.BeetleJuicePart == 1)
            {
                uiManager.SetIconBox(uiManager.jesseIcon, "Jesse");
                uiManager.SetTextBox("Thank you for showing mercy! Remember to use the elixir I gave you.");
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
                soundManager.PlayClick();
                yield return new WaitForSeconds(0.1f);
            }
            if (PlayerData.BeetleJuicePart == 2)
            {
                uiManager.SetIconBox(uiManager.lunaIcon, "Luna");
                uiManager.SetTextBox("Thank you for destroying those beetles! The Metalon blood will boost your power.");
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
                soundManager.PlayClick();
                yield return new WaitForSeconds(0.1f);
            }

            if (PlayerData.ExcalibwhereCompleted)
            {
                uiManager.SetIconBox(uiManager.arthurIcon, "Arthur");
                uiManager.SetTextBox("I hope the breastplate I gave you is helpful. It will protect you against the dragon's attacks.");
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
                soundManager.PlayClick();
                yield return new WaitForSeconds(0.1f);
            }

            uiManager.SetIconBox(uiManager.mayorIcon, "Mayor");
            uiManager.SetTextBox("The villagers are very thankful for your help. Please accept our assistance. Good luck!");
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
            soundManager.PlayClick();
            yield return new WaitForSeconds(0.1f);
            // Bad Ending
            yield return fade.BlackIn();
            StartCoroutine(BossDialog());
        }

        public IEnumerator BossDialog()
        {
            soundManager.StopMusic();
            soundManager.PlayLava();
            uiManager.SetIconBox(uiManager.yvryrIcon, "Yvryr");
            uiManager.SetTextBox("So, you've come to challenge me...");
            yield return fade.BlackOut();
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
            playerMovement.canMove = false;
            playerRotation.enabled = false;
            uiManager.SetTextBox("So, you've come to challenge me...");
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.V));
            soundManager.PlayClick();
            uiManager.SetTextBox("You are brave to come to my lair. You must know by now that I have no desire to spare you.");
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
            soundManager.PlayClick();
            uiManager.SetTextBox("I will not hesitate to destroy you this time.");
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.V));
            soundManager.PlayClick();
            uiManager.SetTextBox("I will not hesitate to destroy you this time.");
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.V));
            soundManager.PlayClick();
            uiManager.UnSetTextBox();
            controls.SetActive(true);
            playerMovement.SetLocalCameraPosition(new Vector3(0, 2, 0));
            playerMovement.SetLocalCameraRotation(Quaternion.Euler(0, 0, 0));
            playerRotation.enabled = true;
            playerMovement.canMove = true;
            bossMovement.fight = true;
            soundManager.StopMusic();
            soundManager.PlayBoss();
            bossMovement.StartCoroutine("Fight");
            playerMovement.Fight();
            if (soulMovement.gameObject.activeSelf)
                soulMovement.Fight();
            StartCoroutine(CycleDialog());
        }

        public IEnumerator CycleDialog()
        {
            while (true)
            {
                var choice = Random.Range(0, 5);
                switch (choice)
                {
                    case (0):
                        uiManager.SetTextBox("I am eternal!");
                        break;
                    case (1):
                        uiManager.SetTextBox("You cannot defeat me.");
                        break;
                    case (2):
                        uiManager.SetTextBox("Prepare to die.");
                        break;
                    case (3):
                        uiManager.SetTextBox("Resistance is futile.");
                        break;
                    case (4):
                        uiManager.SetTextBox("You are weak against me.");
                        break;
                    case (5):
                        uiManager.SetTextBox("I am Yvryr, King of Dragons!");
                        break;
                }
                StartCoroutine(ListenForKey());
                yield return new WaitForSeconds(3);
                uiManager.UnSetTextBox();
                yield return new WaitForSeconds(10);
            }
        }

        public IEnumerator EndBattle()
        {
            yield return fade.BlackIn();
            //yield return new WaitForSeconds(1);
            soundManager.StopMusic();
            yield return new WaitForSeconds(1);
            playerMovement.canMove = false;
            playerRotation.enabled = false;
            bossMovement.DisableHealth();
            bossMovement.gameObject.SetActive(false);
            uiManager.UnSetTextBox();
            // High Reputation Ending
            if (PlayerData.Reputation >= 0)
            {
                uiManager.EndingScreen(0);
                uiManager.SetTextBoxBig(
                    "Dragon Defeated! \nYou helped the villagers and defeated the dragon. " +
                    "You return to the village as a hero, where the villagers congratulate you and build a new house for you to live in.");
            }
            // Low Reputation Ending
            else
            {
                uiManager.EndingScreen(1);
                uiManager.SetTextBoxBig("Dragon Defeated! \nYou did not help the villagers but you defeated the dragon. " +
                                        "You return to the village to seek refuge, but the villagers remember your actions and dismiss you. " +
                                        "You return to your destroyed home and begin rebuilding it.");
            }
            yield return fade.BlackOut();
            soundManager.PlayWin();
        }

        IEnumerator ListenForKey()
        {
            while (textBox.activeInHierarchy)
            {
                if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.V))
                {
                    uiManager.UnSetTextBox();
                }
                yield return null;
            }
        }
    }

}
