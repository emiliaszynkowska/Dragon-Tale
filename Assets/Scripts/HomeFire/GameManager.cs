using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Linq.Enumerable;

namespace HomeFire
{
    public class GameManager : MonoBehaviour
    {
        public UIManager uiManager;
        public Material skyMaterial;

        void Start()
        {
            RenderSettings.skybox = skyMaterial;
            StartCoroutine(WaitForStart());
        }

        IEnumerator WaitForStart()
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
            uiManager.UnSetTextBox();
        }

    }
}
