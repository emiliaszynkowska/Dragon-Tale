using System.Collections;
using UnityEngine;

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
