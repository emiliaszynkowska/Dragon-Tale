using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            StartCoroutine(WaitForStart());
        }

        IEnumerator WaitForStart()
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            soundManager.PlayClick();
            uiManager.UnSetTextBox();
        }

    }
}
