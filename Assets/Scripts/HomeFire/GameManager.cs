using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using static System.Linq.Enumerable;
using Home;

namespace HomeFire
{
    public class GameManager : MonoBehaviour
    {
        public SoundManager soundManager;
        public UIManager uiManager;
        public Material skyMaterial;
        public PlayerMovement playerMovement;
        public PlayerRotation playerRotation;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Menu();
        }
        
        public void Menu()
        {
            uiManager.Menu();
        }
        
        void Start()
        {
            RenderSettings.skybox = skyMaterial;
            if (SceneManager.GetActiveScene().name == "HomeFire" && PlayerData.DragonsTalePart == 0)
            {
                StartCoroutine(WaitForStart());
            }
            
        }

        IEnumerator WaitForStart()
        {
            playerMovement.canMove = false;
            playerRotation.enabled = false;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.V));
            soundManager.PlayClick();
            uiManager.UnSetTextBox();
            uiManager.SetUIBar();
            uiManager.menu.gameObject.SetActive(true);
            PlayerData.DragonsTalePart = 1;
        }
    }

    
}
