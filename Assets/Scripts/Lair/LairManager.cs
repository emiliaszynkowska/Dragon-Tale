using System.Collections;
using Home;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Lair
{
    public class LairManager : MonoBehaviour
    {
        public UIManager uiManager;
        public SoundManager soundManager;
        public PlayerMovement playerMovement;
        public PlayerRotation playerRotation;
        public BossMovement bossMovement;
        public GameObject marker;
        public Fade fade;
        public Compass compass;
        public QuestMarker questMaker;
        public GameObject questDetails;
        public GameObject controls;
        public GameObject textBox;
        public SoulMovement soulMovement;
        public Potion potion;
        public TextMeshProUGUI mapControls;
        public Reset death;

        private bool fighting;

        private void Start()
        {
            //PlayerData.Health = 100;
            //PlayerData.Resistance = 1;
            //PlayerData.Attack = 1;
            //PlayerData.Reputation = -0.5f;
            //PlayerData.SoulSaved = true;
            potion.Show();
            potion.CheckEmpty();
            if (PlayerData.SprintPotion) mapControls.text = "Zoom In: R\nZoom Out: F\nMenu: ESC\nSprint: SHIFT";
            StartCoroutine(compass.AddQuestMarker(questMaker));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) { 
                Menu();
            }
        }
        
        public void Menu()
        {
            uiManager.Menu();
        }
        
        public IEnumerator BossBattle()
        {
            yield return fade.BlackIn();
            soundManager.PlayClick();
            yield return new WaitForSeconds(1);
            //Destroy(marker);
            uiManager.uiBar.SetActive(false);
            questDetails.SetActive(false);
            playerMovement.canMove = false;
            playerRotation.enabled = false;
            playerMovement.SetCameraPosition(new Vector3(45, 60, -5));
            playerMovement.SetCameraRotation(Quaternion.Euler(45, 0, 0));
            controls.SetActive(false);
            yield return fade.BlackOut();
            StartCoroutine(BossDialog());
        }

        public IEnumerator BossDialog()
        {

            playerMovement.canMove = false;
            playerRotation.enabled = false;
            uiManager.SetTextBox("So, you've come to challenge me...");
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.V));
            soundManager.PlayClick();
            uiManager.SetTextBox(
                "You are brave to come to my lair. You must know by now that I have no desire to spare you.");
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.V));
            soundManager.PlayClick();
            uiManager.SetTextBox("I will not hesitate to destroy you this time.");
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.V));
            soundManager.PlayClick();
            uiManager.UnSetTextBox();
            controls.SetActive(true);
            
            uiManager.uiBar.SetActive(true);
            uiManager.UpdateReputation();
            questDetails.SetActive(true);
            compass.gameObject.SetActive(false);
            playerMovement.SetLocalCameraPosition(new Vector3(0, 2, 0));
            playerMovement.SetLocalCameraRotation(Quaternion.Euler(0, 0, 0));
            playerRotation.enabled = true;
            playerMovement.canMove = true;
            bossMovement.fight = true;
            soundManager.StopMusic();
            soundManager.PlayBoss();
            bossMovement.StartCoroutine("Fight");
            yield return new WaitForSeconds(3);
            if (soulMovement.gameObject.activeSelf)
                soulMovement.Fight();
            StartCoroutine(CycleDialog());
        }

        public IEnumerator CycleDialog()
        {
            fighting = true;
            while (fighting)
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
            fighting = false;
            soulMovement.fighting = false;
            controls.SetActive(false);
            uiManager.uiBar.SetActive(false);
            questDetails.SetActive(false);
            uiManager.UnSetTextBox();
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
            if (PlayerData.Reputation > 0)
            {
                uiManager.EndingScreen(0);
                uiManager.SetTextBoxBig(
                    "Dragon Defeated! \nYou helped (x) villagers and defeated the dragon. " +
                    "You return to the village as a hero, where the villagers congratulate you and build a new house for you to live in.");
            }
            else
            {
                // Low Reputation Ending
                uiManager.EndingScreen(1);
                 uiManager.SetTextBoxBig("Dragon Defeated! \nYou helped (x) villagers and defeated the dragon. " +
                                             "You return to the village to seek refuge, but the villagers remember your actions and dismiss you. " +
                                             "You return to your destroyed home and begin rebuilding it.");
            }
            yield return fade.BlackOut();
            soundManager.PlayWin();
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            death.Menu();
            SceneManager.LoadScene("Start");
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
