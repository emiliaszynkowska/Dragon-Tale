using System.Collections;
using Home;
using UnityEngine;
using UnityEngine.UI;

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
        public LairFade fade;

        public IEnumerator BossBattle()
        {
            fade.Out();
            soundManager.PlayClick();
            yield return new WaitForSeconds(1);
            Destroy(marker);
            uiManager.uiBar.SetActive(false);
            playerMovement.canMove = false;
            playerRotation.enabled = false;
            playerMovement.SetCameraPosition(new Vector3(45, 60, -5));
            playerMovement.SetCameraRotation(Quaternion.Euler(45, 0, 0));
            fade.In();
            StartCoroutine(BossDialog());
        }

        public IEnumerator BossDialog()
        {
            uiManager.SetTextBox("So, you've come to challenge me...");
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            soundManager.PlayClick();
            uiManager.SetTextBox(
                "You are brave to come to my lair. You must know by now that I have no desire to spare you.");
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            soundManager.PlayClick();
            uiManager.SetTextBox("I will not hesitate to destroy you this time.");
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            soundManager.PlayClick();
            uiManager.UnSetTextBox();
            playerMovement.SetLocalCameraPosition(new Vector3(0, 2, 0));
            playerMovement.SetLocalCameraRotation(Quaternion.Euler(0, 0, 0));
            playerRotation.enabled = true;
            playerMovement.canMove = true;
            bossMovement.fight = true;
            soundManager.StopMusic();
            soundManager.PlayBoss();
            bossMovement.StartCoroutine("Fight");
            yield return new WaitForSeconds(10);
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
                        uiManager.SetTextBox("I am Yvryr, king of dragons!");
                        break;
                }

                yield return new WaitForSeconds(10);
            }
        }

        public IEnumerator EndBattle()
        {
            fade.Out();
            yield return new WaitForSeconds(1);
            soundManager.StopMusic();
            yield return new WaitForSeconds(1);
            playerMovement.canMove = false;
            playerRotation.enabled = false;
            bossMovement.DisableHealth();
            bossMovement.gameObject.SetActive(false);
            uiManager.UnSetTextBox();
            // High Reputation Ending
            uiManager.EndingScreen(0);
            uiManager.SetTextBoxBig(
                "Dragon Defeated! \nYou helped (x) villagers and defeated the dragon. "+
                "You return to the village as a hero, where the villagers congratulate you and build a new house for you to live in.");
            // Low Reputation Ending
            // uiManager.EndingScreen(1);
            // uiManager.SetTextBoxBig("Dragon Defeated! \nYou helped (x) villagers and defeated the dragon. " +
            //                             "You return to the village to seek refuge, but the villagers remember your actions and dismiss you. " +
            //                             "You return to your destroyed home and begin rebuilding it.");
            fade.In();
            soundManager.PlayWin();
        }

    }

}
