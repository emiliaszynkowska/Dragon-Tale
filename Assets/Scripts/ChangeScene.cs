using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HomeFire
{
    public class ChangeScene : MonoBehaviour
    {

        //public RawImage img;
        public Fade fade;
        public string scene;
        void OnTriggerEnter(Collider other)
        {
            StartCoroutine(Switch());
        }

        IEnumerator Switch()
        {
            yield return fade.FadeIn();
            if (scene == "Village")
            {
                if (SceneManager.GetActiveScene().name == "HomeFire" || SceneManager.GetActiveScene().name == "Home")
                {
                    PlayerData.VillageExit = 0;
                }
                else if (SceneManager.GetActiveScene().name == "Lair")
                {
                    PlayerData.VillageExit = 1;
                }
            }
            SceneManager.LoadScene(scene);
        }
    }
}
