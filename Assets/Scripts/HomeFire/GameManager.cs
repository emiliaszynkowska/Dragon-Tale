using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using static System.Linq.Enumerable;

namespace HomeFire
{
    public class GameManager : MonoBehaviour
    {
        public SoundManager soundManager;
        public UIManager uiManager;
        public Material skyMaterial;


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
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.V));
            soundManager.PlayClick();
            uiManager.UnSetTextBox();
            PlayerData.DragonsTalePart = 1;
        }
    }

    
}
